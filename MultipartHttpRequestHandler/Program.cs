using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MultipartHttpRequestHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            String url = "http://dicomserver.co.uk:81/wado/studies/1.2.826.0.1.3680043.6.7026.5202.20150315133048.160.2/series/1.2.826.0.1.3680043.6.5932.1974.20150315133048.160.3/instances/1.2.826.0.1.3680043.6.85.8307.20150315133048.160.4";
            var client = new HttpClient();

            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Type", 
                "multipart / related; type = \"application/dicom\"");


            var dataFromSrv = client.GetAsync(url);
            var result = dataFromSrv.Result;

            var multiPartProvider = result.Content.ReadAsMultipartAsync();
            var httpContents = multiPartProvider.Result.Contents;
            foreach (var item in httpContents)
            {
                Console.WriteLine(item.Headers.ContentType);
                String tempFileName = Path.GetTempFileName() + "dcm";
                item.ReadAsStreamAsync().Result.CopyTo(File.Create(tempFileName));
                Console.WriteLine($"File Created at {tempFileName}");
            }
        }
    }
}
