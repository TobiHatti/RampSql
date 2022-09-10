namespace RampSQL.Query
{
    public class WhereConnector : IQuerySection
    {
        public WhereQuery<WhereConnector> And
        {
            get
            {
                return null;
            }
        }

        public WhereQuery<WhereConnector> Or
        {
            get
            {
                return null;
            }
        }

        public WhereConnector SectEnd
        {
            get
            {
                return null;
            }
        }
    }
}
