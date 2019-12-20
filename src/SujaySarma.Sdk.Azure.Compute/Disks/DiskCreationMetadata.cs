
using Newtonsoft.Json;
using SujaySarma.Sdk.Azure.Compute.Common;

namespace SujaySarma.Sdk.Azure.Compute.Disks
{
    /// <summary>
    /// Metadata about the creation of the disk
    /// </summary>
    public class DiskCreationMetadata
    {
        /// <summary>
        /// Source of creation of the disk
        /// </summary>
        [JsonProperty("createOption")]
        public DiskCreationOptionsEnum CreationMode { get; set; } = DiskCreationOptionsEnum.Empty;

        /// <summary>
        /// If createOption is Copy, this is the ARM id of the source snapshot or disk.
        /// </summary>
        [JsonProperty("sourceResourceId")]
        public string? SourceSnapshotOrDiskResourceId { get; set; } = null;

        /// <summary>
        /// If this field is set, this is the unique id identifying the source of this resource.
        /// </summary>
        [JsonProperty("sourceUniqueId")]
        public string? SourceUniqueId { get; set; } = null;

        /// <summary>
        /// If createOption is Import, this is the URI of a blob to be imported into a managed disk.
        /// </summary>
        [JsonProperty("sourceUri")]
        public string? SourceBlobUri { get; set; } = null;

        /// <summary>
        /// Required if createOption is Import. The Azure Resource Manager identifier of the 
        /// storage account containing the blob to import as a disk.
        /// </summary>
        [JsonProperty("storageAccountId")]
        public string? SourceBlobStorageAccountId { get; set; } = null;

        /// <summary>
        /// If createOption is Upload, this is the size of the contents of the upload including the VHD footer. 
        /// This value should be between 20972032 (20 MiB + 512 bytes for the VHD footer) and 35183298347520 bytes 
        /// (32 TiB + 512 bytes for the VHD footer).
        /// </summary>
        [JsonProperty("uploadSizeBytes")]
        public int? VhdUploadSizeInBytes { get; set; } = null;

        /// <summary>
        /// Disk source information.
        /// </summary>
        [JsonProperty("imageReference")]
        public DiskImageReference? SourceDiskImageReference { get; set; } = null;


        public DiskCreationMetadata() { }
    }
}
