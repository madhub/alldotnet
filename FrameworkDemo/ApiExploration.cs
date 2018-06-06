using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrameworkDemo
{
    public class ApiExploration
    {
        public  static IEnumerable<Stream> GetStreams(string path,string dirName)
        {
            foreach (string file in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
            {
                string directoryName = Path.GetFileName(Path.GetDirectoryName(file));
                if (directoryName == dirName)
                {
                    using (Stream stream = File.OpenRead(file))
                    {
                        yield return stream;
                    }
                }
            }
        }
        public void Execute()
        {
            var filesStreams = GetStreams(@"c:\temp", "somename");
        }
    }


    
    public static class StreamExtensions
    {
        private static int ReadStreamBufferSize = 1024 * 10;
        public static void CopyPartialStream(this Stream inputStream, Stream outputStream,
            long start, long end)
        {
            int count = 0;
            long remainingBytes = end - start + 1;
            long position = start;
            byte[] buffer = new byte[ReadStreamBufferSize];

            inputStream.Position = start;
            do
            {
                try
                {
                    if (remainingBytes > ReadStreamBufferSize)
                        count = inputStream.Read(buffer, 0, ReadStreamBufferSize);
                    else
                        count = inputStream.Read(buffer, 0, (int)remainingBytes);
                    outputStream.Write(buffer, 0, count);
                }
                catch (Exception error)
                {
                    break;
                }
                position = inputStream.Position;
                remainingBytes = end - position + 1;
            } while (position <= end);
        }
    }

    public class DataConsumer
    {
        public void Demo()
        {
            IDataProvider dataProvider = new FileBasedDataProvider();
            var streams = dataProvider.GetDataObjects("", StreamContentType.Json);
            foreach (var stream in streams)
            {
                // write the stream to Http output stream
            }
        }

    }

    public enum StreamContentType
    {
        Json,
        Xml,
        DicomPart10
    }
    public interface IDataProvider
    {
        IEnumerable<Task<Stream>> GetDataObjects(string queryConstraints, StreamContentType streamContentType);
    }
    public class FileBasedDataProvider : IDataProvider
    {
        public IEnumerable<Task<Stream>> GetDataObjects(string queryConstraints, StreamContentType streamContentType)
        {
            var files = Directory.EnumerateFiles(@"c:\temp");
            foreach (var file in files)
            {
                yield return Task.FromResult((Stream)File.OpenRead(file));
            }
        }
    }

    public class HttpBasedDataProvider : IDataProvider
    {
        public IEnumerable<Task<Stream>> GetDataObjects(string queryConstraints, StreamContentType streamContentType)
        {
            // assuming you already know the bytes offset & length by reading the series model 
            HttpClient httpClient = new HttpClient();
            var message = httpClient.GetAsync("someurl");
            var httpByteStream = message.Result.Content.ReadAsStreamAsync().Result;
            int objectLength = 10000;
            while (true)
            {
                MemoryStream memoryStream = new MemoryStream(objectLength);
                httpByteStream.CopyPartialStream(memoryStream, httpByteStream.Position, objectLength);
                yield return Task.FromResult((Stream)memoryStream);
            }

        }
    }
}
