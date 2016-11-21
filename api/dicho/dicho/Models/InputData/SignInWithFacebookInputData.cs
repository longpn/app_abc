using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dicho.Models.InputData
{
    public class SignInWithFacebookInputData
    {
        [Required]
        [MaxLength(30)]
        public string facebook_id { get; set; }

        [Required]
        [MaxLength(512)]
        public string facebook_token { get; set; }

        [Required]
        [MaxLength(50)]
        public string device_firmware_id { get; set; }

      
        public SignInWithFacebookInputData()
        {
            this.facebook_id = string.Empty;
            this.facebook_token = string.Empty;
            this.device_firmware_id = string.Empty;
        }


    }
}