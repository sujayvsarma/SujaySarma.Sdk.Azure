using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

namespace SujaySarma.Sdk.Azure.Compute.DiskImages
{
    /// <summary>
    /// The source user image of a virtual hard disk
    /// </summary>
    public class DiskImage : AzureObjectBase
    {

        /// <summary>
        /// Properties of the disk image
        /// </summary>
        [JsonProperty("properties")]
        public DiskImageProperties? Properties { get; set; } = null;

        public DiskImage() : base() { }
    }
}
