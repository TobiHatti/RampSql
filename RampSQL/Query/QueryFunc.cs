using System.Text;

namespace RampSQL.Query
{
    public class QueryFunc
    {
        public string Function { get; set; }
        public string Alias { get; set; }
        public object[] Parameters { get; set; }

        public QueryFunc(SqlFunction function) : this(function, null) { }
        public QueryFunc(SqlFunction function, string alias, params object[] parameters) => SetFunc(function.ToString(), alias, parameters);
        public QueryFunc(MySqlFunction function) : this(function, null) { }
        public QueryFunc(MySqlFunction function, string alias, params object[] parameters) => SetFunc(function.ToString(), alias, parameters);

        private void SetFunc(string function, string alias, params object[] parameters)
        {
            Function = function;
            Alias = alias;
            Parameters = parameters;
        }

        public override string ToString()
        {
            StringBuilder query = new StringBuilder();
            query.Append($"{Function}(");
            bool first = true;
            foreach (object parameter in Parameters)
            {
                if (!first) query.Append(',');
                query.Append(parameter.ToString());
                first = false;
            }
            query.Append(')');
            if (!string.IsNullOrEmpty(Alias)) query.Append($" AS {Alias}");
            return query.ToString();
        }
    }
}
