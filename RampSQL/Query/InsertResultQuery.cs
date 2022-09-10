namespace RampSQL.Query
{
    public class InsertResultQuery : IQuerySection
    {
        private IQuerySection parent;
        public InsertResultQuery(IQuerySection parent) { this.parent = parent; }
        public InsertResultQuery GetLastID()
        {
            return this;
        }
    }
}
