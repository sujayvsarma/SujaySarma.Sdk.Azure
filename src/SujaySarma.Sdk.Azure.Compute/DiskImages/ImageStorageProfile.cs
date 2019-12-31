using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Initialize a storage profile
        /// </summary>
        /// <param name="primaryDisk">Properties of the primary or OS disk</param>
        /// <param name="isZoneResilient">Flag indicating if the storage or disk image is zone-resilient</param>
        /// <param name="dataDisks">Collection of data disks in the image</param>
        public ImageStorageProfile(ImageOSDisk primaryDisk, bool isZoneResilient = false, IEnumerable<ImageDataDisk>? dataDisks = null)
        {
            IsZoneResilient = isZoneResilient;
            PrimaryDisk = primaryDisk ?? throw new ArgumentNullException(nameof(primaryDisk));
            DataDisks = (((dataDisks == null) || (dataDisks.Count() == 0)) ? null : new List<ImageDataDisk>());

            if (((dataDisks != null) && (dataDisks.Count() > 0)) && (DataDisks != null))
            {
                DataDisks.AddRange(dataDisks);
            }
        }

        /// <summary>
        /// Set primary disk (for an unmanaged disk)
        /// </summary>
        /// <param name="osName">Type of OS on the disk</param>
        /// <param name="isGeneralized">If true, image is generalied, suitable to create a new VM</param>
        /// <param name="storageType">Type of storage account stored on (cannot be UltraSSD_LRS)</param>
        /// <param name="caching">Type of caching used on the disk</param>
        /// <param name="sizeInGB">Size in GB (must be from 1 to 1023)</param>
        /// <param name="vhdBlobUri">Uri to VHD file stored as a blob in an Azure Storage account</param>
        public void SetPrimaryDisk(OSTypeNamesEnum osName, bool isGeneralized, DiskSkuNamesEnum storageType, CachingTypeNamesEnum caching, int sizeInGB, string vhdBlobUri)
            => PrimaryDisk = new ImageOSDisk(osName, isGeneralized, storageType, caching, sizeInGB, vhdBlobUri);

        /// <summary>
        /// Set primary disk (for an managed disk)
        /// </summary>
        /// <param name="osName">Type of OS on the disk</param>
        /// <param name="isGeneralized">If true, image is generalied, suitable to create a new VM</param>
        /// <param name="storageType">Type of storage account stored on (cannot be UltraSSD_LRS)</param>
        /// <param name="caching">Type of caching used on the disk</param>
        /// <param name="sizeInGB">Size in GB (must be from 1 to 1023)</param>
        /// <param name="diskUri">Uri to the managed disk</param>
        public void SetPrimaryDisk(OSTypeNamesEnum osName, bool isGeneralized, DiskSkuNamesEnum storageType, CachingTypeNamesEnum caching, int sizeInGB, ResourceUri diskUri)
            => PrimaryDisk = new ImageOSDisk(osName, isGeneralized, storageType, caching, sizeInGB, diskUri);


        /// <summary>
        /// Clear all data disks from the set
        /// </summary>
        public void ClearDataDisks() => DataDisks = null;

        /// <summary>
        /// Attach an unmanaged data disk
        /// </summary>
        /// <param name="attachedLUN">The logical unit number the disk is attached on to the VM</param>
        /// <param name="storageType">Type of storage account stored on (cannot be UltraSSD_LRS)</param>
        /// <param name="caching">Type of caching used on the disk</param>
        /// <param name="sizeInGB">Size in GB (must be from 1 to 1023)</param>
        /// <param name="vhdBlobUri">Uri to VHD file stored as a blob in an Azure Storage account</param>
        public void AttachDataDisk(int attachedLUN, DiskSkuNamesEnum storageType, CachingTypeNamesEnum caching, int sizeInGB, string vhdBlobUri)
        {
            if (DataDisks == null)
            {
                DataDisks = new List<ImageDataDisk>();
            }

            DataDisks.Add(new ImageDataDisk(attachedLUN, storageType, caching, sizeInGB, vhdBlobUri));
        }

        /// <summary>
        /// Attach a managed data disk
        /// </summary>
        /// <param name="attachedLUN">The logical unit number the disk is attached on to the VM</param>
        /// <param name="storageType">Type of storage account stored on (cannot be UltraSSD_LRS)</param>
        /// <param name="caching">Type of caching used on the disk</param>
        /// <param name="sizeInGB">Size in GB (must be from 1 to 1023)</param>
        /// <param name="diskUri">Uri to the managed disk</param>
        public void AttachDataDisk(int attachedLUN, DiskSkuNamesEnum storageType, CachingTypeNamesEnum caching, int sizeInGB, ResourceUri diskUri)
        {
            if (DataDisks == null)
            {
                DataDisks = new List<ImageDataDisk>();
            }

            DataDisks.Add(new ImageDataDisk(attachedLUN, storageType, caching, sizeInGB, diskUri));
        }

    }
}
