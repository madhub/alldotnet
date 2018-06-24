using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exploration
{
    public static class Enumerables
    {
        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            foreach (T item in @this)
            {
                action(item);
            }
        }
    }
    public class Loh
    {
        static Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
        public static void Demo()
        {
            
            Enumerable.Range(1, 6000).ForEach(i =>
            {
                keyValuePairs.Add(i, i);
            });
            Console.WriteLine(keyValuePairs.Count);
            
        }
    }
}
