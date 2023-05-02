using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;

using System;

namespace SujaySarma.Sdk.Azure.Compute.Encryption
{
    /// <summary>
    /// Options to encrypt the contents/data in the disk, at rest, using user/platform-provided keys
    /// </summary>
    public class DiskDataEncryptionProperties
    {
        /// <summary>
        /// ResourceId of the Azure Disk Encryption Set to use for enabling encryption at rest
        /// </summary>
        [JsonProperty("diskEncryptionSetId")]
        public string EncryptionSetResourceId { get; set; } = string.Empty;

        /// <summary>
        /// The type of key used to encrypt the data of the disk
        /// </summary>
        [JsonProperty("type")]
        public DiskDataEncryptionTypeNamesEnum Type { get; set; }


        public DiskDataEncryptionProperties() { }

        /// <summary>
        /// Initialize properties for data disk encryption
        /// </summary>
        /// <param name="type">Type of encryption</param>
        /// <param name="encryptionSetId">ResourceUri to the Azure Disk Encryption Set to be used</param>
        public DiskDataEncryptionProperties(DiskDataEncryptionTypeNamesEnum type, ResourceUri encryptionSetId)
        {
            if (!Enum.IsDefined(typeof(DiskDataEncryptionTypeNamesEnum), type)) { throw new ArgumentOutOfRangeException(nameof(type)); }
            if ((encryptionSetId == null) || (!encryptionSetId.IsValid) ||
                (!encryptionSetId.Is(ResourceUriCompareLevel.Provider, "Microsoft.Compute")) || (!encryptionSetId.Is(ResourceUriCompareLevel.Type, "diskEncryptionSets")))
            {
                throw new ArgumentException(nameof(encryptionSetId));
            }

            Type = type;
            EncryptionSetResourceId = encryptionSetId.ToString();
        }
    }
}
