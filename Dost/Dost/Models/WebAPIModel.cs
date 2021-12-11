using Dost.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace Dost.Models
{
    public class WebAPIModel
    {

    }
    public class CheckMobileModel
    {
        public string MobileNo { get; set; }
        public string DeviceId { get; set; }

        public DataSet CheckMobileNo(string OTP)
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@OTP",OTP),
                new SqlParameter("@DeviceId",DeviceId)
            };
            DataSet ds = DBHelper.ExecuteQuery("CheckMobileNo", para);
            return ds;
        }
    }


    public class CheckOTPModel
    {
        public string MobileNo { get; set; }
        public string OTP { get; set; }

        public DataSet CheckOTP()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@OTP",OTP)
            };
            DataSet ds = DBHelper.ExecuteQuery("CheckOTP", para);
            return ds;
        }
    }

    public class GenerateOTPModel
    {
        public string MobileNo { get; set; }

        public DataSet GenerateOTP(string OTP)
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@OTP",OTP)
            };
            DataSet ds = DBHelper.ExecuteQuery("GenerateOTP", para);
            return ds;
        }
    }
    public class ShoppingDashBoardData
    {


        public List<BannerDetails> lstbanner { get; set; }

        public List<MainCategoryDetailsForDash> lstmaincategory { get; set; }

        public List<OtherDetails> lstotherdetails { get; set; }

        public string Status { get; set; }


        public DataSet GetDashBoardDataForMobile()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDataForMobile");
            return ds;
        }



    }
    public class Response
    {
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
    }
    public class Upload
    {
        public string Fk_UserId { get; set; }
        public string ImageURL { get; set; }

        public DataSet UpdateImage()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserID",Fk_UserId ) ,
                                      new SqlParameter("@ProfilePic", ImageURL),
                                   //   new SqlParameter("@ProfilePic", ProfilePicture)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateImage", para);
            return ds;
        }

    }
    public class MainCategoryDetailsForDash
    {
        public string PK_MainCategoryID { get; set; }
        public string MainCategoryName { get; set; }
        public string Image { get; set; }
        public List<OtherData> ProductList { get; set; }
    }
    public class OtherDetails
    {
        public string Name { get; set; }
        public string Pk_Id { get; set; }
        public string Image { get; set; }
    }
    public class OtherData
    {
        public string Pk_ProductId { get; set; }
        public string ProductName { get; set; }
        public string MRP { get; set; }
        public string OfferedPrice { get; set; }
        public string Images { get; set; }
    }
    public class RegistrationModel
    {
        public string SponsorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public DateTime DOB { get; set; }
        //set this value as android if device android ios if iphone or web in case of web browsers
        //public string RegistrationBy { get; set; }
        public string Gender { get; set; }
        public string Pincode { get; set; }
        //generate and set from backend
        public string Password { get; set; }

        public DataSet Registration()
        {
            SqlParameter[] para = {
                new SqlParameter("@SponsorId",SponsorId),
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@FirstName",FirstName),
                new SqlParameter("@LastName",LastName),
                //new SqlParameter("@RegistrationBy",RegistrationBy),
                new SqlParameter("@DOB",DOB),
                new SqlParameter("@Gender",Gender),
                new SqlParameter("@PinCode",Pincode),
                new SqlParameter("@Leg",""),
                new SqlParameter("@Password",Password)
            };
            DataSet ds = DBHelper.ExecuteQuery("Registration_New_Mobile", para);
            return ds;
        }
    }
    public class LoginModel
    {
        public string MobileNo { get; set; }
        public string Password { get; set; }
        public string DeviceId { get; set; }

        public DataSet Login()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@DeviceId",DeviceId),
                new SqlParameter("@Password",Password)
            };
            DataSet ds = DBHelper.ExecuteQuery("Login_Mobile", para);
            return ds;
        }

    }

    public class ForgetPasswordModel
    {
        public string MobileNo { get; set; }

        public DataSet ForgetPassword(string TempPassword)
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@OTP",TempPassword)
            };
            DataSet ds = DBHelper.ExecuteQuery("ForgetPassword", para);
            return ds;
        }
        public DataSet GetMobilenumber()
        {
            SqlParameter[] para = {
                                new SqlParameter("@Mobile_No", MobileNo)

            };

            DataSet ds = DBHelper.ExecuteQuery("GetMobileNumber", para);
            return ds;
        }
    }
    public class GetProductImage
    {
        public string Fk_ProductId { get; set; }
        public List<List> List { get; set; }
        public DataSet ProductImageList()
        {
            SqlParameter[] para =
              {

                                         new SqlParameter("@Fk_ProductId",Fk_ProductId)
            };
            DataSet ds = DBHelper.ExecuteQuery("ProductImageList", para);
            return ds;

        }
    }
    public class List
    {
        public string ProductImage { get; set; }
        public string Fk_ProductId { get; set; }
        public string Fk_ProductImageid { get; set; }
    }
    public class ResetPasswordModel
    {
        public string MobileNo { get; set; }
        public string TempPassword { get; set; }
        public string Password { get; set; }

        public DataSet ResetPassword()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@TempPassword",TempPassword),
                new SqlParameter("@NewPassword",Password)
            };
            DataSet ds = DBHelper.ExecuteQuery("ResetPassword", para);
            return ds;
        }
    }
    public class UnsuseCoupon
    {
        public string CouponCode { get; set; }
        public int CategoryCode { get; set; }
        public string Fk_UserId { get; set; }
        public DataSet GetUsedUnUsedcouponsAssociate()
        {
            SqlParameter[] para = {

                                        new SqlParameter("@CouponCode", CouponCode),
                                        new SqlParameter("@Pk_CategoryId", CategoryCode),
                                        new SqlParameter("@UserId", Fk_UserId),
                                        //new SqlParameter("@OwnerID", OwnerID ),
                                        //new SqlParameter("@OwnrName",OwnerName ),
                                        //new SqlParameter("@RegToId", RegisteredTo ),
                                        //new SqlParameter("@RegToName",RegisteredToName ),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetUnusedUsedCouponsAssociate", para);
            return ds;
        }
    }
    public class WalletResponse
    {

        public string CouponCode { get; set; }
        public string CategoryName { get; set; }
        public string CreatedDate { get; set; }
        public string CuopnStatus { get; set; }
        public string CouponPrice { get; set; }
        public string EventName { get; set; }
        public List<WalletResponse> lstunusedpins { get; set; }
    }
    public class UseCoupon
    {
        public string CouponCode { get; set; }
        public string CategoryCode { get; set; }
        public string Fk_UserId { get; set; }
        public string Status { get; set; }
        public List<WalletResponseUse> lstunusedpins { get; set; }
        public DataSet GetUsedcouponsAssociate()
        {
            SqlParameter[] para = {

                                        new SqlParameter("@CouponCode", CouponCode),
                                        new SqlParameter("@Pk_CategoryId", CategoryCode),
                                        new SqlParameter("@UserId", Fk_UserId),
                                        //new SqlParameter("@OwnerID", OwnerID ),
                                        //new SqlParameter("@OwnrName",OwnerName ),
                                        //new SqlParameter("@RegToId", RegisteredTo ),
                                        //new SqlParameter("@RegToName",RegisteredToName ),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetUsedCouponsAssociate", para);
            return ds;
        }
    }
    public class WalletResponseUse
    {

        public string CouponCode { get; set; }
        public string CategoryName { get; set; }
        public string CreatedDate { get; set; }
        public string CuopnStatus { get; set; }
        public string CouponPrice { get; set; }
        public string RegisteredTo { get; set; }
        public string EventName { get; set; }

    }

    public class TreeAPI
    {
        public string LoginId { get; set; }
        public string Fk_headId { get; set; }
        public DataSet GetTree()
        {
            SqlParameter[] para = {   new SqlParameter("@LoginId", LoginId),
                 new SqlParameter("@Fk_headId", Fk_headId)
                                  };

            DataSet ds = DBHelper.ExecuteQuery("GetTree", para);
            return ds;
        }
    }

    public class TreeAPIResponse
    {
        public string LoginId { get; set; }
        public string Fk_headId { get; set; }
        public List<Tree> GetGenelogy { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
    public class Tree
    {
        public string Fk_UserId { get; set; }
        public string SponsorId { get; set; }
        public string Fk_ParentId { get; set; }
        public string TeamPermanent { get; set; }
        public string LoginId { get; set; }
        public string Fk_SponsorId { get; set; }
        public string MemberName { get; set; }
        public string MemberLevel { get; set; }

        public string Id { get; set; }
        public string Leg { get; set; }

        public string ActivationDate { get; set; }
        public string ActiveLeft { get; set; }
        public string ActiveRight { get; set; }
        public string InactiveLeft { get; set; }
        public string InactiveRight { get; set; }
        public string BusinessLeft { get; set; }
        public string BusinessRight { get; set; }
        public string ImageURL { get; set; }
    }
    public class Distributor
    {
        public string YourName { get; set; }
        public string Designation { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string CompanySize { get; set; }
        public string GST { get; set; }
        public string PAN { get; set; }
        public string CompanyAddress { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Website { get; set; }
        public string AboutCompany { get; set; }
        public string Fk_UserId { get; set; }
        public DataSet SaveDistrubutorRecord()
        {
            SqlParameter[] para =
            {

                new SqlParameter("@YourName",YourName),
                new SqlParameter("@Designation",Designation),
                new SqlParameter("@CompanyName",CompanyName),
                new SqlParameter("@CompanyType",CompanyType),
                new SqlParameter("@CompanySize",CompanySize) ,
                 new SqlParameter("@GST",GST) ,
                  new SqlParameter("@PAN",PAN) ,
                   new SqlParameter("@CompanyAddress",CompanyAddress) ,
                    new SqlParameter("@Email",Email) ,
                     new SqlParameter("@Mobile",Mobile) ,
                       new SqlParameter("@Website",Website) ,
                         new SqlParameter("@AboutCompany",AboutCompany) ,
                          new SqlParameter("@Fk_UserId",Fk_UserId) ,
                 new SqlParameter("@AddedBy",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("AddDistributor", para);
            return ds;
        }


    }
    public class BannerDetails
    {
        internal List<BannerDetails> lstbanner;
        public string BannerImage { get; set; }
    }
    public class SubCategoryDetailsForDash
    {
        public string PK_SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string Image { get; set; }
    }


    public class DigiDashBoardData
    {
        public List<BannerDetails> lstbanner { get; set; }
        public string Status { get; set; }
        public DataSet GetDashBoardDataForMobile()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDataForMobile");
            return ds;
        }
    }
    public class EncKey
    {
        public static string KeyName = "ALMKOLKJ12345ANB";
        public static string ProfilePicURL = "https://dost.click";
    }
    public class DashBoardData
    {
        public string Pk_UserId { get; set; }
        public DataSet GetUserDashboardProfile()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@Fk_UserId",Pk_UserId),
                                   };
            DataSet ds = DBHelper.ExecuteQuery("GetBusinessDashboard", param);
            return ds;
        }
    }
    public class GetPassbook
    {
        public string Fk_UserId { get; set; }

        public DataSet GetPassBookBalance()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetPassbookdetails", para);
            return ds;
        }
    }
    public class WithdrawalMobile
    {
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string AddedBy { get; set; }

        public DataSet SaveWithDrawalRequest()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@AddedBy", AddedBy)

                                     };
            DataSet ds = DBHelper.ExecuteQuery("WithdrawlRequestMobile", para);
            return ds;
        }


    }
    public class DirectCommission
    {
        public string LoginId { get; set; }
        public string PageNo { get; set; }

        public DataSet GetDirectCommision()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@PageNo", PageNo)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectCommisionForMobile", para);
            return ds;
        }


    }
    public class DirectCommisionList
    {
        public string FromLoginId { get; set; }
        public string FromUserName { get; set; }
        public string Amount { get; set; }
        public string IncomeType { get; set; }
        public string Date { get; set; }
    }
    public class PaidCommision
    {
        public string LoginId { get; set; }
        public string PageNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public DataSet GetPaidCommission()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId",LoginId),
                 new SqlParameter("@FromDate",FromDate),
                  new SqlParameter("@ToDate",ToDate),
                  new SqlParameter("@PageNo",PageNo)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPaidCommisionForMobile", para);
            return ds;
        }
    }
    public class PaidCommisionList
    {
        public string Description { get; set; }
        public string PaymentDate { get; set; }
        public string TransactionDate { get; set; }
        public string TransactionNo { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
    }
    public class StaockStatement
    {
        public string Fk_UserId { get; set; }
        public string PageNo { get; set; }

        public DataSet GetStockStatement()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@PageNo", PageNo)

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetDDCoinWalletLedgerForMobile", para);
            return ds;
        }
    }
    public class StaockStatementList
    {
        public string Narration { get; set; }
        public string DrAmount { get; set; }
        public string CrAmount { get; set; }
        public string AddedOn { get; set; }
        public string EwalletBalance { get; set; }
    }
    public class FranchiseFund
    {
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string AddedBy { get; set; }
        public string PaymentMode { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string BankBranch { get; set; }
        public string DocumentImg { get; set; }
        public string BankName { get; set; }

        public DataSet SaveFranchiseeFund()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@PaymentMode", PaymentMode) ,
                                      new SqlParameter("@DDChequeNo", DDChequeNo) ,
                                      new SqlParameter("@DDChequeDate", DDChequeDate) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                      new SqlParameter("@DocumentImg", DocumentImg),
                                      new SqlParameter("@BankName", BankName),
                                       new SqlParameter("@AddedBy", AddedBy)

                                     };
            DataSet ds = DBHelper.ExecuteQuery("SaveFranchiseeFund", para);
            return ds;
        }


    }
    public class FranchiseStatement
    {
        public string Fk_UserId { get; set; }
        public string PageNo { get; set; }

        public DataSet GetFranchiseeWalletLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@PageNo", PageNo)

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetFranchiseeWalletLedgerForMobile", para);
            return ds;
        }
    }
    public class FranchiseStatementList
    {
        public string Narration { get; set; }
        public string DrAmount { get; set; }
        public string CrAmount { get; set; }
        public string AddedOn { get; set; }
        public string EwalletBalance { get; set; }
    }
    public class DistributorCommissionHistory
    {
        public string Fk_UserId { get; set; }
        public string PageNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DataSet getDistributorcommisionHistory()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@PageNo", PageNo),
                                        new SqlParameter("@FromDate", FromDate),
                                          new SqlParameter("@ToDate", ToDate)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("DistributorCommissionHistory", para);
            return ds;
        }
    }
    public class DistributorCommissionHistoryList
    {
        public string Narration { get; set; }
        public string Amount { get; set; }
        public string date { get; set; }
    }
    public class BusinessProfile
    {
        public string ProfileName { get; set; }
        public string ProfileType { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public string Designation { get; set; }
        public string FK_UserId { get; set; }
        public string Checked { get; set; }
        public bool IsActive { get; set; }
        public bool IsProfileTurnedOff { get; set; }
        public string Name { get; set; }
        public string PK_ProfileId { get; set; }
        public string ProfilePic { get; set; }


    }
    public class UpdateBusinessProfile
    {
        public string ProfileName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public string FK_UserId { get; set; }
        public string PK_ProfileId { get; set; }
        public string ProfilePic { get; set; }
        public string Status { get; set; }
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
                  new SqlParameter ("@AddedBy",FK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBusinessInfo", para);
            return ds;
        }
    }

    public class ProfileStatus
    {
        public string PK_ProfileId { get; set; }
        public bool IsActive { get; set; }
        public string PK_UserId { get; set; }
        public DataSet UpdateProfileStatus()
        {
            SqlParameter[] para ={
                new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                 new SqlParameter ("@IsCheked",IsActive),
                  new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfileStatus", para);
            return ds;
        }
    }
    public class Profile2
    {
        public string PK_ProfileId { get; set; }
        public bool IsActive { get; set; }
        public string PK_UserId { get; set; }
        public DataSet UpdateProfileStatus()
        {
            SqlParameter[] para ={
                new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                 new SqlParameter ("@IsCheked",IsActive),
                  new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfileStatus", para);
            return ds;
        }
    }
    public class Profile1
    {
        public string PK_ProfileId { get; set; }
        public DataSet GetBusinessProfileById()
        {
            SqlParameter[] para ={
                new SqlParameter ("@PK_ProfileId",PK_ProfileId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBusinessProfileById", para);
            return ds;
        }
    }
    public class Leg1
    {
        public string PK_ProfileId { get; set; }
        public string Leg { get; set; }
        public string PK_UserId { get; set; }
        public DataSet UpdateLeg()
        {
            SqlParameter[] para ={
                new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                 new SqlParameter ("@Leg",Leg),
                  new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBusinessInfo", para);
            return ds;
        }
    }
    public class NFCProfile
    {
        public string PK_UserId { get; set; }
        public string PK_ProfileId { get; set; }
        public DataSet GetNFCProfileData()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UerId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetNFCProfileData_NewMobile", para);
            return ds;
        }
        public DataSet GetFfcContactDetails()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                 new SqlParameter ("@FK_ProfileId",PK_ProfileId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetFfcContactDetailsMobile", para);
            return ds;
        }
        public DataSet GetBusinessInfo()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UerId",PK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBusinessInfoForMobile", para);
            return ds;
        }
        public DataSet GetRecentAnalytics()
        {
            SqlParameter[] para ={
                new SqlParameter ("@FK_UserId",PK_UserId)
            };

            DataSet ds = DBHelper.ExecuteQuery("GetRecentAnalytics", para);
            return ds;
        }
        public DataSet GetListForInvoice()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@Fk_UserId",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetListForInvoice", para);
            return ds;
        }
    }
    public class ContactList
    {
        public string Pk_NfcProfileId { get; set; }
        public string Contact { get; set; }
        public string Type { get; set; }
        public bool IsIncluded { get; set; }
        public bool IsWhatsup { get; set; }
        public string Fk_UserId { get; set; }
    }
    public class EmailList
    {
        public string Pk_NfcProfileId { get; set; }
        public string Email { get; set; }
        public bool IsIncluded { get; set; }
        public string Type { get; set; }
        public string Fk_UserId { get; set; }

    }
    public class WebLink
    {
        public string Pk_NfcProfileId { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public string Redirection { get; set; }
        public bool IsRedirect { get; set; }
        public bool IsIncluded { get; set; }
        public string Fk_UserId { get; set; }
    }
    public class SocialMediaLink
    {
        public string Pk_NfcProfileId { get; set; }
        public string Link { get; set; }
        public string Type { get; set; }
        public string Redirection { get; set; }
        public bool IsRedirect { get; set; }
        public bool IsIncluded { get; set; }
        public string Fk_UserId { get; set; }
    }
    public class ContactList1
    {
        public int Pk_NfcProfileId { get; set; }
        public int IsIncluded { get; set; }

    }
    public class EmailList1
    {
        public int Pk_NfcProfileId { get; set; }
        public int IsIncluded { get; set; }


    }
    public class WebLink1
    {
        public int Pk_NfcProfileId { get; set; }
        public int IsIncluded { get; set; }

    }
    public class SocialMediaLink1
    {
        public int Pk_NfcProfileId { get; set; }
        public int IsIncluded { get; set; }

    }
    public class UPdateNFCProfileModel
    {
        public int Pk_NfcProfileId { get; set; }
        public string Content { get; set; }
        public bool IsWhatsapp { get; set; }
        public bool IsRedirect { get; set; }
        public bool IsInclude { get; set; }
        public string Type { get; set; }
        public string PK_UserId { get; set; }
        public DataSet SaveUpdateContactNoNfc()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                new SqlParameter ("@Content",Content),
                new SqlParameter ("@Iswhatsaap",IsWhatsapp),
                new SqlParameter ("@IsRedirect",IsRedirect),
                new SqlParameter ("@IsInclude",IsInclude),
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
                 new SqlParameter ("@AddedBy",1),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateNFCUserProfile", para);
            return ds;
        }
    }
    public class DeleteNFC
    {
        public string Pk_NfcProfileId { get; set; }
        public string FK_UserId { get; set; }
        public DataSet DeleteNFCProfileData()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Pk_NfcProfileId",Pk_NfcProfileId),
                  new SqlParameter ("@DeletedBy",FK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteNFCProfileData", para);
            return ds;
        }
    }
    public class NFCEmail
    {
        public string Email { get; set; }
        public bool IsInclude { get; set; }
        public string PK_UserId { get; set; }
        public DataSet SaveUpdateContactNoNfc()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                new SqlParameter ("@Content",Email),
                 new SqlParameter ("@Type","Email"),
                new SqlParameter ("@IsInclude",IsInclude),
                 new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("AddUpdateContactNo", para);
            return ds;
        }
    }
    public class NFCContact
    {
        public string ContactNo { get; set; }
        public bool IsWhatsapp { get; set; }
        public bool IsInclude { get; set; }
        public string PK_UserId { get; set; }
        public DataSet SaveUpdateContactNoNfc()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                new SqlParameter ("@Type","ContactNo"),
                new SqlParameter ("@Content",ContactNo),
                  new SqlParameter ("@Iswhatsaap",IsWhatsapp),
                  new SqlParameter ("@IsInclude",IsInclude),
                 new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("AddUpdateContactNo", para);
            return ds;
        }
    }
    public class NFCWebLink
    {
        public string WebLink { get; set; }
        public bool IsRedirect { get; set; }
        public bool IsInclude { get; set; }
        public string PK_UserId { get; set; }
        public DataSet SaveUpdateContactNoNfc()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                new SqlParameter ("@Content",WebLink),
                new SqlParameter ("@Type","WebLink"),
                  new SqlParameter ("@IsRedirect",IsRedirect),
                  new SqlParameter ("@IsInclude",IsInclude),
                 new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("AddUpdateContactNo", para);
            return ds;
        }
    }
    public class NFCSocialMedia
    {
        public string SocialMedia { get; set; }
        public bool IsRedirect { get; set; }
        public bool IsInclude { get; set; }
        public string PK_UserId { get; set; }
        public DataSet SaveUpdateContactNoNfc()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                new SqlParameter ("@Content",SocialMedia),
                new SqlParameter ("@Type","SocialMedia"),
                  new SqlParameter ("@IsRedirect",IsRedirect),
                  new SqlParameter ("@IsInclude",IsInclude),
                 new SqlParameter ("@AddedBy",PK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("AddUpdateContactNo", para);
            return ds;
        }
    }
    public class UPdateNFCProfile
    {
        public string PK_UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePic { get; set; }
        public string Summary { get; set; }
        public string Leg { get; set; }


        public DataSet UpdateProfileInfo()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                new SqlParameter ("@FirstName",FirstName),
                 new SqlParameter ("@LastName",LastName),
                new SqlParameter ("@ProfilePic",ProfilePic),
                new SqlParameter ("@Summary",Summary),
                new SqlParameter ("@Leg",Leg),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBusinessInfo", para);
            return ds;
        }
    }
    public class BusinessInformation
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ProfilePic { get; set; }
        public string PK_UserId { get; set; }

        public DataSet InsertBusinessInfo()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),
                new SqlParameter ("@FirstName",FirstName),
                new SqlParameter ("@LastName",LastName),
                new SqlParameter ("@Designation",Designation),
                new SqlParameter ("@BusinessName",BusinessName),
                new SqlParameter ("@Description",Description),
                new SqlParameter ("@Status",Status),
                new SqlParameter ("@ProfilePic",ProfilePic),
            };
            DataSet ds = DBHelper.ExecuteQuery("InsertBusinessInfo", para);
            return ds;
        }
    }


    public class NFC2
    {
        public DataSet GetNFC()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetNFC");
            return ds;
        }
    }
    public class NFCList
    {
        public string PK_EventId { get; set; }
        public string EventName { get; set; }
        public string BinaryBV { get; set; }
        public string ReferalBV { get; set; }
        public string EventImage { get; set; }
        public string ProductPrice { get; set; }
        public string Discount { get; set; }

        public string Brand { get; set; }
        //public string Price { get; set; }
        //public string OfferPrice { get; set; }
        public string EventDescription { get; set; }
        public string ProductCode { get; set; }
        public string instock { get; set; }
    }
    public class MyNFC
    {
        public string PK_CategoryId { get; set; }
        public DataSet GetNFCReport()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_CategoryId",PK_CategoryId)

                                  };

            DataSet ds = DBHelper.ExecuteQuery("GetBookingEventDetailsByAdmin", para);
            return ds;
        }
    }
    public class MyNFCList
    {
        public string PK_EventId { get; set; }
        public string EventName { get; set; }
        public string Offername { get; set; }
        public string EventPrice { get; set; }
        public string EventImage { get; set; }
        public string OfferValidity { get; set; }
        public string CouponCode { get; set; }
        public string NoOfSeats { get; set; }
        public string Description { get; set; }
    }
    public class TransferCoupon
    {
        public string CouponCode { get; set; }
        public int CategoryCode { get; set; }
        public string Fk_UserId { get; set; }
        public DataSet GetcouponsAssociate()
        {
            SqlParameter[] para = {

                                        new SqlParameter("@CouponCode", CouponCode),
                                        new SqlParameter("@Pk_CategoryId", CategoryCode),
                                        new SqlParameter("@UserId", Fk_UserId),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetCouponsAssociateforMobile", para);
            return ds;
        }
    }
    public class TransferCouponList
    {
        public string CouponCode { get; set; }
        public string CategoryName { get; set; }
        public string CreatedDate { get; set; }
        public string CuopnStatus { get; set; }
        public string CouponPrice { get; set; }
        public string EventName { get; set; }
    }
    public class NFCDetailsPurchase
    {
        public int EventId { get; set; }
        public int PlanId { get; set; }
        public string Fk_EventId { get; set; }
        public string Fk_UserId { get; set; }
        public int WalletBalance { get; set; }
        public int FWalletBalance { get; set; }
        public bool FWalletShow { get; set; }
        public int? PK_EventId { get; set; }
        public string EventType { get; set; }
        public string Category { get; set; }
        public DataSet SubscriptionList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_EventId",Fk_EventId)
            };
            DataSet ds = DBHelper.ExecuteQuery("SubscriptionList", para);
            return ds;
        }
        public DataSet GetWalletMoney()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletMoney", para);
            return ds;
        }
        public DataSet GetUserAddressBook()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetUserAddressBook", para);
            return ds;
        }
        public DataSet CheckCategory()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_EventId",PK_EventId)
            };
            DataSet ds = DBHelper.ExecuteQuery("CheckCategoryList", para);
            return ds;
        }
        public DataSet EventList()

        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_EventId",PK_EventId)
            };
            DataSet ds = DBHelper.ExecuteQuery("EventList", para);
            return ds;
        }
    }
    public class NFCDetailsPurchase1
    {
        public int EventId { get; set; }
        public string Fk_EventId { get; set; }


    }
    public class EventDetailAPI
    {
        public string EventId { get; set; }
        public DataSet EventList()

        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_EventId",EventId)
            };
            DataSet ds = DBHelper.ExecuteQuery("EventList", para);
            return ds;
        }

    }
    public class NFCProduct
    {
        public string Pk_PlanId { get; set; }
        public string PlanName { get; set; }
        public string ProductPrice { get; set; }
        public string Validity { get; set; }
        public string PlanId { get; set; }
    }
    public class SaveNFC
    {
        public int NoOfSeats { get; set; }
        public int Pk_NfcProfileId { get; set; }
        public string TotalPrice { get; set; }
        public string PK_EventId { get; set; }
        public string PlanId { get; set; }
        public string ReferralBV { get; set; }
        public string TeamBV { get; set; }
        public string DeliveryCharge { get; set; }
        public string TotalPayable { get; set; }
        public string Gst { get; set; }
        public string Dwallet { get; set; }
        public string Fwallet { get; set; }
        public string Discount { get; set; }
        public string AddressId { get; set; }
        public string PinCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Address { get; set; }
        public string AddedBy { get; set; }
        public DataSet SaveEventDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_EventId",PK_EventId),
                new SqlParameter("@NoOfSeats",NoOfSeats),
                new SqlParameter("@TotalPrice",TotalPrice),
                new SqlParameter("@PINCode",PinCode),
                new SqlParameter("@State",State),
                new SqlParameter("@City",City),
                new SqlParameter("@Address",Address),
                new SqlParameter("@AddressId",AddressId),
                new SqlParameter("@PlanId",PlanId),
                new SqlParameter("@ReferralBV",ReferralBV),
                new SqlParameter("@TeamBV",TeamBV),
                new SqlParameter("@DeliveryCharge",DeliveryCharge),
                new SqlParameter("@Gst",Gst),
                new SqlParameter("@TotalPayable",TotalPayable),
                new SqlParameter("@Dwallet",Dwallet),
                new SqlParameter("@Fwallet",Fwallet),
                new SqlParameter("@Discount",Discount),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveEventdetailsMobile", para);
            return ds;
        }

    }
    public class SaveTransferCoupon
    {

        public string LoginId { get; set; }
        public string CouponCode { get; set; }
        public string AddedBy { get; set; }
        public DataSet TransferCoupon()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@CouponCode", CouponCode),
                                    new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("TransferCoupon", para);
            return ds;
        }

    }
    public class state
    {
        public string PinCode { get; set; }
        public DataSet GetStateCity()
        {
            SqlParameter[] para = { new SqlParameter("@Pincode", PinCode) };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);
            return ds;
        }
    }
    public class Users
    {
        public string Fk_UserId { get; set; }
        public DataSet GetWalletMoney()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletMoney", para);
            return ds;
        }
        public DataSet GetHealthCardDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetHealthCardDetails", para);
            return ds;
        }
        public DataSet GetAdharCardDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAdharCardDetails", para);
            return ds;
        }
        public DataSet GetPanCardDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPanCardDetails", para);
            return ds;
        }
    }

    public class KYCDocumentMobile
    {
        public string UserId { get; set; }
        public string AdharNumber { get; set; }
        public string AdharImage { get; set; }
        public string PanNumber { get; set; }
        public string PanImage { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentImage { get; set; }
        public string DocumentStatus { get; set; }
        public string MemberAccNo { get; set; }
        public string IFSCCode { get; set; }
        public string MemberBankName { get; set; }
        public string MemberBranch { get; set; }
        public string ActionStatus { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public DataSet UploadKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",UserId) ,
                                      new SqlParameter("@AdharNumber", AdharNumber) ,
                                      new SqlParameter("@AdharImage", AdharImage) ,
                                      new SqlParameter("@PanNumber", PanNumber),
                                      new SqlParameter("@PanImage", PanImage) ,
                                      new SqlParameter("@DocumentNumber", DocumentNumber) ,
                                      new SqlParameter("@DocumentImage", DocumentImage),
                                        new SqlParameter("@Action", ActionStatus),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UploadKYCForMobile", para);
            return ds;
        }
    }
    public class NFCProfileById
    {
        public string LoginId { get; set; }
        public string Fk_UserId { get; set; }
        public DataSet getNFCProfileData()
        {
            SqlParameter[] para = {

                                        new SqlParameter("@Loginid", LoginId),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetNFCProfileDataForMobile", para);
            return ds;
        }
    }
    public class NFCProfileModelMobile
    {

        public string LoginId { get; set; }
        public string Pk_UserId { get; set; }
        public string Code { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string ProfilePic { get; set; }
        public string Summary { get; set; }
        public string UserCode { get; set; }
        public string Leg { get; set; }
        public bool IsProfileTurnedOff { get; set; }

    }
    public class NFcType
    {
        public string TypeName { get; set; }
        public List<TypeList> TypeItem { get; set; }
    }
    public class TypeList
    {
        public string Content { get; set; }
        public string Type { get; set; }
        public string IsWhatsApp { get; set; }
        public string RedirectionLink { get; set; }
    }
    public class BusinessDashboardRequest
    {
        public string Fk_UserId { get; set; }
        public DataSet GetBusinessDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetBusinessDashBoardForMobile", para);
            return ds;
        }
        public DataSet GetNFCActivationStatus()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",Fk_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetNFCActivationStatus", para);
            return ds;
        }
        public DataSet CheckResellerStatus()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",Fk_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("CheckResellerStatus", para);
            return ds;
        }
    }
    public class BusinessDashboard
    {
        public string UserId { get; set; }
        public string LoginId { get; set; }
        public string MobileNo { get; set; }
        public string DOJ { get; set; }
        public string DOA { get; set; }
        public string LastLogin { get; set; }
        public decimal TotalEarning { get; set; }
        public string TotalTeam { get; set; }
        public decimal TotalDirect { get; set; }
        public decimal TodaysEarning { get; set; }
        public decimal TodaysTeam { get; set; }
    }
    public class NFCRequestAPI
    {
        public string Fk_UserId { get; set; }
        public string Code { get; set; }
        public DataSet GetUsedNFC()
        {
            SqlParameter[] para =
          {
                new SqlParameter("@DistributorId",Fk_UserId),
                new SqlParameter("@Code",Code)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetUsedNFC", para);
            return ds;
        }
        public DataSet GetUnusedNFC()
        {
            SqlParameter[] para =
          {
                new SqlParameter("@DistributorId",Fk_UserId),
                new SqlParameter("@Code",Code)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetUnusedNFC", para);
            return ds;
        }
    }
    public class UsedNFCAPI
    {
        public int SrNo { get; set; }
        public string PK_NFCId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ActivatedOn { get; set; }
        public string Code { get; set; }
        public string NFCStatus { get; set; }

    }
    public class UnusedNFCAPI
    {
        public int SrNo { get; set; }
        public string PK_NFCId { get; set; }
        public string AllotedOn { get; set; }
        public string AssignedBy { get; set; }
        public string Code { get; set; }
        public string NFCStatus { get; set; }
    }
    public class InventoryRequest
    {
        public string CategoryId { get; set; }
        public string FK_UserId { get; set; }
        public decimal Quantity { get; set; }

        public DataSet RequestForNFC()
        {
            SqlParameter[] para = {
                new SqlParameter("@Quantity",Quantity),
                 new SqlParameter("@CategoryId",CategoryId),
                 new SqlParameter("@FK_UserId",FK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("RequestForNFC", para);
            return ds;
        }
    }
    public class NFCstatus
    {
        public bool IsProfileTurnedOff { get; set; }
        public string PK_UserId { get; set; }
        public DataSet UpdateProfileOnOffStatus()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",PK_UserId),

                new SqlParameter ("@IsProfileTurnedOff",IsProfileTurnedOff)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfileOnOffStatus", para);
            return ds;
        }
    }
    public class Product
    {
        public string ProductId { get; set; }
        public DataSet GetNFCForInventory()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetNFCForInventory");
            return ds;
        }
    }
    public class ProductResponse
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
    }
    public class InventoryData
    {
        public string FK_UserId { get; set; }
        public DataSet GetNFCForInventory()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",FK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetInventoryCount", para);
            return ds;
        }
    }
    public class securityDetail
    {
        public string FK_UserId { get; set; }
        public string Email { get; set; }
        public DataSet VerifyEmailCode()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Email",Email),
                new SqlParameter("@Fk_UserId",FK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("VerifyEmailCode", para);
            return ds;
        }
    }
    public class Url
    {
        public string Link { get; set; }
        public string Name { get; set; }
    }
    public class Aadhar
    {
        public string PK_UserId { get; set; }
        public string AadharNo { get; set; }
        public string AadharImage { get; set; }
        public DataSet UploadAdhar()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_UserId",PK_UserId),
                new SqlParameter("@AadharNo",AadharNo),
                new SqlParameter("@AadharImage",AadharImage),
            };
            DataSet ds = DBHelper.ExecuteQuery("UploadAdhar", para);
            return ds;
        }
    }
    public class Pan
    {
        public string PK_UserId { get; set; }
        public string PanNo { get; set; }
        public string PanImage { get; set; }
        public DataSet UploadPan()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_UserId",PK_UserId),
                new SqlParameter("@PanNo",PanNo),
                new SqlParameter("@PanImage",PanImage),
            };
            DataSet ds = DBHelper.ExecuteQuery("UploadPan", para);
            return ds;
        }
    }
    public class Document
    {
        public string PK_UserId { get; set; }
        public string DocumentNo { get; set; }
        public string DocumentImage { get; set; }
        public DataSet UploadDocument()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_UserId",PK_UserId),
                new SqlParameter("@DocumentNo",DocumentNo),
                new SqlParameter("@DocumentImage",DocumentImage),
            };
            DataSet ds = DBHelper.ExecuteQuery("UploadDocument", para);
            return ds;
        }
    }
    public class Photo
    {
        public string PK_UserId { get; set; }
        public string ProfilePic { get; set; }
        public DataSet UploadProfilePic()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_UserID",PK_UserId),
                new SqlParameter("@ProfilePic",ProfilePic),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfilePic", para);
            return ds;
        }

    }
    public class Passbook
    {
        public string PK_UserId { get; set; }
        public string Image { get; set; }
        public DataSet UploadPassbookImage()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_UserID",PK_UserId),
                new SqlParameter("@Image",Image),
            };
            DataSet ds = DBHelper.ExecuteQuery("UploadPassbookImage", para);
            return ds;
        }

    }
    public class NFCProfileLog
    {
        public string Code { get; set; }
        public string IP { get; set; }
        public string Device { get; set; }
        public string Location { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }
        public string PinCode { get; set; }
        public DataSet InsertLog()
        {
            SqlParameter[] para ={
                new SqlParameter ("@NFCCode",Code),
                new SqlParameter ("@Browser",""),
                new SqlParameter ("@IP",IP),
                new SqlParameter ("@Medium","App"),
                new SqlParameter ("@Lat",Lat),
                new SqlParameter ("@Long",Long),
                new SqlParameter ("@Location",Location),
                new SqlParameter ("@ZipCode",PinCode)

            };
            DataSet ds = DBHelper.ExecuteQuery("InsertLogMobile", para);
            return ds;
        }
    }
    public class SponsorForReseller
    {
        public string FK_SponosrId { get; set; }
        public string SponsorName { get; set; }
        public string SponsorCode { get; set; }
        public DataSet GetSponsorForReseller()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetSponsorForReseller");
            return ds;
        }
    }
    public class NFCModelAPI
    {
        public string Browser { get; set; }
        public string Device { get; set; }
        public string IP { get; set; }
        public string Location { get; set; }
        public string ViewDateTime { get; set; }
    }
    public class SocialMediaRedirection
    {
        public string PK_NFCProfileId { get; set; }
        public bool IsRedirect { get; set; }
        public DataSet UpdateStatus()
        {
            SqlParameter[] para ={
                new SqlParameter ("@PK_NFCProfileId",PK_NFCProfileId),
                new SqlParameter ("@IsRedirect",IsRedirect),

            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateNFCProfileRedirectionMobile", para);
            return ds;
        }
    }
    public class Whatsapp
    {
        public string PK_NFCProfileId { get; set; }
        public bool IsWhatsapp { get; set; }
        public string FK_UserId { get; set; }
        public DataSet EditWhatsappNo()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Pk_NfcProfileId",PK_NFCProfileId),
                new SqlParameter ("@IsCheked",IsWhatsapp),
                new SqlParameter ("@AddedBy",FK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("EditWhatsappNo", para);
            return ds;
        }
    }
    public class InvoiceAPI
    {
        public string ActivationDate { get; set; }
        public string Description { get; set; }
        public string EventName { get; set; }
        public string Fk_EventId { get; set; }
        public string Fk_UserId { get; set; }
        public string Image { get; set; }
        public string InvoiceNo { get; set; }
        public string PK_BookingId { get; set; }
        public string ProductPrice { get; set; }
    }
    public class Invoice
    {
        public string InvoiceNo { get; set; }
        public DataSet PrintInvoice()
        {
            SqlParameter[] para =
              {
                 new SqlParameter("@Pk_InvestmentId",InvoiceNo),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetDetailsForInvoice", para);
            return ds;
        }
    }
    public class Include
    {
        public string Pk_NfcProfileId { get; set; }
        public string PK_ProfileId { get; set; }
        public bool IsIncluded { get; set; }
        public string PK_UserId { get; set; }
        public DataSet UpdateIncludedStatus()
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
    }
    public class AddProfile
    {
        public string FK_UserId { get; set; }
        public string ProfileName { get; set; }
        public string ProfileType { get; set; }
        public DataSet InsertProfileName()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Fk_UserId",FK_UserId),
                new SqlParameter ("@ProfileName",ProfileName),
                new SqlParameter ("@ProfileType",ProfileType),
            };
            DataSet ds = DBHelper.ExecuteQuery("InsertProfileName", para);
            return ds;
        }

    }
    public class ProfileAPI
    {
        public string PK_UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string EmailId { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string IFSC { get; set; }
        public string ProfilePicture { get; set; }
        public string AdharNo { get; set; }
        public string Summary { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public string UPIId { get; set; }
        public DataSet UpdateProfile()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserID",PK_UserID ) ,
                                      new SqlParameter("@FirstName", FirstName) ,
                                      new SqlParameter("@LastName", LastName) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Email", EmailId) ,
                                      new SqlParameter("@AccountNo", AccountNumber) ,
                                      new SqlParameter("@BankName", BankName) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                      new SqlParameter("@IFSC", IFSC),
                                      new SqlParameter("@ProfilePic", ProfilePicture),
                                      new SqlParameter("@AdharNo",AdharNo),
                                      new SqlParameter("@Summary",Summary),
                                      new SqlParameter("@Password",Password),
                                      new SqlParameter("@UPIId",UPIId),
                                      new SqlParameter("@Image",Image),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfile", para);
            return ds;
        }

    }
    public class HealthCardDetail
    {
        public string FK_UserId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string CardNo { get; set; }
        public DataSet SaveHealtCardDetails()
        {
            SqlParameter[] para = {new SqlParameter("@Name", Name) ,
                                      new SqlParameter("@FK_UserId",FK_UserId ) ,
                                      new SqlParameter("@FK_CardTypeId",3 ) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                       new SqlParameter("@Gender", Gender) ,
                                       new SqlParameter("@DOB", DOB) ,
                                       new SqlParameter("@CardNo", CardNo) ,
                                  };
            DataSet ds = DBHelper.ExecuteQuery("SaveCardDetails", para);
            return ds;
        }
    }
    public class UpdateHealthCardDetail
    {
        public string PK_CardId { get; set; }
        public string FK_UserId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public DataSet UpdateHealthCardDetails()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@PK_CardId", PK_CardId) ,
                                     new SqlParameter("@Name", Name) ,
                                      new SqlParameter("@FK_UserId",FK_UserId ) ,
                                      new SqlParameter("@FK_CardTypeId",3 ) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                       new SqlParameter("@Gender", Gender) ,
                                      new SqlParameter("@DOB", DOB) ,
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateCardDetails", para);
            return ds;
        }
    }
    public class CardPic
    {
        public string PK_CardId { get; set; }
        public string PK_UserId { get; set; }
        public string Photo { get; set; }
        public DataSet UploadPhoto()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_UserID",PK_UserId),
                 new SqlParameter("@PK_CardId",PK_CardId),
                new SqlParameter("@Photo",Photo),
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdatePhoto", para);
            return ds;
        }

    }
    public class AdharCardDetail
    {
        public string FK_UserId { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public string FatherName { get; set; }
        public string FatherNameH { get; set; }
        public string Mobile { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string GenderH { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AddressH { get; set; }
        public string PinCode { get; set; }
        public string AdharNo { get; set; }
        public DataSet SaveAdharCardDetails()
        {
            SqlParameter[] para = {new SqlParameter("@Name", Name) ,
                                      new SqlParameter("@NameH", NameH),
                                      new SqlParameter("@FK_UserId",FK_UserId ) ,
                                      new SqlParameter("@FatherName",FatherName ) ,
                                      new SqlParameter("@FatherNameH",FatherNameH ) ,
                                      new SqlParameter("@FK_CardTypeId",1 ) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Gender", Gender) ,
                                      new SqlParameter("@GenderH", GenderH) ,
                                      new SqlParameter("@Email", Email) ,
                                      new SqlParameter("@Address", Address) ,
                                      new SqlParameter("@AddressH", AddressH) ,
                                      new SqlParameter("@DOB", DOB) ,
                                      new SqlParameter("@PinCode", PinCode) ,
                                      new SqlParameter("@CardNo", AdharNo) ,
                                  };
            DataSet ds = DBHelper.ExecuteQuery("SaveCardDetails", para);
            return ds;
        }
    }
    public class PanCardDetail
    {
        public string FK_UserId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
        public string PanNo { get; set; }
        public DataSet SavePanCardDetails()
        {
            SqlParameter[] para = {new SqlParameter("@Name", Name) ,
                                      new SqlParameter("@FatherName", FatherName) ,
                                      new SqlParameter("@FK_UserId",FK_UserId ) ,
                                      new SqlParameter("@FK_CardTypeId",2 ) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Gender", Gender) ,
                                      new SqlParameter("@Email", Email) ,
                                      new SqlParameter("@Address", Address) ,
                                      new SqlParameter("@DOB", DOB) ,
                                      new SqlParameter("@CardNo", PanNo) ,
                                  };
            DataSet ds = DBHelper.ExecuteQuery("SaveCardDetails", para);
            return ds;
        }
    }
    public class UpdateAdharCardDetail
    {
        public string PK_CardId { get; set; }
        public string FK_UserId { get; set; }
        public string Name { get; set; }
        public string NameH { get; set; }
        public string FatherName { get; set; }
        public string FatherNameH { get; set; }
        public string Mobile { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string GenderH { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AddressH { get; set; }
        public string PinCode { get; set; }
        public DataSet UpdateAdharCardDetails()
        {
            SqlParameter[] para = { new SqlParameter("@PK_CardId", PK_CardId) ,
                                      new SqlParameter("@FK_UserId",FK_UserId ) ,
                                      new SqlParameter("@Name", Name) ,
                                      new SqlParameter("@NameH", NameH) ,
                                      new SqlParameter("@FatherName", FatherName) ,
                                      new SqlParameter("@FatherNameH", FatherNameH) ,
                                      new SqlParameter("@FK_CardTypeId",1 ) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Gender", Gender) ,
                                      new SqlParameter("@GenderH", GenderH) ,
                                      new SqlParameter("@Email", Email) ,
                                      new SqlParameter("@Address", Address) ,
                                      new SqlParameter("@AddressH", AddressH) ,
                                      new SqlParameter("@PinCode", PinCode) ,
                                      new SqlParameter("@DOB", DOB) ,
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateCardDetails", para);
            return ds;
        }
    }
    public class UpdatePanCardDetail
    {
        public string PK_CardId { get; set; }
        public string FK_UserId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DOB { get; set; }
        public DataSet UpdatePanCardDetails()
        {
            SqlParameter[] para = { new SqlParameter("@PK_CardId", PK_CardId) ,
                                      new SqlParameter("@Name", Name) ,
                                      new SqlParameter("@FatherName", FatherName) ,
                                      new SqlParameter("@FK_UserId",FK_UserId ) ,
                                      new SqlParameter("@FK_CardTypeId",2 ) ,
                                      new SqlParameter("@Mobile", Mobile) ,
                                      new SqlParameter("@Gender", Gender) ,
                                      new SqlParameter("@Email", Email) ,
                                      new SqlParameter("@Address", Address) ,
                                      new SqlParameter("@DOB", DOB) ,
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateCardDetails", para);
            return ds;
        }
    }
    public class Request
    {
        public string Body { get; set; }
    }
    public class UpdateBusinessProfileForMobile
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public string FK_UserId { get; set; }
        public string PK_ProfileId { get; set; }
        public List<ContactList1> ContactList { get; set; }
        public List<EmailList1> EmailList { get; set; }
        public List<WebLink1> WebLinkList { get; set; }
        public List<SocialMediaLink1> SocialLink { get; set; }
        public DataTable dtcontact { get; set; }
        public DataTable dtemail { get; set; }
        public DataTable dtweblink { get; set; }
        public DataTable dtsocial { get; set; }
        public DataSet UpdateBusinessInfo()
        {
            SqlParameter[] para ={
                  new SqlParameter ("@PK_ProfileId",PK_ProfileId),
                  new SqlParameter ("@FK_UserId",FK_UserId),
                  new SqlParameter ("@FirstName",FirstName),
                  new SqlParameter ("@LastName",LastName),
                  new SqlParameter ("@BusinessName",BusinessName),
                  new SqlParameter ("@Description",Description),
                  new SqlParameter ("@AddedBy",FK_UserId),
                  new SqlParameter("@dtcontact",dtcontact),
                  new SqlParameter("@dtemail",dtemail),
                  new SqlParameter("@dtweblink",dtweblink),
                  new SqlParameter("@dtsocial",dtsocial)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBusinessInfoNew", para);
            return ds;
        }
    }
    #region Addtocard for mobile
    public class Addtocard
    {
        public string FK_UserId { get; set; }
        public string FK_EventId { get; set; }
        public string NumberOfItems { get; set; }
        public string ItemType { get; set; }

        public DataSet AddToCard()
        {
            SqlParameter[] para =
                {
                new SqlParameter("@FK_UserId",FK_UserId ),
                 new SqlParameter("@FK_EventId",FK_EventId ),
                 new SqlParameter("@TotalItems",NumberOfItems ),
                  new SqlParameter("@ItemType",ItemType )

            };
            DataSet ds = DBHelper.ExecuteQuery("[SP_SaveAddToCart]", para);
            return ds;
        }
    }
    #endregion
    #region card list for mobile
    public class Shop1
    {
        public string Fk_UserId { get; set; }

        public List<Shop> lstproduct { get; set; }
        public string ProductName { get; set; }
        public string OfferPrice { get; set; }
        public string BV { get; set; }
        public string ProductCode { get; set; }
        public string EventDescription { get; set; }
        public string EventImage { get; set; }
        public string NoOfSeats { get; set; }
        public string Brand { get; set; }
        public string TotalPrice { get; set; }
        public string PK_EventId { get; set; }
        public string DeliverCharge { get; set; }
        public string IGST { get; set; }
        public string UserName { get; set; }
        public DataSet AddToCartListMobile()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId",Fk_UserId )

            };
            DataSet ds = DBHelper.ExecuteQuery("SP_AddToCartList", para);
            return ds;
        }
    }
    public class Cart
    {
        public string Fk_UserId { get; set; }
        public string PK_EventId { get; set; }
        public DataSet AddToCartList()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId",Fk_UserId )
            };
            DataSet ds = DBHelper.ExecuteQuery("SP_AddToCartList", para);
            return ds;
        }

    }
    public class ShopMobile
    {
        public string TotalPrice { get; set; }
        public string Brand { get; internal set; }
        public string BV { get; set; }
        public string DeliverCharge { get; set; }
        public string EventDescription { get; set; }
        public string EventImage { get; set; }
        public string IGST { get; set; }
        public string NoOfSeats { get; set; }
        public string OfferPrice { get; set; }
        public string PK_EventId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
    }
    #endregion

    #region update card list
    public class updateAddtocard
    {
        public string FK_UserId { get; set; }
        public string FK_EventId { get; set; }
        public string NumberOfItems { get; set; }

        public DataSet updateAddToCard()
        {
            SqlParameter[] para =
                {
                new SqlParameter("@FK_UserId",FK_UserId ),
                 new SqlParameter("@FK_EventId",FK_EventId ),
                 new SqlParameter("@TotalItem",NumberOfItems )

            };
            DataSet ds = DBHelper.ExecuteQuery("[SP_checktotalitem]", para);
            return ds;
        }
    }
    #endregion

    #region DeleteLIst
    public class DeleteLIst
    {
        public string FK_UserId { get; set; }
        public string FK_EventId { get; set; }


        public DataSet DeleteLIstitem()
        {
            SqlParameter[] para =
                {
                new SqlParameter("@FK_UserId",FK_UserId ),
                 new SqlParameter("@FK_EventId",FK_EventId ),


            };
            DataSet ds = DBHelper.ExecuteQuery("[SP_Deleteitem]", para);
            return ds;
        }
    }
    #endregion
    public class DashboardResponse
    {
        public List<DashboardMessageData> lst { get; set; }

        public List<DashboardInvestment> lstInvestment { get; set; }
    }
    public class DashboardMessageData
    {
        public string Pk_MessageId { get; set; }
        public string Fk_UserId { get; set; }
        public string MemberName { get; set; }
        public string MessageTitle { get; set; }
        public string AddedOn { get; set; }
        public string Message { get; set; }
        public string cssclass { get; set; }
        public string ProfilePic { get; set; }
    }
    public class DashboardInvestment
    {
        public string ProductName { get; set; }
        public string Amount { get; set; }
        public string Status { get; set; }
    }
    public class DashboardMessage
    {
        public string Message { get; set; }
        public string MessageBy { get; set; }
        public string UserId { get; set; }
        public DataSet SaveMessage()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Message", Message),
                                      new SqlParameter("@AddedBy", UserId),
                                      new SqlParameter("@MessageBy", MessageBy),
                                      new SqlParameter("@Fk_UserId", UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("InsertMessage", para);
            return ds;
        }
    }
    public class DashBoardMobile
    {
        public string UserId { get; set; }

        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            return ds;
        }
        public DashboardResponse GetInvestment()
        {
            DashboardResponse model = new DashboardResponse();
            List<DashboardInvestment> lstInvestment = new List<DashboardInvestment>();
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    DashboardInvestment Obj = new DashboardInvestment();
                    Obj.ProductName = r["ProductName"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.Status = r["Status"].ToString();

                    lstInvestment.Add(Obj);
                }
                model.lstInvestment = lstInvestment;
            }
            return model;
        }
        public DashboardResponse GetAllMessages()
        {
            DashboardResponse model = new DashboardResponse();
            List<DashboardMessageData> lst = new List<DashboardMessageData>();
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetMessages", para);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DashboardMessageData Obj = new DashboardMessageData();
                    Obj.Pk_MessageId = r["Pk_MessageId"].ToString();
                    Obj.Fk_UserId = r["Fk_UserId"].ToString();
                    Obj.MemberName = r["Name"].ToString();
                    Obj.MessageTitle = r["MessageTitle"].ToString();
                    Obj.AddedOn = r["AddedOn"].ToString();
                    Obj.Message = r["Message"].ToString();
                    Obj.cssclass = r["cssclass"].ToString();
                    Obj.ProfilePic = r["ProfilePic"].ToString();
                    lst.Add(Obj);
                }
                model.lst = lst;
            }
            return model;
        }
    }
    public class ProfileMobile
    {
        public string UserId { get; set; }
        public DataSet GetUserProfile()
        {
            SqlParameter[] para = { new SqlParameter("@UserId", UserId) };
            DataSet ds = DBHelper.ExecuteQuery("UserProfileMobile", para);
            return ds;
        }
    }
    public class Sponsor
    {
        public string SponsorId { get; set; }
        public string AddedBy { get; set; }
        public string SponsorName { get; set; }

        public string Leg { get; set; }
        public DataSet UpdateSponsor()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SponserId", SponsorId),
                                      new SqlParameter("@UpdatedBy", AddedBy),
                                      new SqlParameter("@SponsorName", SponsorName),
                                      new SqlParameter("@Leg", Leg)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateSponsorDetails", para);
            return ds;
        }
    }
    public class SaveAddress
    {
        public string Fk_UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DataSet SaveUserAddress()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",Fk_UserId),
                new SqlParameter("@FirstName",FirstName),
                new SqlParameter("@LastName",LastName),
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@Email",Email),
                new SqlParameter("@Pincode",Pincode),
                new SqlParameter("@State",State),
                new SqlParameter("@City",City),
                new SqlParameter("@Address",Address)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveUserAddress", para);
            return ds;
        }
    }
    public class EventDetail
    {
        public string EventImage { get; internal set; }
        public string EventName { get; internal set; }
        public int PK_EventId { get; internal set; }
        public string CategoryId { get; set; }
    }
    public class EventDetailRequest
    {
        public string EventId { get; set; }
        public DataSet EventList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_EventId",EventId)
            };
            DataSet ds = DBHelper.ExecuteQuery("EventList", para);
            return ds;
        }
        public DataSet GetAllRelatedEvent()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_EventId",EventId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAllRelatedEvent", para);
            return ds;
        }
    }
    public class RelatedEvent
    {
        public string EndDate { get; set; }
        public string EventDescription { get; set; }
        public string EventImage { get; set; }
        public string EventLocation { get; set; }
        public string EventName { get; set; }
        public string NoOfSeats { get; set; }
        public string CategoryId { get; set; }

        public string OfferName { get; set; }
        public int PK_EventId { get; set; }
        public string StartDate { get; set; }
    }
    public class SubscriptionRequest
    {
        public string CategoryId { get; set; }
        public string UserId { get; set; }

        public DataSet GetBookingEventDetails()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_CategoryId",CategoryId),
                new SqlParameter("@FK_UserId",UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBookingEventDetails", para);
            return ds;
        }
    }
    public class SponsorDetail
    {
        public string LoginId { get; set; }

        public string Leg { get; set; }
        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberName", para);

            return ds;
        }
        public DataSet GetLegDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Leg", Leg),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetLegDetails", para);

            return ds;
        }
    }
    public class SponsorMobile
    {
        public string LoginId { get; set; }

        public DataSet GetSponsor()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetSponsor", para);
            return ds;
        }
    }
    public class GuestDashboardDetail
    {
        public string BannerImage { get; set; }
        public string BannerTitle { get; set; }
        public string BId { get; set; }
        public string Description { get; set; }
        public string FavStatus { get; set; }
        public bool IsAddFav { get; set; }
        public string Trailer { get; set; }
    }
    public class TicketRequest
    {
        public string UserId { get; set; }
        public string EventId { get; set; }
        public DataSet GetEventDetails()
        {
            SqlParameter[] para ={
                new SqlParameter("@PK_EventId",EventId),
                new SqlParameter("@FK_UserId",UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("EventDetailsForBooking", para);
            return ds;
        }
    }
    public class RequestMobile
    {
        public string UserId { get; set; }
        public string PageNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Fk_PlanId { get; set; }

        public DataSet GetKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID", UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetKYCDocuments", para);
            return ds;
        }
        public DataSet GetEWalletRequest()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletRequest", para);
            return ds;
        }
        public DataSet EwalletLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", UserId),
                                      new SqlParameter ("@PageNo",PageNo),
                                      new SqlParameter ("@FromDate",FromDate),
                                      new SqlParameter ("@ToDate",ToDate),
                                      new SqlParameter ("@Fk_PlanId",Fk_PlanId)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletLedgerForMobile", para);
            return ds;
        }
        public DataSet GetDCMIReportForAssociate()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@Fk_userid", UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetDCMIForAssociate", para);
            return ds;
        }
    }
    public class KYCDocument
    {
        public string UserId { get; set; }
        public string AdharNumber { get; set; }
        public string AdharImage { get; set; }
        public string PanNumber { get; set; }
        public string PanImage { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentImage { get; set; }
        public string DocumentStatus { get; set; }
        public string MemberAccNo { get; set; }
        public string IFSCCode { get; set; }
        public string MemberBankName { get; set; }
        public string MemberBranch { get; set; }
        public DataSet UploadKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",UserId ) ,
                                      new SqlParameter("@AdharNumber", AdharNumber) ,
                                      new SqlParameter("@AdharImage", AdharImage) ,
                                      new SqlParameter("@PanNumber", PanNumber),
                                      new SqlParameter("@PanImage", PanImage) ,
                                      new SqlParameter("@DocumentNumber", DocumentNumber) ,
                                      new SqlParameter("@DocumentImage", DocumentImage),
                                      new SqlParameter("@AccountNo", MemberAccNo),
                                      new SqlParameter("@IFSCCode",IFSCCode),
                                      new SqlParameter("@BankName",MemberBankName),
                                      new SqlParameter("@BranchName",MemberBranch)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UploadKYC", para);
            return ds;
        }
    }
    public class WalletMobile
    {
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string PaymentMode { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string BankBranch { get; set; }
        public string DocumentImg { get; set; }
        public string BankName { get; set; }
        public string AddedBy { get; set; }
        public string RequestCode { get; set; }
        public string Status { get; set; }
        public string AddedOn { get; set; }

        public DataSet SaveEWalletRequest()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@PaymentMode", PaymentMode) ,
                                      new SqlParameter("@DDChequeNo", DDChequeNo) ,
                                      new SqlParameter("@DDChequeDate", DDChequeDate) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                      new SqlParameter("@DocumentImg", DocumentImg),
                                          new SqlParameter("@BankName", BankName),
                                            new SqlParameter("@AddedBy", AddedBy)

                                     };
            DataSet ds = DBHelper.ExecuteQuery("EwalletRequest", para);
            return ds;
        }
    }
    public class TransferEwallet
    {
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string AddedBy { get; set; }


        public DataSet TransferEwalletToEwallet()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@AddedBy", AddedBy),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("TransferEwalletToEwallet", para);
            return ds;
        }
    }
    public class WalletLedgerMobile
    {
        public int SrNo { get; set; }
        public string AddedOn { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string EwalletBalance { get; set; }
        public string Narration { get; set; }
    }
    public class DirectListRequest
    {
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Narration { get; set; }
        public string FromActivationDate { get; set; }
        public string ToActivationDate { get; set; }
        public string Leg { get; set; }
        public string Status { get; set; }
        public DataSet GetDirectList()
        {

            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectList", para);
            return ds;
        }
        public DataSet GetDirectListL2()
        {

            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectListL2", para);
            return ds;
        }
        public DataSet GetDownlineList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status), };
            DataSet ds = DBHelper.ExecuteQuery("DownlineList", para);
            return ds;
        }

    }
    public class DirectListMobile
    {
        public string Email { get; set; }
        public string JoiningDate { get; set; }
        public string Leg { get; set; }
        public string LoginId { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Package { get; set; }
        public string PermanentDate { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string Status { get; set; }
    }
    public class Payout
    {
        public string UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PageNo { get; set; }
        public DataSet PayoutLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", UserId),
                                       new SqlParameter("@FromDate", FromDate),
                                        new SqlParameter("@ToDate", ToDate),
                                        new SqlParameter("@PageNo", PageNo),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutLedgerForMobile", para);
            return ds;
        }
    }
    public class PayoutReportData
    {
        public string BinaryIncome { get; set; }
        public string ClosingDate { get; set; }
        public string DirectIncome { get; set; }
        public string DisplayName { get; set; }
        public string EncryptLoginID { get; set; }
        public string EncryptPayoutNo { get; set; }
        public string GrossAmount { get; set; }
        public string LeadershipBonus { get; set; }
        public string LoginId { get; set; }
        public string NetAmount { get; set; }
        public string PayoutNo { get; set; }
        public string ProcessingFee { get; set; }
        public string ProductWallet { get; set; }
        public string TDSAmount { get; set; }
    }
    public class PayoutReport
    {
        public string LoginId { get; set; }
        public string PayoutNo { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PageNo { get; set; }
        public DataSet GetPayoutReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@PayoutNo", PayoutNo),
                                         new SqlParameter("@FromDate", FromDate),
                                         new SqlParameter("@ToDate", ToDate),
                                         new SqlParameter("@PageNo",PageNo)

            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutReportForMemberForMobile", para);
            return ds;
        }
    }
    public class SubscriptionResponse
    {
        public string SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string Price { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string NoOfSeats { get; set; }
        public string SubscriptionImage { get; set; }
        public string SubscriptionDescription { get; set; }
        public string LoginId { get; set; }
        public string MobileNo { get; set; }
        public string CreatedDate { get; set; }
        public string offername { get; set; }
        public string OfferValidity { get; set; }
        public string CouponCode { get; set; }
        public string IGST { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string ReferralPercentage { get; set; }
        public string BinaryPercentage { get; set; }
        public string DeliveryCharges { get; set; }
        public string OfferPrice { get; set; }
        public string Discount { get; set; }

    }
    public class ActivateUserid
    {
        public string LoginId { get; set; }
        public string CouponCode { get; set; }
        public string CouponPrice { get; set; }
        public string AddedBy { get; set; }
        public string Result { get; set; }
        public string TopupDate { get; set; }
        public DataSet ActivateUserId()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@CouponCode", CouponCode),
                                    new SqlParameter("@CouponPrice", CouponPrice),
                                    new SqlParameter("@AddedBy",AddedBy),
                                     new SqlParameter("@TopupDate",TopupDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("CouponTopUpByUser", para);
            return ds;
        }
    }
    public class GetData
    {
        public DataSet GetAllEvent()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetAllEvent");
            return ds;
        }
        public DataSet GetAllSubscription()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetAllSubscriptions");
            return ds;
        }
    }
    public class SubCategory
    {
        public string SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryIcon { get; set; }
        public List<ProductItem> Products { get; set; }
    }
    public class ProductItem
    {
        public string Name { get; set; }
        public string PK_SubCategoryId { get; set; }
        public string Image { get; set; }
        public string DeliveryCharges { get; set; }
        public string IGST { get; set; }
        public string OfferPrice { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string Discount { get; set; }
        public string ShortDescription { get; set; }
        public string Price { get; set; }
        public string Pk_EventId { get; set; }

    }
    public class Event
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string EventLocation { get; set; }
        public string EventImage { get; set; }
    }
    public class Subscription
    {
        public string SubscriptionId { get; set; }
        public string SubscriptionName { get; set; }
        public string CreatedDate { get; set; }
        public string SubscriptionImage { get; set; }
        public string SubCategory { get; set; }
    }
    public class ReferalProgram
    {
        public string SponsorId { get; set; }
        public string Leg { get; set; }
        public string UserId { get; set; }

        public DataSet UpdateSponsor()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@SponserId", SponsorId),
                                      new SqlParameter("@UpdatedBy", UserId),
                                      new SqlParameter("@Leg", Leg)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateSponsorDetails", para);
            return ds;
        }
    }
    public class Menu
    {
        public string Status { get; set; }

        public List<menuDetails> menu { get; set; }

        public DataSet getserviceType()
        {


            DataSet dsResult = DBHelper.ExecuteQuery("GetServicesTypeListForMobile");
            return dsResult;
        }
        public DataSet GetMenu()
        {


            DataSet dsResult = DBHelper.ExecuteQuery("GetMenuForMobile");
            return dsResult;
        }

    }
    public class Data1
    {
        public List<menuDetails> menu { get; set; }
    }
    public class menuDetails
    {

        public string MainServiceType { get; set; }
        public string Preority { get; set; }
        public string InputType { get; set; }
        public List<menuData> menuData { get; set; }
    }
    public class menuData
    {
        public string ServiceIcon { get; set; }
        public string Service { get; set; }

        public string ServiceUrl { get; set; }
    }
    public class updateAddress
    {
        public string Fk_UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PK_AddressId { get; set; }
        public DataSet updateUserAddress()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_UserId",Fk_UserId),
                new SqlParameter("@FirstName",FirstName),
                new SqlParameter("@LastName",LastName),
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@Email",Email),
                new SqlParameter("@Pincode",Pincode),
                new SqlParameter("@State",State),
                new SqlParameter("@City",City),
                new SqlParameter("@Address",Address),
                new SqlParameter("@PK_AddressId",PK_AddressId)
            };
            DataSet ds = DBHelper.ExecuteQuery("sp_updateaddress", para);
            return ds;
        }
    }
    public class Balance
    {
        public string FK_AddressId { get; set; }
        public string FK_UserId { get; set; }
        public DataSet GetAvailableBalanceForCheckout()
        {
            SqlParameter[] para ={
                  new SqlParameter ("@FK_AddressId",FK_AddressId),
                  new SqlParameter ("@FK_UserId",FK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAvailableBalanceForCheckout", para);
            return ds;
        }
        public DataSet AddToCartList()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId",FK_UserId )
            };
            DataSet ds = DBHelper.ExecuteQuery("[SP_AddToCartList]", para);
            return ds;
        }
        public DataSet GetUserAddressBookForId()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_UserId",FK_UserId),
                 new SqlParameter("@FK_AddressId",FK_AddressId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetUserAddressBook", para);
            return ds;
        }
    }
}