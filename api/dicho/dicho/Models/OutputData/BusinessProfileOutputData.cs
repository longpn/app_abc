using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Models.OutputData
{
    public class BusinessProfileOutputData : UserProfileOutputData
    {


        public long business_rating { get; set; }

        public long business_total_transaction { get; set; }

        public decimal business_total_buying_free { get; set; }

        public long business_number_of_transaction { get; set; }

        public BusinessProfileOutputData()
        {
            this.business_number_of_transaction = 0;
            this.business_rating = 0;
            this.business_total_buying_free =0;
            this.business_total_transaction = 0;
        }
    }
}