using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dost.Models;
using System.Data;
using System.IO;
using Dost.Filter;

namespace Dost.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult UserDashboard()
        {
            Dashboard obj = new Dashboard();
            List<Dashboard> lstinvestment = new List<Dashboard>();
            obj.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetAssociateDashboard();
            Dashboard model = new Dashboard();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.TotalDownline = ds.Tables[0].Rows[0]["TotalDownline"].ToString();
                ViewBag.TotalDirects = ds.Tables[0].Rows[0]["TotalDirect"].ToString();
                ViewBag.PayoutWalletBalance = ds.Tables[0].Rows[0]["PayoutWalletBalance"].ToString();
                ViewBag.TotalPayout = ds.Tables[0].Rows[0]["TotalPayout"].ToString();
                ViewBag.TotalDeduction = ds.Tables[0].Rows[0]["TotalDeduction"].ToString();
                ViewBag.TotalAdvance = ds.Tables[0].Rows[0]["TotalAdvance"].ToString();
                ViewBag.TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString();
                ViewBag.TotalInActive = ds.Tables[0].Rows[0]["TotalInActive"].ToString();
                ViewBag.Recognition = ds.Tables[0].Rows[0]["Recoginition"].ToString();
                ViewBag.TotalReseller = ds.Tables[0].Rows[0]["TotalReseller"].ToString();
                ViewBag.TotalDistributor = ds.Tables[0].Rows[0]["TotalDistributor"].ToString();
                ViewBag.TotalAirDrop = ds.Tables[0].Rows[0]["TotalAirDrop"].ToString();
                ViewBag.TotalIncome = ds.Tables[0].Rows[0]["TotalIncome"].ToString();
            }
            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                ViewBag.PaidBusinessLeft = ds.Tables[2].Rows[0]["PaidBusinessLeft"].ToString();
                ViewBag.PaidBusinessRight = ds.Tables[2].Rows[0]["PaidBusinessRight"].ToString();
                ViewBag.TotalBusinessLeft = ds.Tables[2].Rows[0]["TotalBusinessLeft"].ToString();
                ViewBag.TotalBusinessRight = ds.Tables[2].Rows[0]["TotalBusinessRight"].ToString();
                ViewBag.CarryLeft = ds.Tables[2].Rows[0]["CarryLeft"].ToString();
                ViewBag.CarryRight = ds.Tables[2].Rows[0]["CarryRight"].ToString();
                ViewBag.PaidBusinessLeftNew = ds.Tables[2].Rows[0]["PaidBusinessLeft"].ToString();
                ViewBag.PaidBusinessRightNew = ds.Tables[2].Rows[0]["PaidBusinessRight"].ToString();
                ViewBag.TotalBusinessLeftNew = ds.Tables[2].Rows[0]["TotalBusinessLeft"].ToString();
                ViewBag.TotalBusinessRightNew = ds.Tables[2].Rows[0]["TotalBusinessRight"].ToString();
                ViewBag.CarryLeftNew = ds.Tables[2].Rows[0]["CarryLeft"].ToString();
                ViewBag.CarryRightNew = ds.Tables[2].Rows[0]["CarryRight"].ToString();
            }

            if (ds != null && ds.Tables[3].Rows.Count > 0)
            {
                ViewBag.LoginId = ds.Tables[3].Rows[0]["LoginId"].ToString();
                ViewBag.DisplayName = ds.Tables[3].Rows[0]["Name"].ToString();
                ViewBag.JoiningDate = ds.Tables[3].Rows[0]["JoiningDate"].ToString();
                model.SponserId = ds.Tables[3].Rows[0]["SponsorId"].ToString();
                model.SponsorName = ds.Tables[3].Rows[0]["SponsorName"].ToString();
                model.Leg = ds.Tables[3].Rows[0]["Leg"].ToString();
                model.Fk_SponsorId1 = ds.Tables[3].Rows[0]["FK_SponsorId"].ToString();
                Session["SponserId"] = ds.Tables[3].Rows[0]["SponsorId"].ToString();
                Session["SponsorId"] = ds.Tables[3].Rows[0]["SponsorId"].ToString();
                model.IsAcceptanceTNC = ds.Tables[3].Rows[0]["IsAcceptanceTNC"].ToString();
                Session["IsAcceptanceTNC"] = ds.Tables[3].Rows[0]["IsAcceptanceTNC"].ToString();
            }
            if (ds != null && ds.Tables[4].Rows.Count > 0)
            {

                ViewBag.PaidBusinessLeftNew = ds.Tables[4].Rows[0]["PaidBusinessLeft"].ToString();
                ViewBag.PaidBusinessRightNew = ds.Tables[4].Rows[0]["PaidBusinessRight"].ToString();
                ViewBag.TotalBusinessLeftNew = ds.Tables[4].Rows[0]["TotalBusinessLeft"].ToString();
                ViewBag.TotalBusinessRightNew = ds.Tables[4].Rows[0]["TotalBusinessRight"].ToString();
                ViewBag.CarryLeftNew = ds.Tables[4].Rows[0]["CarryLeft"].ToString();
                ViewBag.CarryRightNew = ds.Tables[4].Rows[0]["CarryRight"].ToString();
            }
            if (ds.Tables != null && ds.Tables.Count > 1 && ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
            {
                Session["UserNFCCode"] = Crypto.EncryptNFC(ds.Tables[5].Rows[0]["Code"].ToString());
            }
            else
            {
                Session["UserNFCCode"] = "na";
            }
            #region Messages

            model.Fk_UserId = Session["Pk_UserId"].ToString();

            List<Dashboard> lst1 = new List<Dashboard>();

            DataSet ds11 = model.GetAllMessagesfordashboard();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Dashboard Obj = new Dashboard();
                    Obj.Pk_MessageId = r["Pk_MessageId"].ToString();
                    Obj.Fk_UserId = r["Fk_UserId"].ToString();
                    Obj.MemberName = r["Name"].ToString();
                    Obj.MessageTitle = r["MessageTitle"].ToString();
                    Obj.AddedOn = r["AddedOn"].ToString();
                    Obj.Message = r["Message"].ToString();
                    Obj.cssclass = r["cssclass"].ToString();
                    Obj.ProfilePic = r["ProfilePic"].ToString();
                    lst1.Add(Obj);
                }
                model.lstmessages = lst1;
            }
            #endregion Messages

            #region Investment
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    Dashboard Obj = new Dashboard();
                    Obj.ProductName = r["ProductName"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.Status = r["Status"].ToString();

                    lstinvestment.Add(Obj);
                }
                model.lstinvestment = lstinvestment;
            }
            #endregion Investment
            return View(model);
        }
        public ActionResult UserProfile()
        {
            Profile objprofile = new Profile();

            List<Profile> lstprofile = new List<Profile>();
            objprofile.LoginId = Session["LoginId"].ToString();
            Profile obj = new Profile();
            DataSet ds = objprofile.GetUserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.MiddleName = ds.Tables[0].Rows[0]["MiddleName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.EmailId = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                obj.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                obj.AccountNumber = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();
                obj.ProfilePicture = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                obj.Summary = ds.Tables[0].Rows[0]["Summary"].ToString();
                obj.AccountHolderName= ds.Tables[0].Rows[0]["BankHolderName"].ToString();
                obj.DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString();
                obj.UPIStatus = "Status : " + ds.Tables[0].Rows[0]["UPIStatus"].ToString();
                obj.PaytmStatus = "Status : " + ds.Tables[0].Rows[0]["PaytmStatus"].ToString();
                obj.PhonePeStatus = "Status : " + ds.Tables[0].Rows[0]["PhonePeStatus"].ToString();
                obj.GooglePayStatus = "Status : " + ds.Tables[0].Rows[0]["GooglePayStatus"].ToString();
                obj.UPI = ds.Tables[0].Rows[0]["UPI"].ToString();
                obj.PhonePe = ds.Tables[0].Rows[0]["PhonePe"].ToString();
                obj.Paytm = ds.Tables[0].Rows[0]["Paytm"].ToString();
                obj.GooglePay = ds.Tables[0].Rows[0]["GooglePay"].ToString();

            }
            obj.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet ds1 = obj.GetKYCDocuments();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                obj.AdharNumber = ds1.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.AdharImage = ds1.Tables[0].Rows[0]["AdharImage"].ToString();
                obj.AdharStatus = "Status : " + ds1.Tables[0].Rows[0]["AdharStatus"].ToString();
                obj.PanNumber = ds1.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.PanImage = ds1.Tables[0].Rows[0]["PanImage"].ToString();
                obj.PanStatus = "Status : " + ds1.Tables[0].Rows[0]["PanStatus"].ToString();
                obj.DocumentStatus = "Status : " + ds1.Tables[0].Rows[0]["DocumentStatus"].ToString();
                obj.MemberAccNo = ds1.Tables[1].Rows[0]["MemberAccNo"].ToString();
                obj.MemberBankName = ds1.Tables[1].Rows[0]["MemberBankName"].ToString();
                obj.IFSCCode = ds1.Tables[1].Rows[0]["IFSCCode"].ToString();
                obj.MemberBranch = ds1.Tables[1].Rows[0]["MemberBranch"].ToString();

            }
            return View(obj);
        }
        [HttpPost]
        [ActionName("UserProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePersonalInfo( Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.PK_UserID = Session["Pk_userId"].ToString();
                DataSet ds = obj.UpdatePersonalInfo();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Updated successfully..";
                        FormName = "UserProfile";
                        Controller = "User";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "UserProfile";
                Controller = "User";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("UserProfile")]
        [OnAction(ButtonName = "btnBankUpdate")]
        public ActionResult UpdateBankDetails(HttpPostedFileBase postedCheque, Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (postedCheque != null)
                {
                    obj.DocumentImage = "/images/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(postedCheque.FileName);
                    postedCheque.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));
                }
                obj.PK_UserID = Session["Pk_userId"].ToString();
                DataSet ds = obj.UpdateBankInfo();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Updated successfully..";
                        FormName = "UserProfile";
                        Controller = "User";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "UserProfile";
                Controller = "User";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("UserProfile")]
        [OnAction(ButtonName = "btnKYCUpdate")]
        public ActionResult KYCDocuments(IEnumerable<HttpPostedFileBase> postedAadhar, IEnumerable<HttpPostedFileBase> postedPan, Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                foreach (var file in postedAadhar)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        obj.AdharImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.AdharImage)));
                    }
                }
                foreach (var file in postedPan)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));
                    }
                }
           
                obj.Fk_UserId = Session["Pk_userId"].ToString();

                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                      
                        TempData["UpdateProfile"] = "Updated successfully..";
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "UserProfile";
                Controller = "User";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("UserProfile")]
        [OnAction(ButtonName = "btnWalletUpdate")]
        public ActionResult UpdateWalletinfo(Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.Fk_UserId = Session["Pk_userId"].ToString();
                DataSet ds = obj.UpdateWalletDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Updated successfully..";
                        FormName = "UserProfile";
                        Controller = "User";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "UserProfile";
                Controller = "User";
            }
            return RedirectToAction(FormName, Controller);
        }
    }
}