using RampSQL.Exceptions;
using RampSQL.Schema;
using System;
using System.Collections.Generic;

namespace RampSQL.Binder
{
    public class RampModelBinder
    {

        public class BindEntry
        {
            public RampColumn Column { get; set; }
            public Type Type { get; set; }
            public Func<object> GetProperty { get; set; }
            public Action<object> SetProperty { get; set; }
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

        public RampModelBinder Bind<T>(RampColumn column, Func<T> getProperty, Action<T> setProperty)
        {
            if (column.GetType() != typeof(T)) throw new RampBindingException();
            Binds.Add(new BindEntry()
            {
                Type = typeof(T),
                Column = column,
                GetProperty = () => getProperty(),
                SetProperty = (e) => setProperty((T)e)
            });
            return this;
        }

        public RampModelBinder BindPrimaryKey<T>(RampColumn column, Func<T> getProperty, Action<T> setProperty)
        {
            if (column.GetType() != typeof(T)) throw new RampBindingException();
            PrimaryKey = new BindEntry()
            {
                Type = typeof(T),
                Column = column,
                GetProperty = () => getProperty(),
                SetProperty = (e) => setProperty((T)e)
            };
            return this;
        }
    }
}
