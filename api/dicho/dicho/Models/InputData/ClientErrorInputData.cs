using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace dicho.Models.InputData
{
    public class ClientErrorInputData
    {
        [Required]
        [MaxLength(1024)]
        public string ErrorMessage { get; set; }

        [Required]
        [MaxLength(15)]
        public string Platform { get; set; }


        public long UserID { get; set; }

        public ClientErrorInputData()
        {
            this.ErrorMessage = string.Empty;
            this.Platform = string.Empty;
            this.UserID = 0;
        }
    }
}