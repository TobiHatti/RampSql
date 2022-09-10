namespace RampSQL.Query
{
    public class WhereExtSelector : GroupQuery, IQuerySection
    {
        public WhereQuery<WhereExtConnector> Where
        {
            get
            {
                return null;
            }
        }
    }
}
