using Dost.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using Dost.Filter;
namespace Dost.Controllers
{
    public class AccountsController : AdminBaseController
    {
        public ActionResult Topup()
        {
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[1].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            return View();
        }
        public ActionResult FillAmount(string ProductId)
        {
            Wallet obj = new Wallet();
            obj.Package = ProductId;
            DataSet ds = obj.BindPriceByProduct();
            if (ds.Tables != null && ds.Tables[1].Rows.Count > 0)
            {
                obj.Amount = ds.Tables[1].Rows[0]["ProductPrice"].ToString();
            }
            else { }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetMemberName(string LoginId)
        {
            Common obj = new Common();
            obj.ReferBy = LoginId;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {


                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();

                obj.Result = "Yes";

            }
            else { obj.Result = "No"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TopUpByAdmin(Wallet obj)
        {
            try
            {
                obj.TopUpDate = Common.ConvertToSystemDate(obj.TopUpDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.CouponTopupByAdmin();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Topup"] = "User Activated  By Coupon  successfully";
                    }
                    else
                    {
                        TempData["Topup"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["Topup"] = ex.Message;
            }
            return RedirectToAction("Topup", "Accounts");
        }

        #region ApprovePayoutRequest
        public ActionResult ApprovepayoutRequest()
        {
            Wallet objewallet = new Wallet();


            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.GetPayoutRequest();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Amount = dr["Amount"].ToString();
                    Objload.LoginId = dr["LoginId"].ToString();
                    Objload.DisplayName = dr["Name"].ToString();
                    Objload.Status = dr["Status"].ToString();
                    Objload.Pk_RequestId = dr["Pk_RequestId"].ToString();
                    Objload.AddedOn = dr["RequestedDate"].ToString();
                    lst.Add(Objload);
                }
                objewallet.lstPayoutrequest = lst;
            }
            return View(objewallet);
        }
        public ActionResult ApproveRequest(string Id)
        {
            try
            {
                Wallet obj = new Wallet();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Pk_RequestId = Id;
                obj.Status = "Approved";
                DataSet ds = obj.ApprovePayoutRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["ApproveRequest"] = "Payout Approved Successfully";
                    }
                    else
                    {
                        TempData["ApproveRequest"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["ApproveRequest"] = ex.Message;
            }
            return RedirectToAction("ApprovepayoutRequest", "Accounts");
        }
        public ActionResult DeclineRequest(string Id)
        {
            try
            {
                Wallet obj = new Wallet();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Pk_RequestId = Id;
                obj.Status = "Declined";
                DataSet ds = obj.ApprovePayoutRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["ApproveRequest"] = "Payout Declined Successfully";
                    }
                    else
                    {
                        TempData["ApproveRequest"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["ApproveRequest"] = ex.Message;
            }
            return RedirectToAction("ApprovepayoutRequest", "Accounts");
        }
        #endregion ApprovePayoutRequest

        #region ApproveEwalletRequest
        public ActionResult ApproveEwalletRequest()
        {
            Wallet objewallet = new Wallet();


            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.GetEWalletRequest();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Pk_RequestId = dr["Pk_RequestId"].ToString();
                    Objload.LoginId = dr["LoginId"].ToString();
                    Objload.DisplayName = dr["Name"].ToString();
                    Objload.Amount = dr["Amount"].ToString();
                    Objload.RequestCode = dr["RequestCode"].ToString();
                    Objload.PaymentMode = dr["PaymentMode"].ToString();
                    Objload.Status = dr["Status"].ToString();
                    Objload.DocumentImg = dr["ImageURL"].ToString();
                    Objload.BankName = dr["BankName"].ToString();
                    Objload.BankBranch = dr["BankBranch"].ToString();
                    Objload.DDChequeNo = dr["ChequeDDNo"].ToString();
                    Objload.DDChequeDate = dr["ChequeDDDate"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    lst.Add(Objload);
                }
                objewallet.WalletRequestList = lst;
            }
            return View(objewallet);
        }
        public ActionResult ApproveEwallet(string Id)
        {
            try
            {
                Wallet obj = new Wallet();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Pk_RequestId = Id;
                obj.Status = "Approved";
                DataSet ds = obj.ApproveEwalletRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["EApproveRequest"] = "Wallet Request Approved Successfully";
                    }
                    else
                    {
                        TempData["EApproveRequest"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["EApproveRequest"] = ex.Message;
            }
            return RedirectToAction("ApproveEwalletRequest", "Accounts");
        }
        public ActionResult DeclineEwwalletRequest(string Id)
        {
            try
            {
                Wallet obj = new Wallet();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Pk_RequestId = Id;
                obj.Status = "Declined";
                DataSet ds = obj.ApproveEwalletRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["EApproveRequest"] = "Digiwallet Request Declined Successfully";
                    }
                    else
                    {
                        TempData["EApproveRequest"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["EApproveRequest"] = ex.Message;
            }
            return RedirectToAction("ApproveEwalletRequest", "Accounts");
        }
        #endregion ApproveEwalletRequest

        #region ApproveFranchiseewalletRequest
        public ActionResult ApproveFranchiseewalletRequest()
        {
            Wallet objewallet = new Wallet();


            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.GetFranchiseeWalletRequest();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Pk_RequestId = dr["Pk_RequestId"].ToString();
                    Objload.LoginId = dr["LoginId"].ToString();
                    Objload.DisplayName = dr["Name"].ToString();
                    Objload.Amount = dr["Amount"].ToString();
                    Objload.RequestCode = dr["RequestCode"].ToString();
                    Objload.PaymentMode = dr["PaymentMode"].ToString();
                    Objload.Status = dr["Status"].ToString();
                    Objload.DocumentImg = dr["ImageURL"].ToString();
                    Objload.BankName = dr["BankName"].ToString();
                    Objload.BankBranch = dr["BankBranch"].ToString();
                    Objload.DDChequeNo = dr["ChequeDDNo"].ToString();
                    Objload.DDChequeDate = dr["ChequeDDDate"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    lst.Add(Objload);
                }
                objewallet.WalletRequestList = lst;
            }
            return View(objewallet);
        }
        public ActionResult ApproveFranchiseewallet(string Id)
        {
            try
            {
                Wallet obj = new Wallet();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Pk_RequestId = Id;
                obj.Status = "Approved";
                DataSet ds = obj.ApproveFranchiseewalletRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["EApproveRequest"] = "Franchisee Wallet Request Approved Successfully";
                    }
                    else
                    {
                        TempData["EApproveRequest"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["EApproveRequest"] = ex.Message;
            }
            return RedirectToAction("ApproveFranchiseewalletRequest", "Accounts");
        }
        public ActionResult DeclineFranchiseewalletRequest(string Id)
        {
            try
            {
                Wallet obj = new Wallet();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Pk_RequestId = Id;
                obj.Status = "Declined";
                DataSet ds = obj.ApproveFranchiseewalletRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["EApproveRequest"] = "Franchisee wallet Request Declined Successfully";
                    }
                    else
                    {
                        TempData["EApproveRequest"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["EApproveRequest"] = ex.Message;
            }
            return RedirectToAction("ApproveFranchiseewalletRequest", "Accounts");
        }
        #endregion ApproveFranchiseewalletRequest

        #region ReTopUp

        public ActionResult RetopUp()
        {
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[1].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            Wallet model = new Wallet();
            model.Package = "4";
            return View(model);
        }

        public ActionResult RetopUpAction(Wallet obj)
        {
            try
            {
                obj.TopUpDate = Common.ConvertToSystemDate(obj.TopUpDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Package = "4";

                DataSet ds = obj.ReTopup();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Class"] = "alert alert-success";
                        TempData["Topup"] = "Re-TopUp Done successfully";
                    }
                    else
                    {
                        TempData["Class"] = "alert alert-danger";
                        TempData["Topup"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["Class"] = "alert alert-danger";
                TempData["Topup"] = ex.Message;
            }
            return RedirectToAction("RetopUp", "Accounts");
        }
        #endregion

        #region ProductTopUp
        public ActionResult ProductTopUp()
        {
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[2].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[2].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            return View();
        }

        public ActionResult TopUpByAdminProduct(Wallet obj)
        {
            try
            {
                obj.TopUpDate = Common.ConvertToSystemDate(obj.TopUpDate, "dd/MM/yyyy");
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.TopUpIdByAdminProduct();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["ProductTopup"] = "TopUp Done successfully";
                    }
                    else
                    {
                        TempData["ProductTopup"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["ProductTopup"] = ex.Message;
            }
            return RedirectToAction("ProductTopUp", "Accounts");
        }



        #endregion ProductTopUp
        #region CouponDetails
        public ActionResult GetCouponDetails(string CouponCode)
        {
            Wallet obj = new Wallet();
            obj.ReferBy = CouponCode;
            DataSet ds = obj.GetCouponDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    obj.CouponPrice = ds.Tables[0].Rows[0]["CouponPrice"].ToString();
                    obj.CouponCode = ds.Tables[0].Rows[0]["CouponCode"].ToString();
                    obj.EventName = ds.Tables[0].Rows[0]["usedFor"].ToString();
                    obj.Result = "Yes";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    obj.Result = "No";
                    obj.ErrorMessage = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }


            }
            else
            {

            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FranchiseWalletLedger()
        {
            return View();
        }
        [HttpPost]
        [ActionName("FranchiseWalletLedger")]
        [OnAction(ButtonName = "Search")]
        public ActionResult FranchiseeWalletLedger(Wallet objewallet)
        {
         
            List<Wallet> lst = new List<Wallet>();
            objewallet.FromDate = string.IsNullOrEmpty(objewallet.FromDate) ? null : Common.ConvertToSystemDate(objewallet.FromDate, "dd/MM/yyyy");
            objewallet.ToDate = string.IsNullOrEmpty(objewallet.ToDate) ? null : Common.ConvertToSystemDate(objewallet.ToDate, "dd/MM/yyyy");
            DataSet ds = objewallet.GetFranchiseeWalletLedgerByAdmin();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.LoginId = dr["LoginId"].ToString();
                    Objload.UserCode = dr["UserCode"].ToString();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["CurrentDate"].ToString();
                    Objload.EwalletBalance = dr["Balance"].ToString();

                    lst.Add(Objload);
                }
                objewallet.lstewalletledger = lst;
            }
            return View(objewallet);
        }
        public ActionResult AdminDigiCoinLedger()
        {
            return View();
        }
        [HttpPost]
        [ActionName("AdminDigiCoinLedger")]
        [OnAction(ButtonName = "Search")]
        public ActionResult DigiCoinLedgerByAdmin(Wallet objewallet)
        {
           
            List<Wallet> lst = new List<Wallet>();
            objewallet.FromDate = string.IsNullOrEmpty(objewallet.FromDate) ? null : Common.ConvertToSystemDate(objewallet.FromDate, "dd/MM/yyyy");
            objewallet.ToDate = string.IsNullOrEmpty(objewallet.ToDate) ? null : Common.ConvertToSystemDate(objewallet.ToDate, "dd/MM/yyyy");
            DataSet ds = objewallet.GetDDCoinWalletLedgerByAdmin();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["CurrentDate"].ToString();
                    Objload.EwalletBalance = dr["Balance"].ToString();

                    lst.Add(Objload);
                }
                objewallet.lstewalletledger = lst;
            }
            return View(objewallet);
        }
        #endregion
    }
}
