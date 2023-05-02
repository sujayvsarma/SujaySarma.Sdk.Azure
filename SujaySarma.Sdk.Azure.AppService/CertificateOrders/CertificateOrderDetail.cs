using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.AppService.CertificateOrders
{
    /// <summary>
    /// Details of an existing certificate order
    /// </summary>
    public class CertificateOrderDetail : AzureObjectBase
    {

        /// <summary>
        /// Properties
        /// </summary>
        [JsonProperty("properties")]
        public CertificateOrderDetailProperties Properties { get; set; } = new CertificateOrderDetailProperties();


        /// <summary>
        /// Default constructor
        /// </summary>
        public CertificateOrderDetail() { }
    }
}
