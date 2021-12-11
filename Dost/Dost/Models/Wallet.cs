using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dost.Models
{
    public class Wallet:Common
    {
        #region Property
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string BankBranch { get; set; }
        public string DocumentImg { get; set; }
        public string EwalletBalance { get; set; }
        public string MobileNo { get; set; }
        public string ReceiverMobile { get; set; }
        public string SenderMobile { get; set; }
        public string ReceiverName { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string TransferAmount { get; set; }
        public string PayoutBalance { get; set; }
        public string WithDrawAmount { get; set; }
        public string Narration { get; set; }
        public string DrAmount { get; set; }
        public string CrAmount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Fk_PlanId { get; set; }
        public List<Wallet> lstewalletledger { get; set; }
        public string Pk_RequestId { get;  set; }
        public string Status { get;  set; }
        public List<Wallet> WalletRequestList { get;  set; }
      
        public List<Dashboard> lstuser { get; set; }
        #endregion
        public DataSet GetDigiWalletBalance()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEWalletBalance", para);
            return ds;
        }
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
        public DataSet GetEwalletBalnce()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletBalance", para);
            return ds;
        }
        public DataSet TransferEwalletToEwallet()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", MobileNo),
                                      new SqlParameter("@Amount", TransferAmount),
                                      new SqlParameter("@AddedBy", AddedBy),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("TransferEwalletToEwallet", para);
            return ds;
        }
        public DataSet GetpayoutBalance()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),

                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutBalance", para);
            return ds;
        }
        public DataSet SavePayoutRequest()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", WithDrawAmount),
                                      new SqlParameter("@AddedBy", AddedBy)

                                     };
            DataSet ds = DBHelper.ExecuteQuery("WithdrawlRequest", para);
            return ds;
        }
        public DataSet EwalletLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@Fk_PlanId", Fk_PlanId),
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletLedger", para);
            return ds;
        }
       
      


        #region WalletRequest
        public DataSet ApproveEwalletRequest()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@Pk_RequestId", Pk_RequestId),
                                         new SqlParameter("@Status", Status)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ApproveDeclineEwalletRequest", para);
            return ds;
        }
        public DataSet ApproveWithdrawlwalletRequest()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@Pk_RequestId", Pk_RequestId),
                                         new SqlParameter("@Status", Status)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ApproveDeclineWithdrawlRequest", para);
            return ds;
        }
        public DataSet ApproveFranchiseewalletRequest()
        {
            SqlParameter[] para = {

                                       new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@Pk_RequestId", Pk_RequestId),
                                         new SqlParameter("@Status", Status)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ApproveDeclineFranchiseewalletRequest", para);
            return ds;
        }
        public DataSet GetWithdrawlWalletRequest()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetWithdrawlWalletRequest");
            return ds;
        }
        #endregion
    }

}