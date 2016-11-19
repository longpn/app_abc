using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using dicho.Models.Security;
using dicho.Models;
using dicho.Utilities;
using dicho.DatabaseInteract;

namespace dicho.Authentication
{
    public class ApiAuthentication : DelegatingHandler
    {

        async protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            RequestedApiModel apiLog = GetApiLogInfor(request);

            ApiResponseModel httpResponse = new ApiResponseModel();

            if (apiLog.IsSupportedVersion)
            {

                #region Authenticate API

                if (!apiLog.IsAllowPassAuthentication)
                {
                    if (!IsValidateHeaderFormat(request))
                    {
                        apiLog.HttpStatusCode = (int)HttpStatusCode.NotAcceptable;
                        httpResponse.AddMeta(HttpStatusCode.NotAcceptable, "NotAcceptable", "Your request is invalid");
                        // Logs API
                        LogDatabaseInteract.ApiLog(apiLog);
                        return request.CreateResponse(HttpStatusCode.OK, httpResponse);
                    }
                    else
                    {
                        string clientAppID = request.Headers.Authorization.Parameter.Split(':')[0];
                        apiLog.UserID = TokenManager.GetUserIDFromServerCache(clientAppID);
                        Enums.TokenValidation validate = Enums.TokenValidation.Invalid;
                        if (apiLog.UserID == 3)
                        {
                            validate = Enums.TokenValidation.Valid;
                        }
                        else
                        {
                            validate = TokenManager.AuthorizeRequest(request);
                        }

                        switch (validate)
                        {
                            case Enums.TokenValidation.Invalid:
                                {
                                    apiLog.HttpStatusCode = (int)HttpStatusCode.Unauthorized;
                                    httpResponse.AddMeta(HttpStatusCode.Unauthorized, "Access Denied", "Your token is invalid or expired");

                                    // Logs API
                                    LogDatabaseInteract.ApiLog(apiLog);
                                    return request.CreateResponse(HttpStatusCode.OK, httpResponse);
                                }
                            case Enums.TokenValidation.Valid:
                                {
                                    break;
                                }
                            default:
                                goto case Enums.TokenValidation.Invalid;
                        }
                    }

                }

                #endregion


                #region Invokes API and Response to client

                HttpResponseMessage response = await base.SendAsync(request, cancellationToken);


                string errorDetail = string.Empty;
                bool isValid = true;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (response.Content != null)
                    {
                        httpResponse.Body = await response.Content.ReadAsAsync<object>();
                        if (httpResponse.Body.GetType() == typeof(OutputDataModel))
                        {
                            OutputDataModel output = (OutputDataModel)httpResponse.Body;
                            if (output.StatusCode == 400)
                            {
                                httpResponse.AddMeta(HttpStatusCode.BadRequest, "BadRequest", "Paramter(s) is not valid");
                                httpResponse.Body = "";
                                isValid = false;
                            }
                        }
                    }
                    else
                    {
                        httpResponse.AddMeta(HttpStatusCode.OK, response.ReasonPhrase, errorDetail);
                    }
                }
                else
                {
                    if (response.Content != null)
                    {
                        var error = await response.Content.ReadAsAsync<System.Web.Http.HttpError>();
                        errorDetail = !string.IsNullOrEmpty(error.ExceptionMessage) ? error.ExceptionMessage : !string.IsNullOrEmpty(error.MessageDetail) ? error.MessageDetail : error.Message;
                        try
                        {
                            int statusCode = (int)response.StatusCode;
                            string exceptionError = !string.IsNullOrEmpty(error.ExceptionMessage) ? error.ExceptionMessage : errorDetail;
                            LogDatabaseInteract.ServiceErrorLog(apiLog.UserID, apiLog.ApiName, request.RequestUri.AbsoluteUri, apiLog.Platform, exceptionError, statusCode, apiLog.ClientAppVersion);
                        }
                        catch { }
                    }
                }

                if (isValid)
                {
                    httpResponse.AddMeta(response.StatusCode, response.ReasonPhrase, errorDetail);
                }



                apiLog.HttpStatusCode = (int)HttpStatusCode.OK;
                // Logs API
                LogDatabaseInteract.ApiLog(apiLog);
                return request.CreateResponse(HttpStatusCode.OK, httpResponse);

                #endregion
            }
            else
            {
                //Unsupported the old version
                apiLog.HttpStatusCode = (int)HttpStatusCode.UpgradeRequired;
                httpResponse.AddMeta(HttpStatusCode.UpgradeRequired, "UpgradeApplication", "Upgrade Application is required.");

                // Logs API
                LogDatabaseInteract.ApiLog(apiLog);
                return request.CreateResponse(HttpStatusCode.OK, httpResponse);
            }

        }



        /// <summary>
        /// Validates header format
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool IsValidateHeaderFormat(HttpRequestMessage request)
        {
            bool result = false;
            if (request.Headers.Authorization != null)
            {
                if (request.Headers.Authorization.Scheme.ToLower() == "dc")
                {
                    if (request.Headers.Authorization.Parameter.Contains(":"))
                    {
                        if (request.Headers.Where(cc => cc.Key == "DateTime").Count() > 0)
                        {
                            result = true;
                        }
                    }
                }
            }

            return result;
        }



        /// <summary>
        /// Gets details information of ClientApp's request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private RequestedApiModel GetApiLogInfor(HttpRequestMessage request)
        {
            RequestedApiModel apiLog = new RequestedApiModel();
            // apiLog.RequestedDate = DateTime.UtcNow;

            string userAgent = request.Headers.UserAgent.ToString();

            apiLog.UserAgent = userAgent;
            apiLog.IPAddress = ClientInforHelper.GetClientIPAddress(request);
            apiLog.ClientAppVersion = ClientInforHelper.GetVersion(userAgent);
            apiLog.Platform = ClientInforHelper.GetPlatform(userAgent);

            apiLog.ContentLength = request.Content.Headers.ContentLength.HasValue ? request.Content.Headers.ContentLength.Value : 0;

            string[] segment = request.RequestUri.Segments;
            if (segment != null && segment.Count() > 0)
            {
                apiLog.ApiName = segment.Last();
            }
            else
            {
                apiLog.ApiName = "Unknown";
            }


            apiLog.ApiVersion = GetApiVersion(request.RequestUri.LocalPath);

            return apiLog;
        }



        /// <summary>
        /// Reads the version of API
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        private string GetApiVersion(string localPath)
        {
            string apiVersion = string.Empty;
            if (!string.IsNullOrEmpty(localPath) && localPath.Contains("/"))
            {
                string[] segment = localPath.Split('/');

                if (segment.Count() > 2)
                {
                    switch (segment[2])
                    {
                        case "1.0":
                        case "2.0":
                        case "2.1":
                            {
                                apiVersion = segment[2];
                                break;
                            }
                        default:
                            break;
                    }

                }
            }

            return apiVersion;
        }


    }
}