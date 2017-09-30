using System;
using System.Reflection;

namespace Quotes.Data.Utils
{
    public class TypeExtensions
    {
        public static T Parse<T>(string s)
        {
            var t = typeof(T);
            var parse = t.GetMethod("Parse", new[] { typeof(string) });

            if (parse == null)
                throw new MethodAccessException($"The Parse method does not exist for type {t.Name}.");
            try
            {
                return (T)parse.Invoke(null, new object[] { s });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
