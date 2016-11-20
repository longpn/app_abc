using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Models.OutputData
{
    public class SignInWithEmailAddressOutputData
    {
        public long user_id { get; set; }


        public string username { get; set; }

        public string fullname { get; set; }

        public string role { get; set; }

        public string avatar { get; set; }

        public string client_app_id { get; set; }

        public string access_token { get; set; }



        public SignInWithEmailAddressOutputData()
        {
            this.user_id = 0;
            this.fullname = string.Empty;
            this.username = string.Empty;
            this.role = string.Empty;
            this.avatar = string.Empty;
            this.client_app_id = string.Empty;
            this.access_token = string.Empty;

        }
    }
}