using RampSql.QueryBuilder;
using RampSql.QueryConnectors;
using RampSql.Schema;

namespace RampSql.QuerySections
{
    public class HavingQuery : IRampQuery
    {
        protected RampQueryData data;
        internal HavingQuery(RampQueryData data) { this.data = data; }



        public HavingConnector DevProperty(IRampColumn column, object value)
        {
            return new HavingConnector(data);
        }

        public string RealName => null;
        public string QuotedSelectorName => null;
        public string AliasDeclaring => null;
        public bool HasAlias => !string.IsNullOrEmpty(data.QueryAlias);
        public RampQueryData GetData() => data;
        public void AsAlias(string alias) => data.QueryAlias = alias;
        public object[] GetParameterValues() => new object[0];
    }
}
