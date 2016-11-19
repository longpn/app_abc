using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dicho.Models
{
    public class Enums
    {
        /// <summary>
        /// Status when validates a token from client side
        /// </summary>
        public enum TokenValidation
        {
            Valid,
            SuspendedAccount,
            Invalid
        }

        /// <summary>
        /// User role
        /// </summary>
        public class UserRole
        {
            public const string User = "User";
            public const string Admin = "Admin";
            public const string Business = "Business";
        }

        public enum NotificationType
        {
            /// <summary>
            /// Notify when the voice has been successfully created
            /// </summary>
            CreatedVoice = 57000,

            ///// <summary>
            ///// Notify to seller when buyer request
            ///// </summary>
            //BuyingRequest = 57001
        }

        /// <summary>
        /// Defines all the status code of API body
        /// </summary>
        public enum StatusCode
        {
            /// <summary>
            /// 
            /// </summary>
            Successful = 50001,
            /// <summary>
            /// The user account need to verify to complete the process register new account
            /// </summary>
            NeedVerificationUserAccount = 50002,

            /// <summary>
            /// The user account has already been registered
            /// </summary>
            AlreadyUserAccount = 50003,

            /// <summary>
            /// The account has been registered unsuccessfully
            /// </summary>
            FaliedRegisterUserAccount = 50004,

            /// <summary>
            /// The password is incorrect
            /// </summary>
            IncorrectPassword = 50005,

            /// <summary>
            /// The account does not exist
            /// </summary>
            NotExistedAccount = 50006,

            /// <summary>
            /// The device notification has unsuccessfully updated.
            /// </summary>
            FailedUpdateDeviceNotification = 50007,

            /// <summary>
            /// The email address verification is unsuccessfully
            /// </summary>
            FaliedVeriftEmailAddress = 50008,

            /// <summary>
            /// The email address code is invalid
            /// </summary>
            IncorrectCode = 50009,

            /// <summary>
            /// Sign in with facebook account is unsuccessful
            /// </summary>
            FailedSignInWithFacebook = 50010,


            /// <summary>
            /// You has already been linked with facebook account
            /// </summary>
            AlreadyLinkedFacebookAccount = 50011,

            /// <summary>
            /// The facebook account has been linked by another account
            /// </summary>
            ExistedFacebookAccount = 50012,

            /// <summary>
            /// The facebook account has been unsuccessfully linked.
            /// </summary>
            FaliedLinkFacebookAccount = 50013,

            /// <summary>
            /// Get user'profile is unsuccessful
            /// </summary>
            FailedGetUserProfile = 50014,

            /// <summary>
            /// The business has been unsuccessfully created.
            /// </summary>
            FailedCreatingBusiness = 50015,

            /// <summary>
            /// The categoryIDs format is invalid
            /// </summary>
            WrongFormatCategoryID = 50016,

            /// <summary>
            /// The business name has been existed
            /// </summary>
            ExistedBusinessName = 50017,

            /// <summary>
            /// The use's profile has been unsuccessfully updated.
            /// </summary>
            FailedUpdateProfile = 50018,


            /// <summary>
            /// The bill has been unsuccessfully uped
            /// </summary>
            FailedUpBill = 50019,

            /// <summary>
            /// The file type are unsupported
            /// </summary>
            UnsupportedFileType = 50020,


            /// <summary>
            /// The voice file attachment has reached a limitation
            /// </summary>
            LimitedVoiceFileAttachment = 50021,

            /// <summary>
            /// Need to verify the forgot password
            /// </summary>
            NeedVerificationForgotPassword = 50022,


            /// <summary>
            /// The forgot password has been unsuccessfully submitted
            /// </summary>
            FailedForgotPassword = 50023,

            /// <summary>
            /// The user are unsuccessful logged
            /// </summary>
            FailedSignInLog = 50024,

            /// <summary>
            /// The voice are unsuccessful got
            /// </summary>
            FailedGetVoiceDetail = 50025,

            /// <summary>
            /// The voice does not exist
            /// </summary>
            NotExistedVoice = 50026,


            /// <summary>
            /// The client error has been unsuccessfully logged
            /// </summary>
            FailedLogClientError = 50027,

            /// <summary>
            /// The voice has been unsuccessfully edited
            /// </summary>
            FailedEditVoice = 50028,

            /// <summary>
            /// The user is not owner's voice 
            /// </summary>
            NotOwnerVoice = 50029,

            /// <summary>
            /// The voice is processing  
            /// </summary>
            VoiceProcessing = 50030,

            /// <summary>
            /// The voice is resolved
            /// </summary>
            VoiceResolved = 50031,

            /// <summary>
            /// The voice is closed
            /// </summary>
            VoiceClosed = 50032,

            /// <summary>
            /// The account has already been activated
            /// </summary>
            ActivatedAccount = 50033,

            /// <summary>
            /// The forgot password has been unsuccessfully verified
            /// </summary>
            FailedVerifyForgotPassword = 50034,


            /// <summary>
            /// The request reset password does not exist or was verified
            /// </summary>
            NotExistedForgotPassword = 50035,

            /// <summary>
            /// The forgot password code is incorrect
            /// </summary>
            IncorrectForgotPasswordCode = 50036,

            /// <summary>
            /// The comment has been unsuccessfully added
            /// </summary>
            FailedAddComment = 50037,

            /// <summary>
            /// The like has been unsuccessfully added
            /// </summary>
            FailedAddLike = 50038,

            /// <summary>
            /// The time upload bill has been unsuccessfully expired
            /// </summary>
            ExpiredUploadBill = 50039,
        }


        public enum MetaStatusCode
        {
            Ok = 200,

            /// <summary>
            /// The parameter(s) are invalid
            /// </summary>
            BadRequest = 400,

            /// <summary>
            /// The application is required to upgrade
            /// </summary>
            UpgradeRequired = 426

        }
    }
}