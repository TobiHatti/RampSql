using RampSql.QueryBuilder;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class WhereQuery<Connector> : IRampQuery
    {
        protected RampQueryData data;
        internal WhereQuery(RampQueryData data) { this.data = data; }

        public WhereQuery<Connector> SectStart
        {
            get
            {
                return this;
            }
        }

        public Connector Is(IRampColumn column, IRampColumn column2) { return this; }
        public Connector Is(IRampColumn column, IRampQuery query) { return this; }
        public Connector Is(IRampColumn column, object value) { return this; }
        public Connector Not(IRampColumn column, IRampColumn column2) { return this; }
        public Connector Not(IRampColumn column, IRampQuery query) { return this; }
        public Connector Not(IRampColumn column, object value) { return this; }
        public Connector Like(IRampColumn column, object value, LikeWildcard likeWildcard = LikeWildcard.Unspecified) { return this; }
        public Connector NotLike(IRampColumn column, object value, LikeWildcard likeWildcard = LikeWildcard.Unspecified) { return this; }
        public Connector In(IRampColumn column, params object[] values) { return this; }
        public Connector IsNull(IRampColumn column) { return this; }
        public Connector IsNotNull(IRampColumn column) { return this; }

    }
}
