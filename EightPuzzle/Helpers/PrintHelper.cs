using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightPuzzle.Helpers
{
    internal static class PrintHelper
    {
        public static void PrintResult<T>(this IEnumerable<T> enumerable) where T : IEnumerable<byte>
        {
            foreach (var elem in enumerable)
            {
                foreach (var x in elem)
                {
                    Console.Write($"{x} ");
                }
                Console.WriteLine();
            }
        }
    }
}
