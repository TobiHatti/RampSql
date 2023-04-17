using RampSql.QueryBuilder;

namespace RampSql.QuerySections
{
    public class LimitQuery : IRampQuery
    {
        protected RampQueryData data;
        internal LimitQuery(RampQueryData data) { this.data = data; }

        public LimitQuery Shift(int offset) => Limit(ulong.MaxValue, (ulong)offset);
        public LimitQuery Shift(ulong offset) => Limit(ulong.MaxValue, offset);
        public LimitQuery Limit(int limitCount) => Limit((ulong)limitCount, 0);
        public LimitQuery Limit(ulong limitCount) => Limit(limitCount, 0);
        public LimitQuery Limit(int limitCount, int offset) => Limit((ulong)limitCount, (ulong)offset);
        public LimitQuery Limit(ulong limitCount, ulong offset)
        {
            data.SelectLimit = limitCount;
            data.SelectOffset = offset;
            return this;
        }


        public string RealName => null;
        public string QuotedSelectorName => null;
        public string AliasDeclaring => null;
        public bool HasAlias => !string.IsNullOrEmpty(data.QueryAlias);
        public RampQueryData GetData() => data;
        public void AsAlias(string alias) => data.QueryAlias = alias;
    }
}
