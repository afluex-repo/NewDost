using BusinessLayer;
using Dost.Filter;
using Dost.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Dost.Controllers
{
    public class WalletController : BaseController
    {
        // GET: Wallet
        public ActionResult Wallet()
        {
            Wallet obj = new Wallet();
            #region ddlpaymentmode
            List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            try
            {
                obj.LoginId = Session["LoginId"].ToString();
                ViewBag.DigiWalletBalance = "0";
                ViewBag.DigiCoin = "0";
                ViewBag.FranchiseeBalance = "0";
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj.GetpayoutBalance();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.PayoutBalance = ds.Tables[0].Rows[0]["Balance"].ToString();
                }
                DataSet dsBal = obj.GetDigiWalletBalance();
                if (dsBal != null && dsBal.Tables.Count > 0)
                {
                    if (dsBal.Tables[0].Rows.Count > 0)
                    {
                        ViewBag.DigiWalletBalance = string.Format("{0:0.00}", dsBal.Tables[0].Rows[0]["Balance"]);
                        obj.EwalletBalance = dsBal.Tables[0].Rows[0]["Balance"].ToString();
                        ViewBag.ActivationDate = dsBal.Tables[0].Rows[0]["JoiningDate"].ToString();
                        ViewBag.CardHolderName = dsBal.Tables[0].Rows[0]["BankHolderName"].ToString();
                    }
                    if (dsBal.Tables[1].Rows.Count > 0)
                    {
                        ViewBag.DigiCoin = dsBal.Tables[1].Rows[0]["Balance"].ToString();
                        ViewBag.DigiActivationDate = dsBal.Tables[0].Rows[0]["DigiJoiningDate"].ToString();
                        ViewBag.DigiCardHolderName = dsBal.Tables[0].Rows[0]["DigiBankHolderName"].ToString();
                    }
                    if (dsBal.Tables[2].Rows.Count > 0)
                    {
                        ViewBag.FranchiseeBalance = dsBal.Tables[2].Rows[0]["Balance"].ToString();
                    }
                }
                #region Walletledger
                List<Wallet> lst = new List<Wallet>();
                Dashboard model = new Dashboard();

                model.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet dsledger = model.EwalletLedger();
                if (dsledger.Tables != null && dsledger.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsledger.Tables[0].Rows)
                    {
                        Wallet Objload = new Wallet();
                        Objload.Narration = dr["Narration"].ToString();
                        Objload.DrAmount = dr["DrAMount"].ToString();
                        Objload.CrAmount = dr["CrAmount"].ToString();
                        Objload.AddedOn = dr["CurrentDate"].ToString();
                        Objload.EwalletBalance = dr["Balance"].ToString();

                        lst.Add(Objload);
                    }
                    obj.lstewalletledger = lst;
                }
                List<Dashboard> lstuser = new List<Dashboard>();
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet dsUser = obj.userlist();
                if (dsUser.Tables != null && dsUser.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsUser.Tables[0].Rows)
                    {
                        Dashboard model1 = new Dashboard();
                        model1.DisplayName = dr["Name"].ToString();
                        model1.ProfilePic = dr["Profile"].ToString();
                        model1.Mobile = dr["LoginId"].ToString();
                        lstuser.Add(model1);
                    }
                    obj.lstUser = lstuser;
                }
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
            return View(obj);
        }
        [HttpPost]
        [ActionName("Wallet")]
        [OnAction(ButtonName = "btnAddFund")]
        public ActionResult AddFund(Wallet obj, HttpPostedFileBase fileProfilePicture)
        {

            try
            {
                if (fileProfilePicture != null && fileProfilePicture.ContentLength > 0)
                {
                    obj.DocumentImg = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(fileProfilePicture.FileName);
                    fileProfilePicture.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImg)));
                }
                obj.LoginId = Session["Loginid"].ToString();
                obj.AddedBy = Session["Pk_UserId"].ToString();
                obj.DDChequeDate = string.IsNullOrEmpty(obj.DDChequeDate) ? null : Common.ConvertToSystemDate(obj.DDChequeDate, "dd/MM/yyyy");
                DataSet ds = obj.SaveEWalletRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Ewallet"] = "Wallet Requested Successfully.";
                        TempData["Wallet"] = "Yes";
                    }
                    else
                    {
                        TempData["Ewallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();


                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Ewallet"] = ex.Message;

            }
            return RedirectToAction("Wallet", "Wallet");
        }
        [HttpPost]
        [ActionName("Wallet")]
        [OnAction(ButtonName = "btnTransfer")]
        public ActionResult TransferEwallet(Wallet obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_UserId"].ToString();
                DataSet ds = obj.TransferEwalletToEwallet();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        obj.ReceiverMobile = ds.Tables[0].Rows[0]["ToMobileNo"].ToString();
                        obj.SenderMobile = ds.Tables[0].Rows[0]["FromMobileNo"].ToString();
                        obj.ReceiverName = ds.Tables[0].Rows[0]["ToName"].ToString();
                        obj.SenderName = ds.Tables[0].Rows[0]["FromName"].ToString();
                        obj.SenderEmail = ds.Tables[0].Rows[0]["FromEmail"].ToString();
                        obj.ReceiverEmail = ds.Tables[0].Rows[0]["ToEmail"].ToString();
                        TempData["Ewallet"] = "wallet fund transfered successfully.";
                        TempData["Wallet"] = "Yes";
                        try
                        {
                            string str = "Rs. " + obj.TransferAmount + " has been successfully transferred to the user " + obj.ReceiverName + " from your DOST Wallet. Thanks Team - DOST Inc.";
                            string str2 = "You have received Rs. " + obj.TransferAmount + " from the user " + obj.SenderName + " in your DOST Wallet. Team - DOST INC";
                            BLSMS.sendSMSUpdated(str, obj.SenderMobile);
                            BLSMS.sendSMSUpdated(str2, obj.ReceiverMobile);
                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            string Subject1 = "Wallet Transfer";
                            string Subject2 = "Fund Received";
                            string MailBody1 = "INR " + obj.Amount + " has been successfully transferred from your DOST wallet to user " + obj.ReceiverName + ".<br><br>For any issue please reach us through email or through DOST helpline.";
                            string MailBody2 = "INR " + obj.Amount + " has been successfully credited into your DOST wallet from the user " + obj.SenderName + "<br><br>For any issue please reach us through email or through DOST helpline.";
                            BLMail.SendTransactionMail(obj.SenderName, MailBody1, Subject1, obj.SenderEmail);
                            BLMail.SendTransactionMail(obj.ReceiverName, MailBody2, Subject2, obj.ReceiverEmail);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        TempData["Ewallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Ewallet"] = ex.Message;

            }
            return RedirectToAction("Wallet", "Wallet");

        }
        [HttpPost]
        [ActionName("Wallet")]
        [OnAction(ButtonName = "btnWithdraw")]
        public ActionResult SavePayoutRequest(Wallet obj)
        {
            try
            {

                obj.AddedBy = Session["Pk_UserId"].ToString();
                DataSet ds = obj.SavePayoutRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        TempData["Wallet"] = "Yes";
                        TempData["EWallet"] = "Withdrawl Request Save Successfully.";
                    }
                    else if (ds.Tables[0].Rows[0]["ErrorMessage"].ToString() == "Account Details are not yet updated. Please update your account details.")
                    {
                        TempData["EWallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                    else
                    {
                        TempData["EWallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["EWallet"] = ex.Message;

            }
            return RedirectToAction("Wallet", "Wallet");
        }
        public ActionResult QuickTransferEwallet(string Mobile, string Amount)
        {
            Wallet obj = new Wallet();
            try
            {
                obj.MobileNo = Mobile;
                obj.TransferAmount = Amount;
                obj.AddedBy = Session["Pk_UserId"].ToString();
                DataSet ds = obj.TransferEwalletToEwallet();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        obj.ReceiverMobile = ds.Tables[0].Rows[0]["ToMobileNo"].ToString();
                        obj.SenderMobile = ds.Tables[0].Rows[0]["FromMobileNo"].ToString();
                        obj.ReceiverName = ds.Tables[0].Rows[0]["ToName"].ToString();
                        obj.SenderName = ds.Tables[0].Rows[0]["FromName"].ToString();
                        obj.SenderEmail = ds.Tables[0].Rows[0]["FromEmail"].ToString();
                        obj.ReceiverEmail = ds.Tables[0].Rows[0]["ToEmail"].ToString();
                        obj.Result = "Yes";
                        try
                        {
                            string str = "Rs. " + obj.TransferAmount + " has been successfully transferred to the user " + obj.ReceiverName + " from your DOST Wallet. Thanks Team - DOST Inc.";
                            string str2 = "You have received Rs. " + obj.TransferAmount + " from the user " + obj.SenderName + " in your DOST Wallet. Team - DOST INC";
                            BLSMS.sendSMSUpdated(str, obj.SenderMobile);
                            BLSMS.sendSMSUpdated(str2, obj.ReceiverMobile);
                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            string Subject1 = "Wallet Transfer";
                            string Subject2 = "Fund Received";
                            string MailBody1 = "INR " + obj.TransferAmount + " has been successfully transferred from your DOST wallet to user " + obj.ReceiverName + ".<br><br>For any issue please reach us through email or through DOST helpline.";
                            string MailBody2 = "INR " + obj.TransferAmount + " has been successfully credited into your DOST wallet from the user " + obj.SenderName + "<br><br>For any issue please reach us through email or through DOST helpline.";
                            BLMail.SendTransactionMail(obj.SenderName, MailBody1, Subject1, obj.SenderEmail);
                            BLMail.SendTransactionMail(obj.ReceiverName, MailBody2, Subject2, obj.ReceiverEmail);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
              obj.Result   = ex.Message;

            }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetMemberNameByMobileNo(string LoginId)
        {
            Common obj = new Common();
            obj.ReferBy = LoginId;
            DataSet ds = obj.GetMemberDetailsByMobile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "No"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Statement()
        {
            Wallet objewallet = new Wallet();
            List<Wallet> lst = new List<Wallet>();
            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = objewallet.EwalletLedger();
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
        [HttpPost]
        public ActionResult Statement(Wallet objewallet)
        {
            objewallet.FromDate = string.IsNullOrEmpty(objewallet.FromDate) ? null : Common.ConvertToSystemDate(objewallet.FromDate, "dd/MM/yyyy");
            objewallet.ToDate = string.IsNullOrEmpty(objewallet.ToDate) ? null : Common.ConvertToSystemDate(objewallet.ToDate, "dd/MM/yyyy");
            objewallet.Fk_UserId = Session["Pk_UserId"].ToString();
            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.EwalletLedger();
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

        //public BankDetails GetBankDetails(string IFSC)
        //{
        //    try
        //    {
        //        using (var client = new WebClient())
        //        {
        //            BankDetails _bank = new BankDetails();
        //            client.Headers.Add("DY-X-Authorization: 8fe69b30f07a07692796fbd751d625a7d0920d1c");
        //            var result = client.DownloadString("https://ifsc.datayuge.com/api/v1/" + IFSC.Trim());
        //            var BankResult = JsonConvert.DeserializeObject<BankDetails>(result);
        //            _bank.bank = BankResult.bank;
        //            _bank.IFSC = BankResult.IFSC;
        //            _bank.MICR = BankResult.MICR;
        //            _bank.branch = BankResult.branch;
        //            _bank.Address = BankResult.Address;
        //            _bank.Contact = BankResult.Contact;
        //            _bank.City = BankResult.City;
        //            _bank.District = BankResult.District;
        //            _bank.State = BankResult.State;
        //            return _bank;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        #region
        public ActionResult ApplyFiterForWalletActivity(string TypeDate)
        {
            Dashboard model = new Dashboard();
            List<Wallet> lst = new List<Wallet>();
            model.Fk_UserId = Session["Pk_UserId"].ToString();
            model.TypeDate = TypeDate;
            DataSet dst = model.getdatetypefilter();
            if (dst.Tables != null && dst.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dst.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["CurrentDate"].ToString();
                    Objload.EwalletBalance = dr["Balance"].ToString();

                    lst.Add(Objload);
                    model.Result = "Yes";
                }
                model.lstewalletledger = lst;

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}