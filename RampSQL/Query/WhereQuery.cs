using RampSQL.Schema;

namespace RampSQL.Query
{
    public class WhereQuery<Connector> : IQuerySection
    {
        private IQuerySection parent;
        public WhereQuery(IQuerySection parent) { this.parent = parent; }

        public WhereQuery<Connector> SectStart
        {
            get
            {
                return null;
            }
        }

        public Connector Is(RampColumn column, object value)
        {
            return default(Connector);
        }

        public Connector Not(RampColumn column, object value)
        {
            return default(Connector);
        }

        public Connector Like(RampColumn column, object value, LikeWildcard likeWildcard = LikeWildcard.Unspecified)
        {
            return default(Connector);
        }

        public Connector NotLike(RampColumn column, object value, LikeWildcard likeWildcard = LikeWildcard.Unspecified)
        {
            return default(Connector);
        }
    }
}
