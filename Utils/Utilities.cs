using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class Utilities
    {
        public static string format = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffff";
        public static string DateTimeToString(DateTime dt)
        {
            return dt.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string ExceptionToString(
            this Exception ex,
            Action<StringBuilder> customFieldsFormatterAction)
        {
            var description = new StringBuilder();
            description.AppendFormat("{0}: {1}", ex.GetType().Name, ex.Message);

            if (customFieldsFormatterAction != null)
                customFieldsFormatterAction(description);

            if (ex.InnerException != null)
            {
                description.AppendFormat(" ---> {0}", ex.InnerException);
                description.AppendFormat(
                    "{0}   --- End of inner exception stack trace ---{0}",
                    Environment.NewLine);
            }

            description.Append(ex.StackTrace);

            return description.ToString();
        }
    }
}
}
