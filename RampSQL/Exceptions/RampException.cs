using System;
using System.Text;

namespace RampSQL.Exceptions
{
    public class RampException : Exception
    {
        public RampException() : base(MessageFormat(string.Empty)) { }

        public RampException(string message) : base(MessageFormat(message)) { }

        public RampException(string message, Exception inner) : base(MessageFormat(message), inner) { }

        public static string MessageFormat(string message)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Warning: RampSQL Exception thrown!");
            if (string.IsNullOrEmpty(message)) sb.AppendLine("No further information given.");
            else sb.AppendLine(message);

            return sb.ToString();
        }
    }

    public class RampBindingException : Exception
    {
        public RampBindingException() : base(MessageFormat(string.Empty)) { }

        public RampBindingException(string message) : base(MessageFormat(message)) { }

        public RampBindingException(string message, Exception inner) : base(MessageFormat(message), inner) { }

        public static string MessageFormat(string message)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Warning: RampSQL Binding Exception thrown!");
            sb.AppendLine("The types of the bindable property must be identical to the assigned column of the ramp-schema");
            if (string.IsNullOrEmpty(message)) sb.AppendLine("No further information given.");
            else sb.AppendLine(message);

            return sb.ToString();
        }
    }
}
