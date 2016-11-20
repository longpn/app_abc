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
        public string FacebookID { get; set; }

        [Required]
        [MaxLength(512)]
        public string FacebookToken { get; set; }

        [Required]
        [MaxLength(50)]
        public string DeviceFirmwareID { get; set; }


        [MaxLength(128)]
        public string AppID { get; set; }

        [Required]
        [MaxLength(3)]
        public string LanguageCode { get; set; }


        public SignInWithFacebookInputData()
        {
            this.FacebookID = string.Empty;
            this.FacebookToken = string.Empty;
            this.DeviceFirmwareID = string.Empty;
            this.AppID = string.Empty;
            this.LanguageCode = string.Empty;
        }


    }
}