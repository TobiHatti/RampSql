using RampSql.QueryBuilder;
using RampSql.Schema;
using System.Reflection;
using System.Text;

namespace RampSql
{
    public class QueryEngine<Schema> where Schema : IRampSchema
    {

        private IRampQuery? query = null;

        public QueryEngine(Func<Schema, IRampQueryInitiator, IRampQuery> query)
        {
            Schema db = (Schema)Activator.CreateInstance(typeof(Schema));
            IRampQueryInitiator initiator = new RampQueryInitiator();
            this.query = query(db, initiator);
        }

        public IRampQuery GetQuery()
        {
            return query;
        }

        public static string ShowSchema()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"S:{typeof(Schema).Name}");
            foreach (PropertyInfo table in typeof(Schema).GetProperties())
            {
                if (table.PropertyType.GetInterfaces().Contains(typeof(IRampTable)))
                {
                    sb.AppendLine($"├─ T:{table.Name}");
                    foreach (PropertyInfo column in table.PropertyType.GetProperties())
                    {
                        if (column.PropertyType.GetInterfaces().Contains(typeof(IRampColumn)))
                        {
                            sb.AppendLine($"│  ├─ C:{column.Name}");
                        }
                    }
                    sb.AppendLine($"│");
                }
            }
            return sb.ToString();
        }
    }
}
