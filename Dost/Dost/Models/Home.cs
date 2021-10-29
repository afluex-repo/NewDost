using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dost.Models
{
    public class Home
    {
        #region property
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string FormName { get; set; }
        public string ControllerName { get; set; }
        public string UserType { get; set; }
        #endregion
        #region Login
        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
                                new SqlParameter("@Password",Password)};
            DataSet ds = DBHelper.ExecuteQuery("Login", para);
            return ds;
        }
        #endregion
    }
}