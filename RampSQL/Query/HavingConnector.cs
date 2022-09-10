namespace RampSQL.Query
{
    public class HavingConnector : OrderQuery, IQuerySection
    {
        public HavingQuery And
        {
            get
            {
                return null;
            }
        }

        public HavingQuery Or
        {
            get
            {
                return null;
            }
        }
    }
}
