namespace RampSQL.Query
{
    public class HavingQuery : IQuerySection
    {
        private IQuerySection parent;
        public HavingQuery(IQuerySection parent) { this.parent = parent; }
    }
}
