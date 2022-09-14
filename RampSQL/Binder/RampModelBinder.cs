using RampSQL.Exceptions;
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
            ReferenceArray
        }

        public class BindEntry
        {
            public RampColumn Column { get; set; }
            public RampColumn ReferenceColumn { get; set; }
            public BindType BindType { get; set; } = BindType.Primitive;
            public Type Type { get; set; }
            public Func<object> Get { get; set; }
            public Action<object> Set { get; set; }
        }

        public RampTable Target { get; private set; }

        public List<BindEntry> Binds { get; } = new List<BindEntry>();
        public BindEntry PrimaryKey { get; private set; }

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

        public RampModelBinder Bind<T>(RampColumn column, Func<T> getProperty, Action<T> setProperty) where T : struct
        {
            if (column.GetType() != typeof(T)) throw new RampBindingException();
            Binds.Add(CreateBindEntry(column, null, getProperty, setProperty, BindType.Primitive));
            return this;
        }

        public RampModelBinder Bind(RampColumn column, Func<string> getProperty, Action<string> setProperty)
        {
            if (column.GetType() != typeof(string)) throw new RampBindingException();
            Binds.Add(CreateBindEntry(column, null, getProperty, setProperty, BindType.Primitive));
            return this;
        }

        public RampModelBinder BindPrimaryKey<T>(RampColumn column, Func<T> getProperty, Action<T> setProperty) where T : struct
        {
            if (column.GetType() != typeof(T)) throw new RampBindingException();
            PrimaryKey = CreateBindEntry(column, null, getProperty, setProperty, BindType.Primary);
            return this;
        }

        public RampModelBinder BindPrimaryKey(RampColumn column, Func<string> getProperty, Action<string> setProperty)
        {
            if (column.GetType() != typeof(string)) throw new RampBindingException();
            PrimaryKey = CreateBindEntry(column, null, getProperty, setProperty, BindType.Primary);
            return this;
        }

        public RampModelBinder ReferenceBind<T>(RampColumn localColumn, RampColumn referenceColumn, Func<T> getProperty, Action<T> setProperty) where T : IRampBindable
        {
            if (localColumn.GetType() != typeof(T) || referenceColumn.GetType() != typeof(T)) throw new RampBindingException();
            Binds.Add(CreateBindEntry(localColumn, referenceColumn, getProperty, setProperty, BindType.Reference));
            return this;
        }

        public RampModelBinder ReferenceBind<T>(RampColumn localColumn, RampColumn referenceColumn, Func<T[]> getProperty, Action<T[]> setProperty) where T : IRampBindable
        {
            if (localColumn.GetType() != typeof(T) || referenceColumn.GetType() != typeof(T)) throw new RampBindingException();
            Binds.Add(CreateBindEntry(localColumn, referenceColumn, getProperty, setProperty, BindType.ReferenceArray));
            return this;
        }

        private BindEntry CreateBindEntry<T>(RampColumn localColumn, RampColumn referenceColumn, Func<T> getProperty, Action<T> setProperty, BindType type)
        {
            return new BindEntry()
            {
                Type = typeof(T),
                BindType = type,
                Column = localColumn,
                ReferenceColumn = referenceColumn,
                Get = () => getProperty(),
                Set = (e) => setProperty((T)Convert.ChangeType(e, typeof(T)))
            };
        }
    }
}
