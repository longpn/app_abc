using dicho.Models;
using dicho.Models.InputData;
using dicho.Models.Security;
using dicho.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace dicho.DatabaseInteract
{
    public class LogDatabaseInteract
    {
        /// <summary>
        /// Logs the request information coming the service
        /// </summary>
        /// <param name="value"></param>
        public static void ApiLog(RequestedApiModel value)
        {
            int takenTime = (DateTime.UtcNow - value.RequestedDate).Milliseconds;
            Task.Factory.StartNew(() =>
            {
                using (DichoDataContext db = new DichoDataContext())
                {
                    var item = db.proc_ApiLog_Insert(value.UserID, value.ApiName, value.ApiVersion, value.IPAddress, value.Platform, value.UserAgent, value.ClientAppVersion, value.HttpStatusCode, (int)value.ContentLength, takenTime);
                }
            });
        }

        /// <summary>
        /// Logs the error occurred at service 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="apiName"></param>
        /// <param name="uri"></param>
        /// <param name="platform"></param>
        /// <param name="errorMessage"></param>
        /// <param name="statusCode"></param>
        /// <param name="clientAppVersion"></param>
        public static void ServiceErrorLog(long? userID, string apiName, string uri, string platform, string errorMessage, int statusCode, string clientAppVersion)
        {
            Task.Factory.StartNew(() =>
            {
                using (DichoDataContext db = new DichoDataContext())
                {
                    var item = db.proc_ServerError_Insert(userID, apiName, uri, platform, errorMessage, statusCode, clientAppVersion);
                }
            });
        }

        public static OutputDataModel ClientErrorLog(ClientErrorInputData value)
        {
            OutputDataModel outputData = new OutputDataModel();
            using (DichoDataContext db = new DichoDataContext())
            {
                var item = db.proc_ClientError_Insert(value.UserID, value.ErrorMessage, value.Platform).FirstOrDefault();
                if (!string.IsNullOrEmpty(item))
                {
                    switch (item)
                    {
                        case "Successful":
                            {
                                outputData.code = (int)Enums.StatusCode.Successful;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.Successful);

                                //Send out an email to admin
                                break;
                            }
                        case "Failed":
                            {
                                outputData.code = (int)Enums.StatusCode.FailedLogClientError;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedLogClientError);
                                break;
                            }
                        default:
                            goto case "Failed";
                    }

                }
                else
                {
                    outputData.code = (int)Enums.StatusCode.FailedLogClientError;
                    outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedLogClientError);
                }
            }

            return outputData;
        }
    }
}