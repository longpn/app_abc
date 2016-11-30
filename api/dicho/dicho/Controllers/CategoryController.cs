using dicho.Authentication;
using dicho.DatabaseInteract;
using dicho.Models;
using dicho.Models.InputData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace dicho.Controllers
{
     [ActionParameterValidation]
    public class CategoryController : ApiController
    {
         [Route("api/v1/category/{type:int}")]
         public OutputDataModel Get(int type)
         {
             OutputDataModel outputData = new OutputDataModel();
             outputData = CategoryInteract.GetAll(type);
             return outputData;
         }
    }
}