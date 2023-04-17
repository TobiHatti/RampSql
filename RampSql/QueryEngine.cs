using RampSql.QueryBuilder;
using RampSql.Schema;
using System.Reflection;
using System.Text;

namespace RampSql
{
    public class QueryEngine<Schema> where Schema : IRampSchema
    {
        private Dictionary<Type, RampSchema> rampSchemas = new Dictionary<Type, RampSchema>();

        private IRampQuery? query = null;

        public QueryEngine(Func<Schema, RampQueryInitiator, IRampQuery> query)
        {
            RegisterSchema(typeof(Schema));

            Schema db = (Schema)rampSchemas[typeof(Schema)].Instance;

            RampQueryInitiator initiator = new RampQueryInitiator();
            this.query = query(db, initiator);
        }

        public IRampQuery GetQuery()
        {
            return query;
        }

        public string GetQueryString()
        {
            return new RampBuilder(query.GetData()).Build();
        }

        private RampSchema RegisterSchema(Type schemaType)
        {
            if (!rampSchemas.ContainsKey(schemaType)) rampSchemas.Add(schemaType, new RampSchema(schemaType));
            return rampSchemas[schemaType];
        }


        public static string ShowSchema()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"S:{typeof(Schema).Name}");
            foreach (PropertyInfo table in typeof(Schema).GetProperties())
            {
                if (table.PropertyType.GetInterfaces().Contains(typeof(IRampTable)))
                {
                    sb.AppendLine($"├─ T:{table.Name} {table.GetHashCode()}");
                    foreach (PropertyInfo column in table.PropertyType.GetProperties())
                    {
                        if (column.PropertyType.GetInterfaces().Contains(typeof(IRampColumn)))
                        {
                            sb.AppendLine($"│  ├─ C:{column.Name} {column.GetHashCode()}");
                        }
                    }
                    sb.AppendLine($"│");
                }
            }
            return sb.ToString();
        }
    }
}
