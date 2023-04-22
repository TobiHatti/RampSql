namespace RampSql.QueryBuilder
{
    public class HavingSelector : OrderQuery, IRampQuery
    {
        internal HavingSelector(RampQueryData data) : base(data) { }

        public HavingQuery Having
        {
            get
            {
                return new HavingQuery(data);
            }
        }
    }
}
