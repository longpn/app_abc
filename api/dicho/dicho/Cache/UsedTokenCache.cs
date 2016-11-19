using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace dicho.Cache
{
    public class UsedTokenCache
    {

        private ObjectCache usedTokenDataCache = null;


        private static UsedTokenCache _instance;



        private UsedTokenCache()
        {

            usedTokenDataCache = MemoryCache.Default;
        }


        public static UsedTokenCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UsedTokenCache();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Adds an access token to the cache and expired after 24 hours
        /// </summary>
        /// <param name="accessToken"></param>
        public void AddAccessToken(string accessToken)
        {

            if (usedTokenDataCache != null)
            {
                var existedAccessToken = usedTokenDataCache.Get(accessToken);
                if (existedAccessToken == null)
                {
                    var expiration = DateTimeOffset.UtcNow.AddHours(24);
                    usedTokenDataCache.Add(accessToken, accessToken, expiration);
                }
            }
        }

        /// <summary>
        /// Validates whether access token has been existed or not
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public bool IsExistAccessToken(string accessToken)
        {
            if (usedTokenDataCache != null)
            {
                var existedAccessToken = usedTokenDataCache.Get(accessToken);
                if (!string.IsNullOrEmpty(existedAccessToken.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}