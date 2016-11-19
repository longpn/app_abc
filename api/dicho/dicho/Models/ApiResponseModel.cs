using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace dicho.Models
{
        /// <summary>
        /// Represents for content response class
        /// </summary>
        public class ApiResponseModel
        {
            public Dictionary<string, string> Meta { get; set; }
            public object Body { get; set; }

            public ApiResponseModel()
            {
                this.Meta = new Dictionary<string, string>();
                this.Body = string.Empty;
            }

            public void AddMeta(HttpStatusCode code, string message, string detail)
            {
                this.Meta.Add("Code", code.GetHashCode().ToString());
                this.Meta.Add("Message", message);
                this.Meta.Add("Detail", detail);
            }

            public void AddMeta(int code, string message, string detail)
            {
                this.Meta.Add("Code", code.GetHashCode().ToString());
                this.Meta.Add("Message", message);
                this.Meta.Add("Detail", detail);
            }

            public void AddBody(int code, string message, string detail)
            {
                OutputDataModel outputData = new OutputDataModel();
                outputData.StatusCode = code;
                outputData.StatusDescription = message;
                this.Body = outputData;
            }



        }
}