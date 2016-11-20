using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Models.OutputData
{
    public class UserProfileOutputData
    {

        public long user_id { get; set; }

        public string username { get; set; }

        public string fullname { get; set; }

        public string social_name { get; set; }

        public string role { get; set; }

        public bool is_active { get; set; }

        public string avatar { get; set; }

        public string email { get; set; }

        public string gender { get; set; }

        public string dob { get; set; }

        public string phone_number { get; set; }

        public string zalo_id { get; set; }

        public int country_id { get; set; }

        public int city_id { get; set; }

        public int disc_id { get; set; }

        public int number_member { get; set; }

        public int number_child { get; set; }

        public decimal save_money { get; set; }

        public bool is_vegan  {get;set;}

        public UserProfileOutputData()
        {
            this.avatar = string.Empty;
            this.city_id = 0;
            this.country_id = 0;
            this.disc_id = 0;
            this.dob = string.Empty;
            this.email = string.Empty;
            this.fullname = string.Empty;
            this.gender = string.Empty;
            this.is_active = false;
            this.is_vegan = false;
            this.number_child = 0;
            this.number_member = 0;
            this.phone_number = string.Empty;
            this.role = string.Empty;
            this.save_money = 0;
            this.social_name = string.Empty;
            this.user_id = 0;
            this.username = string.Empty;
            this.zalo_id = string.Empty;
        }
    }
}