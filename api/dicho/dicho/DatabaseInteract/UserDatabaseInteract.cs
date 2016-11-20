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
                var item = db.proc_User_SignInWithFacebookAccount(value.FacebookID, value.FacebookToken).FirstOrDefault();
                if (item != null)
                {
                    switch (item.Status)
                    {
                        case "Successful":
                            {
                                outputData.StatusCode = (int)Enums.StatusCode.Successful;
                                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.Successful);

                                SignInWithEmailAddressOutputData signIn = new SignInWithEmailAddressOutputData();
                                signIn.user_id = item.user_id;
                                signIn.username = item.username;
                                signIn.fullname = item.full_name;
                                signIn.role = item.role;
                                signIn.avatar = item.avatar;
                                //signIn.AvatarUrl = AzureStorageHelper.GetAvatarUrl(signIn.UserID, item.Avatar);
                                signIn.client_app_id = TokenManager.GenerateClientAppID();
                                signIn.access_token = TokenManager.GenerateAccessToken(signIn.user_id, value.DeviceFirmwareID, signIn.client_app_id);

                                outputData.Data = signIn;
                                return outputData;
                            }
                    }
                }
            }


            // Get information from facebook account
            FacebookInfoInputData facebookInfor = FacebookHelper.GetFacebookAccountInfor(value.FacebookToken);

            // Register With facebook account
            using (DichoDataContext db = new DichoDataContext())
            {
                var item = db.proc_User_RegisterWithSocialNetworkAccount(value.FacebookID, value.FacebookToken, facebookInfor.FacebookAccount,
                    facebookInfor.FirstName, facebookInfor.LastName, "Facebook", facebookInfor.Gender, "", value.DeviceFirmwareID, value.AppID).FirstOrDefault();
                if (item != null)
                {
                    switch (item.Status)
                    {
                        case "Successful":
                            {
                                outputData.StatusCode = (int)Enums.StatusCode.Successful;
                                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.Successful);

                                SignInWithEmailAddressOutputData signIn = new SignInWithEmailAddressOutputData();
                                signIn.user_id = item.user_id;
                                signIn.username = item.username;
                                signIn.fullname = item.full_name;
                                signIn.role = item.role;
                                signIn.avatar = item.avatar;
                                //signIn.AvatarUrl = AzureStorageHelper.GetAvatarUrl(signIn.UserID, item.Avatar);
                                signIn.client_app_id = TokenManager.GenerateClientAppID();
                                signIn.access_token = TokenManager.GenerateAccessToken(signIn.user_id, value.DeviceFirmwareID, signIn.client_app_id);

                                outputData.Data = signIn;
                                break;
                            }
                        default:
                            {
                                outputData.StatusCode = (int)Enums.StatusCode.FailedSignInWithFacebook;
                                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedSignInWithFacebook);
                                break;
                            }

                    }
                }
                else
                {
                    outputData.StatusCode = (int)Enums.StatusCode.FailedSignInWithFacebook;
                    outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedSignInWithFacebook);
                }
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
                                outputData.StatusCode = (int)Enums.StatusCode.Successful;
                                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.Successful);


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
                                outputData.StatusCode = (int)Enums.StatusCode.NotExistedAccount;
                                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.NotExistedAccount);
                                break;
                            }
                        default:
                            {
                                outputData.StatusCode = (int)Enums.StatusCode.FailedGetUserProfile;
                                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedGetUserProfile);
                                break;
                            }
                    }
                }
                else
                {

                    outputData.StatusCode = (int)Enums.StatusCode.FailedGetUserProfile;
                    outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedGetUserProfile);

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
                                outputData.StatusCode = (int)Enums.StatusCode.Successful;
                                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.Successful);
                                if (!string.IsNullOrEmpty(value.avatar))
                                {
                                    //Moves the temp file to container of user
                                    // AzureStorageHelper.MoveTempFileToUserContainer(userID, value.AvatarUrl);
                                }
                                break;
                            }
                        case "NotExisted":
                            {
                                outputData.StatusCode = (int)Enums.StatusCode.NotExistedAccount;
                                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.NotExistedAccount);
                                break;
                            }
                        case "Failed":
                            {
                                outputData.StatusCode = (int)Enums.StatusCode.FailedUpdateProfile;
                                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedUpdateProfile);
                                break;
                            }
                        default:
                            {
                                outputData.StatusCode = (int)Enums.StatusCode.FailedUpdateProfile;
                                outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedUpdateProfile);
                                break;
                            }
                    }
                }
                else
                {
                    outputData.StatusCode = (int)Enums.StatusCode.FailedUpdateProfile;
                    outputData.StatusDescription = MessageHelper.GetStatusDecription(Enums.StatusCode.FailedUpdateProfile);

                }
            }

            return outputData;
        }
    }
}