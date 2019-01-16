using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Extensions
{
    //Created to get list of ProductTypes usig extension method.. called by IEnumberableExtensions class
    public static class ReflectionExtension
    {
        public static string GetPropertyValue<T> (this T item, string propertyName)
        {
            return item.GetType().GetProperty(propertyName).GetValue(item, null).ToString();
        }
    }
}
