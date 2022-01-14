using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dost.Filter;
using Dost.Models;
using System.Data;
using System.Data.SqlClient;
namespace Dost.Controllers
{
    public class AdminController : AdminBaseController
    {
      // GET: Admin
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult GenerateNFCCode()
        {
            NFCData model = new NFCData();
            return View();
        }
        [HttpPost]
        public ActionResult GenerateNFCCode(Home model)
        {
            DataSet ds = new DataSet();
            model.AddedBy = Session["Pk_AdminId"].ToString();
            for (int i = 1; i <= model.Quantity; i++)
            {
                Random ran = new Random();
                model.Code = ran.Next(111111, 999999).ToString();
                model.Code = 'D' + model.Code;
                model.EncCode = Crypto.EncryptNFC(model.Code);
                ds = model.GenerateNFCCode();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        i = i - 1;
                    }
                }
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["Msg"] = "NFC Code generated successfully.";
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("AdminLogin", "Home");
        }
        public ActionResult NFCPurchaseHistory()
        {
            Reports model = new Reports();
            List<Reports> listNFC = new List<Reports>();
            model.PK_CategoryId = "3";
            DataSet ds = model.GetNFCReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.PK_EventId = r["PK_EventId"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.MobileNo = r["Mobile"].ToString();
                    Obj.BuyDate = r["BuyDate"].ToString();
                    Obj.EventName = r["EventName"].ToString();
                    Obj.Name = r["Name"].ToString();
                    Obj.PlanName = r["PlanName"].ToString();
                    Obj.Validity = r["Validity"].ToString();
                    Obj.EventLocation = r["EventLocation"].ToString();
                    Obj.CategoryName = r["CategoryName"].ToString();
                    Obj.Offername = r["offername"].ToString();
                    Obj.EventPrice = r["EventPrice"].ToString();
                    Obj.OfferValidity = r["OfferValidity"].ToString();
                    Obj.CouponCode = r["CouponCode"].ToString();
                    Obj.NoOfSeats = r["NoOfSeats"].ToString();
                    Obj.EventImage = r["EventImage"].ToString();
                    Obj.StartDate = r["StartDate"].ToString();
                    Obj.Description = r["EventDescription"].ToString();
                    Obj.TotalPrice = r["TotalPrice"].ToString();
                    Obj.Address = r["DeliveryAddress"].ToString();
                    Obj.State = r["State"].ToString();
                    Obj.City = r["City"].ToString();
                    Obj.PinCode = r["PinCode"].ToString();
                    listNFC.Add(Obj);
                }
                model.NfcList = listNFC;
            }
            return View(model);
        }
        public ActionResult SubscriptionPurchaseHistory()
        {
            Reports model = new Reports();
            List<Reports> listNFC = new List<Reports>();
            model.PK_CategoryId = "2";
            DataSet ds = model.GetNFCReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports Obj = new Reports();
                    Obj.PK_EventId = r["PK_EventId"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.MobileNo = r["Mobile"].ToString();
                    Obj.BuyDate = r["BuyDate"].ToString();
                    Obj.EventName = r["EventName"].ToString();
                    Obj.Name = r["Name"].ToString();
                    Obj.PlanName = r["PlanName"].ToString();
                    Obj.Validity = r["Validity"].ToString();
                    Obj.EventLocation = r["EventLocation"].ToString();
                    Obj.CategoryName = r["CategoryName"].ToString();
                    Obj.Offername = r["offername"].ToString();
                    Obj.EventPrice = r["EventPrice"].ToString();
                    Obj.OfferValidity = r["OfferValidity"].ToString();
                    Obj.CouponCode = r["CouponCode"].ToString();
                    Obj.NoOfSeats = r["NoOfSeats"].ToString();
                    Obj.EventImage = r["EventImage"].ToString();
                    Obj.StartDate = r["StartDate"].ToString();
                    Obj.Description = r["EventDescription"].ToString();
                    Obj.TotalPrice = r["TotalPrice"].ToString();
                    listNFC.Add(Obj);
                }
                model.NfcList = listNFC;
            }
            return View(model);
        }
        public ActionResult AllotNFC(NFCData model)
        {
            List<NFCData> lstevent = new List<NFCData>();

            DataSet ds = model.GetNFCDataList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    NFCData obj = new NFCData();
                    obj.PK_NFCId = r["PK_NFCId"].ToString();
                    //obj.Name = r["Name"].ToString();
                    //obj.MobileNo = r["Mobile"].ToString();
                    //obj.Email = r["Email"].ToString();
                    //obj.IsCodeAlloted = Convert.ToBoolean(r["IsCodeAlloted"]);
                    obj.Code = r["Code"].ToString();
                    // obj.NFCStatus = r["NFCStatus"].ToString();
                    lstevent.Add(obj);
                }
                model.lst = lstevent;
            }
            return View(model);
        }
        public ActionResult NFCAllotment(string LoginId, string Code, string PK_NFCId, string Count, string TotalCount)
        {
            NFCData obj = new NFCData();
            obj.LoginId = LoginId;
            obj.Code = Code;
            obj.PK_NFCId = PK_NFCId;
            obj.Count = Count;
            obj.Quantity = TotalCount;
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            DataSet ds = obj.NFCAllotment();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {

                    obj.Result = "Yes";
                    TempData["Msg"] = "NFC Alloted successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    obj.Result = "No";
                    TempData["Msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                obj.Result = "No";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CancelAllotment(string PK_NFCId)
        {
            NFCData obj = new NFCData();
            try
            {
                obj.PK_NFCId = PK_NFCId;
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.CancelNFCAllotment();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["NFC"] = "Allotment cancelled.";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["NFC"] = ds.Tables[0].Rows[0]["Msg"].ToString();
                    }
                }
                else
                {
                    obj.Result = "No";
                }
            }
            catch (Exception ex)
            {
                TempData["NFC"] = ex.Message;
            }
            return RedirectToAction("AllotNFC", "Admin");
        }
        public ActionResult AllotedNFCList()
        {
            NFCData model = new NFCData();
            List<NFCData> lstevent = new List<NFCData>();

            DataSet ds = model.GetAlloteNFC();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    NFCData obj = new NFCData();
                    obj.PK_NFCId = r["PK_NFCId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.MobileNo = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.IsCodeAlloted = Convert.ToBoolean(r["IsCodeAlloted"]);
                    obj.Code = r["Code"].ToString();
                    obj.NFCStatus = r["NFCStatus"].ToString();
                    lstevent.Add(obj);
                }
                model.lst = lstevent;
            }
            #region Distributor
            clsDistributor obj1 = new clsDistributor();
            List<SelectListItem> dist = new List<SelectListItem>();
            DataSet ds1 = obj1.DistributorList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        dist.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    dist.Add(new SelectListItem { Text = r["DisplayName"].ToString(), Value = r["FK_UserId"].ToString() });
                    count++;
                }
            }

            ViewBag.Distributor = dist;

            #endregion
            return View(model);
        }
        [HttpPost]
        public ActionResult AllotedNFCList(NFCData model)
        {
            List<NFCData> lstevent = new List<NFCData>();

            DataSet ds = model.GetAlloteNFC();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    NFCData obj = new NFCData();
                    obj.PK_NFCId = r["PK_NFCId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.MobileNo = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.IsCodeAlloted = Convert.ToBoolean(r["IsCodeAlloted"]);
                    obj.Code = r["Code"].ToString();
                    obj.NFCStatus = r["NFCStatus"].ToString();
                    lstevent.Add(obj);
                }
                model.lst = lstevent;
            }
            #region Distributor
            clsDistributor obj1 = new clsDistributor();
            List<SelectListItem> dist = new List<SelectListItem>();
            DataSet ds1 = obj1.DistributorList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        dist.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    dist.Add(new SelectListItem { Text = r["DisplayName"].ToString(), Value = r["FK_UserId"].ToString() });
                    count++;
                }
            }

            ViewBag.Distributor = dist;

            #endregion
            return View(model);
        }
        public ActionResult RequestNFCList()
        {
            Home model = new Home();
            List<Home> lst = new List<Home>();
            DataSet ds = model.RequestedNFCList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Home obj = new Home();
                    obj.RequestId = r["PK_RequestId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.DisplayName = r["DisplayName"].ToString();
                    obj.Quantity = Convert.ToInt32(r["Quantity"]);
                    obj.Category = r["CategoryName"].ToString();
                    obj.ToDate = r["RequestedDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult AcceptRequest(string id)
        {
            Home model = new Home();
            model.RequestId = id;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            List<Home> lst = new List<Home>();
            DataSet ds = model.AcceptRequest();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["Msg"] = "Accepted successfully";
                }
                else
                {
                    TempData["Msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return RedirectToAction("RequestNFCList", "Admin");
        }
        public ActionResult CancelRequest(string id)
        {
            Home model = new Home();
            model.RequestId = id;
            model.AddedBy = Session["Pk_AdminId"].ToString();
            List<Home> lst = new List<Home>();
            DataSet ds = model.CancelRequest();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["Msg"] = "Cancelled successfully";
                }
                else
                {
                    TempData["Msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return RedirectToAction("RequestNFCList", "Admin");
        }
        public ActionResult DebitAmountFromWallet(string LoginId, string Count)
        {
            NFCData obj = new NFCData();
            obj.LoginId = LoginId;
            obj.AddedBy = Session["Pk_AdminId"].ToString();
            obj.Quantity = Count;
            DataSet ds = obj.DebitAmountFromWallet();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {

                    obj.Result = "Yes";
                    TempData["Msg"] = "NFC Alloted successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    obj.Result = "No";
                    TempData["Msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                obj.Result = "No";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PurchaseandOrder()
        {
            Master model = new Master();

            List<Master> lst = new List<Master>();
            DataSet ds = model.GetListForInvoiceAdmin();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.DisplayName = r["Name"].ToString();
                    obj.Fk_UserId = r["Fk_UserId"].ToString();
                    obj.UserCode = r["UserCode"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Status = r["InvoiceStatus"].ToString();
                    obj.InvoiceNo = r["PK_InvestmentId"].ToString();
                    obj.ActivationDate = r["ActivatedOn"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult PurchaseandOrder(Master model)
        {
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetListForInvoiceAdmin();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.DisplayName = r["Name"].ToString();
                    obj.Fk_UserId = r["Fk_UserId"].ToString();
                    obj.UserCode = r["UserCode"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Status = r["InvoiceStatus"].ToString();
                    obj.InvoiceNo = r["PK_InvestmentId"].ToString();
                    obj.ActivationDate = r["ActivatedOn"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult UpdateInvoiceStatus(string InvoiceNo, string Status)
        {
            Master model = new Master();
            model.InvoiceNo = InvoiceNo;
            if (Status == "Free")
            {
                model.Status = "Free/Gift";
            }
            else
            {
                model.Status = Status;
            }
            DataSet ds = model.UpdateInvoiceStatus();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["Msg"] = "Status updated successfully";
                }
                else
                {
                    TempData["Msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return RedirectToAction("PurchaseandOrder", "Admin");
        }
        public ActionResult ApproveUPI()
        {
            Profile model = new Profile();
            List<Profile> lst = new List<Profile>();
            DataSet ds = model.GetAllUserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Profile obj = new Profile();
                    obj.DisplayName = r["Name"].ToString();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.UPI = r["UPI"].ToString();
                    obj.Status = r["UPIStatus"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }

        public ActionResult ApproveUPIRequest(string Id)
        {
            try
            {
                Profile obj = new Profile();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.PK_UserID = Id;
                obj.Status = "Approved";
                DataSet ds = obj.ApproveUPIRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["UPI"] = "UPI Approved Successfully";
                    }
                    else
                    {
                        TempData["UPI"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["UPI"] = ex.Message;
            }
            return RedirectToAction("ApproveUPI", "Admin");
        }
    }
}