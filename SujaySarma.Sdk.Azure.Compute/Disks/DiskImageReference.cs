
using Newtonsoft.Json;

namespace SujaySarma.Sdk.Azure.Compute.Disks
{
    /// <summary>
    /// Reference to a disk image
    /// </summary>
    public class DiskImageReference
    {
        /// <summary>
        /// A relative uri containing either a Platform Image Repository or user image reference.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// If the disk is created from an image's data disk, this is an index that indicates which of 
        /// the data disks in the image to use. For OS disks, this field is null.
        /// </summary>
        [JsonProperty("lun")]
        public string? LUN { get; set; } = null;


        public DiskImageReference() { }
    }
}
