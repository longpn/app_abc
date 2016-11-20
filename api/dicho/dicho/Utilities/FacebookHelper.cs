using dicho.Models.InputData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Utilities
{
    public class FacebookHelper
    {
        /// <summary>
        /// Reads basis information from facebook account
        /// </summary>
        /// <param name="facebookToken"></param>
        /// <returns></returns>
        public static FacebookInfoInputData GetFacebookAccountInfor(string facebookToken)
        {
            FacebookInfoInputData facebookInfor = new FacebookInfoInputData();

            try
            {
                Facebook.FacebookClient client = new Facebook.FacebookClient(facebookToken);

                dynamic infor = client.Get("/v2.3/me");

                facebookInfor.FacebookID = infor.id;

                facebookInfor.FirstName = infor.first_name;
                facebookInfor.LastName = infor.last_name;
                facebookInfor.Locale = infor.locale;
                facebookInfor.Gender = infor.gender;


                dynamic result = client.Get("/v2.3/me/friends");
                long friend = result.summary.total_count;

                if (friend > 0)
                {
                    facebookInfor.NumberOfFriends = Int32.Parse(friend.ToString());
                }


                try
                {
                    //Get facebook avatar url
                    string facebookAvatar = string.Format("/v2.4/{0}/picture?redirect=false", facebookInfor.FacebookID);
                    dynamic avatar = client.Get(facebookAvatar);
                    facebookInfor.FacebookAvatarUrl = avatar.data.url;
                }
                catch { }


                facebookInfor.FacebookAccount = infor.email;
            }
            catch
            {

            }
            return facebookInfor;
        }
    }
}