using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CWI.BooMapper.Tests.Mocks
{
    internal class MemoryDataReader : IDataReader
    {
        private readonly DataTable table;
        private IEnumerator<DataTableRow> enumerator;
        private bool disposed;

        public MemoryDataReader()
        {
            table = new DataTable();
        }

        public object this[string name]
        {
            get
            {
                CheckRead();
                throw new NotImplementedException();
            }
        }

        public object this[int i]
        {
            get
            {
                CheckRead();
                return enumerator.Current.Values[i];
            }
        }

        public int Depth
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int FieldCount
        {
            get
            {
                return table.Columns.Count;
            }
        }

        public bool IsClosed
        {
            get
            {
                return disposed;
            }
        }

        public int RecordsAffected
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            disposed = true;
        }

        public bool GetBoolean(int i)
        {
            CheckDisposed();
            CheckRead();
            return Convert.ToBoolean(this[i]);
        }

        public byte GetByte(int i)
        {
            CheckDisposed();
            CheckRead();
            return Convert.ToByte(this[i]);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            CheckDisposed();
            CheckRead();
            return Convert.ToChar(this[i]);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            CheckDisposed();
            CheckRead();
            return table.Columns[i].Type.FullName;
        }

        public DateTime GetDateTime(int i)
        {
            CheckDisposed();
            CheckRead();
            return Convert.ToDateTime(this[i]);
        }

        public decimal GetDecimal(int i)
        {
            CheckDisposed();
            CheckRead();
            return Convert.ToDecimal(this[i]);
        }

        public double GetDouble(int i)
        {
            CheckDisposed();
            CheckRead();
            return Convert.ToDouble(this[i]);
        }

        public Type GetFieldType(int i)
        {
            CheckDisposed();
            return table.Columns[i].Type;
        }

        public float GetFloat(int i)
        {
            CheckDisposed();
            CheckRead();
            return Convert.ToSingle(this[i]);
        }

        public Guid GetGuid(int i)
        {
            CheckDisposed();
            CheckRead();
            return Guid.Parse(this[i].ToString());
        }

        public short GetInt16(int i)
        {
            CheckDisposed();
            CheckRead();
            return Convert.ToInt16(this[i]);
        }

        public int GetInt32(int i)
        {
            CheckDisposed();
            CheckRead();
            return Convert.ToInt32(this[i]);
        }

        public long GetInt64(int i)
        {
            CheckDisposed();
            CheckRead();
            return Convert.ToInt64(this[i]);
        }

        public string GetName(int i)
        {
            CheckDisposed();
            return table.Columns[i].Name;
        }

        public int GetOrdinal(string name)
        {
            CheckDisposed();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                if (table.Columns[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            CheckDisposed();
            CheckRead();
            return this[i].ToString();
        }

        public object GetValue(int i)
        {
            CheckDisposed();
            CheckRead();
            return this[i];
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            CheckDisposed();
            CheckRead();
            return this[i] == DBNull.Value || this[i] == null;
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            CheckDisposed();

            if (enumerator == null)
            {
                enumerator = table.Rows.GetEnumerator();
            }
            return enumerator.MoveNext();
        }

        private void CheckRead()
        {
            if (enumerator == null)
            {
                throw new InvalidOperationException("Tentativa de leitura sem chamada do método Read.");
            }
        }

        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("table");
            }
        }

        public void AddColumns(params string[] columns)
        {
            CheckDisposed();
            table.Columns.AddRange(columns.Select(c => new DataTableColumn(c)));
        }

        public void AddValues(params object[] values)
        {
            CheckDisposed();

            for (int i = 0; i < values.Length; i++)
            {
                table.Columns[i].Type = values[i]?.GetType() ?? typeof(object);
            }
            table.Rows.Add(new DataTableRow()
            {
                Values = values.ToList()
            });
        }

        System.Data.DataTable IDataReader.GetSchemaTable()
        {
            throw new NotImplementedException();
        }
    }

    internal class DataTable
    {
        public List<DataTableColumn> Columns { get; set; } = new List<DataTableColumn>();

        public List<DataTableRow> Rows { get; set; } = new List<DataTableRow>();
    }

    internal class DataTableColumn
    {
        public DataTableColumn(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public Type Type { get; set; }
    }

    internal class DataTableRow
    {
        public List<object> Values { get; set; }
    }
}
