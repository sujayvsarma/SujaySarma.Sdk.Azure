using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Providers
{
    /// <summary>
    /// Results of a resource provider listing operation
    /// </summary>
    public class ResourceProviderListResult : ListResultWithContinuations<ResourceProvider>
    {
        public ResourceProviderListResult() : base() { }
    }
}
