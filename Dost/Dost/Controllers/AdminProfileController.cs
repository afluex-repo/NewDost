using Dost.Filter;
using Dost.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dost.Controllers
{
    public class AdminProfileController : AdminBaseController
    {
        public ActionResult AssociateList()
        {
            Reports model = new Reports();
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.AssociateStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            List<Reports> lst = new List<Reports>();
            //model.FromDate = DateTime.Now.Date.AddDays(-3).ToString("dd/MM/yyyy");
            //model.ToDate = DateTime.Now.Date.AddDays(0).ToString("dd/MM/yyyy");
            DataSet ds = model.GetAssociateList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());
                    obj.Email = (r["Email"].ToString());
                    obj.SponsorId = (r["SponsorId"].ToString());
                    obj.SponsorName = (r["SponsorName"].ToString());
                    obj.isBlocked = (r["isBlocked"].ToString());
                    obj.Status = r["MemberStatus"].ToString();
                    obj.PermanentDate = r["ActivationDate"].ToString();
                    lst.Add(obj);
                }
                model.lstassociate = lst;


            }
            return View(model);
        }
        [HttpPost]
        [ActionName("AssociateList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AssociateListBy(Reports model)
        {
            List<Reports> lst = new List<Reports>();

            DataSet ds = model.GetAssociateList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());
                    obj.Email = (r["Email"].ToString());
                    obj.SponsorId = (r["SponsorId"].ToString());
                    obj.SponsorName = (r["SponsorName"].ToString());
                    obj.isBlocked = (r["isBlocked"].ToString());
                    obj.Status = r["MemberStatus"].ToString();
                    obj.PermanentDate = r["ActivationDate"].ToString();
                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.AssociateStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            return View(model);
        }

        public ActionResult GetSponserDetails(string ReferBy)
        {
            Common obj = new Common();
            obj.ReferBy = ReferBy;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();

                obj.Result = "Yes";

            }
            else { obj.Result = "Invalid SponsorId"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateAssociateProfile(string LoginID)
        {
            #region ddlgender
            List<SelectListItem> ddlgender = Common.BindGender();
            ViewBag.ddlgender = ddlgender;
            #endregion ddlgender
            #region Relation
            List<SelectListItem> Relation = Common.BindRealation();
            ViewBag.Relation = Relation;
            #endregion
            Profile obj = new Profile();
            obj.LoginId = LoginID;

            DataSet ds = obj.GetUserProfile();

            if (ds != null && ds.Tables.Count > 0)
            {
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.EmailId = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.AccountNumber = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                obj.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();
                obj.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                obj.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                obj.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                obj.Relation = ds.Tables[0].Rows[0]["GaurdianRelation"].ToString();
                obj.Gender = ds.Tables[0].Rows[0]["Gender"].ToString();
                obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                obj.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.RealtionName = ds.Tables[0].Rows[0]["GaurdianName"].ToString();
                obj.AccountHolder = ds.Tables[0].Rows[0]["BankHolderName"].ToString();
                obj.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                obj.UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString();
                obj.PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString();
                obj.AadharImage = ds.Tables[0].Rows[0]["AdharImage"].ToString();
                obj.DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString();
                // return View(obj);
            }
            return View(obj);
        }

        [HttpPost]
        [ActionName("UpdateAssociateProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateAssociateProfile(IEnumerable<HttpPostedFileBase> postedAadhar, IEnumerable<HttpPostedFileBase> postedPan, IEnumerable<HttpPostedFileBase> postedPassbook, Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                foreach (var file in postedAadhar)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        obj.AadharImage = "/KYCDocuments/" + Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                        file.SaveAs(System.IO.Path.Combine(Server.MapPath(obj.AadharImage)));
                    }
                }
                foreach (var file in postedPan)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                        file.SaveAs(System.IO.Path.Combine(Server.MapPath(obj.PanImage)));
                    }
                }
                foreach (var file in postedPassbook)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        obj.DocumentImage = "/KYCDocuments/" + Guid.NewGuid() + System.IO.Path.GetExtension(file.FileName);
                        file.SaveAs(System.IO.Path.Combine(Server.MapPath(obj.DocumentImage)));
                    }
                }
                obj.Password = Crypto.Encrypt(obj.Password);
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateAssociateProfileByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfilebyadmin"] = "Profile updated successfully..";
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfilebyadmin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfilebyadmin"] = ex.Message;
                FormName = "AssociateList";
                Controller = "AdminProfile";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult BlockAssociate(Profile obj, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.BlockAssociate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "Associate Blocked";
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                    }
                    else
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
                FormName = "AssociateList";
                Controller = "AdminProfile";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult UnblockAssociate(Profile obj, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UnblockAssociate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "Associate Blocked";
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                    }
                    else
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "AdminProfile";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
                FormName = "AssociateList";
                Controller = "AdminProfile";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult AdminProfile()
        {
            Profile obj = new Profile();
            obj.PK_UserID = Session["Pk_AdminId"].ToString();
            DataSet ds = obj.GetAdminProfile();


            if (ds != null && ds.Tables.Count > 0)
            {
                obj.FirstName = ds.Tables[0].Rows[0]["Name"].ToString();
                obj.LoginId = ds.Tables[0].Rows[0]["LoginID"].ToString();
                obj.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                obj.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();

            }
            return View(obj);
        }

        [HttpPost]
        [ActionName("AdminProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateAdminProfile(Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UpdateAdminProfile();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Profile updated successfully..";
                        FormName = "AdminProfile";
                        Controller = "Profile";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AdminProfile";
                        Controller = "Profile";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "AdminProfile";
                Controller = "Profile";
            }
            return RedirectToAction(FormName, Controller);
        }

        public ActionResult GetStateCity(string PinCode)
        {
            Common obj = new Common();
            obj.PinCode = PinCode;
            DataSet ds = obj.GetStateCity();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.Result = "1";
            }
            else
            {
                obj.Result = "Invalid PinCode";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WelcomeLetter(string id)
        {
            Reports model = new Reports();
            model.LoginId = id;
            DataSet ds = model.GetAssociateList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.RefNo = ds.Tables[0].Rows[0]["RefNo"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.DATE = ds.Tables[0].Rows[0]["DATE"].ToString();
            }
            return View();
        }
        public ActionResult ActivateUser(string FK_UserID)
        {
            Profile model = new Profile();
            try
            {
                model.Fk_UserId = FK_UserID;
                model.ProductID = "1";
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.ActivateUserByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User activated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminProfile");
        }
        public ActionResult DeactivateUser(string lid)
        {
            Profile model = new Profile();
            try
            {
                model.LoginId = lid;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.DeactivateUserByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User deactivated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminProfile");
        }





    }
}
