using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Quotes.Data.Utils
{
    public class TypeExtensions
    {
        public static T Parse<T>(string s)
        {
            var t = typeof(T);
            // Attempt to execute the Parse method on the type if it exists. 
            var parse = t.GetMethod("Parse", new Type[] { typeof(string) });

            if (parse == null)
                throw new MethodAccessException(String.Format("The Parse method does not exist for type {0}.", t.Name));
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
