using System;
using CWI.BooCommands.MySql;
using MySql.Data.MySqlClient;

namespace CWI.BooCommands.Tests.MySql.Commands
{
    public class ListarUsuariosStoredProcedureCommand : MySqlStoredProcedureCommand
    {
        public ListarUsuariosStoredProcedureCommand(IDatabase database)
            : base("ListarUsuarios", database)
        {
            Parameters.Add(new MySqlParameter("count", MySqlDbType.Int32)
            {
                Direction = System.Data.ParameterDirection.Output
            });
        }

        public int Count
        {
            get
            {
                return Convert.ToInt32(Parameters["count"].Value);
            }
        }
    }
}
