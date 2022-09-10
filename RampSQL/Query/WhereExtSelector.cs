namespace RampSQL.Query
{
    public class WhereExtSelector : GroupQuery, IQuerySection
    {
        public WhereExtSelector(IQuerySection parent) : base(parent) { }
        public WhereQuery<WhereExtConnector> Where
        {
            get
            {
                return null;
            }
        }
    }
}
