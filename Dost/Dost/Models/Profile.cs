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
        #region create new key (RJ)

         public string Email { get; set; }
          public string FatherName { get; set; }
      // public string PinCode { get; set; }
        public String  DOB { get; set; }
        public string Address { get; set; }
        public string NameH { get; set; }
        public string FatherNameH { get; set; }
        public string AddressH { get; set; }
        public string CardNumber { get; set; }
        public string DOB_ID { get; set; }
        public string First_NM { get; set; }
        public string Pan_Number { get; set; }
        public string PinCode { get;  set; }
        public string AadharImage { get;  set; }
        #endregion
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
        public DataSet UpdateProfilePic()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserID",PK_UserID ) ,
                                      new SqlParameter("@ProfilePic", ProfilePicture)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfilePic", para);
            return ds;
        }

        public DataSet GetCardDetail()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserId",Fk_UserId),
           /* // new SqlParameter("@FK_CardTypeId", "1")*/ };
            DataSet ds = DBHelper.ExecuteQuery("SP_GetCardDetail", para);
            return ds;
        }

        #region update card userprofile
        public DataSet Updatecarduserprofile()
        {
            SqlParameter[] para ={  
                                      new SqlParameter("@FirstName", FirstName) ,
                                       new SqlParameter("@FatherName", FatherName) ,
                                      new SqlParameter("@DOB",DOB) ,
                                      new SqlParameter("@Gender", Gender) ,
                                      new SqlParameter("@Mobile", Mobile),
                                        new SqlParameter("@Email", Email) ,
                                        new SqlParameter("@Pincode", Pincode),
                                       new SqlParameter("@Address", Address) ,
                                      new SqlParameter("@NameH", NameH) ,
                                     // new SqlParameter("@FatherNameH", FatherNameH) ,
                                      new SqlParameter("@AddressH", AddressH),
                                      new SqlParameter("@FatherNameH", FatherNameH) ,
                                      new SqlParameter("@FK_UserId", Fk_UserId ),
                                      new SqlParameter("@CardNumber", CardNumber)
                                      
                                  };
            DataSet ds = DBHelper.ExecuteQuery("SP_UpdateCardDetail", para);
            return ds;
        }

        #endregion
        public DataSet ApproveUPIRequest()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", PK_UserID),
                                  new SqlParameter("@Status", Status),
              new SqlParameter("@UpdatedBy", AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("ApproveUPIRequest", para);
            return ds;
        }
        #region pancard update
        public DataSet GetCardPanDetail()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserId",Fk_UserId),
           /* // new SqlParameter("@FK_CardTypeId", "1")*/ };
            DataSet ds = DBHelper.ExecuteQuery("SP_GetPANDetail", para);
            return ds;
        }
        #endregion


        #region update pancard userprofile
        public DataSet UpdatePan_carduserprofile()
        {
            SqlParameter[] para ={
                                      new SqlParameter("@FirstName",First_NM) ,
                                      new SqlParameter("@DOB",DOB_ID) ,
                                      new SqlParameter("@FK_UserId",Fk_UserId ),
                                      new SqlParameter("@CardNumber",Pan_Number)

                                  };
            DataSet ds = DBHelper.ExecuteQuery("SP_Update_PanCardDetail", para);
            return ds;
        }
        public DataSet UpdateAssociateProfileByAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID",LoginId) ,
                                      new SqlParameter("@FirstName", FirstName) ,
                                      new SqlParameter("@LastName", LastName) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Email", EmailId) ,
                                      new SqlParameter("@AccountNo", AccountNumber) ,
                                      new SqlParameter("@BankName", BankName) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                      new SqlParameter("@IFSC", IFSC),
                                      new SqlParameter("@UpdatedBy", UpdatedBy) ,
                                      new SqlParameter("@SponsorId", SponsorId),
                                        new SqlParameter("@PanNumber", PanNumber) ,
                                         new SqlParameter("@RealtionName", RealtionName) ,
                                          new SqlParameter("@Relation", Relation) ,
                                           new SqlParameter("@Address", Address) ,
                                            new SqlParameter("@State", State) ,
                                             new SqlParameter("@City", City) ,
                                             new SqlParameter("@Gender", Gender) ,
                                              new SqlParameter("@PinCode", PinCode),
                                               new SqlParameter("@BankHolderName", AccountHolder),
                                                new SqlParameter("@AdharNo", AdharNo),
                                                new SqlParameter("@Password",Password),
                                                new SqlParameter("@PanImage", PanImage),
                                                new SqlParameter("@AdharImage", AadharImage),
                                                new SqlParameter("@DocumentImage", DocumentImage),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateAssociateProfileByAdmin", para);
            return ds;
        }
        public DataSet BlockAssociate()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                  new SqlParameter("@BlockedBy", UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("BlockAssociate", para);
            return ds;
        }

        public DataSet UnblockAssociate()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                  new SqlParameter("@BlockedBy", UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("UnblockAssociate", para);
            return ds;
        }
        public DataSet GetAdminProfile()
        {
            SqlParameter[] para = { new SqlParameter("@PK_AdminID", PK_UserID) };
            DataSet ds = DBHelper.ExecuteQuery("GetAdminProfile", para);
            return ds;
        }

        public DataSet UpdateAdminProfile()
        {
            SqlParameter[] para = { new SqlParameter("@PK_AdminID", UpdatedBy),
                                  new SqlParameter("@Name", FirstName)};
            DataSet ds = DBHelper.ExecuteQuery("UpdateAdminProfile", para);
            return ds;
        }
        public DataSet DeactivateUserByAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID", LoginId),
                                   new SqlParameter("@UpdatedBy", UpdatedBy) };
            DataSet ds = DBHelper.ExecuteQuery("DeactivateUser", para);
            return ds;
        }
        public DataSet ActivateUserByAdmin()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID", Fk_UserId),
                                    new SqlParameter("@FK_ProductID", ProductID),
                                    new SqlParameter("@UpdatedBy", UpdatedBy)};
            DataSet ds = DBHelper.ExecuteQuery("ActivateUserByAdmin", para);
            return ds;
        }
        #endregion
    }
}