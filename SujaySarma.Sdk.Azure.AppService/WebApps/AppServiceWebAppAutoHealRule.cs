using Newtonsoft.Json;

using System.Collections.Generic;

namespace SujaySarma.Sdk.Azure.AppService.WebApps
{
    /// <summary>
    /// A rule for auto-healing an Azure App Service Web App
    /// </summary>
    public class AppServiceWebAppAutoHealRule
    {

        /// <summary>
        /// Actions to be executed when a rule is triggered.
        /// </summary>
        [JsonProperty("actions")]
        public List<AppServiceWebAppAutoHealAction> Actions { get; set; } = new List<AppServiceWebAppAutoHealAction>();

        /// <summary>
        /// Conditions that describe when to execute auto-heal actions
        /// </summary>
        [JsonProperty("triggers")]
        public List<AppServiceWebAppAutoHealTrigger> Triggers { get; set; } = new List<AppServiceWebAppAutoHealTrigger>();

        /// <summary>
        /// Default constructor
        /// </summary>
        public AppServiceWebAppAutoHealRule() { }

        /// <summary>
        /// Add a logging action
        /// </summary>
        /// <param name="execTime">Execution time (as a time-string) before event is written to log</param>
        public void AddLogging(string execTime = "00:00:30")
        {
            if (Actions == null)
            {
                Actions = new List<AppServiceWebAppAutoHealAction>();
            }

            Actions.Add(
                    new AppServiceWebAppAutoHealAction()
                    {
                        ActionType = AppServiceWebAppAutoHealActionType.LogEvent,
                        MinimumProcessExecutionTimeBeforeRun = execTime
                    }
                );
        }

        /// <summary>
        /// Add a app-recycle action
        /// </summary>
        /// <param name="execTime">Execution time (as a time-string) before app is recycled</param>
        public void AddRecycle(string execTime = "00:00:30")
        {
            if (Actions == null)
            {
                Actions = new List<AppServiceWebAppAutoHealAction>();
            }

            Actions.Add(
                    new AppServiceWebAppAutoHealAction()
                    {
                        ActionType = AppServiceWebAppAutoHealActionType.Recycle,
                        MinimumProcessExecutionTimeBeforeRun = execTime
                    }
                );
        }

        /// <summary>
        /// Add a custom action
        /// </summary>
        /// <param name="commandLine">Executable to run</param>
        /// <param name="parameters">Parameters to pass in</param>
        /// <param name="execTime">Execution time (as a time-string "hh:mm:ss"), before action is executed</param>
        public void AddCustom(string commandLine, string? parameters = null, string execTime = "00:00:30")
        {
            if (Actions == null)
            {
                Actions = new List<AppServiceWebAppAutoHealAction>();
            }

            Actions.Add(
                    new AppServiceWebAppAutoHealAction()
                    {
                        ActionType = AppServiceWebAppAutoHealActionType.CustomAction,
                        MinimumProcessExecutionTimeBeforeRun = execTime,
                        CustomAction = new AppServiceWebAppAutoHealCustomAction()
                        {
                            Executable = commandLine,
                            CommandLine = parameters
                        }
                    }
                );
        }

        /// <summary>
        /// Add a trigger based on private bytes consumed
        /// </summary>
        /// <param name="bytesThreshold">Threshold of number of bytes</param>
        public void AddTriggerBytesConsumed(int bytesThreshold)
        {
            if (Triggers == null)
            {
                Triggers = new List<AppServiceWebAppAutoHealTrigger>();
            }

            Triggers.Add(new AppServiceWebAppAutoHealTrigger() { OnPrivateBytesConsumed = bytesThreshold });
        }

        /// <summary>
        /// Add a trigger based on the total requests received in a given time period
        /// </summary>
        /// <param name="requestsThreshold">Threshold of the requests count</param>
        /// <param name="timeInterval">Time interval as a time-string "hh:mm:ss"</param>
        public void AddTriggerTotalRequests(int requestsThreshold, string timeInterval)
        {
            if (Triggers == null)
            {
                Triggers = new List<AppServiceWebAppAutoHealTrigger>();
            }

            Triggers.Add(new AppServiceWebAppAutoHealTrigger()
            {
                OnRequest = new AppServiceWebAppAutoHealRequestBasedTrigger()
                {
                    TimeInterval = timeInterval,
                    TotalRequestCount = requestsThreshold
                }
            });
        }

        /// <summary>
        /// Add a trigger based on the slow requests in a given time period
        /// </summary>
        /// <param name="responseDelay">Response delay as a time-string "hh:mm:ss"</param>
        /// <param name="requestsThreshold">Threshold of the requests count</param>
        /// <param name="timeInterval">Time interval as a time-string "hh:mm:ss"</param>
        public void AddTriggerSlowRequests(string responseDelay, int requestsThreshold, string timeInterval)
        {
            if (Triggers == null)
            {
                Triggers = new List<AppServiceWebAppAutoHealTrigger>();
            }

            Triggers.Add(new AppServiceWebAppAutoHealTrigger()
            {
                OnSlowRequest = new AppServiceWebAppAutoHealSlowRequestTrigger()
                {
                    TimeInterval = timeInterval,
                    TotalRequestCount = requestsThreshold,
                    ResponseDelay = responseDelay
                }
            });
        }

        /// <summary>
        /// Add trigger based on response code
        /// </summary>
        /// <param name="statusCode">HTTP response code</param>
        /// <param name="requestsThreshold">Threshold of the requests count</param>
        /// <param name="timeInterval">Time interval as a time-string "hh:mm:ss"</param>
        /// <param name="subStatusCode">HTTP response sub-code</param>
        /// <param name="win32Error">Win32 Error Code (if available)</param>
        public void AddTriggerResponseStatus(int statusCode, int requestsThreshold, string timeInterval, int? subStatusCode = null, int? win32Error = null)
        {
            if (Triggers == null)
            {
                Triggers = new List<AppServiceWebAppAutoHealTrigger>();
            }

            Triggers.Add(new AppServiceWebAppAutoHealTrigger()
            {
                OnResponseStatusCodes = new List<AppServiceWebAppAutoHealStatusCodeTrigger>()
                {
                    new AppServiceWebAppAutoHealStatusCodeTrigger()
                    {
                        Status = statusCode,
                        SubStatus = subStatusCode,
                        TimeInterval = timeInterval,
                        TotalRequestCount = requestsThreshold,
                        Win32ErrorCode = win32Error
                    }
                }
            });
        }
    }
}
