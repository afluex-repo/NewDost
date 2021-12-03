using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Dost.Models
{
    public class Password
    {
        public string PKUserID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string UpdatedBy { get; set; }
        public string PasswordType { get; set; }
        public string ConfirmPassword { get; set; }
        public string Mobile { get; set; }
        public string Fk_UserId { get; set; }
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string OTP { get;  set; }

        public DataSet UpdatePassword()
        {
            SqlParameter[] para = { new SqlParameter("@PasswordType",PasswordType ) ,
                                      new SqlParameter("@OldPassword", OldPassword) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ChangePassword", para);
            return ds;
        }
        #region ForgetPassword

        public DataSet GetMobilenumber()
        {
            SqlParameter[] para = {
                                new SqlParameter("@Mobile_No", Mobile),
                                new SqlParameter("@Fk_UserId",Fk_UserId)

            };

            DataSet ds = DBHelper.ExecuteQuery("GetMobileForTransactionPassword", para);
            return ds;
        }
        #endregion
        public DataSet GenerateOTPFORPassword()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",Mobile),
                new SqlParameter("@UserId",Fk_UserId),
                new SqlParameter("@OTP",OTP)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateOTPForTransaction", para);
            return ds;
        }
        public DataSet ValidateOTP()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",Mobile),
                new SqlParameter("@UserId",Fk_UserId),
                new SqlParameter("@OTP",OTP)
            };
            DataSet ds = DBHelper.ExecuteQuery("ValidateOTPForTransaction", para);
            return ds;
        }
        public DataSet ResetPassword()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",Mobile),
                new SqlParameter("@NewPassword",NewPassword),
                new SqlParameter("@OTP",OTP)
            };
            DataSet ds = DBHelper.ExecuteQuery("SetTransactionPassword", para);
            return ds;
        }
    }
    public class PasswordMobile
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public DataSet UpdatePassword()
        {
            SqlParameter[] para = { new SqlParameter("@PasswordType",'P' ) ,
                                      new SqlParameter("@OldPassword", OldPassword) ,
                                      new SqlParameter("@NewPassword", NewPassword) ,
                                      new SqlParameter("@UpdatedBy", UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ChangePassword", para);
            return ds;
        }


    }
   
}