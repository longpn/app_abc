using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Models.OutputData
{
    public class CategoryOutputData
    {
        public int category_id { get; set; }

        public string category_name { get; set; }

        public CategoryOutputData()
        {
            this.category_id = 0;
            this.category_name = string.Empty;
        }
    }
}