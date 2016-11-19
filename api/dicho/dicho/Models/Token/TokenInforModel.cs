using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Models.Token
{
    public class TokenInforModel
    {
        public long UserID { get; set; }

        public string SharedSecretKey { get; set; }

        public TokenInforModel()
        {
            this.UserID = 0;
            this.SharedSecretKey = string.Empty;
        }
    }
}