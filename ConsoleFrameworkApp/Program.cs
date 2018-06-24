using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleFrameworkApp
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
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 50;i++)
            {
                Console.WriteLine("Calling Demo...");
                Demo();
                Thread.Sleep(1000);
            }
            
            
            Console.ReadLine();
        }

        
        public static void Demo()
        {
            Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
            Enumerable.Range(1, 6000).ForEach(i =>
            {
                keyValuePairs.Add(i, i);
            });
            Console.WriteLine(keyValuePairs.Count);
            //Console.ReadLine();

        }
    }


}
