using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Life30.Helpers
{
    public static class StringHelper
    {
        public static string space(this String str)
        {
            var result = "";
            foreach (var car in str)
            {
                if (char.IsUpper(car))
                {
                    result += " " + car.ToString().ToUpper();
                }
                else
                {
                    result += car;
                }
            }
            return result;
        }

        public static bool IsUpper(this string value)
        {
            // Consider string to be uppercase if it has no lowercase letters.
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsLower(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsLower(this string value)
        {
            // Consider string to be lowercase if it has no uppercase letters.
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsUpper(value[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
