using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dost.Models
{
    public class Profile:Common
    {
        #region Property
        public string AdharNo { get; set; }
        public string UserCode { get; set; }
        public string Summary { get; set; }
        public string DCMI { get; set; }
        public string ProfilePic { get; set; }
        public string EncryptLoginID { get; set; }
        public string EncryptPayoutNo { get; set; }
        public string RealtionName { get; set; }
        public string LoginId { get; set; }
        public string ProductID { get; set; }
        public string Gender { get; set; }
        public string Relation { get; set; }
        public string PK_UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string JoiningDate { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public List<Profile> profilelst { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string IFSC { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        public string LeadershipBonus { get; set; }
        public string AccountHolder { get; set; }
        public string Pin { get; set; }
        public string OTP { get; set; }
        public string ConfirmPin { get; set; }
        public bool IsCreatedPin { get; set; }
        public string OldPin { get; set; }
        public HttpPostedFileBase postedFile { get; set; }
        public string UPI { get; set; }
        public string Image { get; set; }
        public List<Profile> lst { get; set; }
        public string Status { get; set; }
        public string AdharNumber { get; set; }
        public string AdharImage { get; set; }
        public string AdharStatus { get; set; }
        public string PanNumber { get; set; }
        public string PanImage { get; set; }
        public string PanStatus { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentImage { get; set; }
        public string DocumentStatus { get; set; }
        public string MemberAccNo { get; set; }
        public string IFSCCode { get; set; }
        public string MemberBankName { get; set; }
        public string MemberBranch { get; set; }
        public string KYCNumber { get; set; }
        public string KYCStatus { get; set; }
        public string KYCAttachment { get; set; }

        public string UPIStatus { get; set; }
        public string Paytm { get; set; }
        public string PaytmStatus { get; set; }
        public string PhonePe { get; set; }
        public string PhonePeStatus { get; set; }
        public string GooglePay { get; set; }
        public string GooglePayStatus { get; set; }

        #endregion
        public DataSet GetUserProfile()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId) };
            DataSet ds = DBHelper.ExecuteQuery("UserProfile", para);
            return ds;
        }
        public DataSet GetAllUserProfile()
        {
            DataSet ds = DBHelper.ExecuteQuery("UserProfileAll");
            return ds;
        }
        public DataSet UpdatePersonalInfo()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserID",PK_UserID ) ,
                                      new SqlParameter("@FirstName", FirstName) ,
                                       new SqlParameter("@MiddleName", MiddleName) ,
                                      new SqlParameter("@LastName", LastName) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Email", EmailId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdatePersonalInfo", para);
            return ds;
        }
        public DataSet UpdateBankInfo()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserID",PK_UserID ) ,
                                      new SqlParameter("@IFSC", IFSC) ,
                                       new SqlParameter("@BankName", BankName) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                      new SqlParameter("@AccountNumber", AccountNumber) ,
                                      new SqlParameter("@AccountHolderName", AccountHolderName),
                                      new SqlParameter("@DocumentImage",DocumentImage)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("Updatebankdetails", para);
            return ds;
        }
        public DataSet GetKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetKYCDocuments", para);
            return ds;
        }
        public DataSet UploadKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",Fk_UserId ) ,
                                      new SqlParameter("@AdharNumber", AdharNo) ,
                                      new SqlParameter("@AdharImage", AdharImage) ,
                                      new SqlParameter("@PanNumber", PanNumber),
                                      new SqlParameter("@PanImage", PanImage)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UploadKYC", para);
            return ds;
        }
        public DataSet UpdateWalletDetails()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",Fk_UserId ) ,
                                      new SqlParameter("@UPI", UPI) ,
                                       new SqlParameter("@Paytm", Paytm) ,
                                      new SqlParameter("@PhonePe", PhonePe) ,
                                      new SqlParameter("@GooglePay", GooglePay)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateWalletDetails", para);
            return ds;
        }
    }
}