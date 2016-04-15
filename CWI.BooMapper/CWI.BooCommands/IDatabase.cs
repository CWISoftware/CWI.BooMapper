using System;
using System.Data.Common;

namespace CWI.BooCommands
{
    public interface IDatabase : IDisposable
    {
        DbConnection Connection { get; }

        DbTransaction Transaction { get; }
    }
}
