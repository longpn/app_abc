using dicho.Authentication;
using dicho.DatabaseInteract;
using dicho.Models;
using dicho.Models.InputData;
using dicho.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace dicho.Controllers
{
    [ActionParameterValidation]
    public class UserController : ApiController
    {
        /// <summary>
        /// Register with the email address
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/1.0/User/RegisterWithEmailAddress")]
        public OutputDataModel RegisterWithEmailAddressV1(RegisterWithEmailAddressInputData value)
        {
            OutputDataModel outputData = new OutputDataModel();
            outputData = UserDatabaseInteract.RegisterWithEmailAddress(value);
            return outputData;
        }


        


        /// <summary>
        /// Sign in with the email address
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/1.0/User/SignInWithEmailAddress")]
        public OutputDataModel SignInWithEmailAddressV1(SignInWithEmailAddressInputData value)
        {
            OutputDataModel outputData = new OutputDataModel();
            outputData = UserDatabaseInteract.SignInWithEmailAddress(value);
            return outputData;
        }

        


        /// <summary>
        /// Sign in with facebook account
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/1.0/User/SignInWithFacebook")]
        public OutputDataModel SignInWithFacebookV1(SignInWithFacebookInputData value)
        {
            return UserDatabaseInteract.SignInWithFacebookAccount(value);
        }

        /// <summary>
        /// Get user's profile
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/1.0/users")]
        public OutputDataModel GetUserProfile()
        {
            OutputDataModel outputData = new OutputDataModel();
            long userID = 2;// ClientInforHelper.GetUserID(this.Request);

            if (userID > 0)
            {
                outputData = UserDatabaseInteract.GetUserProfile(userID);
            }
            else
            {
                outputData.StatusCode = (int)Enums.StatusCode.NotExistedAccount;
                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.NotExistedAccount);
            }
            return outputData;
        }

        /// <summary>
        /// Update user's profile
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/1.0/users")]
        public HttpResponseMessage UpdateProfile()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            OutputDataModel outputData = new OutputDataModel();
            long userID = ClientInforHelper.GetUserID(this.Request);

            if (userID > 0)
            {
                UpdateProfileInputData profile = new UpdateProfileInputData();

                #region Read fields

                var forms = HttpContext.Current.Request.Form;
                if (forms != null)
                {
                    foreach (var item in forms.AllKeys)
                    {
                        switch (item.ToLower())
                        {
                            case "address":
                                {
                                    profile.address = forms.Get(item);
                                    break;
                                }
                            case "avatar":
                                {
                                    profile.avatar = forms.Get(item);
                                    break;
                                }
                            case "city_id":
                                {
                                    profile.city_id = int.Parse(forms.Get(item));
                                    break;
                                }
                            case "country_id":
                                {
                                    profile.country_id = int.Parse(forms.Get(item));
                                    break;
                                }
                            case "disc_id":
                                {
                                    profile.disc_id = int.Parse(forms.Get(item));
                                    break;
                                }
                            case "email":
                                {
                                    profile.email = forms.Get(item);
                                    break;
                                }
                           
                            case "fullname":
                                {
                                    profile.fullname = forms.Get(item);
                                    break;
                                }
                            case "gender":
                                {
                                    profile.gender = forms.Get(item);
                                    break;
                                }
                            case "dob":
                                {
                                    profile.dob = DateTime.Parse(forms.Get(item));
                                    break;
                                }
                            case "is_vegan":
                                {
                                    profile.is_vegan = Boolean.Parse( forms.Get(item));
                                    break;
                                }
                            case "number_child":
                                {
                                    profile.number_child = int.Parse(forms.Get(item));
                                    break;
                                }
                            case "number_member":
                                {
                                    profile.number_member = int.Parse(forms.Get(item));
                                    break;
                                }
                            case "phone_number":
                                {
                                    profile.phone_number = forms.Get(item);
                                    break;
                                }
                            case "zalo_id":
                                {
                                    profile.zalo_id = forms.Get(item);
                                    break;
                                }
                        }
                    }

                }

                #endregion

                #region Read files
                var files = HttpContext.Current.Request.Files;
                if (files != null && files.Count > 0)
                {
                    string fileName = string.Empty;
                    //CloudBlobContainer container = AzureStorageHelper.GetTempContainer();
                    var file = files.Get(files.AllKeys[0]);

                    if (FileHelper.IsValidImage(file.FileName))
                    {
                        fileName = FileHelper.FormatFileName(file.FileName);
                        //CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
                        //blob.Properties.ContentType = file.ContentType;
                        //blob.UploadFromStreamAsync(file.InputStream).Wait();
                        profile.avatar = fileName;
                    }
                    else
                    {
                        outputData.StatusCode = (int)Enums.StatusCode.UnsupportedFileType;
                        outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.UnsupportedFileType);
                        return Request.CreateResponse(HttpStatusCode.OK, outputData);
                    }
                }



                #endregion
                // Validates all fields coming from client side
                this.Validate<UpdateProfileInputData>(profile);
                if (this.ModelState.IsValid)
                {
                    outputData = UserDatabaseInteract.UpdateProfile(profile, userID);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Parameter(s) is invalid");
                }

            }
            else
            {
                outputData.StatusCode = (int)Enums.StatusCode.NotExistedAccount;
                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.NotExistedAccount);
            }
            return Request.CreateResponse(HttpStatusCode.OK, outputData); ;
        }


       

       

    }
}