using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StreetConsole.Extensions.String
{
    static class StringExtension
    {
        public static string[] SplitStringIntoRoadElements(this string str)
        {
            if (str.IndexOf(')') == (str.Length - 1))
            {
                var word1 = str.Substring(0, str.IndexOf('(')).TrimEnd();
                var word2 = str.Substring(str.IndexOf('('));

                return new string[] { word1, word2};
            }
            else
            {
                var word4 = str.Substring(0, str.IndexOf('('));
                var word5 = str.Substring(str.IndexOf('('), str.IndexOf(')') - str.IndexOf('(') + 1);

                var word6 = word4 + word5;

                var word7 = str.Substring(str.IndexOf(')') + 1).TrimStart();

                return new string[] { word6, word7 };
            }
        }

        public static int[] ConvertStringToInt(this string str)
        {
            var ar = new int[2];

            if (str.Contains("("))
            {
                ar[0] = int.Parse(str.Substring(1, str.IndexOf(" ") - 1));
                ar[1] = int.Parse(str.Trim(')').Substring(str.IndexOf("-") + 1));
            }
            else
            {
                ar[0] = int.Parse(str.Substring(0, str.IndexOf(" ") + 1));
                ar[1] = int.Parse(str.Trim(')').Substring(str.IndexOf("-") + 1));
            }

            return ar;
        }
    }
}
