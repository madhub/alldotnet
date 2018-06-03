using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetRequestPipeLineDemo
{
    public class Context
    {
    }

    public delegate Task RequestDelegate(Context context);
    /// <summary>
    /// 
    /// </summary>
    public interface IPipelineBuilder
    {
        IPipelineBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);
    }
    /// <summary>
    /// 
    /// </summary>
    public class PipelineBuilder : IPipelineBuilder
    {
        private readonly IList<Func<RequestDelegate, RequestDelegate>> _components = 
            new List<Func<RequestDelegate, RequestDelegate>>();

        public IPipelineBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            this._components.Add(middleware);
            return this;
        }
    }
    public class AspnetRequestPipeLineDemo
    {
    }
}
