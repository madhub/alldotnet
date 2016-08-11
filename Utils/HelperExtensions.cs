using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class HelperExtensions
    {
        
            public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
            {
                foreach (T element in source)
                {
                    action(element);
                }
            }

        public static long ToUnixTimeStamp(this DateTime dateTimeUtc)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan timeSpan = dateTimeUtc - epoch;
            return Convert.ToInt64(timeSpan.TotalSeconds);
        }
        public static string UserFriendlyFullMessage(this Exception exception)
        {
            StringBuilder exceptionBuilder = new StringBuilder();
            while (exception != null)
            {
                exceptionBuilder.AppendLine(exception.Message);
                exception = exception.InnerException;
            }
            return exceptionBuilder.ToString();
        }
        public static string ToUserFriendlyString(this Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        public static SecureString ToSecureString(this string s)
        {
            if (s == null)
                return null;

            SecureString psw = new SecureString();
            foreach (char c in s.ToCharArray())
            {
                psw.AppendChar(c);
            }
            return psw;
        }
        public static bool IsBetween(this int theNumber, int lower, int higher)
        {
            return (theNumber >= lower) && (theNumber <= higher);
        }
        public static string TrimPrefix(this string str, string prefix)
        {
            if (!String.IsNullOrEmpty(str) && !String.IsNullOrEmpty(prefix) && str.StartsWith(prefix))
            {
                return str.Substring(prefix.Length);
            }
            return str;
        }

        public static string TrimSuffix(this string str, string suffix)
        {
            if (!String.IsNullOrEmpty(str) && !String.IsNullOrEmpty(suffix) && str.EndsWith(suffix))
            {
                return str.Remove(str.Length - suffix.Length);
            }
            return str;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            if (list == null)
                return true;

            if (list is ICollection<T>)
                return ((ICollection<T>)list).Count == 0;
            else
                return !list.Any();
        }

    }
}
