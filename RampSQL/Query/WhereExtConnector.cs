namespace RampSQL.Query
{
    public class WhereExtConnector : GroupQuery, IQuerySection
    {
        public WhereExtConnector(IQuerySection parent) : base(parent) { }
        public WhereQuery<WhereExtConnector> And
        {
            get
            {
                return null;
            }
        }

        public WhereQuery<WhereExtConnector> Or
        {
            get
            {
                return null;
            }
        }

        public WhereExtConnector SectEnd
        {
            get
            {
                return null;
            }
        }
    }
}
