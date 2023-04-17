using RampSql.Schema;

namespace RampSql.QueryBuilder
{
    public class HavingQuery : QueryHead, IRampQuery
    {
        public HavingQuery(RampQueryData data) : base(data) { }

        public HavingConnector DevProperty(IRampColumn column, object value)
        {
            return new HavingConnector(data);
        }
    }
}
