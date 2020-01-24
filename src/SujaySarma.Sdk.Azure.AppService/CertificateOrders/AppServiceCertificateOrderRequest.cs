using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.AppService.CertificateOrders
{
    /// <summary>
    /// Request structure for an App Service Certificate order
    /// </summary>
    public class AppServiceCertificateOrderRequest : AzureObjectBase
    {

        /// <summary>
        /// Properties of the request
        /// </summary>
        [JsonProperty("properties")]
        public AppServiceCertificateOrderRequestProperties Properties { get; set; } = new AppServiceCertificateOrderRequestProperties();

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceCertificateOrderRequest() { }
    }
}
