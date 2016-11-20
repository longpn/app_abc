using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dicho.Models.InputData
{
    public class SignInWithEmailAddressInputData
    {
        [Required]
        [MaxLength(256)]
        public string email { get; set; }

        [Required]
        [MaxLength(50)]
        public string password { get; set; }

        [Required]
        [MaxLength(50)]
        public string device_firmware_id { get; set; }

        
        public SignInWithEmailAddressInputData()
        {
            this.email = string.Empty;
            this.password = string.Empty;
            this.device_firmware_id = string.Empty;
        }
    }
}