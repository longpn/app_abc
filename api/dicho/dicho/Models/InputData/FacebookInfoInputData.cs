using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Models.InputData
{
    public class FacebookInfoInputData
    {
        public string FacebookID { get; set; }

        public string FacebookAccount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Locale { get; set; }

        public int NumberOfFriends { get; set; }

        public string FacebookAvatarUrl { get; set; }

        public FacebookInfoInputData()
        {
            this.FacebookID = string.Empty;
            this.FacebookAccount = string.Empty;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Gender = string.Empty;
            this.Locale = string.Empty;
            this.NumberOfFriends = 0;
            this.FacebookAvatarUrl = string.Empty;
        }

    }
}