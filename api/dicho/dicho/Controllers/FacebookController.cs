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
    public class FacebookController : ApiController
    {
        /// <summary>
        /// Sign in with facebook account
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [Route("api/v1/facebook")]
        public OutputDataModel Post(SignInWithFacebookInputData value)
        {
            return UserDatabaseInteract.SignInWithFacebookAccount(value);
        }
    }
}