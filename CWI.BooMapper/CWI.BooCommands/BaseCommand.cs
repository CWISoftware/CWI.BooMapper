using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using CWI.BooMapper.Services.Relational;

namespace CWI.BooCommands
{
    public abstract class BaseCommand<TCommand> : IDisposable where TCommand : DbCommand
    {
        private readonly TCommand nativeCommand;

        public BaseCommand(string commandText, IDatabase database)
        {
            Database = database;
#pragma warning disable S1699 // Constructors should only call non-overridable methods
            nativeCommand = CreateCommand(commandText, database);
#pragma warning restore S1699 // Constructors should only call non-overridable methods
        }

        ~BaseCommand()
        {
            Dispose(false);
        }

        public bool HasParameters
        {
            get
            {
                return Parameters != null;
            }
        }

        public IDatabase Database
        {
            get;
            private set;
        }

        public DbParameterCollection Parameters
        {
            get
            {
                return nativeCommand.Parameters;
            }
        }

        protected abstract TCommand CreateCommand(string commandText, IDatabase database);

        public virtual int ExecuteNonQuery()
        {
            return nativeCommand.ExecuteNonQuery();
        }

        public virtual object ExecuteScalar()
        {
            return nativeCommand.ExecuteScalar();
        }

        public virtual IDataReader ExecuteReader()
        {
            return nativeCommand.ExecuteReader();
        }

        public virtual T ExecuteMapper<T>(string key, IRelationalMapperService mService)
        {
            return mService.Map<T>(key, ExecuteReader());
        }

        public virtual IEnumerable<T> ExecuteMapperCollection<T>(string key, IRelationalMapperService mService)
        {
            return mService.MapCollection<T>(key, ExecuteReader());
        }

        public virtual IEnumerable<T> ExecuteMapperCollection<T>()
        {
            return ExecuteMapperCollection<T>(false);
        }

        public virtual IEnumerable<T> ExecuteMapperCollection<T>(bool useConvert)
        {
            List<T> result = new List<T>();

            using (var reader = ExecuteReader())
            {
                while (reader.Read())
                {
                    if (useConvert)
                    {
                        result.Add((T)Convert.ChangeType(reader.GetValue(0), typeof(T)));
                    }
                    else
                    {
                        result.Add((T)reader.GetValue(0));
                    }
                }
            }

            return result;
        }

        #region IDisposable Support
        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                nativeCommand.Dispose();
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
