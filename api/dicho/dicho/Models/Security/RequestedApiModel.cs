using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace dicho.Models.Security
{
    public class RequestedApiModel
    {
        public long UserID { get; set; }

        public string ApiName { get; set; }

        public string ApiVersion { get; set; }

        public string Platform { get; set; }

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public DateTime RequestedDate { get; set; }


        /// <summary>
        /// Represents for HttpStatusCode code
        /// </summary>
        public int HttpStatusCode { get; set; }

        public long ContentLength { get; set; }

        public string ClientAppVersion { get; set; }

        /// <summary>
        /// Determines the version of ClientApp that supported by server
        /// </summary>
        public bool IsSupportedVersion
        {
            get
            {
                bool isSupportedVersion = false;

                System.Version allowVersion = null;
                System.Version requestVersion;
                System.Version.TryParse(this.ApiVersion, out requestVersion);
                if (requestVersion != null)
                {
                    switch (this.Platform)
                    {
                        case "Android":
                            {
                                allowVersion = new Version("1.0");
                                int item = requestVersion.CompareTo(allowVersion);
                                if (item >= 0)
                                {
                                    isSupportedVersion = true;
                                    //1 greater
                                    //0 equal
                                    //-1 less than 
                                }
                                break;
                            }
                        case "iOS":
                            {
                                allowVersion = new Version("1.0");
                                int item = requestVersion.CompareTo(allowVersion);
                                if (item >= 0)
                                {
                                    isSupportedVersion = true;
                                }
                                break;
                            }
                        default:
                            {
                                //isSupportedVersion = true;
                                break;
                            }
                    }
                }
                return isSupportedVersion;
            }
        }

        /// <summary>
        /// Allows to pass authenticate
        /// Note: This is a list the APIs don't need to authenticate.
        /// </summary>
        public bool IsAllowPassAuthentication
        {
            get
            {
                bool result = false;
                if (!string.IsNullOrEmpty(this.ApiName))
                {

                    switch (this.ApiName.ToLower())
                    {
                        case "registerwithemailaddress":
                        case "signinwithemailaddress":
                        case "signinwithfacebook":
                        case "forgotpassword":
                        case "resendforgotpassword":
                        case "error":
                        case "resendverificationemail":
                        case "verifyforgotpassword":
                        case "registerwithbusiness":
                        case "category":
                        case "verifyemailaddress":
                            {
                                result = true;
                                break;
                            }
                        default:
                            break;
                    }
                }

                return result;
            }
        }

        public string ClientAppID { get; set; }



        public RequestedApiModel()
        {
            this.UserID = 0;
            this.ApiName = string.Empty;
            this.Platform = string.Empty;
            this.IPAddress = string.Empty;
            this.UserAgent = string.Empty;
            this.RequestedDate = DateTime.UtcNow;
            this.ApiVersion = string.Empty;
            this.HttpStatusCode = 0;
            this.ContentLength = 0;
            this.ClientAppVersion = string.Empty;
            this.ClientAppID = string.Empty;
        }
    }
}