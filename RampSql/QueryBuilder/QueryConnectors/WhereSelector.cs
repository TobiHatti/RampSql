namespace RampSql.QueryBuilder
{
    public class WhereSelector : QueryHead, IRampQuery
    {
        internal WhereSelector(RampQueryData data) : base(data) { }

        public WhereQuery<WhereConnector> Where
        {
            get
            {
                return new WhereQuery<WhereConnector>(data);
            }
        }
    }
}
