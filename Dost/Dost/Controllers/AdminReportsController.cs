using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dost.Models;
using System.Data;
using Dost.Filter;

namespace Dost.Controllers
{
    public class AdminReportsController : AdminBaseController
    {
        // GET: AdminReports
        #region withdrawl
        public ActionResult WithdrawlRequestList()
        {
            Wallet objewallet = new Wallet();
            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.GetWithdrawlWalletRequest();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Pk_RequestId = dr["Pk_RequestId"].ToString();
                    Objload.LoginId = dr["LoginId"].ToString();
                    Objload.DisplayName = dr["Name"].ToString();
                    Objload.Amount = dr["Amount"].ToString();
                    Objload.Status = dr["Status"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    lst.Add(Objload);
                }
                objewallet.WalletRequestList = lst;
            }
            return View(objewallet);
        }
        public ActionResult Approve(string Id)
        {
            try
            {
                Wallet obj = new Wallet();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Pk_RequestId = Id;
                obj.Status = "Approved";
                DataSet ds = obj.ApproveWithdrawlwalletRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["EApproveRequest"] = "Withdrawl Request Approved Successfully";
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
            return RedirectToAction("WithdrawlRequestList", "AdminReports");
        }
        public ActionResult Decline(string Id)
        {
            try
            {
                Wallet obj = new Wallet();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Pk_RequestId = Id;
                obj.Status = "Declined";
                DataSet ds = obj.ApproveWithdrawlwalletRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["EApproveRequest"] = "Withdrawl Request Declined Successfully";
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
            return RedirectToAction("WithdrawlRequestList", "AdminReports");
        }
        #endregion
        public ActionResult TopupReport()
        {
            Reports newdata = new Reports();
            List<Reports> lst1 = new List<Reports>();
            DataSet ds11 = newdata.GetTopupReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.PK_CategoryId = r["PK_CategoryId"].ToString();
                    Obj.CategoryName = r["CategoryName"].ToString();
                    Obj.PK_EventId = r["PK_eventId"].ToString();
                    Obj.EventName = r["EventName"].ToString();
                    Obj.BookingCode = r["BookingCode"].ToString();
                    Obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    Obj.TopupAmt = r["TopupAmt"].ToString();
                    Obj.UsedFor = r["UsedFor"].ToString();
                    Obj.ActivationStatus = r["ActivationStatus"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();

                    lst1.Add(Obj);
                }
                newdata.lsttopupreport = lst1;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.BindTopupStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion

            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindEventCategory();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["CategoryName"].ToString(), Value = r["PK_CategoryId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;
            #endregion
            #region GetEvent

            List<SelectListItem> ddlEvent = new List<SelectListItem>();
            ddlEvent.Add(new SelectListItem { Text = "Select Event", Value = "0" });
            ViewBag.ddlEvent = ddlEvent;
            #endregion
            #region GetPlan

            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
            ViewBag.ddlPlan = ddlPlan;
            #endregion
            return View(newdata);
        }
        [HttpPost]
        [ActionName("TopupReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TopupReportBy(Reports newdata)
        {

            List<Reports> lst1 = new List<Reports>();
            newdata.CategoryCode = newdata.CategoryCode == "0" ? null : newdata.CategoryCode;
            newdata.FromDate = string.IsNullOrEmpty(newdata.FromDate) ? null : Common.ConvertToSystemDate(newdata.FromDate, "dd/MM/yyyy");
            newdata.ToDate = string.IsNullOrEmpty(newdata.ToDate) ? null : Common.ConvertToSystemDate(newdata.ToDate, "dd/MM/yyyy");
            DataSet ds11 = newdata.GetTopupReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.PK_CategoryId = r["PK_CategoryId"].ToString();
                    Obj.CategoryName = r["CategoryName"].ToString();
                    Obj.PK_EventId = r["PK_eventId"].ToString();
                    Obj.EventName = r["EventName"].ToString();
                    Obj.BookingCode = r["BookingCode"].ToString();
                    Obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    Obj.TopupAmt = r["TopupAmt"].ToString();
                    Obj.UsedFor = r["UsedFor"].ToString();
                    Obj.ActivationStatus = r["ActivationStatus"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    lst1.Add(Obj);
                }
                newdata.lsttopupreport = lst1;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.BindTopupStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            #region Category Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindEventCategory();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["CategoryName"].ToString(), Value = r["PK_CategoryId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            #region GetEvent

            List<SelectListItem> ddlEvent = new List<SelectListItem>();
            ddlEvent.Add(new SelectListItem { Text = "Select Event", Value = "0" });
            ViewBag.ddlEvent = ddlEvent;
            #endregion
            #region GetPlan

            List<SelectListItem> ddlPlan = new List<SelectListItem>();
            ddlPlan.Add(new SelectListItem { Text = "Select Plan", Value = "0" });
            ViewBag.ddlPlan = ddlPlan;
            #endregion
            return View(newdata);
        }
        //public ActionResult PrintTopUp(string ToLoginID)
        //{
        //    List<Reports> list = new List<Reports>();
        //    Reports model = new Reports();
        //    if (ToLoginID != null)
        //    {

        //        model.ToLoginID = ToLoginID;
        //        try
        //        {
        //            DataSet ds = model.PrintTopUp();
        //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow r in ds.Tables[0].Rows)
        //                {
        //                    Reports obj = new Reports();

        //                    obj.Package = r["Package"].ToString();
        //                    obj.Amount = r["Amount"].ToString();
        //                    obj.Quantity = r["Quantity"].ToString();
        //                    obj.ProductName = r["ProductName"].ToString();
        //                    obj.HSNCode = r["HSNCode"].ToString();
        //                    ViewBag.RecieptType = ds.Tables[0].Rows[0]["RecieptType"].ToString();
        //                    ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
        //                    ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
        //                    ViewBag.UpgradtionDate = ds.Tables[0].Rows[0]["UpgradtionDate"].ToString();
        //                    ViewBag.PrintingDate = ds.Tables[0].Rows[0]["PrintingDate"].ToString();
        //                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
        //                    ViewBag.ProductPrice = ds.Tables[1].Rows[0]["PinAMount"].ToString();
        //                    ViewBag.ProductPriceInWords = ds.Tables[1].Rows[0]["ProductPriceInWords"].ToString();
        //                    ViewBag.TaxAmount = ds.Tables[1].Rows[0]["TaxAmount"].ToString();
        //                    ViewBag.Taxable = ds.Tables[1].Rows[0]["Taxable"].ToString();
        //                    ViewBag.CompanyName = SoftwareDetails.CompanyName;
        //                    ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
        //                    ViewBag.Pin1 = SoftwareDetails.Pin1;
        //                    ViewBag.State1 = SoftwareDetails.State1;
        //                    ViewBag.City1 = SoftwareDetails.City1;
        //                    ViewBag.ContactNo = SoftwareDetails.ContactNo;
        //                    ViewBag.LandLine = SoftwareDetails.LandLine;
        //                    ViewBag.Website = SoftwareDetails.Website;
        //                    ViewBag.EmailID = SoftwareDetails.EmailID;
        //                    list.Add(obj);

        //                }


        //                model.lsttopupreport = list;
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //        }

        //    }
        //    return View(model);
        //}

        public ActionResult PrintTopUp(string ToLoginID)
        {
            List<Reports> list = new List<Reports>();
            Reports model = new Reports();
            if (ToLoginID != null)
            {
                model.ToLoginID = ToLoginID;
                try
                {
                    DataSet ds = model.PrintTopUp();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Reports obj = new Reports();

                            obj.FK_InvestmentID = r["Pk_InvestmentId"].ToString();
                            //obj.EncryptKey = Crypto.Encrypt(r["Fk_SaleOrderId"].ToString());
                            //obj.ProductID = r["Fk_ProductId"].ToString();
                            obj.Quantity = r["Quantity"].ToString();
                            obj.MRP = r["Amount"].ToString();
                            obj.IGST = r["IGST"].ToString();
                            obj.CGST = r["CGST"].ToString();
                            obj.SGST = r["SGST"].ToString();
                            obj.FinalAmount = r["Amount"].ToString();
                            //obj.TaxableAmount = r["TaxableAmount"].ToString();
                            obj.ProductName = r["ProductName"].ToString();
                            obj.HSNCode = r["HSNCode"].ToString();

                            ViewBag.OrderNo = r["Pk_InvestmentId"].ToString();
                            ViewBag.UpgradtionDate = ds.Tables[0].Rows[0]["UpgradtionDate"].ToString();


                            ViewBag.Pincode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                            ViewBag.State = ds.Tables[3].Rows[0]["statename"].ToString();
                            ViewBag.City = ds.Tables[3].Rows[0]["Districtname"].ToString();

                            ViewBag.TotalFinalAmount = ds.Tables[1].Rows[0]["TotalFinalAmount"].ToString();
                            ViewBag.TotalFinalAmountWords = ds.Tables[1].Rows[0]["TotalFinalAmountWords"].ToString();
                            ViewBag.PurchaseDate = ds.Tables[0].Rows[0]["UpgradtionDate"].ToString();
                            ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                            ViewBag.Loginid = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            ViewBag.AssociateAddress = ds.Tables[0].Rows[0]["Address"].ToString();
                            ViewBag.ValueBeforeTax = ds.Tables[1].Rows[0]["Taxable"].ToString();
                            ViewBag.TaxAdded = ds.Tables[1].Rows[0]["TaxAmount"].ToString();

                            ViewBag.CompanyName = SoftwareDetails.CompanyName;
                            ViewBag.CompanyAddress = SoftwareDetails.CompanyAddress;
                            ViewBag.Pin1 = SoftwareDetails.Pin1;
                            ViewBag.GSTNO = SoftwareDetails.GSTNO;
                            ViewBag.State1 = SoftwareDetails.State1;
                            ViewBag.City1 = SoftwareDetails.City1;
                            ViewBag.ContactNo = SoftwareDetails.ContactNo;
                            ViewBag.LandLine = SoftwareDetails.LandLine;
                            ViewBag.Website = SoftwareDetails.Website;
                            ViewBag.EmailID = SoftwareDetails.EmailID;
                            list.Add(obj);

                        }
                        model.lsttopupreport = list;
                    }

                }
                catch (Exception ex)
                {
                }
            }
            return View(model);
        }

        public ActionResult TransactionLog()
        {
            Reports newdata = new Reports();
            List<Reports> lst1 = new List<Reports>();

            DataSet ds11 = newdata.GetTransactionLog();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.AddedOn = r["AddedOn"].ToString();
                    Obj.Action = r["Action"].ToString();
                    Obj.Remarks = r["Remarks"].ToString();

                    lst1.Add(Obj);
                }
                newdata.lsttransactionlog = lst1;
            }


            return View(newdata);
        }
        [HttpPost]
        [ActionName("TransactionLog")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TransactionLogBy(Reports newdata)
        {

            List<Reports> lst1 = new List<Reports>();
            DataSet ds11 = newdata.GetTransactionLog();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.AddedOn = r["AddedOn"].ToString();
                    Obj.Action = r["Action"].ToString();
                    Obj.Remarks = r["Remarks"].ToString();

                    lst1.Add(Obj);
                }
                newdata.lsttransactionlog = lst1;
            }


            return View(newdata);
        }
        public ActionResult KYCDetails(KYCDocuments obj)
        {
            return View(obj);
        }

        [HttpPost]
        [OnAction(ButtonName = "Search")]
        [ActionName("KYCDetails")]
        public ActionResult GetKYCDetails(KYCDocuments objKYC)
        {

            List<KYCDocuments> list = new List<KYCDocuments>();
            objKYC.LoginId = objKYC.LoginId;
            DataSet ds = objKYC.GetKYCDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                KYCDocuments obj = new KYCDocuments();
                obj.AdharNumber = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.AdharImage = ds.Tables[0].Rows[0]["AdharImage"].ToString();
                obj.AdharStatus = "Status : " + ds.Tables[0].Rows[0]["AdharStatus"].ToString();
                obj.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString();
                obj.PanStatus = "Status : " + ds.Tables[0].Rows[0]["PanStatus"].ToString();
                obj.DocumentNumber = ds.Tables[0].Rows[0]["DocumentNumber"].ToString();
                obj.DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString();
                obj.DocumentStatus = "Status : " + ds.Tables[0].Rows[0]["DocumentStatus"].ToString();
                obj.MemberAccNo = ds.Tables[1].Rows[0]["MemberAccNo"].ToString();
                obj.MemberBankName = ds.Tables[1].Rows[0]["MemberBankName"].ToString();
                obj.IFSCCode = ds.Tables[1].Rows[0]["IFSCCode"].ToString();
                obj.MemberBranch = ds.Tables[1].Rows[0]["MemberBranch"].ToString();
                list.Add(obj);
            }
            objKYC.KycDetailList = list;

            return View(objKYC);
        }
    }
}