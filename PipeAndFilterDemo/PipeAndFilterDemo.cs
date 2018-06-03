using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeAndFilterDemo
{
    /// <summary>
    /// The output of this filter is the list of Agent for which CurrentWorkload is less than MaxWorklod
    /// </summary>
    public class FileNameFilter : IFilter<IEnumerable<string>>
    {
        public IEnumerable<string> Execute(IEnumerable<string> input)
        {
            if (input == null || input.Count() < 1)
            {
                return input;
            }

            return input.Where(filename => filename.Contains("S"));
        }
    }

    /// <summary>
    /// The output of this filter is the list of Agent for which CurrentWorkload is less than MaxWorklod
    /// </summary>
    public class FileNameLength : IFilter<IEnumerable<string>>
    {
        public IEnumerable<string> Execute(IEnumerable<string> input)
        {
            if (input == null || input.Count() < 1)
            {
                return input;
            }

            return input.Where(_ => _.Length < 50);
        }
    }
    public class PipeAndFilterDemo
    {
        public void Execute()
        {
            var files = Directory.EnumerateFiles(@"C:\temp");
            var pipeSelectionPipeLine = new FileSelectionPipeline().
                                            Register(new FileNameFilter()).
                                            Register( new FileNameLength());

            var result = pipeSelectionPipeLine.Process(files);
            

        }
    }
}
