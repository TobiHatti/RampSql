namespace RampSQL.Query
{
    public class HavingSelector : OrderQuery, IQuerySection
    {
        public HavingSelector(QueryData data) : base(data) { }
        public HavingQuery Having
        {
            get
            {
                return new HavingQuery(data);
            }
        }
    }
}
