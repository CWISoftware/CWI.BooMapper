using System.Data;
using MySql.Data.MySqlClient;

namespace CWI.BooCommands.MySql
{
    public class MySqlStoredProcedureCommand : BaseCommand<MySqlCommand>
    {
        public MySqlStoredProcedureCommand(string commandText, IDatabase database)
            : base(commandText, database)
        {
        }

        protected override MySqlCommand CreateCommand(string commandText, IDatabase database)
        {
            return new MySqlCommand(commandText, database.Connection as MySqlConnection)
            {
                CommandType = CommandType.StoredProcedure
            };
        }
    }
}
