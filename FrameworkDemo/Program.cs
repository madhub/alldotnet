using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FrameworkDemo
{
    public class Customer
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public override string ToString()
        {
            return String.Format("{0}.{1}", FirstName, LastName);
        }
    }
    public class CloudServer
    {
        public string CloudProvider { get; set; }
        public string ImageId { get; set; }
        public string Size { get; set; }
    }
    public class CloudServersKeyedDictionary : KeyedCollection<string, CloudServer>
    {
        protected override string GetKeyForItem(CloudServer item)
        {
            return item.ImageId;
        }
    }

    public class Singer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Patent Started ...");
                Task childTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Child running. Going to sleep for a sec.");
                    Thread.Sleep(5000);
                    Console.WriteLine("Child finished ");

                }, TaskCreationOptions.AttachedToParent);
                Console.WriteLine("Patent Finished ...");
            }).Wait();

            Console.WriteLine("Waiting ...");
            String s1 = "s1";

            String value = s1 ?? "madhu";
            ParallelForEachDemo();
            //KeyedCollectionDemo();



            //StringJoinTest();
            // HttpClientHandlerTest();

            Console.ReadLine();

        }

        private static void EnumerableAllDemo()
        {
            string[] bands = { "ACDC", "Queen", "Aerosmith", "Iron Maiden", "Megadeth", "Metallica", "Cream", "Oasis", "Abba", "Blur", "Chic", "Eurythmics", "Genesis", "INXS", "Midnight Oil", "Kent", "Madness", "Manic Street Preachers", "Noir Desir", "The Offspring", "Pink Floyd", "Rammstein", "Red Hot Chili Peppers", "Tears for Fears", "Deep Purple", "KISS" };
            // checks whether all elements in the seq has length > 3
            var results = bands.All(a => a.Length > 3);
            var upperCase = bands.All(a =>
            {
                Console.WriteLine(a.ToUpper());
                return true;
            });
        }
        private static void SequenceEqulaDemo()
        {
            string[] bands = { "ACDC", "Queen", "Aerosmith", "Iron Maiden", "Megadeth", "Metallica", "Cream", "Oasis", "Abba", "Blur", "Chic", "Eurythmics", "Genesis", "INXS", "Midnight Oil", "Kent", "Madness", "Manic Street Preachers", "Noir Desir", "The Offspring", "Pink Floyd", "Rammstein", "Red Hot Chili Peppers", "Tears for Fears", "Deep Purple", "KISS" };

            string[] bandsTwo = { "ACDC", "Queen", "Aerosmith", "Iron Maiden", "Megadeth", "Metallica", "Cream", "Oasis", "Abba", "Blur", "Chic", "Eurythmics", "Genesis", "INXS", "Midnight Oil", "Kent", "Madness", "Manic Street Preachers", "Noir Desir", "The Offspring", "Pink Floyd", "Rammstein", "Red Hot Chili Peppers", "Tears for Fears", "Deep Purple", "KISS" };

            var output = bands.SequenceEqual(bandsTwo);
        }
        private static void ToLookUpDemo()
        {
            // A Lookup< TKey, TElement > resembles a Dictionary< TKey, TValue >.The difference is that a Dictionary< TKey, TValue > maps keys to single values,
            // https://msdn.microsoft.com/en-us/library/bb460184(v=vs.100).aspx


        }
        private static void ToDictionary()
        {
            IEnumerable<Singer> singers = new List<Singer>()
                    {
                        new Singer(){Id = 1, FirstName = "Freddie", LastName = "Mercury"}
                        , new Singer(){Id = 2, FirstName = "Elvis", LastName = "Presley"}
                        , new Singer(){Id = 3, FirstName = "Chuck", LastName = "Berry"}
                        , new Singer(){Id = 4, FirstName = "Ray", LastName = "Charles"}
                        , new Singer(){Id = 5, FirstName = "David", LastName = "Bowie"}
                    };

            // converts seq to dictionary using key & value selectors
            var singerDictionary = singers.ToDictionary(s => s.Id, s => String.Format("{0} {1}", s.FirstName, s.LastName));


        }
        private static void UriBuilderDemo()
        {
            UriBuilder uriBuilder1 = new UriBuilder();
            uriBuilder1.Scheme = "http";
            uriBuilder1.Host = "cnn.com";
            uriBuilder1.Path = "americas";
            Uri uri = uriBuilder1.Uri;

            UriBuilder uriBuilder2 = new UriBuilder();
            uriBuilder2.Scheme = "http";
            uriBuilder2.Host = "www.cnn.com";
            uriBuilder2.Path = "americas";
            uriBuilder2.Port = 8089;
            uriBuilder2.UserName = "andras";
            uriBuilder2.Password = "secret";
            uriBuilder2.Query = "search=usa";

            UriBuilder uriBuilder3 = new UriBuilder();
            uriBuilder3.Scheme = "c";
            uriBuilder3.Host = @"temp";
            uriBuilder3.Path = "log.txt";

        }
        private static void MapTest()
        {
            List<CloudServer> cloudServers = new List<CloudServer> {
            new CloudServer() { CloudProvider = "Amazon", ImageId = "aaa", Size = "m2.large" },
            new CloudServer() { CloudProvider = "Rackspace", ImageId = "bbb", Size = "large" },
            new CloudServer() { CloudProvider = "Amazon", ImageId = "ccc", Size = "dw2.xlarge" },
            new CloudServer() { CloudProvider = "Azure", ImageId = "ddd", Size = "small" },
            new CloudServer() { CloudProvider = "Google", ImageId = "eee", Size = "medium" },
            new CloudServer() { CloudProvider = "Azure", ImageId = "fff", Size = "x-large" }};

            // map only the name
            var cloudServerNames = cloudServers.Select(srv => srv.CloudProvider);

            // transform into anonymousTypes with name & length

            var collection = cloudServers.Select(c1 => new { c1.CloudProvider, c1.CloudProvider.Length });

            //The SelectMany operator creates a one - to - many output projection sequence over an input 
            //sequence.SelectMany will return 0 or more output elements for every input element.Let’s see an example.
            var smany = cloudServers.SelectMany(c1 => c1.CloudProvider);




        }
        private static void KeyedCollectionDemo()
        {
            // KeyedCollection object can be used to declare which field of your custom object to use as a key in a Dictionary
            CloudServersKeyedDictionary keyedDict = new CloudServersKeyedDictionary();
            keyedDict.Add(new CloudServer() { CloudProvider = "Amazon", ImageId = "aaa", Size = "m2.large" });
            keyedDict.Add(new CloudServer() { CloudProvider = "Rackspace", ImageId = "bbb", Size = "large" });
            keyedDict.Add(new CloudServer() { CloudProvider = "Amazon", ImageId = "ccc", Size = "dw2.xlarge" });
            keyedDict.Add(new CloudServer() { CloudProvider = "Azure", ImageId = "ddd", Size = "small" });
            keyedDict.Add(new CloudServer() { CloudProvider = "Google", ImageId = "eee", Size = "medium" });
            keyedDict.Add(new CloudServer() { CloudProvider = "Azure", ImageId = "fff", Size = "x-large" });

            // using
            CloudServer aaaServer = keyedDict["aaa"];
        }

        private static void HttpClientHandlerTest()
        {
            HttpClientHandler clientHandler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                UseProxy = true,
            };
            HttpClient client = new HttpClient(clientHandler);
        }

        private static void StringJoinTest()
        {
            var customers = new List<Customer> { new Customer { FirstName = "Joe", LastName = "Kane" }, new Customer { FirstName = "Adam", LastName = "Smith" } };

            var joined = String.Join<Customer>("|", customers);
            Console.WriteLine(joined);
        }

        private static void ParallelForEachDemo()
        {
            int[] integerList = new int[100];
            for (int i = 0; i < integerList.Length; i++)
            {
                integerList[i] = i;
            }

            var result =
                integerList.AsParallel().AsOrdered()
                .Take(10)
                .Select(item => new
                {
                    SourceValue = item,
                    ResultValue = Math.Pow(item, 2)
                });

            foreach (var v in result)
            {
                Console.WriteLine("Source {0}, Result {1}",
                    v.SourceValue, v.ResultValue);
            }
        }
    }
}
