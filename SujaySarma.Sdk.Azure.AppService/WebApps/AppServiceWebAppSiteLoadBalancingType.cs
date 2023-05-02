namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Load balancing configuration for an Azure App Service
    /// </summary>
    public enum AppServiceWebAppSiteLoadBalancingType
    {
        /// <summary>
        /// Route requests to nodes with the least requests at the time
        /// </summary>
        LeastRequests,

        /// <summary>
        /// Route requests to the fastest nodes at the time
        /// </summary>
        LeastResponseTime,

        /// <summary>
        /// Route requests to the same nodes using the request's hash value
        /// </summary>
        RequestHash,

        /// <summary>
        /// Perform weighted round-robin selection of nodes
        /// </summary>
        WeightedRoundRobin,

        /// <summary>
        /// Perform weighted analysis of total traffic being handled by 
        /// a node to pick where to route the traffic
        /// </summary>
        WeightedTotalTraffic
    }
}
