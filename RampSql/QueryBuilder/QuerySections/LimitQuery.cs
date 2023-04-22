namespace RampSql.QueryBuilder
{
    public class LimitQuery : QueryHead, IRampQuery
    {
        internal LimitQuery(RampQueryData data) : base(data) { }

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
    }
}
