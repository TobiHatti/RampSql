using RampSql.Schema;
using System.Reflection;
using System.Text;

namespace RampSql.QueryBuilder
{
    public class QueryEngine<Schema> where Schema : IRampSchema
    {
        private Dictionary<Type, RampSchema> rampSchemas = new Dictionary<Type, RampSchema>();

        private IRampQuery? query = null;

        public QueryEngine(Func<Schema, RampQueryInitiator<Schema>, IRampQuery> query)
        {
            RegisterSchema(typeof(Schema));

            Schema db = (Schema)rampSchemas[typeof(Schema)].Instance;

            RampQueryInitiator<Schema> initiator = new RampQueryInitiator<Schema>();
            initiator.SetSchema(db);
            this.query = query(db, initiator);
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

        public IRampQuery GetRampQuery() => query;
        public RampBuilder GetBuilder() => new RampBuilder(query.GetData());
        public string GetQuery() => GetBuilder().Build().GetQuery();
        public object[] GetParameters() => GetBuilder().Build().GetParameters();
    }
}
