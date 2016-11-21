using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Models
{
    public class OutputDataModel
    {

        public int code { get; set; }
        public string description { get; set; }
        public object Data { get; set; }

        public OutputDataModel()
        {
            this.code = 0;
            this.description = string.Empty;
            this.Data = string.Empty;
        }
    }
}