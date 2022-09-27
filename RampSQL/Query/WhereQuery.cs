using RampSQL.Schema;
using System;

namespace RampSQL.Query
{
    public class WhereQuery<Connector> : IQuerySection
    {
        protected QueryData data;
        public WhereQuery(QueryData data) { this.data = data; }

        public WhereQuery<Connector> SectStart
        {
            get
            {
                data.WhereData.Add(new RampWhereData(null, null, WhereType.SectionStart, LikeWildcard.Unspecified, false));
                return this;
            }
        }

        public Connector Is(RampColumn column, RampColumn column2)
        {
            data.WhereData.Add(new RampWhereData(column, new object[] { column2 }, WhereType.Is, LikeWildcard.Unspecified, false));
            return (Connector)Activator.CreateInstance(typeof(Connector), data);
        }
        public Connector Is(RampColumn column, IQuerySection query)
        {
            data.WhereData.Add(new RampWhereData(column, new object[] { query }, WhereType.Is, LikeWildcard.Unspecified, false));
            return (Connector)Activator.CreateInstance(typeof(Connector), data);
        }
        public Connector Is(RampColumn column, object value)
        {
            data.WhereData.Add(new RampWhereData(column, new object[] { value }, WhereType.Is, LikeWildcard.Unspecified, true));
            return (Connector)Activator.CreateInstance(typeof(Connector), data);
        }
        public Connector Not(RampColumn column, RampColumn column2)
        {
            data.WhereData.Add(new RampWhereData(column, new object[] { column2 }, WhereType.IsNot, LikeWildcard.Unspecified, false));
            return (Connector)Activator.CreateInstance(typeof(Connector), data);
        }
        public Connector Not(RampColumn column, IQuerySection query)
        {
            data.WhereData.Add(new RampWhereData(column, new object[] { query }, WhereType.IsNot, LikeWildcard.Unspecified, false));
            return (Connector)Activator.CreateInstance(typeof(Connector), data);
        }
        public Connector Not(RampColumn column, object value)
        {
            data.WhereData.Add(new RampWhereData(column, new object[] { value }, WhereType.IsNot, LikeWildcard.Unspecified, true));
            return (Connector)Activator.CreateInstance(typeof(Connector), data);
        }

        public Connector Like(RampColumn column, object value, LikeWildcard likeWildcard = LikeWildcard.Unspecified)
        {
            data.WhereData.Add(new RampWhereData(column, new object[] { value }, WhereType.IsLike, likeWildcard, true));
            return (Connector)Activator.CreateInstance(typeof(Connector), data);
        }

        public Connector NotLike(RampColumn column, object value, LikeWildcard likeWildcard = LikeWildcard.Unspecified)
        {
            data.WhereData.Add(new RampWhereData(column, new object[] { value }, WhereType.IsNotLike, likeWildcard, true));
            return (Connector)Activator.CreateInstance(typeof(Connector), data);
        }

        public Connector In(RampColumn column, params object[] values)
        {
            data.WhereData.Add(new RampWhereData(column, values, WhereType.In, LikeWildcard.Unspecified, true));
            return (Connector)Activator.CreateInstance(typeof(Connector), data);
        }

        public object[] GetParameters() => data.GetParameters();
        public override string ToString() => data.RenderQuery();
        public IRampQuery Clone() => new QueryEngine((QueryData)data.Clone());
    }
}
