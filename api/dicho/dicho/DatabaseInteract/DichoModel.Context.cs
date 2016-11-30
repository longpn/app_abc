﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dicho.DatabaseInteract
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DichoDataContext : DbContext
    {
        public DichoDataContext()
            : base("name=DichoDataContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual ObjectResult<proc_ClientAppInfor_GetAllToken_Result> proc_ClientAppInfor_GetAllToken()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_ClientAppInfor_GetAllToken_Result>("proc_ClientAppInfor_GetAllToken");
        }
    
        public virtual ObjectResult<string> proc_ClientAppInfor_Insert(Nullable<long> userID, string deviceFirmwareID, string clientAppID, string token)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var deviceFirmwareIDParameter = deviceFirmwareID != null ?
                new ObjectParameter("DeviceFirmwareID", deviceFirmwareID) :
                new ObjectParameter("DeviceFirmwareID", typeof(string));
    
            var clientAppIDParameter = clientAppID != null ?
                new ObjectParameter("ClientAppID", clientAppID) :
                new ObjectParameter("ClientAppID", typeof(string));
    
            var tokenParameter = token != null ?
                new ObjectParameter("Token", token) :
                new ObjectParameter("Token", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_ClientAppInfor_Insert", userIDParameter, deviceFirmwareIDParameter, clientAppIDParameter, tokenParameter);
        }
    
        public virtual int proc_ApiLog_Insert(Nullable<long> userID, string apiName, string apiVersion, string iPAddress, string platform, string userAgent, string clientAppVersion, Nullable<int> httpStatusCode, Nullable<int> contentLength, Nullable<int> takenTime)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var apiNameParameter = apiName != null ?
                new ObjectParameter("ApiName", apiName) :
                new ObjectParameter("ApiName", typeof(string));
    
            var apiVersionParameter = apiVersion != null ?
                new ObjectParameter("ApiVersion", apiVersion) :
                new ObjectParameter("ApiVersion", typeof(string));
    
            var iPAddressParameter = iPAddress != null ?
                new ObjectParameter("IPAddress", iPAddress) :
                new ObjectParameter("IPAddress", typeof(string));
    
            var platformParameter = platform != null ?
                new ObjectParameter("Platform", platform) :
                new ObjectParameter("Platform", typeof(string));
    
            var userAgentParameter = userAgent != null ?
                new ObjectParameter("UserAgent", userAgent) :
                new ObjectParameter("UserAgent", typeof(string));
    
            var clientAppVersionParameter = clientAppVersion != null ?
                new ObjectParameter("ClientAppVersion", clientAppVersion) :
                new ObjectParameter("ClientAppVersion", typeof(string));
    
            var httpStatusCodeParameter = httpStatusCode.HasValue ?
                new ObjectParameter("HttpStatusCode", httpStatusCode) :
                new ObjectParameter("HttpStatusCode", typeof(int));
    
            var contentLengthParameter = contentLength.HasValue ?
                new ObjectParameter("ContentLength", contentLength) :
                new ObjectParameter("ContentLength", typeof(int));
    
            var takenTimeParameter = takenTime.HasValue ?
                new ObjectParameter("TakenTime", takenTime) :
                new ObjectParameter("TakenTime", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_ApiLog_Insert", userIDParameter, apiNameParameter, apiVersionParameter, iPAddressParameter, platformParameter, userAgentParameter, clientAppVersionParameter, httpStatusCodeParameter, contentLengthParameter, takenTimeParameter);
        }
    
        public virtual ObjectResult<string> proc_ClientError_Insert(Nullable<long> userID, string errorMessage, string platform)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var errorMessageParameter = errorMessage != null ?
                new ObjectParameter("ErrorMessage", errorMessage) :
                new ObjectParameter("ErrorMessage", typeof(string));
    
            var platformParameter = platform != null ?
                new ObjectParameter("Platform", platform) :
                new ObjectParameter("Platform", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_ClientError_Insert", userIDParameter, errorMessageParameter, platformParameter);
        }
    
        public virtual int proc_ServerError_Insert(Nullable<long> userID, string apiName, string uri, string platform, string errorMessage, Nullable<int> statusCode, string clientAppVersion)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var apiNameParameter = apiName != null ?
                new ObjectParameter("ApiName", apiName) :
                new ObjectParameter("ApiName", typeof(string));
    
            var uriParameter = uri != null ?
                new ObjectParameter("Uri", uri) :
                new ObjectParameter("Uri", typeof(string));
    
            var platformParameter = platform != null ?
                new ObjectParameter("Platform", platform) :
                new ObjectParameter("Platform", typeof(string));
    
            var errorMessageParameter = errorMessage != null ?
                new ObjectParameter("ErrorMessage", errorMessage) :
                new ObjectParameter("ErrorMessage", typeof(string));
    
            var statusCodeParameter = statusCode.HasValue ?
                new ObjectParameter("StatusCode", statusCode) :
                new ObjectParameter("StatusCode", typeof(int));
    
            var clientAppVersionParameter = clientAppVersion != null ?
                new ObjectParameter("ClientAppVersion", clientAppVersion) :
                new ObjectParameter("ClientAppVersion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_ServerError_Insert", userIDParameter, apiNameParameter, uriParameter, platformParameter, errorMessageParameter, statusCodeParameter, clientAppVersionParameter);
        }
    
        public virtual ObjectResult<proc_User_Profile_Result> proc_User_Profile(Nullable<long> userID)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_User_Profile_Result>("proc_User_Profile", userIDParameter);
        }
    
        public virtual ObjectResult<proc_User_RegisterWithSocialNetworkAccount_Result> proc_User_RegisterWithSocialNetworkAccount(string socialID, string socialToken, string socialEmailAccount, string socialFirstName, string socialLastName, string socialType, string gender, string avatarUrl, string deviceFirmwareID, string appID)
        {
            var socialIDParameter = socialID != null ?
                new ObjectParameter("SocialID", socialID) :
                new ObjectParameter("SocialID", typeof(string));
    
            var socialTokenParameter = socialToken != null ?
                new ObjectParameter("SocialToken", socialToken) :
                new ObjectParameter("SocialToken", typeof(string));
    
            var socialEmailAccountParameter = socialEmailAccount != null ?
                new ObjectParameter("SocialEmailAccount", socialEmailAccount) :
                new ObjectParameter("SocialEmailAccount", typeof(string));
    
            var socialFirstNameParameter = socialFirstName != null ?
                new ObjectParameter("SocialFirstName", socialFirstName) :
                new ObjectParameter("SocialFirstName", typeof(string));
    
            var socialLastNameParameter = socialLastName != null ?
                new ObjectParameter("SocialLastName", socialLastName) :
                new ObjectParameter("SocialLastName", typeof(string));
    
            var socialTypeParameter = socialType != null ?
                new ObjectParameter("SocialType", socialType) :
                new ObjectParameter("SocialType", typeof(string));
    
            var genderParameter = gender != null ?
                new ObjectParameter("Gender", gender) :
                new ObjectParameter("Gender", typeof(string));
    
            var avatarUrlParameter = avatarUrl != null ?
                new ObjectParameter("AvatarUrl", avatarUrl) :
                new ObjectParameter("AvatarUrl", typeof(string));
    
            var deviceFirmwareIDParameter = deviceFirmwareID != null ?
                new ObjectParameter("DeviceFirmwareID", deviceFirmwareID) :
                new ObjectParameter("DeviceFirmwareID", typeof(string));
    
            var appIDParameter = appID != null ?
                new ObjectParameter("AppID", appID) :
                new ObjectParameter("AppID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_User_RegisterWithSocialNetworkAccount_Result>("proc_User_RegisterWithSocialNetworkAccount", socialIDParameter, socialTokenParameter, socialEmailAccountParameter, socialFirstNameParameter, socialLastNameParameter, socialTypeParameter, genderParameter, avatarUrlParameter, deviceFirmwareIDParameter, appIDParameter);
        }
    
        public virtual ObjectResult<proc_User_SignInWithFacebookAccount_Result> proc_User_SignInWithFacebookAccount(string facebookID, string facebookToken)
        {
            var facebookIDParameter = facebookID != null ?
                new ObjectParameter("FacebookID", facebookID) :
                new ObjectParameter("FacebookID", typeof(string));
    
            var facebookTokenParameter = facebookToken != null ?
                new ObjectParameter("FacebookToken", facebookToken) :
                new ObjectParameter("FacebookToken", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_User_SignInWithFacebookAccount_Result>("proc_User_SignInWithFacebookAccount", facebookIDParameter, facebookTokenParameter);
        }
    
        public virtual ObjectResult<string> proc_User_UpdateProfile(Nullable<long> userID, string fullName, string address, string avatar, string emailAddress, string gender, Nullable<System.DateTime> dOB, string phoneNumber, string zaloId, Nullable<int> countryId, Nullable<int> cityId, Nullable<int> discId, Nullable<int> numberMember, Nullable<int> numberChild, Nullable<bool> isVegan)
        {
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(long));
    
            var fullNameParameter = fullName != null ?
                new ObjectParameter("FullName", fullName) :
                new ObjectParameter("FullName", typeof(string));
    
            var addressParameter = address != null ?
                new ObjectParameter("Address", address) :
                new ObjectParameter("Address", typeof(string));
    
            var avatarParameter = avatar != null ?
                new ObjectParameter("Avatar", avatar) :
                new ObjectParameter("Avatar", typeof(string));
    
            var emailAddressParameter = emailAddress != null ?
                new ObjectParameter("EmailAddress", emailAddress) :
                new ObjectParameter("EmailAddress", typeof(string));
    
            var genderParameter = gender != null ?
                new ObjectParameter("Gender", gender) :
                new ObjectParameter("Gender", typeof(string));
    
            var dOBParameter = dOB.HasValue ?
                new ObjectParameter("DOB", dOB) :
                new ObjectParameter("DOB", typeof(System.DateTime));
    
            var phoneNumberParameter = phoneNumber != null ?
                new ObjectParameter("PhoneNumber", phoneNumber) :
                new ObjectParameter("PhoneNumber", typeof(string));
    
            var zaloIdParameter = zaloId != null ?
                new ObjectParameter("ZaloId", zaloId) :
                new ObjectParameter("ZaloId", typeof(string));
    
            var countryIdParameter = countryId.HasValue ?
                new ObjectParameter("CountryId", countryId) :
                new ObjectParameter("CountryId", typeof(int));
    
            var cityIdParameter = cityId.HasValue ?
                new ObjectParameter("CityId", cityId) :
                new ObjectParameter("CityId", typeof(int));
    
            var discIdParameter = discId.HasValue ?
                new ObjectParameter("DiscId", discId) :
                new ObjectParameter("DiscId", typeof(int));
    
            var numberMemberParameter = numberMember.HasValue ?
                new ObjectParameter("NumberMember", numberMember) :
                new ObjectParameter("NumberMember", typeof(int));
    
            var numberChildParameter = numberChild.HasValue ?
                new ObjectParameter("NumberChild", numberChild) :
                new ObjectParameter("NumberChild", typeof(int));
    
            var isVeganParameter = isVegan.HasValue ?
                new ObjectParameter("IsVegan", isVegan) :
                new ObjectParameter("IsVegan", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_User_UpdateProfile", userIDParameter, fullNameParameter, addressParameter, avatarParameter, emailAddressParameter, genderParameter, dOBParameter, phoneNumberParameter, zaloIdParameter, countryIdParameter, cityIdParameter, discIdParameter, numberMemberParameter, numberChildParameter, isVeganParameter);
        }
    
        public virtual ObjectResult<proc_User_RegisterWithEmailAddress_Result> proc_User_RegisterWithEmailAddress(string userName, string password, string fullName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            var fullNameParameter = fullName != null ?
                new ObjectParameter("FullName", fullName) :
                new ObjectParameter("FullName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_User_RegisterWithEmailAddress_Result>("proc_User_RegisterWithEmailAddress", userNameParameter, passwordParameter, fullNameParameter);
        }
    
        public virtual ObjectResult<proc_User_SignInWithEmailAddress_Result> proc_User_SignInWithEmailAddress(string userName, string password)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            var passwordParameter = password != null ?
                new ObjectParameter("Password", password) :
                new ObjectParameter("Password", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_User_SignInWithEmailAddress_Result>("proc_User_SignInWithEmailAddress", userNameParameter, passwordParameter);
        }
    
        public virtual ObjectResult<string> proc_category_delete(Nullable<int> category_id, string category_status)
        {
            var category_idParameter = category_id.HasValue ?
                new ObjectParameter("category_id", category_id) :
                new ObjectParameter("category_id", typeof(int));
    
            var category_statusParameter = category_status != null ?
                new ObjectParameter("category_status", category_status) :
                new ObjectParameter("category_status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_category_delete", category_idParameter, category_statusParameter);
        }
    
        public virtual int proc_Category_Insert(Nullable<int> type, Nullable<int> sort_order, string name, string description, string icon)
        {
            var typeParameter = type.HasValue ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(int));
    
            var sort_orderParameter = sort_order.HasValue ?
                new ObjectParameter("sort_order", sort_order) :
                new ObjectParameter("sort_order", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("description", description) :
                new ObjectParameter("description", typeof(string));
    
            var iconParameter = icon != null ?
                new ObjectParameter("icon", icon) :
                new ObjectParameter("icon", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("proc_Category_Insert", typeParameter, sort_orderParameter, nameParameter, descriptionParameter, iconParameter);
        }
    
        public virtual ObjectResult<string> proc_category_update(Nullable<long> category_id, Nullable<int> type, Nullable<int> sort_order, string name, string description, string icon, string category_status)
        {
            var category_idParameter = category_id.HasValue ?
                new ObjectParameter("category_id", category_id) :
                new ObjectParameter("category_id", typeof(long));
    
            var typeParameter = type.HasValue ?
                new ObjectParameter("type", type) :
                new ObjectParameter("type", typeof(int));
    
            var sort_orderParameter = sort_order.HasValue ?
                new ObjectParameter("sort_order", sort_order) :
                new ObjectParameter("sort_order", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("name", name) :
                new ObjectParameter("name", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("description", description) :
                new ObjectParameter("description", typeof(string));
    
            var iconParameter = icon != null ?
                new ObjectParameter("icon", icon) :
                new ObjectParameter("icon", typeof(string));
    
            var category_statusParameter = category_status != null ?
                new ObjectParameter("category_status", category_status) :
                new ObjectParameter("category_status", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("proc_category_update", category_idParameter, typeParameter, sort_orderParameter, nameParameter, descriptionParameter, iconParameter, category_statusParameter);
        }
    
        public virtual ObjectResult<proc_Category_Get_Result> proc_Category_Get(Nullable<int> type_id)
        {
            var type_idParameter = type_id.HasValue ?
                new ObjectParameter("type_id", type_id) :
                new ObjectParameter("type_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<proc_Category_Get_Result>("proc_Category_Get", type_idParameter);
        }
    }
}
