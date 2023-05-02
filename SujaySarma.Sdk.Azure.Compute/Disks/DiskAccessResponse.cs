using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.Disks
{
    /// <summary>
    /// Structure containing response for a disk access request
    /// </summary>
    public class DiskAccessResponse
    {
        /// <summary>
        /// The SAS Uri to access the disk using the corresponding request
        /// </summary>
        [JsonProperty("accessSAS")]
        public string SasUri { get; set; } = string.Empty;


        public DiskAccessResponse() { }
    }
}
