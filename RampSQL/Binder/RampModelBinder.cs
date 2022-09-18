using RampSQL.Exceptions;
using RampSQL.Query;
using RampSQL.Reader;
using RampSQL.Schema;
using System;
using System.Collections.Generic;

namespace RampSQL.Binder
{
    public class RampModelBinder
    {
        public enum BindType
        {
            Primitive,
            Primary,
            Reference,
            ReferenceArray,
            BindAll,
            BindAllArray,
            CallingParent
        }

        public class BindEntry
        {
            public RampColumn Column { get; set; }
            public RampColumn ReferenceColumn { get; set; }
            public BindType BindType { get; set; } = BindType.Primitive;
            public Type Type { get; set; }
            public Func<object> Get { get; set; }
            public Action<object> Set { get; set; }
            public Func<RampReader, RampColumn, object> CustomReader { get; set; }
        }

        public class TableLinkEntry
        {
            public RampColumn LocalColumn { get; set; }
            public RampColumn ReferenceColumn { get; set; }
            public TableJoinType RefJoinType { get; set; } = TableJoinType.Inner;
        }

        public RampTable Target { get; private set; }
        public List<TableLinkEntry> TableLinks { get; } = new List<TableLinkEntry>();
        public List<BindEntry> Binds { get; } = new List<BindEntry>();
        public BindEntry PrimaryKey { get; private set; } = null;
        public BindEntry CallingParent { get; private set; } = null;
        public Action<IRampBindable> BeforeLoadEvent { get; set; } = null;
        public Action<IRampBindable> AfterLoadEvent { get; set; } = null;

        public BindEntry this[int id]
        {
            get => Binds[id];
        }

        public BindEntry this[string propertyName]
        {
            get
            {
                foreach (BindEntry entry in Binds)
                    if (entry.Column.UCN == propertyName) return entry;
                return null;
            }
        }

        public BindEntry this[RampColumn column]
        {
            get
            {
                foreach (BindEntry entry in Binds)
                    if (entry.Column == column) return entry;
                return null;
            }
        }

        public RampModelBinder SetTarget(RampTable table)
        {
            Target = table;
            return this;
        }

        public RampModelBinder LinkTable(RampColumn localColumn, RampColumn referenceColumn) => LinkTable(localColumn, referenceColumn, TableJoinType.Inner);
        public RampModelBinder LinkTable(RampColumn localColumn, RampColumn referenceColumn, TableJoinType joinType)
        {
            TableLinkEntry tle = new TableLinkEntry()
            {
                LocalColumn = localColumn,
                ReferenceColumn = referenceColumn,
                RefJoinType = joinType
            };
            if (!ContainsTableLinkEntry(tle)) TableLinks.Add(tle);
            return this;
        }

        public RampModelBinder Bind<T>(RampColumn column, Func<T> getProperty, Action<T> setProperty) where T : struct
        {
            if (column.GetType() != typeof(T)) throw new RampBindingException($"Binding of {column} failed.");
            Binds.Add(CreateBindEntry(column, null, getProperty, setProperty, BindType.Primitive));
            return this;
        }

        public RampModelBinder Bind(RampColumn column, Func<string> getProperty, Action<string> setProperty)
        {
            if (column.GetType() != typeof(string)) throw new RampBindingException($"Binding of {column} failed.");
            Binds.Add(CreateBindEntry(column, null, getProperty, setProperty, BindType.Primitive));
            return this;
        }

        public RampModelBinder Bind<T>(RampColumn column, Func<T> getProperty, Action<T> setProperty, Func<RampReader, RampColumn, T> customReader) where T : struct
        {
            if (column.GetType() != typeof(T)) throw new RampBindingException($"Binding of {column} failed.");
            Binds.Add(CreateBindEntry(column, null, getProperty, setProperty, BindType.Primitive, null, customReader));
            return this;
        }

        public RampModelBinder Bind(RampColumn column, Func<string> getProperty, Action<string> setProperty, Func<RampReader, RampColumn, string> customReader)
        {
            if (column.GetType() != typeof(string)) throw new RampBindingException($"Binding of {column} failed.");
            Binds.Add(CreateBindEntry(column, null, getProperty, setProperty, BindType.Primitive, null, customReader));
            return this;
        }


        public RampModelBinder BindPrimaryKey<T>(RampColumn column, Func<T> getProperty, Action<T> setProperty) where T : struct
        {
            if (column.GetType() != typeof(T)) throw new RampBindingException($"Binding of {column} failed.");
            PrimaryKey = CreateBindEntry(column, null, getProperty, setProperty, BindType.Primary);
            return this;
        }

        public RampModelBinder BindPrimaryKey(RampColumn column, Func<string> getProperty, Action<string> setProperty)
        {
            if (column.GetType() != typeof(string)) throw new RampBindingException($"Binding of {column} failed.");
            PrimaryKey = CreateBindEntry(column, null, getProperty, setProperty, BindType.Primary);
            return this;
        }

        public RampModelBinder ReferenceBind<T>(RampColumn localColumn, RampColumn referenceColumn, Func<T> getProperty, Action<T> setProperty) where T : IRampBindable
        {
            Binds.Add(CreateBindEntry(localColumn, referenceColumn, getProperty, setProperty, BindType.Reference));
            return this;
        }

        public RampModelBinder ReferenceBind<T>(RampColumn localColumn, RampColumn referenceColumn, Func<T[]> getProperty, Action<T[]> setProperty) where T : IRampBindable
        {
            Binds.Add(CreateBindEntry(localColumn, referenceColumn, getProperty, setProperty, BindType.ReferenceArray, typeof(T)));
            return this;
        }

        public RampModelBinder BindAll<T>(Func<T> getProperty, Action<T> setProperty) where T : IRampBindable
        {
            Binds.Add(CreateBindEntry(null, null, getProperty, setProperty, BindType.BindAll, typeof(T)));
            return this;
        }

        public RampModelBinder BindAll<T>(Func<T[]> getProperty, Action<T[]> setProperty) where T : IRampBindable
        {
            Binds.Add(CreateBindEntry(null, null, getProperty, setProperty, BindType.BindAllArray, typeof(T)));
            return this;
        }

        public  RampModelBinder BindCallingParent<T>(Func<T> getProperty, Action<T> setProperty) where T : IRampBindable
        {
            CallingParent = CreateBindEntry(null, null, getProperty, setProperty, BindType.CallingParent);
        }

        private bool ContainsTableLinkEntry(TableLinkEntry newEntry)
        {
            foreach (TableLinkEntry entry in TableLinks)
            {
                if (entry.ReferenceColumn == newEntry.ReferenceColumn && entry.LocalColumn == newEntry.LocalColumn) return true;
            }
            return false;
        }

        private BindEntry CreateBindEntry<T>(RampColumn localColumn, RampColumn referenceColumn, Func<T> getProperty, Action<T> setProperty, BindType type, Type typeOverride = null, Func<RampReader, RampColumn, T> customReader = null)
        {
            Type setType = typeOverride != null ? typeOverride : typeof(T);
            BindEntry be = new BindEntry()
            {
                Type = setType,
                BindType = type,
                Column = localColumn,
                ReferenceColumn = referenceColumn,
                Get = () => getProperty(),
                Set = (e) =>
                {
                    if (typeof(T).IsEnum)
                    {
                        setProperty((T)Enum.Parse(typeof(T), Convert.ToString(e), true));
                    }
                    else
                    {
                        setProperty((T)Convert.ChangeType(e, typeof(T)));
                    }

                },
                CustomReader = null
            };

            if (customReader != null)
            {
                be.CustomReader = (reader, column) => customReader(reader, column);
            }

            return be;
        }
    }
}
