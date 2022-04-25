using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Mvc;

namespace Dost.Models
{
    public class NFC
    {
    }
    public class NFCProfileModel
    {
        public List<NFCContent> NfcContentList { get; set; }
        public string ContentSocialMedia { get; set; }
        public string ContentLocation { get; set; }
        public string ContentWhatsapp { get; set; }
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
       public string Skill { get; set; }
        [AllowHtml]
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
        public string[] LocationList { get; set; }
        public string[] WhatsappList { get; set; }
        public string[] ContactList { get; set; }
        public string[] EmailList { get; set; }
        public string[] WebLinkList { get; set; }
        public string[] SocialLink { get; set; }
        public string[] HideContacts { get; set; }
        public DataTable dtcontact { get; set; }
        public DataTable dtemail { get; set; }
        public DataTable dtweblink { get; set; }
        public DataTable dtsocial { get; set; }
        public DataTable dtwhatsapp { get; set; }
        public DataTable dtlocation { get; set; }
        public string ActiveStatus { get; set; }
        public string ColorCodeContact { get; set; }
        public string ColorCodeRedirection { get; set; }
        public string LogId { get; set; }
        public string DecryptedCode { get; set; }
        public List<UserSkill> lstSkill { get; set; }
        public List<ListSkill> lstTo { get; set; }
        public List<UserLanguage> lstLanguage { get; set; }
        public List<UserAchievement> lstAchievement { get; set; }
        public string BannerImage { get; set; }
        public string PrimaryNumber { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimarySocial { get; set; }
        public string PrimaryWebLink { get; set; }
        public bool IsPrimaryNumber { get; set; }
        public bool IsPrimaryEmail { get; set; }
        public bool IsPrimarySocial { get; set; }
        public bool IsPrimaryWebLink { get; set; }
        public bool IsDislpayValue { get; set; }
        public int IsPrimary { get; set; }
        public string IsDisplay { get; set; }
        public string Body { get; set; }
        [AllowHtml]
        public string EmailBodyHTML { get; set; }
        public string enc { get; set; }
        public DataSet SaveAboutMe()
        {
            SqlParameter[] para ={
                new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                new SqlParameter("@FK_UserId",FK_UserId),
                new SqlParameter("@Description",EmailBodyHTML)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveAboutMe", para);
            return ds;
        }
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
                new SqlParameter ("@Fk_UerId",PK_UserId),
                 new SqlParameter ("@NFCCode",DecryptedCode),
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
        public DataSet GetWhatsappList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetWhatsappList", para);
            return ds;
        }
        public DataSet GetLocationList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLocationList", para);
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
                new SqlParameter ("@NFCCode",DecryptedCode),
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
        public DataSet GetAnalysis()
        {
            SqlParameter[] para ={
                  new SqlParameter ("@FK_UserId",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAnalysis", para);
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
                  new SqlParameter("@dtsocial",dtsocial),
                   new SqlParameter("@dtwhatsapp",dtwhatsapp),
                  new SqlParameter("@dtlocation",dtlocation)

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
        public DataSet UpdateBannerImage()
        {
            SqlParameter[] para ={
                new SqlParameter ("@FK_UserId",FK_UserId),
                new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                new SqlParameter("@BannerImage",BannerImage)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBannerImage", para);
            return ds;
        }
        public DataSet SetPrimaryNumber()
        {
            SqlParameter[] para ={
                new SqlParameter ("@FK_UserId",FK_UserId),
                new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                new SqlParameter("@FK_NFCProfileId",Pk_NfcProfileId),
                new SqlParameter("@IsChecked",IsPrimaryNumber),
                 new SqlParameter("@IsWhatsapp",IsWhatsapp),
                    new SqlParameter("@IsDisplay",IsDislpayValue)
            };
            DataSet ds = DBHelper.ExecuteQuery("SetPrimaryNumber", para);
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
        public string IsPrimary { get; set; }
        public bool IsDisplay { get; set; }
    }
    public class NFCData
    {
        public List<NFCData> lst { get; set; }

        public string Name { get; set; }
        public string LoginId { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public string AddedBy { get; set; }
        public string FK_DistributerId { get; set; }
        public bool IsCodeAlloted { get; set; }
        public string PK_NFCId { get; set; }
        public string NFCStatus { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public string DisplayName { get; set; }
        public string ActivatedOn { get; set; }
        public string AllotedOn { get; set; }
        public string Quantity { get; set; }
        public string Count { get; set; }
        public DataSet GetNFCDataList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetNFCCodeForAllotment");
            return ds;
        }
        public DataSet NFCAllotment()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@PK_NFCId",PK_NFCId),
                new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@Code",Code),
                new SqlParameter("@Count",Count),
                new SqlParameter("@Quantity",Quantity),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("NFCAllotment", para);
            return ds;
        }
        public DataSet DebitAmountFromWallet()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@LoginId",LoginId),
                 new SqlParameter("@Quantity",Quantity),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DebitAmountFromWallet", para);
            return ds;
        }

        public DataSet CancelNFCAllotment()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@PK_NFCId",PK_NFCId),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("CancelNFCAllotment", para);
            return ds;
        }
        public DataSet GetAlloteNFC()
        {
            SqlParameter[] para =
          {
                new SqlParameter("@DistributorId",FK_DistributerId),
                new SqlParameter("@Code",Code)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetNFCDataForAllotmentForAdmin", para);
            return ds;
        }
        public DataSet GetUsedNFC()
        {
            SqlParameter[] para =
          {
                new SqlParameter("@DistributorId",FK_DistributerId),
                new SqlParameter("@Code",Code)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetUsedNFC", para);
            return ds;
        }
        public DataSet GetUnusedNFC()
        {
            SqlParameter[] para =
          {
                new SqlParameter("@DistributorId",FK_DistributerId),
                new SqlParameter("@Code",Code)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetUnusedNFC", para);
            return ds;
        }
    }
    public class VCard
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Organization { get; set; }
        public string JobTitle { get; set; }
        public string Note { get; set; }
        public string StreetAddress { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string CountryName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string OfficialEmail { get; set; }
        public string PersonalEmail { get; set; }
        public string Designation { get; set; }
        public string BusinessName { get; set; }
        public string ProfileName { get; set; }
        public string HomePage { get; set; }
        public List<string> WebLinks { get; set; }
        public List<string> Emails { get; set; }
        public List<string> Contacts { get; set; }
        public List<string> OtherContacts { get; set; }
        public List<string> SocialLinks { get; set; }
        public byte[] Image { get; set; }
        public override string ToString()
        {
            var builder = new StringBuilder();
 
            builder.AppendLine("BEGIN:VCARD");
            builder.AppendLine("VERSION:2.1");
            // Name 
            //builder.AppendLine("N:" + LastName + ";" + FirstName);
            // Full name 
            builder.AppendLine("N:" + FirstName + " " + LastName);
            builder.AppendLine("FN:" + FirstName + " " + LastName);
            //builder.AppendLine("TITLE:" + "");
            if (!string.IsNullOrEmpty(Note))
                builder.AppendLine("NOTE:" + Note);
            // Address  
            //builder.Append("ADR;HOME;PREF:;;");
            //builder.Append(StreetAddress + ";");
            //builder.Append(City + ";;");
            //builder.Append(Zip + ";");
            //builder.AppendLine(CountryName);
            // Other data 
            if (!string.IsNullOrEmpty(ProfileName))
                builder.AppendLine("Profile:" + ProfileName);
            if (!string.IsNullOrEmpty(BusinessName))
                builder.AppendLine("ORG:" + BusinessName);
            if (!string.IsNullOrEmpty(Designation))
                builder.AppendLine("TITLE:" + Designation);

            if (!string.IsNullOrEmpty(Mobile))
                builder.AppendLine("TEL;TYPE=WORK:" + Mobile);
            if (!string.IsNullOrEmpty(Mobile))
                builder.AppendLine("TEL;TYPE=HOME:" + Mobile);
            if (!string.IsNullOrEmpty(OfficialEmail))
                builder.AppendLine("EMAIL;TYPE=Official:" + OfficialEmail);
            if (!string.IsNullOrEmpty(PersonalEmail))
                builder.AppendLine("EMAIL;TYPE=Personal:" + PersonalEmail);


            if (WebLinks != null && WebLinks.Count > 0)
            {
                foreach (var url in WebLinks)
                {
                    builder.AppendLine("URL:" + url);
                }
            }

            if (SocialLinks != null && SocialLinks.Count > 0)
            {
                foreach (var url in SocialLinks)
                {
                    builder.AppendLine("URL:" + url);
                }
            }
            if (Contacts != null && Contacts.Count > 0)
            {
                foreach (var url in Contacts)
                {
                    builder.AppendLine("TEL:" + url);
                }
            }
            if (OtherContacts != null && OtherContacts.Count > 0)
            {
                foreach (var url in OtherContacts)
                {
                    builder.AppendLine("TEL:" + url);
                }
            }
            if (Contacts != null && Contacts.Count > 0)
            {
                foreach (var url in Contacts)
                {
                    builder.AppendLine("TEL:" + url);
                }
            }
            if (Emails != null && Emails.Count > 0)
            {
                foreach (var url in Emails)
                {
                    builder.AppendLine("Email:" + url);
                }
            }
            // Add image
            if (Image != null)
            {
                builder.AppendLine("PHOTO;ENCODING=b:" + Convert.ToBase64String(Image));
            }

            //End
            builder.AppendLine("END:VCARD");
            return builder.ToString();
        }
    }
    public class ListSkill
    {
        public string Skill { get; set; }
    }
    public class UserSkill
    {
        public string Response { get; set; }
        public string Message { get; set; }
        public string Pk_SkillId { get; set; }
        public string Skill { get; set; }
        public string FK_UserId { get; set; }
        public string PK_BusinessProfileId { get; set; }
        public DataSet SaveSkill()
        {
            SqlParameter[] para ={
                  new SqlParameter ("@FK_UserId",FK_UserId),
                  new SqlParameter ("@Skill",Skill),
                  new SqlParameter ("@PK_BusinessProfileId",PK_BusinessProfileId),
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveSkill", para);
            return ds;
        }
    }
    public class UserLanguage
    {
        public string Response { get; set; }
        public string Message { get; set; }
        public string Pk_LanguageId { get; set; }
        public string Language { get; set; }
        public string FK_UserId { get; set; }
        public string PK_BusinessProfileId { get; set; }
        public DataSet SaveLanguage()
        {
            SqlParameter[] para ={
                  new SqlParameter ("@FK_UserId",FK_UserId),
                  new SqlParameter ("@Language",Language),
                  new SqlParameter ("@PK_BusinessProfileId",PK_BusinessProfileId),
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveLanguage", para);
            return ds;
        }
    }
    public class UserAchievement
    {
        public string Response { get; set; }
        public string Message { get; set; }
        public string Pk_AchievementId { get; set; }
        public string Achievement { get; set; }
        public string FK_UserId { get; set; }
        public string PK_BusinessProfileId { get; set; }
        public DataSet SaveAchievement()
        {
            SqlParameter[] para ={
                  new SqlParameter ("@FK_UserId",FK_UserId),
                  new SqlParameter ("@Achievement",Achievement),
                  new SqlParameter ("@PK_BusinessProfileId",PK_BusinessProfileId),
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveAchievement", para);
            return ds;
        }
        public DataSet DeleteAchievement()
        {
            SqlParameter[] para ={
                  new SqlParameter ("@FK_UserId",FK_UserId),
                  new SqlParameter ("@Pk_AchievementId",Pk_AchievementId),
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteAchievement", para);
            return ds;
        }
    }
}