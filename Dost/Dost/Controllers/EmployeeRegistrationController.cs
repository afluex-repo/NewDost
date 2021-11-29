using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dost.Models;
using System.Data;
using System.Data.SqlClient;
namespace Dost.Controllers
{
    public class EmployeeRegistrationController : AdminBaseController
    {
        // GET: EmployeeRegistration
        #region Employee
        public ActionResult EmployeeRegistration()
        {



            #region ddlUserType
            Common obj = new Common();
            List<SelectListItem> ddlUserType = new List<SelectListItem>();
            DataSet ds11 = obj.BindUserTypeForRegistration();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                { ddlUserType.Add(new SelectListItem { Text = r["UserType"].ToString(), Value = r["PK_UserTypeId"].ToString() }); }
            }

            ViewBag.ddlUserType = ddlUserType;
            #endregion

            return View();
        }
        public ActionResult EmployeeDetails()
        {

            List<EmployeeRegistrations> lst = new List<EmployeeRegistrations>();
            EmployeeRegistrations emp = new EmployeeRegistrations();
            DataSet ds = emp.GetEmployeeData();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    EmployeeRegistrations Objload = new EmployeeRegistrations();
                    Objload.Name = dr["Name"].ToString();
                    Objload.LoginId = dr["LoginId"].ToString();
                    Objload.Mobile = dr["Contact"].ToString();
                    Objload.Email = dr["Email"].ToString();
                    Objload.EducationQualififcation = dr["EducationQualifiacation"].ToString();

                    lst.Add(Objload);
                }
                emp.lstemp = lst;
            }
            return View(emp);
        }
        public ActionResult SaveEmployeeRegistration(string Name, string Mobile, string Email, string Qualification, string Fk_UserTypeId)
        {

            #region ddlUserType
            Common obj = new Common();
            List<SelectListItem> ddlUserType = new List<SelectListItem>();
            DataSet ds11 = obj.BindUserTypeForRegistration();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                { ddlUserType.Add(new SelectListItem { Text = r["UserType"].ToString(), Value = r["PK_UserTypeId"].ToString() }); }
            }

            ViewBag.ddlUserType = ddlUserType;
            #endregion
            EmployeeRegistrations objregi = new EmployeeRegistrations();
            try
            {

                objregi.Name = Name;
                objregi.Mobile = Mobile;
                objregi.Email = Email;

                objregi.DOB = string.IsNullOrEmpty(objregi.DOB) ? null : Common.ConvertToSystemDate(objregi.DOB, "dd-MM-yyyy");
                objregi.EducationQualififcation = Qualification;

                objregi.Fk_UserTypeId = Fk_UserTypeId;
                objregi.CreatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = objregi.SaveEmpoyeeData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        objregi.Message = "Employee Registration successfully";
                    }
                    else
                    {
                        objregi.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(objregi, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex) { }
            return Json(objregi, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}