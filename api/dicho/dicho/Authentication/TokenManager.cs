using dicho.Cache;
using dicho.DatabaseInteract;
using dicho.Models;
using dicho.Models.Token;
using dicho.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace dicho.Authentication
{
    public class TokenManager
    {

        public static bool IsExistClientAppID(string clientAppID)
        {

            bool result = false;

            if (TokenCache.Instance.IsExistClientAppID(clientAppID))
            {
                result = true;
            }
            return result;

        }


        //#endregion


        /// <summary>
        /// Generates an ClientAppID
        /// </summary>
        /// <returns>ClientAppID</returns>
        public static string GenerateClientAppID()
        {
            string clientID = Guid.NewGuid().ToString().Replace("-", "");
            string clientAppID = clientID.Substring(0, 20).ToLower();
            while (IsExistClientAppID(clientAppID))
            {
                clientID = Guid.NewGuid().ToString().Replace("-", "");
                clientAppID = clientID.Substring(0, 20).ToLower();
            }

            return clientAppID;
        }

        private static string GenerateAccessToken()
        {
            string accessToken = string.Format("{0}{1}", Guid.NewGuid(), Guid.NewGuid());

            accessToken = accessToken.Replace("-", "");
            return accessToken;
        }








        /// <summary>
        /// Adds user's token. If this user has already existed a token, it will use this token that not provide a new
        /// </summary>
        /// <param name="token"></param>
        /// <param name="userID"></param>
        /// <param name="createdUtcDate"></param>
        /// <param name="expiredUtcDate"></param>
        /// <returns></returns>
        public static string GenerateAccessToken(long userID, string deviceFirmwareID, string clientAppID)
        {

            string accessToken = GenerateAccessToken();

            TokenInforModel value = new TokenInforModel();
            value.UserID = userID;
            value.SharedSecretKey = accessToken;

            Task.Factory.StartNew(() =>
            {
                string existedClientID = TokenDatabaseInteract.AddToken(userID, deviceFirmwareID, clientAppID, accessToken);

                TokenCache.Instance.Add(clientAppID, value);
                TokenCache.Instance.Remove(existedClientID);

            });

            return accessToken;
        }








        /// <summary>
        /// Loads all user's token when WebAPI started
        /// </summary>
        public static void FetchTokenToCache()
        {
            Task.Factory.StartNew(() =>
            {
                var tokens = TokenDatabaseInteract.GetAllToken();

                //Fetch all token to Azure Cache
                TokenCache.Instance.FetchDataToCache(tokens);
            });
        }




        /// <summary>
        /// Gets an UserID from the cache using ClientAppID
        /// </summary>
        /// <param name="clientAppID"></param>
        /// <returns></returns>
        public static long GetUserIDFromServerCache(string clientAppID)
        {
            long userID = 0;

            userID = TokenCache.Instance.GetUserID(clientAppID);

            //if (GlobalConfig.IsHostingCloudService)
            //{
            //    userID = SharedKeyCache.Instance.GetUserID(clientAppID);
            //}
            //else
            //{
            //    userID = RAMCache.Instance.GetUserID(clientAppID);
            //}

            return userID;
        }




        /// <summary>
        /// Authenticates the request from ClientApp associated with ClientAppID and access token for every single request
        /// Note: Support only the request has attached ClientAppID information
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static Enums.TokenValidation AuthorizeRequest(HttpRequestMessage request)
        {
            Enums.TokenValidation result = Enums.TokenValidation.Invalid;
            string clientSignature = request.Headers.Authorization.Scheme.Split('|')[1];

            if (!UsedTokenCache.Instance.IsExistAccessToken(clientSignature))
            {
                string clientAppID = request.Headers.Authorization.Scheme.Split('|')[0];

                // Read from Azure Cache
                string sharedKey = TokenCache.Instance.GetSharedToken(clientAppID);

                if (!string.IsNullOrEmpty(sharedKey))
                {
                    string accessToken = EncodeHelper.HashAccessToken(request, sharedKey);

                    if (accessToken == clientSignature)
                    {
                        result = Enums.TokenValidation.Valid;
                        //Store Access Token to cache
                        UsedTokenCache.Instance.AddAccessToken(accessToken);
                    }
                }
            }

            return result;
        }

    }
}