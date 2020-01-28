namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// Type of App Service connection string
    /// </summary>
    public enum AppServiceWebAppConnectionStringType
    {
        /// <summary>
        /// Service default
        /// </summary>
        Default = Custom,

        /// <summary>
        /// Custom connection
        /// </summary>
        Custom = 0,

        /// <summary>
        /// API hub
        /// </summary>
        ApiHub,

        /// <summary>
        /// DocumentDB database
        /// </summary>
        DocDb,

        /// <summary>
        /// Azure Event Hub
        /// </summary>
        EventHub,

        /// <summary>
        /// MySQL or MariaDB server
        /// </summary>
        MySql,

        /// <summary>
        /// Azure Notification Hub
        /// </summary>
        NotificationHub,

        /// <summary>
        /// PostgreSQL database
        /// </summary>
        PostgreSQL,

        /// <summary>
        /// Redis cache
        /// </summary>
        RedisCache,

        /// <summary>
        /// SQL Azure database
        /// </summary>
        SQLAzure,

        /// <summary>
        /// SQL Server database
        /// </summary>
        SQLServer,

        /// <summary>
        /// Azure Service Bus
        /// </summary>
        ServiceBus
    }
}
