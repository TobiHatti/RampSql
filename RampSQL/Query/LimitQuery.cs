namespace RampSQL.Query
{
    public class LimitQuery : IQuerySection
    {
        protected QueryData data;
        public LimitQuery(QueryData data) { this.data = data; }
        public LimitQuery Limit(ulong limitCount)
        {
            data.SelectLimit = limitCount;
            return this;
        }

        public LimitQuery Limit(ulong limitCount, int offset)
        {
            data.SelectLimit = limitCount;
            data.SelectOffset = offset;
            return this;
        }

        public LimitQuery Shift(int offset)
        {
            data.SelectOffset = offset;
            return this;
        }

        public object[] GetParameters() => data.GetParameters();
        public override string ToString() => data.RenderQuery();
    }
}
