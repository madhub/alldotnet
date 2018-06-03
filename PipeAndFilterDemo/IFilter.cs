namespace PipeAndFilterDemo
{
    /// <summary>
    /// A filter to be registered in the message processing pipeline
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilter<T>
    {
        /// <summary>
        /// Filter implementing this method would perform processing on the input type T
        /// </summary>
        /// <param name="input">The input to be executed by the filter</param>
        /// <returns></returns>
        T Execute(T input);
    }
}
