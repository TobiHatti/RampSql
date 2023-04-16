using RampSql.QueryBuilder;

namespace RampSql.QuerySections
{
    public class LimitQuery : IRampQuery
    {
        protected RampQueryData data;
        internal LimitQuery(RampQueryData data) { this.data = data; }

        public LimitQuery Limit(ulong limitCount) { return this; }
        public LimitQuery Limit(ulong limitCount, int offset) { return this; }
        public LimitQuery Shift(int offset) { return this; }
    }
}
