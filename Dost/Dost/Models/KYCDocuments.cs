using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Dost.Models
{
    public class KYCDocuments
    {
        public List<KYCDocuments> KycDetailList { get; set; }
        public string PKUserID { get; set; }
        public string AdharNumber { get; set; }
        public string AdharImage { get; set; }
        public string AdharStatus { get; set; }
        public string PanNumber { get; set; }
        public string PanImage { get; set; }
        public string PanStatus { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentImage { get; set; }
        public string DocumentStatus { get; set; }
		public string MemberAccNo{get;set;}
		public string IFSCCode {get;set;}
		public string MemberBankName{get;set;}
		public string MemberBranch{get;set; }
        public string LoginId { get; set; }
        public DataSet UploadKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",PKUserID ) ,
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

        public DataSet GetKYCDocuments()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserID",PKUserID )};
            DataSet ds = DBHelper.ExecuteQuery("GetKYCDocuments", para);
            return ds;
        }

        public DataSet GetKYCDetails()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId) };
            DataSet ds = DBHelper.ExecuteQuery("GetKYCDetails", para);
            return ds;
        }

    }
}