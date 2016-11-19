﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace dicho.Utilities
{
    public class ClientInforHelper
    {
        /// <summary>
        /// Gets Ip from user's request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIPAddress(HttpRequestMessage request)
        {
            string ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(ipaddress))
            {
                if (request.Properties.ContainsKey("MS_HttpContext"))
                {
                    ipaddress = ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                }
            }

            return ipaddress;
        }

        /// <summary>
        /// Gets Ip from user's request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIPAddress(HttpRequestBase request)
        {

            string ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            //if (string.IsNullOrEmpty(ipaddress))
            //{
            //    if (request.pro .Properties.ContainsKey("MS_HttpContext"))
            //    {
            //        ipaddress = ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            //    }
            //}
            // ipaddress = "115.79.47.46";

            return ipaddress;
        }


        /// <summary>
        /// Splits device model from user agent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static string GetDeviceMode(string userAgent)
        {
            string deviceModel = string.Empty;
            if (userAgent.Contains(";"))
            {
                List<string> rawString = userAgent.Split(';').ToList();
                if (rawString.Count > 2)
                {
                    if (rawString[2].Contains(")"))
                    {
                        List<string> raw = rawString[2].Split(')').ToList();
                        if (raw.Count > 1)
                        {
                            deviceModel = raw[0].Trim();
                        }

                    }
                }
            }

            return deviceModel;

        }


        //public static bool IsIpAddressFromVN(HttpRequestBase request)
        //{
        //    bool result = false;

        //    string ipAddress = GetClientIPAddress(request);
        //    string url = string.Format("http://ipinfo.io/{0}/geo", ipAddress);

        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = client.GetAsync(url).Result;

        //    string json = response.Content.ReadAsStringAsync().Result;

        //    var obj = JsonConvert.DeserializeObject<dynamic>(json);

        //    if (obj != null)
        //    {
        //        if (!string.IsNullOrEmpty(obj.country.Value))
        //        {
        //            if (obj.country.Value.ToString() == "VN")
        //            {
        //                result = true;
        //            }
        //        }
        //    }

        //    return result;
        //}

        //public static Enums.CountryCode GetCountryCode(HttpRequestBase request)
        //{
        //    Enums.CountryCode countryCode = Enums.CountryCode.Unknow;
        //    string ipAddress = GetClientIPAddress(request);
        //    string url = string.Format("http://ipinfo.io/{0}/geo", ipAddress);

        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = client.GetAsync(url).Result;
        //    string json = response.Content.ReadAsStringAsync().Result;

        //    var obj = JsonConvert.DeserializeObject<dynamic>(json);

        //    if (obj != null && obj.country !=null )
        //    {
        //        string code =obj.country.Value;
        //        switch (code)
        //        {
        //            case "VN":
        //                {
        //                    countryCode = Enums.CountryCode.VN;
        //                    break;
        //                }
        //            case "US":
        //                {
        //                    countryCode = Enums.CountryCode.US;
        //                    break;
        //                }
        //            case "CA":
        //                {
        //                    countryCode = Enums.CountryCode.CA;
        //                    break;
        //                }
        //            default:
        //                {
        //                    countryCode = Enums.CountryCode.Unknow;
        //                    break;
        //                }
        //        }


        //    }

        //    return countryCode;

        //}




        /// <summary>
        /// Format AppID(google) or UID(apple)
        /// </summary>
        /// <param name="appID"></param>
        /// <returns></returns>
        public static string FormatAppID(string appID)
        {
            string str = string.Empty;

            if (!string.IsNullOrEmpty(appID))
            {
                if (appID.Contains("]"))
                {
                    List<string> arrAppID = appID.Split(']').ToList();
                    str = arrAppID[0].Replace("[", "");
                }
                else
                {
                    //This case is UID for apple
                    str = appID;
                }

            }

            return str;
        }


        /// <summary>
        /// Gets the version of ClientApp
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static string GetVersion(string userAgent)
        {
            string version = "Unknown";
            if (!string.IsNullOrEmpty(userAgent) && userAgent.Contains("VoiceMobile") && userAgent.Contains("/"))
            {
                string deviceInfor = userAgent.Split('/')[1];
                if (deviceInfor.Contains("("))
                {
                    version = deviceInfor.Split('(')[0];
                }
            }
            return version;
        }

        /// <summary>
        /// Gets the platform of ClientApp
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static string GetPlatform(string userAgent)
        {
            string platform = "Unknown";
            if (!string.IsNullOrEmpty(userAgent) && userAgent.Contains("VoiceMobile") && userAgent.Contains("("))
            {
                string deviceInfor = userAgent.Split('(')[1];
                if (deviceInfor.Contains(";"))
                {
                    platform = deviceInfor.Split(';')[0];
                }
            }
            return platform;
        }


        /// <summary>
        /// Get
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientAppID(HttpRequestMessage request)
        {
            string clientAppID = string.Empty;
            var authorization = request.Headers.Authorization;
            if (authorization != null)
            {
                if (!string.IsNullOrEmpty(authorization.Parameter))
                {
                    if (authorization.Parameter.Contains(":"))
                    {
                        clientAppID = authorization.Parameter.Split(':')[0];
                    }
                }
            }

            return clientAppID;
        }

        /// <summary>
        /// Gets UserID from the cache server using clientAppID.
        /// Note: Support only the clientApp using ClientAppID with new authentication as Microsoft's Authentication
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static long GetUserID(HttpRequestMessage request)
        {
            long userID = 0;
            string clientAppID = GetClientAppID(request);
            if (!string.IsNullOrEmpty(clientAppID))
            {
                userID = dicho.Authentication.TokenManager.GetUserIDFromServerCache(clientAppID);
            }

            return userID;
        }

    }
}