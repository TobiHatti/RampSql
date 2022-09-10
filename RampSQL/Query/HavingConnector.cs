namespace RampSQL.Query
{
    public class HavingConnector : OrderQuery, IQuerySection
    {
        public HavingConnector(IQuerySection parent) : base(parent) { }
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
