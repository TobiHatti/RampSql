namespace RampSQL.Query
{
    public class WhereSelector : IQuerySection
    {
        private IQuerySection parent;
        public WhereSelector(IQuerySection parent)
        {
            this.parent = parent;
        }
        public WhereQuery<WhereConnector> Where
        {
            get
            {
                return null;
            }
        }
    }
}
