using System;

namespace CWI.BooMapper.Core.Relational
{
    public sealed class DataException : Exception
    {
        public DataException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
