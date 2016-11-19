using dicho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Utilities
{
    public class MessageHelper
    {
        /// <summary>
        /// Describes the status code
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static string GetStatusDecription(Enums.StatusCode status)
        {
            string message = string.Empty;
            switch (status)
            {
                case Enums.StatusCode.Successful: //50001
                    {
                        message = "Successful";
                        break;
                    }
                case Enums.StatusCode.NeedVerificationUserAccount: //50002
                    {
                        message = "Need to verify the account";
                        break;
                    }
                case Enums.StatusCode.AlreadyUserAccount: //50003
                    {
                        message = "The account has already been registered";
                        break;
                    }
                case Enums.StatusCode.FaliedRegisterUserAccount: //50004
                    {
                        message = "The account has registered a failure";
                        break;
                    }
                case Enums.StatusCode.IncorrectPassword: //50005
                    {
                        message = "The password is incorrect";
                        break;
                    }
                case Enums.StatusCode.NotExistedAccount: //50006
                    {
                        message = "The account does not exist";
                        break;
                    }
                case Enums.StatusCode.FailedUpdateDeviceNotification: //50007
                    {
                        message = "The device notification has unsuccessfully updated";
                        break;
                    }
                case Enums.StatusCode.FaliedVeriftEmailAddress: //50008
                    {
                        message = "The email address has unsuccessfully verified";
                        break;
                    }
                case Enums.StatusCode.IncorrectCode: //50009
                    {
                        message = "The email address code is incorrect";
                        break;
                    }
                case Enums.StatusCode.FailedSignInWithFacebook: //50010
                    {
                        message = "Sign in with facebook account is unsuccessful";
                        break;
                    }
                case Enums.StatusCode.AlreadyLinkedFacebookAccount: //50011
                    {
                        message = "You has already been linked with facebook account";
                        break;
                    }
                case Enums.StatusCode.ExistedFacebookAccount: //50012
                    {
                        message = "The facebook account has been linked by another account";
                        break;
                    }
                case Enums.StatusCode.FaliedLinkFacebookAccount: //50013
                    {
                        message = "The facebook account has been unsuccessfully linked.";
                        break;
                    }
                case Enums.StatusCode.FailedGetUserProfile: //50014
                    {
                        message = "Get user's profile is unsuccessful";
                        break;
                    }
                case Enums.StatusCode.FailedCreatingBusiness: //50015
                    {
                        message = "The business has been unsuccessfully created";
                        break;
                    }
                case Enums.StatusCode.WrongFormatCategoryID: //50016
                    {
                        message = "The categoryIDs format is invalid";
                        break;
                    }
                case Enums.StatusCode.ExistedBusinessName: //50017
                    {
                        message = "The business name has been existed";
                        break;
                    }
                case Enums.StatusCode.FailedUpdateProfile: //50018
                    {
                        message = "The profile has been unsuccessfully updated";
                        break;
                    }
                case Enums.StatusCode.FailedUpBill: //50019
                    {
                        message = "The bill has been unsuccessfully uploaded";
                        break;
                    }
                case Enums.StatusCode.UnsupportedFileType: //50020
                    {
                        message = "The file type are unsupported";
                        break;
                    }
                case Enums.StatusCode.LimitedVoiceFileAttachment: //50021
                    {
                        message = "The voice file attachment has reached a limitation";
                        break;
                    }
                case Enums.StatusCode.NeedVerificationForgotPassword: //50022
                    {
                        message = "Need to verify the forgot password";
                        break;
                    }
                case Enums.StatusCode.FailedForgotPassword: //50023
                    {
                        message = "The forgot password has been unsuccessfully reseted";
                        break;
                    }
                case Enums.StatusCode.FailedSignInLog: //50024
                    {
                        message = "The user are unsuccessful logged";
                        break;
                    }
                case Enums.StatusCode.FailedGetVoiceDetail: //50025
                    {
                        message = "The voice are unsuccessful got";
                        break;
                    }
                case Enums.StatusCode.NotExistedVoice: //50026
                    {
                        message = "The voice does not exist";
                        break;
                    }
                case Enums.StatusCode.FailedLogClientError: //50027
                    {
                        message = "The client error has been unsuccessfully logged";
                        break;
                    }

                case Enums.StatusCode.FailedEditVoice: //50028
                    {
                        message = "The voice has been unsuccessfully edited";
                        break;
                    }
                case Enums.StatusCode.NotOwnerVoice: //50029
                    {
                        message = "TThe user is not owner's voice ";
                        break;
                    }
                case Enums.StatusCode.VoiceProcessing: //50030
                    {
                        message = "The voice is processing";
                        break;
                    }
                case Enums.StatusCode.VoiceResolved: //50031
                    {
                        message = "The voice is resolved";
                        break;
                    }
                case Enums.StatusCode.VoiceClosed: //50032
                    {
                        message = "The voice is closed";
                        break;
                    }
                case Enums.StatusCode.ActivatedAccount: //50033
                    {
                        message = "The account has already been activated";
                        break;
                    }
                case Enums.StatusCode.FailedVerifyForgotPassword: // 50034
                    {
                        message = "The forgot password has been unsuccessfully verified";
                        break;
                    }
                case Enums.StatusCode.NotExistedForgotPassword: // 50035
                    {
                        message = "The request reset password does not exist or was verified";
                        break;
                    }
                case Enums.StatusCode.IncorrectForgotPasswordCode: // 50036
                    {
                        message = "The forgot password code is incorrect";
                        break;
                    }
                case Enums.StatusCode.FailedAddComment: // 50037
                    {
                        message = "The comment has been unsuccessfully added";
                        break;
                    }
                case Enums.StatusCode.FailedAddLike: // 50038
                    {
                        message = "The like has been unsuccessfully added";
                        break;
                    }
                case Enums.StatusCode.ExpiredUploadBill: // 50039
                    {
                        message = "The time upload bill has been unsuccessfully expired";
                        break;
                    }

                default:
                    break;

            }

            return message;
        }



        /// <summary>
        /// Describes the status code of meta api
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetMetaStatusDescription(Enums.MetaStatusCode code)
        {
            string message = string.Empty;
            switch (code)
            {
                case Enums.MetaStatusCode.Ok: // 200
                    {
                        message = "Ok";
                        break;
                    }
                case Enums.MetaStatusCode.BadRequest: // 400
                    {
                        message = "The parameter(s) is invalid";
                        break;
                    }
                case Enums.MetaStatusCode.UpgradeRequired: // 426
                    {
                        message = "The application is required to upgrade";
                        break;
                    }
                default:
                    break;
            }
            return message;
        }

    }
}