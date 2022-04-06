using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dost.Models
{
    public class Permission
    {
        public string PK_AdminId { get; set; }
        public DataSet Emplist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@PK_AdminId",PK_AdminId)
            };
            DataSet ds = DBHelper.ExecuteQuery("EmployeeList", para);
            return ds;
        }

        public DataSet BindFormTypeMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = DBHelper.ExecuteQuery("FormTypeMasterManage", para);

            return ds;

        }
    }
}