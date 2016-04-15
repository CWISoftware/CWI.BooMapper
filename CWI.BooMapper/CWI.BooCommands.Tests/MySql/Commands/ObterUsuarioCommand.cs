using CWI.BooCommands.MySql;
using MySql.Data.MySqlClient;

namespace CWI.BooCommands.Tests.MySql.Commands
{
    internal class ObterUsuarioCommand : MySqlTextCommand
    {
        public const string SQL = "SELECT Id,Nome FROM USUARIOS WHERE Id = @Id";

        public ObterUsuarioCommand(IDatabase database, int id)
            : base(SQL, database)
        {
            Parameters.Add(new MySqlParameter("Id", MySqlDbType.Int32)
            {
                Value = id
            });
        }
    }
}
