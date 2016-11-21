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
    public class LoginController : ApiController
    {

        /// <summary>
        /// Sign in with the email address
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        [Route("api/v1/login")]
        public OutputDataModel Post(SignInWithEmailAddressInputData value)
        {
            OutputDataModel outputData = new OutputDataModel();
            outputData = UserDatabaseInteract.SignInWithEmailAddress(value);
            return outputData;
        }

    }
}