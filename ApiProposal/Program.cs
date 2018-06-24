using System;
using System.Collections.Generic;

namespace ApiProposal
{
    //https://github.com/spring-cloud/spring-cloud-commons/tree/d610fb86b1d5aca7cc2db60f4900ae1c8ebfa1b7/spring-cloud-commons/src/main/java/org/springframework/cloud/client/discovery

    public interface ServiceInstance
    {
        /// <summary>
        /// the service id as registered.
        /// </summary>
        String GetServiceId { get;  set; }
        /// <summary>
        /// the hostname of the registered ServiceInstance
        /// </summary>
        String GetHost { get; set; }
        /// <summary>
        /// the port of the registered ServiceInstance
        /// </summary>
        uint Port { get; set; }
        bool IsSecure { get; set; }
        /// <summary>
        /// the service uri address
        /// </summary>
        Uri GetUri { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>the key value pair metadata associated with the service instance</returns>
        Dictionary<String, String> Metadata { get; set; }

    }
    
    public interface DiscoveryClient
    {

        /**
         * A human readable description of the implementation, used in HealthIndicator
         * @return the description
         */
        String description();

        /**
         * Get all ServiceInstances associated with a particular serviceId
         * @param serviceId the serviceId to query
         * @return a List of ServiceInstance
         */
        List<ServiceInstance> getInstances(String serviceId );

        /**
         * @return all known service ids
         */
        List<String> getServices();

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
