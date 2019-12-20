using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.Compute.DiskImages
{
    /// <summary>
    /// Describes how an image is internally stored
    /// </summary>
    public class ImageStorageProfile
    {

        /// <summary>
        /// Specifies whether an image is zone resilient or not. Default is false. 
        /// Zone resilient images can be created only in regions that provide Zone Redundant Storage (ZRS).
        /// </summary>
        [JsonProperty("zoneResilient")]
        public bool IsZoneResilient { get; set; } = false;

        /// <summary>
        /// Data disks (disks other than the primary disk with the OS) that are in the image
        /// </summary>
        [JsonProperty("dataDisks")]
        public List<ImageDataDisk>? DataDisks { get; set; } = null;

        /// <summary>
        /// The primary OS disk that is in the image
        /// </summary>
        [JsonProperty("osDisk")]
        public ImageOSDisk PrimaryDisk { get; set; } = new ImageOSDisk();


        public ImageStorageProfile() { }

    }
}
