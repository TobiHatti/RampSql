using RampSQL.Schema;
using System;
using System.Collections;
using System.Data.Common;

namespace RampSQL.Reader
{
    public class RampReader : IDisposable
    {
        private DbDataReader reader;

        public RampReader(DbDataReader reader)
        {
            this.reader = reader;
        }

        public object this[int ordinal] => reader[ordinal];
        public object this[string name] => reader[name];
        public object this[RampColumn column] { get => reader[column.UCN]; }

        public int Depth => reader.Depth;
        public int FieldCount => reader.FieldCount;
        public bool HasRows => reader.HasRows;
        public bool IsClosed => reader.IsClosed;
        public int RecordsAffected => reader.RecordsAffected;
        public DbDataReader Reader => reader;

        public void ReadAll(Action<RampReader> readAction)
        {
            while (Read()) readAction(this);
            Reader.Close();
            Dispose();
        }

        public bool GetBoolean(int ordinal) => reader.GetBoolean(ordinal);
        public bool GetBoolean(string columnName) => Convert.ToBoolean(reader[columnName]);
        public bool GetBoolean(RampColumn column) => Convert.ToBoolean(reader[column.UCN]);
        public byte GetByte(int ordinal) => reader.GetByte(ordinal);
        public byte GetByte(string columnName) => Convert.ToByte(reader[columnName]);
        public byte GetByte(RampColumn column) => Convert.ToByte(reader[column.UCN]);
        public long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length) => reader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
        public char GetChar(int ordinal) => reader.GetChar(ordinal);
        public char GetChar(string columnName) => Convert.ToChar(reader[columnName]);
        public char GetChar(RampColumn column) => Convert.ToChar(reader[column.UCN]);
        public long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length) => reader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
        public string GetDataTypeName(int ordinal) => reader.GetDataTypeName(ordinal);
        public DateTime GetDateTime(int ordinal) => reader.GetDateTime(ordinal);
        public DateTime GetDateTime(string columnName) => Convert.ToDateTime(reader[columnName]);
        public DateTime GetDateTime(RampColumn column) => Convert.ToDateTime(reader[column.UCN]);
        public decimal GetDecimal(int ordinal) => reader.GetDecimal(ordinal);
        public decimal GetDecimal(string columnName) => Convert.ToDecimal(reader[columnName]);
        public decimal GetDecimal(RampColumn column) => Convert.ToDecimal(reader[column.UCN]);
        public double GetDouble(int ordinal) => reader.GetDouble(ordinal);
        public double GetDouble(string columnName) => Convert.ToDouble(reader[columnName]);
        public double GetDouble(RampColumn column) => Convert.ToDouble(reader[column.UCN]);
        public IEnumerator GetEnumerator() => reader.GetEnumerator();
        public Type GetFieldType(int ordinal) => reader.GetFieldType(ordinal);
        public float GetFloat(int ordinal) => reader.GetFloat(ordinal);
        public float GetFloat(string columnName) => Convert.ToSingle(reader[columnName]);
        public float GetFloat(RampColumn column) => Convert.ToSingle(reader[column.UCN]);
        public Guid GetGuid(int ordinal) => reader.GetGuid(ordinal);
        public Guid GetGuid(string columnName) => Guid.Parse(Convert.ToString(reader[columnName]));
        public Guid GetGuid(RampColumn column) => Guid.Parse(Convert.ToString(reader[column.UCN]));
        public short GetInt16(int ordinal) => reader.GetInt16(ordinal);
        public short GetInt16(string columnName) => Convert.ToInt16(reader[columnName]);
        public short GetInt16(RampColumn column) => Convert.ToInt16(reader[column.UCN]);
        public int GetInt32(int ordinal) => reader.GetInt32(ordinal);
        public int GetInt32(string columnName) => Convert.ToInt32(reader[columnName]);
        public int GetInt32(RampColumn column) => Convert.ToInt32(reader[column.UCN]);
        public long GetInt64(int ordinal) => reader.GetInt64(ordinal);
        public long GetInt64(string columnName) => Convert.ToInt64(reader[columnName]);
        public long GetInt64(RampColumn column) => Convert.ToInt64(reader[column.UCN]);
        public string GetName(int ordinal) => reader.GetName(ordinal);
        public int GetOrdinal(string name) => reader.GetOrdinal(name);
        public string GetString(int ordinal) => reader.GetString(ordinal);
        public string GetString(string columnName) => Convert.ToString(reader[columnName]);
        public string GetString(RampColumn column) => Convert.ToString(reader[column.UCN]);
        public object GetValue(int ordinal) => reader.GetValue(ordinal);
        public int GetValues(object[] values) => reader.GetValues(values);
        public bool IsDBNull(int ordinal) => reader.IsDBNull(ordinal);

        public bool NextResult() => reader.NextResult();
        public bool Read() => reader.Read();
        public T Get<T>(RampColumn column)
        {
            try
            {
                return (T)Convert.ChangeType(reader[column.UCN], column.GetType());
            }
            catch
            {
                return default(T);
            }
        }

        public T GetEnum<T>(RampColumn column) where T : Enum => (T)Enum.Parse(typeof(T), Convert.ToString(this[column]), true);
        public DbDataReader GetReader() => Reader;
        public void Dispose() => reader.Dispose();
    }
}
