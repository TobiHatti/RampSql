using RampSQL.Schema;
using System.Collections.Generic;

namespace RampSQL.Query
{
    public class SelectQuery : JoinQuery, IQuerySection
    {
        public SelectQuery(QueryData data) : base(data) { }
        public SelectQuery All()
        {
            data.SelectAll = true;
            return this;
        }

        public SelectQuery Count() => Count(null, null);
        public SelectQuery Count(string alias) => Count(null, alias);
        public SelectQuery Count(RampColumn column) => Count(column, null);
        public SelectQuery Count(RampColumn column, string alias)
        {
            data.CountColumns.Add(new KeyValuePair<RampColumn, string>(column, alias));
            return this;
        }

        public SelectQuery Column(RampColumn column)
        {
            data.SelectColumns.Add(new KeyValuePair<RampColumn, string>(column, null));
            return this;
        }

        public SelectQuery Column(RampColumn column, string alias)
        {
            data.SelectColumns.Add(new KeyValuePair<RampColumn, string>(column, alias));
            return this;
        }

        public SelectQuery Columns(params RampColumn[] columns)
        {
            foreach (var column in columns) data.SelectColumns.Add(new KeyValuePair<RampColumn, string>(column, null));
            return this;
        }

        public SelectQuery Value(object value, string alias)
        {
            data.SelectValues.Add(new KeyValuePair<object, string>(value, alias));
            return this;
        }
    }
}
