namespace RampSQL.Query
{
    public class WhereConnector : IQuerySection
    {
        private IQuerySection parent;
        public WhereConnector(IQuerySection parent) { this.parent = parent; }
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
