using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dicho.Models.InputData
{
    public class RegisterWithEmailAddressInputData
    {
        [Required]
        [MaxLength(256)]
        public string email { get; set; }

        [Required]
        [MaxLength(256)]
        public string fullname { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public string password { get; set; }



        public RegisterWithEmailAddressInputData()
        {
            this.email = string.Empty;
            this.password = string.Empty;
            this.fullname = string.Empty;
        }
    }
}