using dicho.Models.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.DatabaseInteract
{
    public class TokenDatabaseInteract
    {
        /// <summary>
        /// Returns all tokens of the entire user
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, TokenInforModel> GetAllToken()
        {

            Dictionary<string, TokenInforModel> tokens = new Dictionary<string, TokenInforModel>();
            using (DichoDataContext dal = new DichoDataContext())
            {
                var items = dal.proc_ClientAppInfor_GetAllToken().ToList();
                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        if (!tokens.ContainsKey(item.client_app_id))
                        {
                            TokenInforModel token = new TokenInforModel();
                            token.UserID = item.user_id;
                            token.SharedSecretKey = item.token;
                            tokens.Add(item.client_app_id, token);
                        }

                    }
                }

                return tokens;
            }

        }

        /// <summary>
        /// Add new or update access token for a device 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="deviceFirmwareID"></param>
        /// <param name="clientAppID"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static string AddToken(long userID, string deviceFirmwareID, string clientAppID, string accessToken)
        {
            string existedToken = string.Empty;

            using (DichoDataContext db = new DichoDataContext())
            {
                var item = db.proc_ClientAppInfor_Insert(userID, deviceFirmwareID, clientAppID, accessToken).FirstOrDefault();
                if (!string.IsNullOrEmpty(item))
                {
                    existedToken = item;
                }
            }

            return existedToken;
        }
    }
}