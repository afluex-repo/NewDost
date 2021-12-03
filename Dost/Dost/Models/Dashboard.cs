using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dost.Models
{
    public class Dashboard:Common
    {
        #region property
        public string SponserId { get; set; }
        public string SponsorName { get; set; }
        public string JoiningDate { get; set; }
        public string Total { get; set; }
        public string Status { get; set; }
        public int? PK_EventId { get; set; }
        public string EventImage { get; set; }
        public string EventType { get; set; }
        public string EventName { get; set; }
        public List<Dashboard> lst { get; set; }
    
         //public string Leg { get; set; }
      
        //
        public int? Pk_DistributorId { get; set; }
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
        public List<Wallet> lstewalletledger { get; set; }
        public List<Dashboard> lstuser { get; set; }
        public string IsDeleted { get; set; }
        public string UpdatedOn { get; set; }
        public int DeletedBy { get; set; }
        public string DeletedOn { get; set; }


        public List<Dashboard> lstmessages { get; set; }
        public string Pk_MessageId { get; set; }
        public string MessageTitle { get; set; }
        public string Message { get; set; }
        public string ProfilePic { get; set; }
        public string MemberName { get; set; }
    
        public string cssclass { get; set; }
        public string UserId { get; set; }

        public string ProductName { get; set; }
        public string Amount { get; set; }

        public List<Dashboard> lstinvestment { get; set; }
        #endregion

        #region Dashboard
        public DataSet userlist()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetUserforTransaction");
            return ds;
        }
        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            return ds;
        }
        public DataSet EwalletLedger()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Fk_UserId", Fk_UserId),
                                     
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletLedgerDashboard", para);
            return ds;
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
        public class DashboardResponse
        {
            public List<DashboardMessageData> lst { get; set; }

            public List<DashboardInvestment> lstInvestment { get; set; }
        }
        public DataSet GetAllMessagesfordashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetMessages", para);
            return ds;
        }
        #endregion
    }
}