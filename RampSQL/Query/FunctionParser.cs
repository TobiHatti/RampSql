using System.Text;

namespace RampSQL.Query
{
    public class FunctionParser
    {
        public static string Parse(SQLFunction function, bool parameterize, params object[] parameters)
        {
            StringBuilder query = new StringBuilder();
            query.Append($"{function}(");
            switch (function)
            {
                case SQLFunction.NOW:
                    // Insert parameters for future methods here
                    break;
            }
            query.Append(")");
            return query.ToString();
        }
    }
}
