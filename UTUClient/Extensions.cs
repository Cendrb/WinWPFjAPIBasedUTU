using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTUClient
{
    static class Extensions
    {
        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("You need to pass in a string, yo!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}
