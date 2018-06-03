using System.Collections.Generic;

namespace PipeAndFilterDemo

{
    /// <summary>
    /// Pipeline to select final list of applicable files
    /// </summary>
    public class FileSelectionPipeline : Pipeline<IEnumerable<string>>
    {
        /// <summary>
        /// Method which executes the filter on a given Input
        /// </summary>
        /// <param name="input">Input on which filtering
        /// needs to happen as implementing in individual filters</param>
        /// <returns></returns>
        public override IEnumerable<string> Process(IEnumerable<string> input)
        {
            foreach (var filter in filters)
            {
                input = filter.Execute(input);
            }

            return input;
        }
    }
}
