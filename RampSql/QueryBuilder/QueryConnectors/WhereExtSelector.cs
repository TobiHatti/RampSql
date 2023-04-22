namespace RampSql.QueryBuilder
{
    public class WhereExtSelector : GroupQuery, IRampQuery
    {
        internal WhereExtSelector(RampQueryData data) : base(data) { }

        public WhereQuery<WhereExtConnector> Where
        {
            get
            {
                return new WhereQuery<WhereExtConnector>(data);
            }
        }
    }
}
