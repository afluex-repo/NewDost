using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Dost.Models
{
    public class NFC
    {
    }
    public class NFCProfileModel
    {
        public List<NFCContent> NfcContentList { get; set; }
        public string ContentSocialMedia { get; set; }
        public string SocialMedia { get; set; }
        public string IsAcceptanceTNC { get; set; }
        public string Status { get; set; }
        public int Pk_NfcProfileId { get; set; }
        public string WebLink { get; set; }
        public string ContentWebLinks { get; set; }
        public string ContentEmail { get; set; }
        public int Id { get; set; }
        public string Flag { get; set; }
        public bool IsWhatsapp { get; set; }
        public bool IsRedirectWeb { get; set; }
        public bool IsRedirectSocial { get; set; }
        public bool IsRedirect { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PK_UserId { get; set; }
        public string PK_ProfileId { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string ProfilePic { get; set; }
        public string FirstName { get; set; }
        public string ProfileName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string Summary { get; set; }
        public List<NFCProfileModel> lst { get; set; }
        public string UserCode { get; set; }
        public string Leg { get; set; }
        public string FK_UserId { get; set; }
        public string Result { get; set; }
        public string PinCode { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Message { get; set; }
        public HttpPostedFileBase fileUploaderControl { get; set; }
        public bool IsProfileTurnedOff { get; set; }
        public string BusinessName { get; set; }
        public string BusinessContact { get; set; }
        public string BusinessMail { get; set; }
        public string Description { get; set; }
        public string About { get; set; }
        public bool IsIncluded { get; set; }
        public bool IsIncludedContact { get; set; }
        public bool IsIncludedEmail { get; set; }
        public bool IsIncludedWeb { get; set; }
        public bool IsIncludedSocial { get; set; }
        public string ProfileType { get; set; }
        public string CardImage { get; set; }
        public string Remarks { get; set; }
        public string[] ContactList { get; set; }
        public string[] EmailList { get; set; }
        public string[] WebLinkList { get; set; }
        public string[] SocialLink { get; set; }
        public DataTable dtcontact { get; set; }
        public DataTable dtemail { get; set; }
        public DataTable dtweblink { get; set; }
        public DataTable dtsocial { get; set; }
        public string ActiveStatus { get; set; }
        public string ColorCode { get; set; }
        public DataSet UpdateProfilePic()
        {
            SqlParameter[] para ={
                new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                new SqlParameter("@ProfilePic",ProfilePic)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfilePicForNFC", para);
            return ds;
        }
        public DataSet UpdateRedirectionUrl()
        {
            SqlParameter[] para ={
                   new SqlParameter ("@Pk_NfcProfileId",Pk_NfcProfileId),
                new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                 new SqlParameter ("@IsCheked",IsIncluded),
                  new SqlParameter ("@AddedBy",FK_UserId),
            };

            DataSet ds = DBHelper.ExecuteQuery("RedirectWebLink", para);
            return ds;
        }
        public DataSet GetNFCProfileData()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UerId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetNFCProfileData_New", para);
            return ds;
        }
        public DataSet GetBusinessProfileById()
        {
            SqlParameter[] para ={
                new SqlParameter ("@PK_ProfileId",PK_ProfileId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBusinessProfileById", para);
            return ds;
        }
        public DataSet GetmailList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetEmailList", para);
            return ds;
        }
        public DataSet GetContactList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetContactList", para);
            return ds;
        }
        public DataSet GetSocialMediaList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetSocialMediaList", para);
            return ds;
        }
        public DataSet GetWebLinkList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetWebLinkList", para);
            return ds;
        }
        public DataSet InsertBusinessInfo()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                new SqlParameter ("@FirstName",FirstName),
                new SqlParameter ("@LastName",LastName),
                new SqlParameter ("@Designation",Designation),
                new SqlParameter ("@Leg",Leg),
                 new SqlParameter ("@BusinessName",BusinessName),
                 new SqlParameter ("@Description",Description),
                 new SqlParameter ("@Status",Status),
                 new SqlParameter ("@ProfilePic",ProfilePic),
            };
            DataSet ds = DBHelper.ExecuteQuery("InsertBusinessInfo", para);
            return ds;
        }
        public DataSet InsertProfileName()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                new SqlParameter ("@ProfileName",ProfileName),
                new SqlParameter ("@ProfileType",ProfileType),
            };
            DataSet ds = DBHelper.ExecuteQuery("InsertProfileName", para);
            return ds;
        }
        public DataSet SaveUpdateContactNoNfc()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                new SqlParameter ("@Content",Content),
                 new SqlParameter ("@Iswhatsaap",IsWhatsapp),
                  new SqlParameter ("@IsInclude",IsIncluded),
                  new SqlParameter ("@IsRedirect",IsRedirect),
                new SqlParameter ("@Type",Type),
                 new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("AddUpdateContactNo", para);
            return ds;
        }
        public DataSet UpdateNfc()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Pk_NfcProfileId",Pk_NfcProfileId),
                new SqlParameter ("@Content",Content),
                 new SqlParameter ("@Iswhatsaap",IsWhatsapp),
                new SqlParameter ("@Type",Type),
                 new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateNFCUserProfile", para);
            return ds;
        }
        public DataSet GetFfcContactDetails()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetFfcContactDetails", para);
            return ds;
        }
        public DataSet GetLegData()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                 new SqlParameter ("@PK_ProfileId",PK_ProfileId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLegData", para);
            return ds;
        }
        public DataSet GetProfileDataForNFC()
        {
            SqlParameter[] para ={
                 new SqlParameter ("@FK_ProfileId",PK_ProfileId),
                new SqlParameter ("@Fk_UserId",PK_UserId),
                new SqlParameter ("@Type",Type),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetProfileDataForNFC", para);
            return ds;
        }
        public DataSet DeleteNFCContact()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Pk_NfcProfileId",Pk_NfcProfileId),
                 new SqlParameter ("@ContactNo",Content),
                  new SqlParameter ("@DeletedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteNFCContact", para);
            return ds;
        }
        public DataSet RedirectLink(bool IsChecked)
        {
            SqlParameter[] para ={
                new SqlParameter ("@Pk_NfcProfileId",Pk_NfcProfileId),
                new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                 new SqlParameter ("@IsCheked",IsChecked),
                  new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("RedirectWebLink", para);
            return ds;
        }
        public DataSet UpdateIncludedStatus(bool IsIncluded)
        {
            SqlParameter[] para ={
                new SqlParameter ("@Pk_NfcProfileId",Pk_NfcProfileId),
                new SqlParameter ("@FK_ProfileId",PK_ProfileId),
                 new SqlParameter ("@IsIncluded",IsIncluded),
                  new SqlParameter ("@Fk_UserId",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateIncludedStatus", para);
            return ds;
        }
        public DataSet EditWhatsappNo(bool IsChecked)
        {
            SqlParameter[] para ={
                new SqlParameter ("@Pk_NfcProfileId",Pk_NfcProfileId),
                 new SqlParameter ("@IsCheked",IsChecked),
                  new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("EditWhatsappNo", para);
            return ds;
        }
        public DataSet UpdateProfileStatus(bool IsChecked)
        {
            SqlParameter[] para ={
                new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                 new SqlParameter ("@IsCheked",IsChecked),
                  new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfileStatus", para);
            return ds;
        }
        public DataSet DeleteNFCProfileData()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Pk_NfcProfileId",Pk_NfcProfileId),
                  new SqlParameter ("@DeletedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteNFCProfileData", para);
            return ds;
        }
        public DataSet UpdateBusinessInfo()
        {
            SqlParameter[] para ={
                  new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                   new SqlParameter ("@ProfileName",ProfileName),
                  new SqlParameter ("@FirstName",FirstName),
                  new SqlParameter ("@LastName",LastName),
                  new SqlParameter ("@BusinessName",BusinessName),
                   new SqlParameter ("@Designation",Designation),
                   new SqlParameter ("@Description",Description),
                  new SqlParameter ("@ProfilePic",ProfilePic),
                  new SqlParameter ("@Leg",Leg),
                  new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBusinessInfo", para);
            return ds;
        }
        public DataSet UpdateProfileOnOffStatus()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),

                new SqlParameter ("@IsProfileTurnedOff",IsProfileTurnedOff)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfileOnOffStatus", para);
            return ds;
        }
        public DataSet UpdateProfileInfo()
        {
            SqlParameter[] para ={
                  new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                  new SqlParameter ("@FK_UserId",FK_UserId),
                  new SqlParameter ("@FirstName",FirstName),
                  new SqlParameter ("@LastName",LastName),
                  new SqlParameter ("@BusinessName",BusinessName),
                   new SqlParameter ("@Designation",Designation),
                   new SqlParameter ("@DOB",DOB),
                  new SqlParameter ("@Description",Description),
                  new SqlParameter ("@Gender",Gender),
                  new SqlParameter ("@AddedBy",FK_UserId),
                  new SqlParameter("@dtcontact",dtcontact),
                  new SqlParameter("@dtemail",dtemail),
                  new SqlParameter("@dtweblink",dtweblink),
                  new SqlParameter("@dtsocial",dtsocial)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBusinessInfoNew", para);
            return ds;
        }
        public DataSet GetUrlForRedirection()
        {
            SqlParameter[] para ={
                  new SqlParameter ("@FK_UserId",FK_UserId),
                  new SqlParameter ("@Type",Type)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetUrlForRedirection", para);
            return ds;
        }
     
    }
    public class NFCContent
    {
        public string Content { get; set; }
        public string Type { get; set; }
        public string IsWhatsApp { get; set; }
        public string PK_NFCProfileId { get; set; }
        public string IsIncluded { get; set; }
        public string IsRedirect { get; set; }
    }
}