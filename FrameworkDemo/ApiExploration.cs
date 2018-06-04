using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
}
