using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dost.Models
{
    public class clsDistributor
    {
        public int? Pk_DistributorId { get; set; }
        public string YourName { get; set; }
        public string Designation { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string CompanySize { get; set; }
        public decimal GST { get; set; }
        public string PAN { get; set; }
        public string CompanyAddress { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Website { get; set; }
        public string AboutCompany { get; set; }
        public int Fk_UserId { get; set; }
        public string IsDeleted { get; set; }
        public int AddedBy { get; set; }
        public string AddedOn { get; set; }
        public int UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public int DeletedBy { get; set; }
        public string DeletedOn { get; set; }

        public string LoginId { get; set; }
        public string Status { get; set; }
        public List<clsDistributor> lst { get; set; }

        public DataSet DistributorList()
        {
            DataSet ds = DBHelper.ExecuteQuery("DistributorList");
            return ds;
        }
        public DataSet ApproveDistributor()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@Pk_DistributorId",Pk_DistributorId),
                new SqlParameter("@ApprovedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("ApproveDistributor", para);
            return ds;
        }
        public DataSet RejectDistributor()
        {
            SqlParameter[] para =
           {
                new SqlParameter("@Pk_DistributorId",Pk_DistributorId),
                new SqlParameter("@RejectedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("RejectDistributor", para);
            return ds;
        }
    }
}