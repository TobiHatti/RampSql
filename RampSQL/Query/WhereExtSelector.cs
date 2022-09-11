namespace RampSQL.Query
{
    public class WhereExtSelector : GroupQuery, IQuerySection
    {
        public WhereExtSelector(QueryData data) : base(data) { }
        public WhereQuery<WhereExtConnector> Where
        {
            get
            {
                return new WhereQuery<WhereExtConnector>(data);
            }
        }
    }
}
