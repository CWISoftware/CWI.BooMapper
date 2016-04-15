using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;

namespace CWI.BooCommands.MySql
{
    public sealed class MySqlDatabase : IDatabase
    {
        private readonly MySqlConnection connection;
        private readonly IsolationLevel isolationLevel;
        private MySqlTransaction transaction;

        public MySqlDatabase(MySqlConnection connection)
            : this(connection, IsolationLevel.ReadUncommitted)
        {
        }

        public MySqlDatabase(MySqlConnection connection, IsolationLevel isolationLevel)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            this.connection = connection;
            this.isolationLevel = isolationLevel;
        }

        ~MySqlDatabase()
        {
            Dispose(false);
        }

        public DbConnection Connection
        {
            get
            {
                return connection;
            }
        }

        public DbTransaction Transaction
        {
            get
            {
                if (transaction == null)
                {
                    transaction = connection.BeginTransaction(isolationLevel);
                }
                return transaction;
            }
        }

        private bool disposed;

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && transaction != null)
                {
                    transaction.Commit();
                }

                connection.Dispose();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
