using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Common;
using SujaySarma.Sdk.Azure.Compute.Common;

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


        private void EnsureProperties()
        {
            if (Properties == null)
            {
                Properties = new DiskImageProperties();
            }
        }

        /// <summary>
        /// Set Hyper V generation
        /// </summary>
        /// <param name="generation">Hyper V generation</param>
        /// <returns>DiskImage</returns>
        public DiskImage WithHyperVGeneration(HyperVGenerationNamesEnum generation)
        {
            EnsureProperties();
            Properties!.WithGeneration(generation);

            return this;
        }

        /// <summary>
        /// Set source VM
        /// </summary>
        /// <param name="virtualMachine">Source VM resource Uri</param>
        /// <returns>DiskImage</returns>
        public DiskImage WithVirtualMachine(ResourceUri virtualMachine)
        {
            EnsureProperties();
            Properties!.WithVirtualMachine(virtualMachine);

            return this;
        }
    }
}
