using System;
using System.Linq;

namespace StructureMapExample.Utils
{
    public static class Extensions
    {
        public static string PadLeftWithContents(this string str, int amount)
        {
            return str.PadLeft(amount + str.Length/2);
        }

        public static string Repeat(this string str, int amount)
        {
            return String.Join("", Enumerable.Repeat(str, amount));
        }

        public static string ToSemiCurrency(this double dbl)
        {
            return String.Format("{0:n}", dbl);
        }
    }
}