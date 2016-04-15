using CWI.BooCommands.MySql;
using MySql.Data.MySqlClient;

namespace CWI.BooCommands.Tests.MySql.Commands
{
    public class ObterUsuarioStoredProcedureCommand : MySqlStoredProcedureCommand
    {
        public const string SQL = "ObterUsuario";

        public ObterUsuarioStoredProcedureCommand(IDatabase database, int id)
            : base(SQL, database)
        {
            Parameters.Add(new MySqlParameter("Id", MySqlDbType.Int32)
            {
                Value = id
            });
        }
    }
}
