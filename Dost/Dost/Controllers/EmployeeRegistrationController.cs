using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dost.Models;
using System.Data;
using System.Data.SqlClient;
using Dost.Filter;

namespace Dost.Controllers
{
    public class EmployeeRegistrationController : AdminBaseController
    {
        // GET: EmployeeRegistration
        #region Employee
        public ActionResult EmployeeRegistration(string Id)
        {
            EmployeeRegistrations emp = new EmployeeRegistrations();
            if (Id != null)
            {
                emp.PkAdminID = Id;
                DataSet ds = emp.GetEmployeeData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    emp.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    emp.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    emp.Mobile = ds.Tables[0].Rows[0]["Contact"].ToString();
                    emp.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    emp.EducationQualififcation = ds.Tables[0].Rows[0]["EducationQualifiacation"].ToString();
                    emp.PkAdminID = ds.Tables[0].Rows[0]["Pk_AdminId"].ToString();
                }
            }


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

            return View(emp);
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
                    Objload.PkAdminID= dr["Pk_AdminId"].ToString();

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

        [HttpPost]
        [ActionName("EmployeeRegistration")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult RegisterUpdate(EmployeeRegistrations model)
        {
            model.CreatedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = model.UpdateRegistration();
            if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                @TempData["EmployeeRegistration"] = "Registration Update Successfull";
            }
            else
            {
                @TempData["EmployeeRegistration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return RedirectToAction("EmployeeDetails", "EmployeeRegistration");
        }

        public ActionResult DeleteRegistration(string ID)
        {
            EmployeeRegistrations model = new EmployeeRegistrations();
            model.PkAdminID = ID;
            model.CreatedBy= Session["Pk_AdminId"].ToString();
            DataSet ds = model.DeleteRegistration();
            if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
            {
                 @TempData["EmployeeRegistration"] = "Registration Delete Successfull";
            }
            else
            {
                @TempData["EmployeeRegistration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }
            return RedirectToAction("EmployeeDetails", "EmployeeRegistration");
        }

    }
   
}