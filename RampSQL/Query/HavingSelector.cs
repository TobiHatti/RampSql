namespace RampSQL.Query
{
    public class HavingSelector : OrderQuery, IQuerySection
    {
        public HavingSelector(IQuerySection parent) : base(parent) { }
        public HavingQuery Having
        {
            get
            {
                return null;
            }
        }
    }
}
