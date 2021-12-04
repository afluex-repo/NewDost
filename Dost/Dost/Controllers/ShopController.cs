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
    public class ShopController : BaseController
    {
        #region shop
        public ActionResult Product(Shop obj)
        {

            List<Shop> lstproduct = new List<Shop>();
            DataSet dsUser = obj.productlist();
            if (dsUser.Tables != null && dsUser.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsUser.Tables[0].Rows)
                {
                    Shop model = new Shop();
                    model.ProductName = dr["EventName"].ToString();
                    model.ProductPrice = dr["Price"].ToString();
                    model.OfferPrice = dr["OfferPrice"].ToString();
                    model.BV = dr["ReferralPercentage"].ToString();
                    model.ProductCode = dr["productcode"].ToString();
                    model.EventDescription = dr["EventDescription"].ToString();
                    model.EventImage = dr["EventImage"].ToString();
                    model.NoOfSeats = dr["NoOfSeats"].ToString();
                    model.Brand = dr["Brand"].ToString();
                    model.PK_EventId = dr["Pk_Eventid"].ToString();
                    lstproduct.Add(model);
                }
                obj.lstproduct = lstproduct;
            }
            return View(obj);
        }
        public ActionResult ProductDescription(string EventId)
        {

            Shop obj = new Shop();
            if (EventId != "")
            {
                obj.PK_EventId = EventId;
                obj.Fk_UserId = Session["Pk_userId"].ToString();
                DataSet ds = obj.productlist();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    obj.ProductName = ds.Tables[0].Rows[0]["EventName"].ToString();
                    obj.ProductPrice = ds.Tables[0].Rows[0]["Price"].ToString();
                    obj.OfferPrice = ds.Tables[0].Rows[0]["OfferPrice"].ToString();
                    obj.BV = ds.Tables[0].Rows[0]["ReferralPercentage"].ToString();
                    obj.ProductCode = ds.Tables[0].Rows[0]["productcode"].ToString();
                    obj.EventDescription = ds.Tables[0].Rows[0]["EventDescription"].ToString();
                    obj.EventImage = ds.Tables[0].Rows[0]["EventImage"].ToString();
                    obj.NoOfSeats = ds.Tables[0].Rows[0]["NoOfSeats"].ToString();
                    obj.Brand = ds.Tables[0].Rows[0]["Brand"].ToString();
                    obj.CGST = ds.Tables[0].Rows[0]["cgst"].ToString();
                    obj.Discount = ds.Tables[0].Rows[0]["Discount"].ToString();
                    obj.PK_EventId = ds.Tables[0].Rows[0]["Pk_EventId"].ToString();
                    ViewBag.TotalItem = ds.Tables[1].Rows[0]["TotalItem"].ToString();
                }

            }
            return View(obj);
        }
        public ActionResult AddToCard(string CardId, string NoofItems)
        {
            Shop model = new Models.Shop();
            model.PK_EventId = CardId;
            model.NumberOfItems = NoofItems;
            model.AddedBy = Session["Pk_userId"].ToString();
            #region Wallet 
            model.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet dsw = model.GetWalletMoney();
            ViewBag.WalletBalance = 0;
            ViewBag.FWalletBalance = 0;
            ViewBag.FWalletShow = false;
            if (dsw != null && dsw.Tables.Count > 0 && dsw.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = double.Parse(dsw.Tables[0].Rows[0][0].ToString()).ToString("n2");
            }
            #endregion
            DataSet ds = model.AddToCard();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    model.PK_EventId = model.PK_EventId;
                    model.Result = "Yes";
                    //return RedirectToAction("CheckOut", new {cardid= cardid });
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["CardMessage"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckOut(string cardid)
        {

            {
                Shop obj = new Shop();
                List<AddressBook> lstAddress = new List<AddressBook>();
                List<Shop> lstproduct = new List<Shop>();
                obj.Fk_UserId = Session["Pk_userId"].ToString();
                #region Wallet 
                DataSet dsw = obj.GetWalletMoney();
                ViewBag.WalletBalance = 0;
                ViewBag.FWalletBalance = 0;
                ViewBag.FWalletShow = false;
                if (dsw != null && dsw.Tables.Count > 0 && dsw.Tables[0].Rows.Count > 0)
                {
                    ViewBag.WalletBalance = double.Parse(dsw.Tables[0].Rows[0][0].ToString()).ToString("");
                }
                #endregion

                #region AddressBook 
                DataSet dsAddress = obj.GetUserAddressBook();
                if (dsAddress != null && dsAddress.Tables.Count > 0 && dsAddress.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsAddress.Tables[0].Rows)
                    {
                        lstAddress.Add(new AddressBook()
                        {
                            Address = row["Address"].ToString(),
                            City = row["City"].ToString(),
                            State = row["State"].ToString(),
                            Pincode = row["Pincode"].ToString(),
                            PK_AddressId = row["PK_AddressId"].ToString(),
                            CompleteAddress = row["CompleteAddress"].ToString(),
                            IsRecentlyUsed = row["IsRecentlyUsed"].ToString(),
                        });
                    }
                }
                obj.lstAddressBook = lstAddress;
                #endregion
                obj.PK_EventId = cardid;
                DataSet dsUser = obj.AddToCartList();
                if (dsUser.Tables != null && dsUser.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsUser.Tables[0].Rows)
                    {
                        Shop model = new Shop();
                        model.ProductName = dr["EventName"].ToString();
                        model.OfferPrice = dr["OfferPrice"].ToString();
                        model.BV = dr["TotalReferalBV"].ToString();
                        model.ProductCode = dr["productcode"].ToString();
                        model.EventDescription = dr["EventDescription"].ToString();
                        model.EventImage = dr["EventImage"].ToString();
                        model.NoOfSeats = dr["TotalItem"].ToString();
                        model.Brand = dr["Brand"].ToString();
                        model.TotalPrice = dr["TotalPrice"].ToString();
                        model.PK_EventId = dr["Pk_Eventid"].ToString();
                        //model.DeliverCharge = dr["DeliveryCharges"].ToString();
                        model.IGST = dr["IGST"].ToString();
                        lstproduct.Add(model);
                    }
                    obj.lstproduct = lstproduct;
                    ViewBag.TotalItem = dsUser.Tables[1].Rows[0]["TotalItem"].ToString();
                    ViewBag.TotalReferalBV = double.Parse(dsUser.Tables[0].Compute("sum(TotalReferalBV)", "").ToString()).ToString("n2");
                    ViewBag.TotalPrice = double.Parse(dsUser.Tables[0].Compute("sum(TotalPrice)", "").ToString()).ToString("");
                    ViewBag.GST = double.Parse(dsUser.Tables[0].Compute("sum(IGST)", "").ToString()).ToString("n2");
                    ViewBag.DeliveryCharge = dsUser.Tables[0].Rows[0]["DeliveryCharges"].ToString();
                    ViewBag.TotalAmount = (decimal.Parse(dsUser.Tables[0].Compute("sum(TotalReferalBV)", "").ToString()) + decimal.Parse(dsUser.Tables[0].Compute("sum(IGST)", "").ToString()) + Convert.ToDecimal(dsUser.Tables[0].Rows[0]["DeliveryCharges"]) + decimal.Parse(dsUser.Tables[0].Compute("sum(TotalPrice)", "").ToString())).ToString();
                    obj.UserName = Session["LoginId"].ToString();
                }
                return View(obj);
            }
        }
        [HttpPost]
        [ActionName("CheckOut")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult Checkoutproduct(Shop obj1)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ProductName", typeof(string));
                dt.Columns.Add("ProductAmount", typeof(string));
                dt.Columns.Add("NoofItems", typeof(string));
                dt.Columns.Add("Pk_EventId", typeof(string));

                string hdrows = Request["hdRows"].ToString();
                for (int i = 1; i <= int.Parse(hdrows) - 1; i++)
                {
                    string ProductName = Request["ProductName_" + i].ToString();
                    decimal TotalPrice = Convert.ToDecimal(Request["TotalPrice_" + i].ToString());
                    string NoofItems = Request["NoofItem_" + i].ToString();
                    string Pk_EventId = Request["PK_EventId_" + i].ToString();
                    DataRow dr = dt.NewRow();
                    dr = dt.NewRow();

                    dr["ProductName"] = ProductName;
                    dr["ProductAmount"] = TotalPrice;
                    dr["NoofItems"] = NoofItems;
                    dr["Pk_EventId"] = Pk_EventId;
                    dt.Rows.Add(dr);
                }

                obj1.dtproductitem = dt;
                obj1.WalletBalance = Request["walletbalance"].ToString();
                obj1.TotalPrice = Request["totalamount"].ToString();
                obj1.NoOfSeats = Request["noofitems"].ToString();
                obj1.AddedBy = Session["Pk_userId"].ToString();
                obj1.BV = Request["totalBV"].ToString();
                obj1.DeliveryCharge = Request["totalBV"].ToString();
                obj1.Gst = Request["totalBV"].ToString();
                DataSet ds = obj1.SaveEventDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["product"] = "check out successfully !";
                    }
                    else
                    {
                        TempData["product"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Plot"] = ex.Message;
            }
            FormName = "product";
            Controller = "Shop";

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult UpdateItems(string EventId, string NoOfItems)
        {
            try
            {
                Shop model = new Shop();
                model.PK_EventId = EventId;
                model.Fk_UserId = Session["Pk_userId"].ToString();
                model.NumberOfItems = NoOfItems;
                DataSet ds = model.UpdateItems();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    model.Result = "yes";
                    DataSet ds1 = model.AddToCartList();
                    if (ds1!= null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        model.BV = double.Parse(ds1.Tables[0].Compute("sum(TotalReferalBV)", "").ToString()).ToString("n2");
                        model.IGST = double.Parse(ds1.Tables[0].Compute("sum(IGST)", "").ToString()).ToString("n2");
                        model.DeliveryCharge = ds1.Tables[0].Rows[0]["DeliveryCharges"].ToString();
                        model.Total = ds1.Tables[1].Rows[0]["TotalItem"].ToString();
                        model.TotalPrice = (decimal.Parse(ds1.Tables[0].Compute("sum(TotalReferalBV)", "").ToString()) + decimal.Parse(ds1.Tables[0].Compute("sum(IGST)", "").ToString())+Convert.ToDecimal(ds1.Tables[0].Rows[0]["DeliveryCharges"]) + decimal.Parse(ds1.Tables[0].Compute("sum(TotalPrice)", "").ToString())).ToString();
                    }
                }
                else
                {
                    model.Result = "no";
                }
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion
        #region pincode
        public ActionResult GetStateCity(string Pincode)
        {
            try
            {
                Common model = new Common();
                model.Pincode = Pincode;

                #region GetStateCity
                DataSet dsStateCity = model.GetStateCity();
                if (dsStateCity != null && dsStateCity.Tables[0].Rows.Count > 0)
                {
                    model.State = dsStateCity.Tables[0].Rows[0]["State"].ToString();
                    model.City = dsStateCity.Tables[0].Rows[0]["City"].ToString();
                    model.Result = "yes";
                }
                else
                {
                    model.State = model.City = "";
                    model.Result = "no";
                }
                #endregion
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        #endregion
        #region
        public ActionResult Deletebookinglist(string PK_EventId)
        {
            //string FormName = "";
            //string Controller = "";
            Shop model = new Shop();
            try
            {


                model.Fk_UserId = Session["Pk_userId"].ToString();
                model.PK_EventId = PK_EventId;
                // model.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.DeleteCardlist();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["shop"] = "Product Removed successfully !";
                    }
                    else
                    {
                        TempData["shop"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["shop"] = ex.Message;
            }
            //FormName = "CheckOut";
            //Controller = "Shop";

            return RedirectToAction("CheckOut", "Shop");

        }
        #endregion
    }
}