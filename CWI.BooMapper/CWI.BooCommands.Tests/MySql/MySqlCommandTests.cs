using System.Data;
using System.Linq;
using CWI.BooCommands.MySql;
using CWI.BooCommands.Tests.Mocks;
using CWI.BooCommands.Tests.MySql.Commands;
using CWI.BooMapper.Services.Relational;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;

namespace CWI.BooCommands.Tests.MySql
{
    [TestClass]
    public class MySqlCommandTest
    {
        private IDatabase database;

        [TestInitialize]
        public void Initialize()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.Database = "test";
            builder.UserID = "root";
            builder.Password = "root";

            database = new MySqlDatabase(new MySqlConnection(builder.ToString()));

            new MySqlTextCommand("DROP TABLE IF EXISTS USUARIOS", database).ExecuteNonQuery();
            new MySqlTextCommand("DROP PROCEDURE IF EXISTS ObterUsuario", database).ExecuteNonQuery();
            new MySqlTextCommand("DROP PROCEDURE IF EXISTS ListarUsuarios", database).ExecuteNonQuery();
            new MySqlTextCommand("CREATE TABLE USUARIOS ( Id INT PRIMARY KEY AUTO_INCREMENT, NOME VARCHAR(50))", database).ExecuteNonQuery();
        }

        [TestCleanup]
        public void Cleanup()
        {
            database.Dispose();
        }

        [TestMethod]
        public void ExecuteReaderTextCommandWithParameter()
        {
            for (int i = 0; i < 5; i++)
            {
                new MySqlTextCommand($"insert into usuarios (nome) values ('teste{i}')", database).ExecuteNonQuery();
            }

            using (IDataReader reader = new ObterUsuarioCommand(database, 3).ExecuteReader())
            {
                Assert.IsNotNull(reader);
                Assert.IsTrue(reader.Read());
                Assert.AreEqual(reader.GetInt32(reader.GetOrdinal("Id")), 3);
                Assert.AreEqual(reader.GetString(reader.GetOrdinal("Nome")), "teste2");
            }
        }

        [TestMethod]
        public void ExecuteMapperTextCommandWithParameter()
        {
            for (int i = 0; i < 5; i++)
            {
                new MySqlTextCommand($"insert into usuarios (nome) values ('teste{i}')", database).ExecuteNonQuery();
            }

            Usuario usuario = new ObterUsuarioCommand(database, 3).ExecuteMapper<Usuario>("MapperUsuario", new BasicRelationalMapperService());
            Assert.IsNotNull(usuario);
            Assert.AreEqual(usuario.Id, 3);
            Assert.AreEqual(usuario.Nome, "teste2");
        }

        [TestMethod]
        public void ExecuteMapperStoredProcedureCommandWithParameters()
        {
            for (int i = 0; i < 5; i++)
            {
                new MySqlTextCommand($"insert into usuarios (nome) values ('teste{i}')", database).ExecuteNonQuery();
            }

            new MySqlTextCommand("create procedure ObterUsuario(id int) begin SELECT Id,Nome FROM USUARIOS u WHERE u.Id = id; end;", database).ExecuteNonQuery();

            Usuario usuario = new ObterUsuarioStoredProcedureCommand(database, 3).ExecuteMapper<Usuario>("MapperUsuario", new BasicRelationalMapperService());
            Assert.IsNotNull(usuario);
            Assert.AreEqual(usuario.Id, 3);
            Assert.AreEqual(usuario.Nome, "teste2");
        }

        [TestMethod]
        public void ExecuteMapperCollectionStoredProcedureCommandWithOutParameters()
        {
            for (int i = 0; i < 5; i++)
            {
                new MySqlTextCommand($"insert into usuarios (nome) values ('teste{i}')", database).ExecuteNonQuery();
            }

            new MySqlTextCommand("create procedure ListarUsuarios(out count int) begin SELECT SQL_CALC_FOUND_ROWS Id, Nome FROM USUARIOS; SET count = found_rows(); end;", database).ExecuteNonQuery();

            using (var command = new ListarUsuariosStoredProcedureCommand(database))
            {
                var usuarios = command.ExecuteMapperCollection<Usuario>("MapperUsuarios", new BasicRelationalMapperService());
                Assert.AreEqual(command.Count, 5);
                Assert.AreEqual(usuarios.Count(), 5);
            }
        }
    }
}
