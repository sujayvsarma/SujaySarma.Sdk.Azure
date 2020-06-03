using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.ResourceGroups
{
    /// <summary>
    /// Options for <see cref="ResourceGroupClient.ExportTemplate(string, Guid, string)"/> function
    /// </summary>
    public class ExportTemplateOptions
    {
        /// <summary>
        /// If TRUE, will include default values for any parameters defined in the template. 
        /// Will have no effect if <see cref="DoNotParameterize"/> is set to TRUE.
        /// </summary>
        public bool IncludeDefaultValuesForParameters { get; set; } = true;

        /// <summary>
        /// Will include comments/descriptions for each item
        /// </summary>
        public bool IncludeComments { get; set; } = true;

        /// <summary>
        /// If TRUE will not create parameters for resource names, instead will hard-code their 
        /// current names.
        /// </summary>
        public bool RetainExistingResourceNames { get; set; } = false;

        /// <summary>
        /// If TRUE, will not create any parameters at all and all values will be hard-coded 
        /// to their current values.
        /// </summary>
        public bool DoNotParameterize { get; set; } = false;

        /// <summary>
        /// If TRUE, includes all the resources in the group. Else, set to FALSE and specify the 
        /// list of resources
        /// </summary>
        public bool IncludeAllResources { get; set; } = true;

        /// <summary>
        /// List of names of resources to export. Must be set if <see cref="IncludeAllResources"/> is false.
        /// </summary>
        public List<string>? ResourceNames { get; set; } = null;


        public ExportTemplateOptions() { }
    }
}
