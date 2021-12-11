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
        public List<Home> lstMenu { get; set; }
        public List<Home> lstsubmenu { get; set; }
        #region property
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string FormName { get; set; }
        public string ControllerName { get; set; }
        public string UserType { get; set; }
        public string Mobile { get; set; }
        public string Name { get; internal set; }
        public string FK_UserId { get;  set; }
        public string TempPassword { get;  set; }
        public string MobileNo { get;  set; }
        public string Email { get;  set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string Response { get;  set; }
        public string PId { get;  set; }
        public string SponsorId { get;  set; }
        public object Leg { get;  set; }
        public string FirstName { get;  set; }
        public string LastName { get;  set; }
        public string RegistrationBy { get;  set; }
        public string Gender { get;  set; }
        public string OTP { get;  set; }
        public string Result { get;  set; }
        public string PinCode { get;  set; }
        public string Pk_AdminId { get; set; }
        public string MenuId { get;  set; }
        public string MenuName { get;  set; }
        public string Icon { get;  set; }
        public string Url { get;  set; }
        public string SubMenuId { get;  set; }
        public string SubMenuName { get;  set; }
        public string UnderPlaceId { get;  set; }
        public string Code { get;  set; }
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
        public DataSet GetMobilenumber()
        {
            SqlParameter[] para = {
                                new SqlParameter("@Mobile_No", Mobile)

            };

            DataSet ds = DBHelper.ExecuteQuery("GetMobileNumber", para);
            return ds;
        }
        public DataSet GenerateTemporaryPassword()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",Mobile),
                new SqlParameter("@UserId",FK_UserId),
                new SqlParameter("@TempPassword",TempPassword)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateTemporaryPassword", para);
            return ds;
        }
        public DataSet ValidateTemporaryPassword()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@UserId",FK_UserId),
                new SqlParameter("@TempPassword",TempPassword)
            };
            DataSet ds = DBHelper.ExecuteQuery("ValidateTemporaryPassword", para);
            return ds;
        }
        public DataSet ResetPassword()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@NewPassword",NewPassword),
                new SqlParameter("@TempPassword",TempPassword)
            };
            DataSet ds = DBHelper.ExecuteQuery("ResetPassword", para);
            return ds;
        }
        public DataSet CheckMobileNo()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",MobileNo)
            };
            DataSet ds = DBHelper.ExecuteQuery("CheckMobileNoWeb", para);
            return ds;
        }
        public DataSet SignUp_Web()
        {
            SqlParameter[] para = {

                                   new SqlParameter("@SponsorId",SponsorId),
                                   new SqlParameter("@FirstName",FirstName),
                                   new SqlParameter("@LastName",LastName),
                                    new SqlParameter("@MobileNo",MobileNo),
                                    new SqlParameter("@RegistrationBy",RegistrationBy),
                                     new SqlParameter("@Gender",Gender),
                                     new SqlParameter("@PinCode",PinCode),
                                     new SqlParameter("@Leg",Leg),
                                     new SqlParameter("@Password",Password),
                                     new SqlParameter("@OTP",OTP),
                                     new SqlParameter("@Email",Email),
                                     new SqlParameter("@UnderPlaceId",UnderPlaceId)
                                   };
            DataSet ds = DBHelper.ExecuteQuery("Registration_New_Web", para);
            return ds;
        }
        public DataSet CheckOTP()
        {
            SqlParameter[] para = {
                new SqlParameter("@MobileNo",MobileNo),
                new SqlParameter("@OTP",OTP)
            };
            DataSet ds = DBHelper.ExecuteQuery("CheckOTPForRegistration", para);
            return ds;
        }
        public DataSet loadHeaderMenu()
        {
            SqlParameter[] para = {
                                new SqlParameter("@PK_AdminId", Pk_AdminId),
                                 new SqlParameter("@UserType", UserType)
            };

            DataSet ds = DBHelper.ExecuteQuery("GetMenuUserWise", para);
            return ds;
        }
        public static Home GetMenus(string Pk_AdminId, string UserType)
        {
            Home model = new Home();
            List<Home> lstmenu = new List<Home>();
            List<Home> lstsubmenu = new List<Home>();

            model.Pk_AdminId = Pk_AdminId;
            model.UserType = UserType;
            DataSet dsHeader = model.loadHeaderMenu();
            if (dsHeader != null && dsHeader.Tables.Count > 0)
            {
                if (dsHeader.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsHeader.Tables[0].Rows)
                    {
                        Home obj = new Home();

                        obj.MenuId = r["PK_FormTypeId"].ToString();
                        obj.MenuName = r["FormType"].ToString();
                        obj.Icon = r["Icon"].ToString();
                        obj.Url = r["Url"].ToString();
                        lstmenu.Add(obj);
                    }

                    model.lstMenu = lstmenu;
                    foreach (DataRow r in dsHeader.Tables[1].Rows)
                    {
                        Home obj = new Home();
                        obj.Url = r["Url"].ToString();
                        obj.MenuId = r["FK_FormTypeId"].ToString();
                        obj.SubMenuId = r["PK_FormId"].ToString();
                        obj.SubMenuName = r["FormName"].ToString();
                        lstsubmenu.Add(obj);
                    }

                    model.lstsubmenu = lstsubmenu;

                }


            }
            return model;

        }
        public DataSet GetNFCAllotmentStatus()
        {
            SqlParameter[] para = { new SqlParameter("@Code", Code) };
            DataSet ds = DBHelper.ExecuteQuery("GetNFCAllotmentStatus", para);
            return ds;
        }
    }
    public class BankDetails
    {
        public string bank { get; set; }
        public string IFSC { get; set; }
        public string MICR { get; set; }
        public string branch { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string State { get; set; }
    }
}