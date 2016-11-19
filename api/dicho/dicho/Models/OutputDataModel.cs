using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Models
{
    public class OutputDataModel
    {

        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public object Data { get; set; }

        public OutputDataModel()
        {
            this.StatusCode = 0;
            this.StatusDescription = string.Empty;
            this.Data = string.Empty;
        }
    }
}