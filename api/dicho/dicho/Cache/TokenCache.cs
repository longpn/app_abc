using dicho.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;

namespace dicho.Cache
{
    public class TokenCache
    {

        private ObjectCache tokenDataCache = null;

        private static TokenCache _instance;



        private TokenCache()
        {
            tokenDataCache = MemoryCache.Default;
        }

        public static TokenCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TokenCache();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Adds an item to cache
        /// </summary>
        /// <param name="clientAppID"></param>
        /// <param name="tokenInfor"></param>
        public void Add(string clientAppID, TokenInforModel tokenInfor)
        {
            if (tokenDataCache != null)
            {
                var existedAccessToken = tokenDataCache.Get(clientAppID);
                if (existedAccessToken == null)
                {
                    var expired = DateTimeOffset.UtcNow.AddYears(1);
                    tokenDataCache.Add(clientAppID, tokenInfor, expired);
                }
            }
        }

        /// <summary>
        /// Gets a shared secret key from the cache using the specified ClientAppID
        /// </summary>
        /// <param name="clientAppID"></param>
        /// <returns></returns>
        public string GetSharedToken(string clientAppID)
        {
            string secretKey = string.Empty;
            if (tokenDataCache != null)
            {
                TokenInforModel infor = (TokenInforModel)tokenDataCache.Get(clientAppID);
                if (infor != null)
                {
                    secretKey = infor.SharedSecretKey;
                }
            }

            return secretKey;
        }

        /// <summary>
        /// Removes an item from the cache using the specified ClientAppID
        /// </summary>
        /// <param name="clientAppID"></param>
        public void Remove(string clientAppID)
        {
            if (tokenDataCache != null)
            {
                var item = tokenDataCache.Get(clientAppID);
                if (item != null)
                {
                    tokenDataCache.Remove(clientAppID);
                }
            }
        }

        /// <summary>
        /// Gets an UserID from the cache using the specified ClientAppID.
        /// </summary>
        /// <param name="clientAppID"></param>
        /// <returns></returns>
        public long GetUserID(string clientAppID)
        {
            long userID = 0;
            if (tokenDataCache != null)
            {
                TokenInforModel infor = (TokenInforModel)tokenDataCache.Get(clientAppID);
                if (infor != null)
                {
                    userID = infor.UserID;
                }
            }
            return userID;
        }





        /// <summary>
        /// Validates whether it has used or not
        /// </summary>
        /// <param name="clientAppID"></param>
        /// <returns></returns>
        public bool IsExistClientAppID(string clientAppID)
        {
            bool result = false;
            if (tokenDataCache != null)
            {
                var existeClientAppID = tokenDataCache.Get(clientAppID);
                if (existeClientAppID != null)
                {
                    result = true;
                }
            }
            return result;


        }




        /// <summary>
        /// Fetches all authentication information to the cache
        /// </summary>
        /// <param name="sharedTokens"></param>
        public void FetchDataToCache(Dictionary<string, TokenInforModel> sharedTokens)
        {
            if (tokenDataCache != null)
            {
                Task.Factory.StartNew(() =>
                {
                    foreach (var item in sharedTokens)
                    {
                        var expired = DateTimeOffset.UtcNow.AddYears(1);
                        tokenDataCache.Add(item.Key, item.Value, expired);
                    }

                });
            }
        }





    }
}