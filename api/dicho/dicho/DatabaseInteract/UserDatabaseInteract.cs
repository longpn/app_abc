using dicho.Authentication;
using dicho.Models;
using dicho.Models.InputData;
using dicho.Models.OutputData;
using dicho.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.DatabaseInteract
{
    public class UserDatabaseInteract
    {
        /// <summary>
        /// Register an new account with the email address
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OutputDataModel RegisterWithEmailAddress(RegisterWithEmailAddressInputData value)
        {
            OutputDataModel outputData = new OutputDataModel();
            string password = EncodeHelper.HashMD5Password(value.password);
            using (DichoDataContext db = new DichoDataContext())
            {
                var item = db.proc_User_RegisterWithEmailAddress(value.email, password,value.fullname).FirstOrDefault();
                if (item != null)
                {
                    switch (item.Status)
                    {
                        case "NeedVerification":
                            {
                                outputData.code = (int)Enums.StatusCode.NeedVerificationUserAccount;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.NeedVerificationUserAccount);

                               // EmailHelper.SendSignUpEmail(item.VerificationCode, value.EmailAddress, value.LanguageCode);
                                break;
                            }
                        case "Existed":
                            {
                                outputData.code = (int)Enums.StatusCode.AlreadyUserAccount;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.AlreadyUserAccount);
                                break;
                            }
                        default:
                            {
                                outputData.code = (int)Enums.StatusCode.FaliedRegisterUserAccount;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FaliedRegisterUserAccount);
                                break;
                            }
                    }
                }
                else
                {
                    outputData.code = (int)Enums.StatusCode.FaliedRegisterUserAccount;
                    outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FaliedRegisterUserAccount);
                }
            }
            return outputData;
        }

        /// <summary>
        /// Sign in with the email address
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OutputDataModel SignInWithEmailAddress(SignInWithEmailAddressInputData value)
        {
            OutputDataModel outputData = new OutputDataModel();
            string password = EncodeHelper.HashMD5Password(value.password);

            using (DichoDataContext db = new DichoDataContext())
            {
                var item = db.proc_User_SignInWithEmailAddress(value.email, password).FirstOrDefault();
                if (item != null)
                {
                    switch (item.Status)
                    {
                        case "Successful":
                            {
                                outputData.code = (int)Enums.StatusCode.Successful;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.Successful);

                                SignInWithEmailAddressOutputData signIn = new SignInWithEmailAddressOutputData();
                                signIn.user_id = item.UserID;
                                signIn.username = value.email;
                                signIn.fullname = item.FullName;
                                signIn.role = item.Role;
                                signIn.avatar = item.Avatar;
                                //signIn.AvatarUrl = AzureStorageHelper.GetAvatarUrl(signIn.UserID, item.Avatar);
                                signIn.client_app_id = TokenManager.GenerateClientAppID();
                                signIn.access_token = TokenManager.GenerateAccessToken(signIn.user_id, value.device_firmware_id, signIn.client_app_id);

                                outputData.Data = signIn;
                                break;
                            }
                        case "NeedVerification":
                            {
                                outputData.code = (int)Enums.StatusCode.NeedVerificationUserAccount;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.NeedVerificationUserAccount);

                                //EmailHelper.SendSignUpEmail(item.VerificationCode, value.EmailAddress, value.LanguageCode);
                                break;
                            }
                        case "IncorrectPassword":
                            {
                                outputData.code = (int)Enums.StatusCode.IncorrectPassword;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.IncorrectPassword);
                                break;
                            }
                        case "NotExisted":
                            {
                                outputData.code = (int)Enums.StatusCode.NotExistedAccount;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.NotExistedAccount);
                                break;
                            }
                        default:
                            goto case "NotExisted";
                    }
                }
                else
                {
                    outputData.code = (int)Enums.StatusCode.NotExistedAccount;
                    outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.NotExistedAccount);
                }
            }

            return outputData;
        }

        /// <summary>
        /// Sign in with facebook account
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static OutputDataModel SignInWithFacebookAccount(SignInWithFacebookInputData value)
        {
            OutputDataModel outputData = new OutputDataModel();

            //Sign in with facebook account
            using (DichoDataContext db = new DichoDataContext())
            {
                var item = db.proc_User_SignInWithFacebookAccount(value.facebook_id, value.facebook_token).FirstOrDefault();
                if (item != null)
                {
                    switch (item.Status)
                    {
                        case "Successful":
                            {
                                outputData.code = (int)Enums.StatusCode.Successful;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.Successful);

                                SignInWithEmailAddressOutputData signIn = new SignInWithEmailAddressOutputData();
                                signIn.user_id = item.user_id;
                                signIn.username = item.username;
                                signIn.fullname = item.full_name;
                                signIn.role = item.role;
                                signIn.avatar = item.avatar;
                                //signIn.AvatarUrl = AzureStorageHelper.GetAvatarUrl(signIn.UserID, item.Avatar);
                                signIn.client_app_id = TokenManager.GenerateClientAppID();
                                signIn.access_token = TokenManager.GenerateAccessToken(signIn.user_id, value.device_firmware_id, signIn.client_app_id);

                                outputData.Data = signIn;
                                return outputData;
                            }
                    }
                }
            }


            // Get information from facebook account
            FacebookInfoInputData facebookInfor = FacebookHelper.GetFacebookAccountInfor(value.facebook_token);
            if(!string.IsNullOrEmpty(facebookInfor.FacebookID))
            {
                // Register With facebook account
                using (DichoDataContext db = new DichoDataContext())
                {
                    var item = db.proc_User_RegisterWithSocialNetworkAccount(value.facebook_id, value.facebook_token, facebookInfor.FacebookAccount,
                        facebookInfor.FirstName, facebookInfor.LastName, "Facebook", facebookInfor.Gender, "", value.device_firmware_id, "").FirstOrDefault();
                    if (item != null)
                    {
                        switch (item.Status)
                        {
                            case "Successful":
                                {
                                    outputData.code = (int)Enums.StatusCode.Successful;
                                    outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.Successful);

                                    SignInWithEmailAddressOutputData signIn = new SignInWithEmailAddressOutputData();
                                    signIn.user_id = item.user_id;
                                    signIn.username = item.username;
                                    signIn.fullname = item.full_name;
                                    signIn.role = item.role;
                                    signIn.avatar = item.avatar;
                                    //signIn.AvatarUrl = AzureStorageHelper.GetAvatarUrl(signIn.UserID, item.Avatar);
                                    signIn.client_app_id = TokenManager.GenerateClientAppID();
                                    signIn.access_token = TokenManager.GenerateAccessToken(signIn.user_id, value.device_firmware_id, signIn.client_app_id);

                                    outputData.Data = signIn;
                                    break;
                                }
                            default:
                                {
                                    outputData.code = (int)Enums.StatusCode.FailedSignInWithFacebook;
                                    outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedSignInWithFacebook);
                                    break;
                                }

                        }
                    }
                    else
                    {
                        outputData.code = (int)Enums.StatusCode.FailedSignInWithFacebook;
                        outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedSignInWithFacebook);
                    }
                }
            }
            else
            {
                outputData.code = (int)Enums.StatusCode.FailedSignInWithFacebook;
                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedSignInWithFacebook);
            }
            
            

            return outputData;
        }

        /// <summary>
        /// Get user's profile  
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static OutputDataModel GetUserProfile(long UserID)
        {
            OutputDataModel outputData = new OutputDataModel();
            using (DichoDataContext db = new DichoDataContext())
            {
                var item = db.proc_User_Profile(UserID).FirstOrDefault();
                if (item != null)
                {
                    switch (item.Status)
                    {
                        case "Successful":
                            {
                                outputData.code = (int)Enums.StatusCode.Successful;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.Successful);


                                if (item.role.Equals(Enums.UserRole.Business))
                                {
                                    BusinessProfileOutputData userInfo = new BusinessProfileOutputData();
                                    userInfo.avatar = item.avatar;
                                    userInfo.city_id = item.city_id;
                                    userInfo.country_id = item.country_id;
                                    userInfo.disc_id = item.disc_id;
                                    userInfo.email = item.email;
                                    userInfo.fullname = item.fullname;
                                    userInfo.gender = item.gender;
                                    userInfo.is_active = item.is_active;
                                    userInfo.is_vegan = item.is_vegan;
                                    userInfo.number_child = item.number_child;
                                    userInfo.number_member = item.number_member;
                                    userInfo.phone_number = item.phone_number;
                                    userInfo.role = item.role;
                                    userInfo.save_money = item.save_money;
                                    userInfo.social_name = item.social_name;
                                    userInfo.user_id = item.user_id;
                                    userInfo.username = item.username;
                                    userInfo.zalo_id = item.zalo_id;

                                    //userInfo.AvatarUrl = AzureStorageHelper.GetAvatarUrl(item.UserID, item.Avatar);
                                    if (item.dob != null)
                                    {
                                        userInfo.dob = item.dob.Value.ToString();
                                    }
                                    userInfo.business_number_of_transaction = item.business_number_of_transaction;
                                    userInfo.business_rating = item.business_rating;
                                    userInfo.business_total_buying_free = item.business_total_buying_free;
                                    userInfo.business_total_transaction = item.business_total_transaction;
                                    outputData.Data = userInfo;
                                }
                                else
                                {
                                    UserProfileOutputData userInfo = new UserProfileOutputData();
                                    userInfo.avatar = item.avatar;
                                    userInfo.city_id = item.city_id;
                                    userInfo.country_id = item.country_id;
                                    userInfo.disc_id = item.disc_id;
                                    userInfo.email = item.email;
                                    userInfo.fullname = item.fullname;
                                    userInfo.gender = item.gender;
                                    userInfo.is_active = item.is_active;
                                    userInfo.is_vegan = item.is_vegan;
                                    userInfo.number_child = item.number_child;
                                    userInfo.number_member = item.number_member;
                                    userInfo.phone_number = item.phone_number;
                                    userInfo.role = item.role;
                                    userInfo.save_money = item.save_money;
                                    userInfo.social_name = item.social_name;
                                    userInfo.user_id = item.user_id;
                                    userInfo.username = item.username;
                                    userInfo.zalo_id = item.zalo_id;

                                    //userInfo.AvatarUrl = AzureStorageHelper.GetAvatarUrl(item.UserID, item.Avatar);
                                    if (item.dob != null)
                                    {
                                        userInfo.dob = item.dob.Value.ToString();
                                    }
                                    outputData.Data = userInfo;
                                }


                                break;
                            }
                        case "NotExisted":
                            {
                                outputData.code = (int)Enums.StatusCode.NotExistedAccount;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.NotExistedAccount);
                                break;
                            }
                        default:
                            {
                                outputData.code = (int)Enums.StatusCode.FailedGetUserProfile;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedGetUserProfile);
                                break;
                            }
                    }
                }
                else
                {

                    outputData.code = (int)Enums.StatusCode.FailedGetUserProfile;
                    outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedGetUserProfile);

                }
            }

            return outputData;
        }

        public static OutputDataModel UpdateProfile(UpdateProfileInputData value, long userID)
        {
            OutputDataModel outputData = new OutputDataModel();
            using (DichoDataContext db = new DichoDataContext())
            {
                var item = db.proc_User_UpdateProfile(userID, value.fullname, value.address, value.avatar, value.email, value.gender, value.dob,
                    value.phone_number, value.zalo_id, value.country_id, value.city_id, value.disc_id, value.number_member, value.number_child, value.is_vegan).SingleOrDefault();
                if (!string.IsNullOrEmpty(item))
                {
                    switch (item.ToString())
                    {
                        case "Successful":
                            {
                                outputData.code = (int)Enums.StatusCode.Successful;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.Successful);
                                if (!string.IsNullOrEmpty(value.avatar))
                                {
                                    //Moves the temp file to container of user
                                    // AzureStorageHelper.MoveTempFileToUserContainer(userID, value.AvatarUrl);
                                }
                                break;
                            }
                        case "NotExisted":
                            {
                                outputData.code = (int)Enums.StatusCode.NotExistedAccount;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.NotExistedAccount);
                                break;
                            }
                        case "Failed":
                            {
                                outputData.code = (int)Enums.StatusCode.FailedUpdateProfile;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedUpdateProfile);
                                break;
                            }
                        default:
                            {
                                outputData.code = (int)Enums.StatusCode.FailedUpdateProfile;
                                outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedUpdateProfile);
                                break;
                            }
                    }
                }
                else
                {
                    outputData.code = (int)Enums.StatusCode.FailedUpdateProfile;
                    outputData.description = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedUpdateProfile);

                }
            }

            return outputData;
        }
    }
}