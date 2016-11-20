using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dicho.Models.InputData
{
    public class UpdateProfileInputData
    {
        [MaxLength(256)]
        public string email { get; set; }

        [MaxLength(20)]
        public string phone_number { get; set; }

        [MaxLength(10)]
        public string gender { get; set; }

        public DateTime? dob { get; set; }

        [Required]
        [MaxLength(256)]
        public string fullname { get; set; }

        [Required]
        [MaxLength(256)]
        public string address { get; set; }

        [MaxLength(256)]
        public string avatar { get; set; }

        public int country_id { get; set; }

        public int city_id { get; set; }

        public int disc_id { get; set; }

        public int number_member { get; set; }

        public int number_child { get; set; }

        public bool is_vegan { get; set; }

        [MaxLength(125)]
        public string zalo_id { get; set; }

        public UpdateProfileInputData()
        {
            this.address = string.Empty;
            this.avatar = string.Empty;
            this.city_id = 0;
            this.country_id = 0;
            this.disc_id = 0;
            this.dob = null;
            this.email = string.Empty;
            this.fullname = string.Empty;
            this.gender = string.Empty;
            this.is_vegan = false;
            this.number_child = 0;
            this.number_member = 0;
            this.phone_number = string.Empty;
            this.zalo_id = string.Empty;
        }
    }
}