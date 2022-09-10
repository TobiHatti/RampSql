namespace RampSQL.Query
{
    public class LimitQuery : IQuerySection
    {
        private IQuerySection parent;
        public LimitQuery(IQuerySection parent) { this.parent = parent; }
        public LimitQuery Limit(ulong limitCount)
        {
            return this;
        }

        public LimitQuery Limit(ulong limitCount, int offset)
        {
            return this;
        }

        public LimitQuery Shift(int offset)
        {
            return this;
        }
    }
}
