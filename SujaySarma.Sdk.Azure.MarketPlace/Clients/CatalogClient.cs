using Newtonsoft.Json;

using SujaySarma.Sdk.Azure.Internal;
using SujaySarma.Sdk.Azure.MarketPlace.Catalogs;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SujaySarma.Sdk.Azure.MarketPlace.Clients
{
    /// <summary>
    /// Interacts with the undocumented Azure "Marketplace Catalogs" API. This API is what provides the data used by the 
    /// primary portal.azure.com UI to show its Create Resource workflow.
    /// </summary>
    public static class CatalogClient
    {


        /// <summary>
        /// Gets the Catalog Index. This is the top-level catalog that contains all other catalogs in the marketplace system
        /// (Examples: "Compute_MP", "Database_MP", etc)
        /// </summary>
        /// <param name="language">2-letter ISO language code (eg: "en")</param>
        /// <param name="locale">4 letter ISO locale code (eg: "en-us")</param>
        /// <returns>Index entries or NULL</returns>
        public static async Task<IndexCatalogMenu?> GetIndex(string language = "en", string locale = "en-us")
        {
            string? json = await GetJson(EndpointTypeEnum.Catalog, language, locale);
            if (json == null)
            {
                return null;
            }

            IndexCatalogMenu menu = JsonConvert.DeserializeObject<IndexCatalogMenu>(json);
            foreach (IndexCatalogMenuItem item in menu.StaticMenu)
            {
                item.Language = language;
                item.Locale = locale;
            }

            foreach (IndexCatalogMenuItem item in menu.DynamicMenu)
            {
                item.Language = language;
                item.Locale = locale;
            }

            return menu;
        }

        /// <summary>
        /// Get the category entries within the index catalog. This is a category or subsection hierarchically under the <seealso cref="IndexCatalog"/>. 
        /// (Examples: "Windows", "Applications", etc)
        /// </summary>
        /// <param name="catalogIndexId">Id of the Catalog Index (that is, <seealso cref="IndexCatalog.CatalogID"/>)</param>
        /// <param name="language">2-letter ISO language code (eg: "en")</param>
        /// <param name="locale">4 letter ISO locale code (eg: "en-us")</param>
        /// <param name="limitedRows">If true, fetches only some data (only applicable for Detail calls)</param>
        /// <returns>List of items in that catalog index</returns>
        public static async Task<List<IndexCatalogCategory>> GetCategories(string catalogIndexId, string language = "en", string locale = "en-us", bool limitedRows = false)
        {
            string? json = await GetJson(EndpointTypeEnum.Detail, language, locale, catalogIndexId, limitedRows);
            if (json == null)
            {
                return new List<IndexCatalogCategory>();
            }

            return JsonConvert.DeserializeObject<List<IndexCatalogCategory>>(json);
        }

        /// <summary>
        /// Get the items under a specific category in the catalog index. These are items in a catalog's category 
        /// (Examples: the types of VMs that could be created in the "Compute" catalog).
        /// </summary>
        /// <param name="catalogIndexId">Id of the Catalog Index (that is, <seealso cref="IndexCatalog.CatalogID"/>)</param>
        /// <param name="catalogSectionName">Name of the section within that catalog (that is, <seealso cref="IndexCatalogCategory.GroupId"/>)</param>
        /// <param name="language">2-letter ISO language code (eg: "en")</param>
        /// <param name="locale">4 letter ISO locale code (eg: "en-us")</param>
        /// <returns>List of items in that catalog index under the provided section</returns>
        public static async Task<List<IndexItem>> GetCategoryItems(string catalogIndexId, string catalogSectionName, string language = "en", string locale = "en-us")
        {
            IList<IndexCatalogCategory> categories = await GetCategories(catalogIndexId, language, locale);
            if (categories.Count == 0)
            {
                return new List<IndexItem>();
            }

            IndexCatalogCategory section = categories.FirstOrDefault(d => d.GroupId.Equals(catalogSectionName));
            if (section == default)
            {
                return new List<IndexItem>();
            }

            return section.Items;
        }

        /// <summary>
        /// Get a single item from a specific category in the catalog index. This is a specific IndexItem returned from the GetCategoryItems() function.
        /// </summary>
        /// <param name="catalogIndexId">Id of the Catalog Index (that is, <seealso cref="IndexCatalog.CatalogID"/>)</param>
        /// <param name="catalogSectionName">Name of the section within that catalog (that is, <seealso cref="IndexCatalogCategory.GroupId"/>)</param>
        /// <param name="idOrNameOrOfferId">The Id or Name or Offer Id of the item</param>
        /// <param name="language">2-letter ISO language code (eg: "en")</param>
        /// <param name="locale">4 letter ISO locale code (eg: "en-us")</param>
        /// <returns>Single item in that catalog index under the provided section or NULL</returns>
        public static async Task<IndexItem?> GetCategoryItem(string catalogIndexId, string catalogSectionName, string idOrNameOrOfferId, string language = "en", string locale = "en-us")
        {
            IList<IndexCatalogCategory> categories = await GetCategories(catalogIndexId, language, locale, false);
            if (categories.Count == 0)
            {
                return null;
            }

            IndexCatalogCategory section = categories.FirstOrDefault(d => d.GroupId.Equals(catalogSectionName));
            if (section == default)
            {
                return null;
            }

            foreach (IndexItem item in section.Items)
            {
                if (item.Id.Equals(idOrNameOrOfferId, StringComparison.InvariantCultureIgnoreCase)
                            || item.OfferId.Equals(idOrNameOrOfferId, StringComparison.InvariantCultureIgnoreCase)
                                || ((item.LegacyOfferId != null) && item.LegacyOfferId.Equals(idOrNameOrOfferId, StringComparison.InvariantCultureIgnoreCase))
                                    || ((item.BigId != null) && item.BigId.Equals(idOrNameOrOfferId, StringComparison.InvariantCultureIgnoreCase))
                                        || item.DisplayName.Equals(idOrNameOrOfferId, StringComparison.InvariantCultureIgnoreCase)
                    )
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Get a single item from a specific category in the catalog index. This is a specific IndexItem returned from the GetCategoryItems() function.
        /// </summary>
        /// <param name="catalogIndexId">Id of the Catalog Index (that is, <seealso cref="IndexCatalog.CatalogID"/>)</param>
        /// <param name="catalogSectionName">Name of the section within that catalog (that is, <seealso cref="IndexCatalogCategory.GroupId"/>)</param>
        /// <param name="idOrNameOrOfferId">The Id or Name or Offer Id of the item</param>
        /// <param name="planIdOrSkuId">The Id or name or SKU Id of the plan</param>
        /// <param name="language">2-letter ISO language code (eg: "en")</param>
        /// <param name="locale">4 letter ISO locale code (eg: "en-us")</param>
        /// <returns>Single item in that catalog index under the provided section or NULL</returns>
        public static async Task<MarketplaceItemPlan?> GetCategoryItemPlan(string catalogIndexId, string catalogSectionName, string idOrNameOrOfferId, string planIdOrSkuId, string language = "en", string locale = "en-us")
        {
            IndexItem? item = await GetCategoryItem(catalogIndexId, catalogSectionName, idOrNameOrOfferId, language, locale);
            if ((item == null) || (item.Plans == null))
            {
                return null;
            }

            foreach (MarketplaceItemPlan plan in item.Plans)
            {
                if (plan.Id.Equals(planIdOrSkuId, StringComparison.InvariantCultureIgnoreCase)
                    || plan.SkuId.Equals(planIdOrSkuId, StringComparison.InvariantCultureIgnoreCase)
                        || plan.PlanId.Equals(planIdOrSkuId, StringComparison.InvariantCultureIgnoreCase)
                            || plan.LegacyPlanId.Equals(planIdOrSkuId, StringComparison.InvariantCultureIgnoreCase))
                {
                    return plan;
                }
            }

            return null;
        }


        /// <summary>
        /// Get Json from the portal catalog API
        /// </summary>
        /// <param name="endpointType">Type of endpoint we want to call</param>
        /// <param name="language">2-letter ISO language code</param>
        /// <param name="locale">4 letter ISO locale code</param>
        /// <param name="categoryId">Category Id (eg: "compute_mp")</param>
        /// <param name="limitedRows">If true, fetches only some data (only applicable for Detail calls)</param>
        /// <returns>Json string. NULL if nothing there was a problem</returns>
        private static async Task<string?> GetJson(EndpointTypeEnum endpointType, string language = "en", string locale = "en-us", string? categoryId = null, bool limitedRows = true)
        {
            if (string.IsNullOrWhiteSpace(language))
            {
                language = "en";
            }

            if (string.IsNullOrWhiteSpace(locale))
            {
                locale = "en-us";
            }

            string localFilePath = Path.Combine(AppContext.BaseDirectory, "_azureSdk", "catalogApi", $"{locale}.{language}");
            string localFileName = Path.Combine(localFilePath, ((endpointType == EndpointTypeEnum.Catalog) ? "catalog.json" : $"{(limitedRows ? "Limited" : "Complete")}_{categoryId!.ToLower()}.json"));
            bool needsReload = false;

            lock (_fsLock)
            {
                if (!Directory.Exists(localFilePath))
                {
                    Directory.CreateDirectory(localFilePath);
                    needsReload = true;
                }

                if ((!System.IO.File.Exists(localFileName)) || ((new FileInfo(localFileName)).LastWriteTimeUtc < DateTime.UtcNow.Date.AddDays(-15)))
                {
                    needsReload = true;
                }
            }

            string? json = null;
            if (needsReload)
            {
                string uri = string.Empty;
                Dictionary<string, string>? parameters = null;
                switch (endpointType)
                {
                    case EndpointTypeEnum.Catalog:
                        uri = "https://catalogapi.azure.com/Catalog/CreateMenus";
                        parameters = new Dictionary<string, string>()
                    {
                        { "x-ms-effective-locale", $"{language}.{locale}" }
                    };
                        break;

                    case EndpointTypeEnum.Detail:
                        uri = "https://catalogapi.azure.com/catalog/portal";
                        parameters = new Dictionary<string, string>()
                    {
                        { "curationArea", "gallery" },
                        { "limitRows", (limitedRows ? "true" : "false") },
                        { "combineReferences", "true" },
                        { "curationId", "20190624.2" },
                        { "presistOrder", "true" },
                        { "x-ms-effective-locale", $"{language}.{locale}" },
                        { "menuId", categoryId! }
                    };
                        break;
                }

                RestApiResponse response = await RestApiClient.GETWithoutAuthentication(
                        uri,
                        CLIENT_API_VERSION,
                        parameters, null,
                        new int[] { 200 },
                        (limitedRows ? 15 : 60)     // all rows can take a LONG time
                    );

                if (response.IsExpectedSuccess && (!response.WasException) && (!string.IsNullOrWhiteSpace(response.Body)))
                {
                    json = response.Body;
                    File.WriteAllText(localFileName, json);
                }
            }
            else
            {
                json = File.ReadAllText(localFileName);
            }

            return json;
        }

        private static readonly object _fsLock = new object();
        private static readonly string CLIENT_API_VERSION = "2018-08-01-beta";

        /// <summary>
        /// Type of endpoint being used
        /// </summary>
        private enum EndpointTypeEnum
        {
            Catalog,
            Detail
        }
    }
}
