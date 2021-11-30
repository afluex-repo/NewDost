﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dost.Models;
using System.Data;
using System.IO;
using Dost.Filter;
using BusinessLayer;

namespace Dost.Controllers
{
    public class MasterController : AdminBaseController
    {
        // GET: Master
        #region NFC
        public ActionResult NFC(string NfcId)
        {

            Master model = new Master();
            #region Offer
            Common obj = new Common();
            List<SelectListItem> ddlOffer = new List<SelectListItem>();
            DataSet ds11 = obj.BindOffer();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                { ddlOffer.Add(new SelectListItem { Text = r["OfferName"].ToString(), Value = r["PK_OfferId"].ToString() }); }
            }

            ViewBag.Offer = ddlOffer;
            #endregion
            #region Category
            List<SelectListItem> ddlcategory = new List<SelectListItem>();
            DataSet cateds = model.categorylist();
            if (cateds != null && cateds.Tables.Count > 0 && cateds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in cateds.Tables[0].Rows)
                { ddlcategory.Add(new SelectListItem { Text = r["CategoryName"].ToString(), Value = r["PK_CategoryId"].ToString() }); }
            }

            ViewBag.Category = ddlcategory;
            #endregion
            if (NfcId != null)
            {
                try
                {
                    model.PK_NFcId = Convert.ToInt32(NfcId);
                    if (model.PK_NFcId != 0)
                    {
                        DataSet ds = model.NFCList();
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.SubCategoryId = ds.Tables[0].Rows[0]["FK_SubCategoryId"].ToString();
                            model.PK_NFcId = Convert.ToInt32(ds.Tables[0].Rows[0]["PK_EventId"]);
                            model.NFCType = ds.Tables[0].Rows[0]["EventName"].ToString();
                            model.Description = ds.Tables[0].Rows[0]["EventDescription"].ToString();
                            model.EventImage = ds.Tables[0].Rows[0]["EventImage"].ToString();
                            model.ProductPrice = ds.Tables[0].Rows[0]["Price"].ToString();
                            model.OfferId = Convert.ToInt32(ds.Tables[0].Rows[0]["FK_OfferId"]);
                            model.IGST = ds.Tables[0].Rows[0]["IGST"].ToString();
                            model.CGST = ds.Tables[0].Rows[0]["CGST"].ToString();
                            model.SGST = ds.Tables[0].Rows[0]["SGST"].ToString();
                            model.OfferPrice = ds.Tables[0].Rows[0]["OfferPrice"].ToString();
                            model.Discount = ds.Tables[0].Rows[0]["Discount"].ToString();
                            model.DeliveryCharge = ds.Tables[0].Rows[0]["DeliveryCharges"].ToString();
                            model.ReferralPercent = ds.Tables[0].Rows[0]["ReferralPercentage"].ToString();
                            model.DistributorCommission = Convert.ToDecimal(ds.Tables[0].Rows[0]["DistributorCommission"]);
                            model.SponsorCommission = Convert.ToDecimal(ds.Tables[0].Rows[0]["SponsorCommission"]);
                            model.Brand = ds.Tables[0].Rows[0]["Brand"].ToString();
                            model.Catalyst = ds.Tables[0].Rows[0]["Catalyst"].ToString();
                            model.Blaze = ds.Tables[0].Rows[0]["Blaze"].ToString();
                            model.Maverick = ds.Tables[0].Rows[0]["Maverick"].ToString();
                            model.Symphony = ds.Tables[0].Rows[0]["Symphony"].ToString();
                            model.Phoenix = ds.Tables[0].Rows[0]["Phoenix"].ToString();
                            model.Quantity = ds.Tables[0].Rows[0]["Noofseats"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("NFC")]
        [OnAction(ButtonName = "btnSave")]
        public ActionResult SaveNFC(Master model, IEnumerable<HttpPostedFileBase> postedFile)
        {
            model.AddedBy = Session["Pk_AdminId"].ToString();
            #region Offer
            Common obj = new Common();
            List<SelectListItem> ddlOffer = new List<SelectListItem>();
            DataSet ds11 = obj.BindOffer();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                { ddlOffer.Add(new SelectListItem { Text = r["OfferName"].ToString(), Value = r["PK_OfferId"].ToString() }); }
            }

            ViewBag.Offer = ddlOffer;
            #endregion
            #region Category
            List<SelectListItem> ddlcategory = new List<SelectListItem>();
            DataSet cateds = model.categorylist();
            if (cateds != null && cateds.Tables.Count > 0 && cateds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in cateds.Tables[0].Rows)
                { ddlcategory.Add(new SelectListItem { Text = r["CategoryName"].ToString(), Value = r["PK_CategoryId"].ToString() }); }
            }

            ViewBag.Category = ddlcategory;
            #endregion
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        model.EventImage = "/images/EventImage/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.EventImage)));
                    }
                }
                if (model.PK_NFcId == 0 || model.PK_NFcId == null)
                {
                    DataSet ds = model.SaveNFCDetails();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["NFCMsg"] = "NFC Details Saved Successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["NFCMsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                else
                {
                    DataSet ds = model.UpdateNFCDetails();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["NFCMsg"] = "NFC Details Update Successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["NFCMsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                model.EventName = string.Empty;
                model.EventDescription = string.Empty;
                model.EventImage = string.Empty;
            }
            catch (Exception ex)
            {

                TempData["NFCMsg"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult NFCList(Master model)
        {
            List<Master> lstevent = new List<Master>();

            model.PK_NFcId = model.PK_NFcId == 0 ? null : model.PK_NFcId;
            DataSet ds = model.NFCList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_NFcId = Convert.ToInt32(r["PK_EventId"].ToString());
                    obj.EventName = r["EventName"].ToString();
                    obj.ProductPrice = r["Price"].ToString();
                    obj.EventDescription = r["EventDescription"].ToString();
                    obj.OfferName = r["OfferName"].ToString();
                    obj.EventImage = r["EventImage"].ToString();
                    obj.BinaryPercent = r["BinaryPercentage"].ToString();
                    obj.ReferralPercent = r["ReferralPercentage"].ToString();
                    obj.Catalyst= r["Catalyst"].ToString();
                    obj.Blaze = r["Blaze"].ToString();
                    obj.Maverick = r["Maverick"].ToString();
                    obj.Maverick = r["Maverick"].ToString();
                    obj.Symphony = r["Symphony"].ToString();
                    obj.Brand = r["Brand"].ToString();
                    lstevent.Add(obj);
                }
                model.lst = lstevent;
            }
            return View(model);
        }
        public ActionResult DeletNFC(int NFCId)
        {
            string FormName = "";
            string Controller = "";

            Master model = new Master();
            model.AddedBy = Session["Pk_AdminId"].ToString();
            if (NFCId != 0)
            {

                try
                {
                    model.PK_NFcId = NFCId;
                    DataSet ds = model.DeleteNFC();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["NFC"] = "NFC Details Deleted Successfully";
                            FormName = "NFCList";
                            Controller = "Master";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["NFC"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "NFCList";
                            Controller = "Master";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["NFC"] = ex.Message;
                    FormName = "NFCList";
                    Controller = "Master";

                }
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
        #region OfferMaster
        public ActionResult OfferMaster(string OfferId)
        {
            Master model = new Master();
            if (OfferId != null)
            {
                try
                {
                    model.OfferId = Convert.ToInt32(OfferId);
                    if (model.OfferId != 0)
                    {
                        DataSet ds = model.OfferList();
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.OfferId = Convert.ToInt32(ds.Tables[0].Rows[0]["PK_OfferId"]);
                            model.OfferName = ds.Tables[0].Rows[0]["OfferName"].ToString();
                            model.CouponCode = ds.Tables[0].Rows[0]["CouponCode"].ToString();
                            model.OfferValidity = ds.Tables[0].Rows[0]["OfferValidity"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult OfferMaster(Master model)
        {
            model.AddedBy = Session["Pk_AdminId"].ToString();
            try
            {
                if (model.OfferId == 0 || model.OfferId == null)
                {
                    DataSet ds = model.SaveOffer();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Offer"] = "Offer Saved Successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["Offer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                else
                {
                    DataSet ds = model.UpdateOffer();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Offer"] = "Offer Update Successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["Offer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                TempData["Offer"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult DeletOffer(int OfferId)
        {
            string FormName = "";
            string Controller = "";

            Master model = new Master();
            model.AddedBy = Session["Pk_AdminId"].ToString();
            if (OfferId != 0)
            {

                try
                {
                    model.OfferId = OfferId;
                    DataSet ds = model.DeleteOffer();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Offer"] = "Offer Deleted Successfully";
                            FormName = "OfferList";
                            Controller = "Master";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["Offer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "OfferList";
                            Controller = "Master";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Offer"] = ex.Message;
                    FormName = "OfferList";
                    Controller = "Master";

                }
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult OfferList(Master model)
        {
            List<Master> lstoffer = new List<Master>();

            model.OfferId = model.OfferId == 0 ? null : model.OfferId;
            DataSet ds = model.OfferList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.OfferId = Convert.ToInt32(r["PK_OfferId"].ToString());
                    obj.OfferName = r["OfferName"].ToString();
                    obj.CouponCode = r["CouponCode"].ToString();
                    obj.OfferValidity = r["OfferValidity"].ToString();
                    lstoffer.Add(obj);
                }
                model.lst = lstoffer;
            }
            return View(model);
        }
        #endregion
        #region DistributtorList
        public ActionResult DistrubuterList(clsDistributor model)
        {
            List<clsDistributor> lstevent = new List<clsDistributor>();

            DataSet ds = model.DistributorList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    clsDistributor obj = new clsDistributor();
                    obj.Pk_DistributorId = Convert.ToInt32(r["Pk_DistributorId"].ToString());
                    obj.YourName = r["YourName"].ToString();
                    obj.Designation = r["Designation"].ToString();
                    obj.CompanyName = r["CompanyName"].ToString();
                    obj.CompanyType = r["CompanyType"].ToString();
                    obj.CompanySize = r["CompanySize"].ToString();
                    obj.CompanyAddress = r["CompanyAddress"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Website = r["Website"].ToString();
                    obj.AboutCompany = r["AboutCompany"].ToString();

                    obj.CompanySize = r["AddedOn"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.LoginId = r["LoginId"].ToString();

                    lstevent.Add(obj);
                }
                model.lst = lstevent;
            }
            return View(model);
        }
        public ActionResult ApproveDistrubutor(int Pk_DistributorId)
        {
            string FormName = "";
            string Controller = "";

            clsDistributor model = new clsDistributor();
            model.AddedBy = Convert.ToInt32(Session["Pk_AdminId"]);
            if (Pk_DistributorId != 0)
            {

                try
                {
                    model.Pk_DistributorId = Pk_DistributorId;
                    DataSet ds = model.ApproveDistributor();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            string msg = "Dear " + ds.Tables[0].Rows[0]["UserName"].ToString() + " , Congratulations ! We welcome you to be the part of DOST supply chain network, your application is approved and now you can start selling, stocking & delivering DOST Services. Team - DOST INC";
                            //send sms
                            try
                            {
                                BLSMS.sendSMSUpdated(msg, ds.Tables[0].Rows[0]["MobileNo"].ToString());
                            }
                            catch (Exception ex)
                            {

                            }
                            //send mail
                            string UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                            string MailBody = "";
                            MailBody = "We feel privileged in adding you in our supply chain network. Now you can manage your distributor account from your user panel. to login please<a href='https://dost.click/'> click here</a><br><br>For any issue please reach us through email or through DOST helpline.";
                            string Subject = "Distributor Confirmation";
                            try
                            {
                                if (ds.Tables[0].Rows[0]["Email"].ToString() != "")
                                {
                                    BLMail.SendDistributorMail(UserName, MailBody, Subject, ds.Tables[0].Rows[0]["Email"].ToString());
                                }

                            }
                            catch (Exception ex)
                            {

                            }
                            TempData["NFC"] = " Distributor Detail Approved Successfully";
                            FormName = "DistrubuterList";
                            Controller = "Master";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["NFC"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "DistrubuterList";
                            Controller = "Master";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["NFC"] = ex.Message;
                    FormName = "DistrubuterList";
                    Controller = "Master";
                }
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult RejectDistrubutor(int Pk_DistributorId)
        {
            string FormName = "";
            string Controller = "";

            clsDistributor model = new clsDistributor();
            model.AddedBy = Convert.ToInt32(Session["Pk_AdminId"]);
            if (Pk_DistributorId != 0)
            {

                try
                {
                    model.Pk_DistributorId = Pk_DistributorId;
                    DataSet ds = model.RejectDistributor();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["NFC"] = " Distributor Detail Rejected Successfully";
                            FormName = "DistrubuterList";
                            Controller = "Master";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["NFC"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "DistrubuterList";
                            Controller = "Master";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["NFC"] = ex.Message;
                    FormName = "DistrubuterList";
                    Controller = "Master";
                }
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
        #region Banner
        public ActionResult Banner(string bannerid)
        {
            Master model = new Master();
            if (bannerid != null)
            {

                try
                {
                    model.BannerId = Convert.ToInt32(bannerid);
                    if (model.BannerId != 0)
                    {
                        DataSet ds = model.BannerList();
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            model.BannerId = Convert.ToInt32(ds.Tables[0].Rows[0]["BannerId"].ToString());
                            model.BannerImage = ds.Tables[0].Rows[0]["BannerImage"].ToString();
                            model.BannerTitle = ds.Tables[0].Rows[0]["BannerTitle"].ToString();
                            model.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                            model.Trailer = ds.Tables[0].Rows[0]["TrailarLink"].ToString();

                            model.IsAddFav = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAddFav"].ToString());

                            if (model.IsAddFav == false)
                            {
                                model.FavStatus = "";
                            }
                            else
                            {
                                model.FavStatus = "checked";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                return View(model);
            }
            else
            {
                return View(model);
            }

        }
        [HttpPost]
        [OnAction(ButtonName = "Banner")]
        [ActionName("Banner")]
        public ActionResult SaveBanner(IEnumerable<HttpPostedFileBase> postedFile, Master model)
        {
            model.AddedBy = Session["Pk_AdminId"].ToString();
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        model.BannerImage = "/images/BannerImage/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.BannerImage)));
                    }
                }
                if (model.BannerId == null)
                {
                    DataSet ds = model.SaveBannerDetails();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["BannerMsg"] = "Banner Details Saved Successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["BannerMsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                else
                {
                    DataSet ds = model.UpdateBannerDetails();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["BannerMsg"] = "Banner Details Update Successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["BannerMsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }

                //model.BannerTitle= string.Empty;
                //model.Description = string.Empty;
                //model.Trailer = string.Empty;
                //model.IsAddFav = false;
                //model.BannerImage = string.Empty;
            }
            catch (Exception ex)
            {

                TempData["BannerMsg"] = ex.Message;
            }
            return Redirect("Banner");
        }
        public ActionResult BannerList(Master model)
        {
            List<Master> lstbanner = new List<Master>();

            model.BannerId = model.BannerId == 0 ? null : model.BannerId;
            DataSet ds = model.BannerList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.BannerId = Convert.ToInt32(r["BannerId"].ToString());
                    obj.BannerImage = r["BannerImage"].ToString();
                    obj.BannerTitle = r["BannerTitle"].ToString();
                    obj.Trailer = r["TrailarLink"].ToString();
                    obj.Description = r["Description"].ToString();

                    obj.IsAddFav = Convert.ToBoolean(r["IsAddFav"].ToString());

                    if (obj.IsAddFav == false)
                    {
                        obj.FavStatus = "";
                    }
                    else
                    {
                        obj.FavStatus = "checked";
                    }
                    lstbanner.Add(obj);
                }
                model.lstBanner = lstbanner;
            }
            return View(model);
        }
        public ActionResult DeleteBanner(int bannerid)
        {
            string FormName = "";
            string Controller = "";

            Master model = new Master();
            model.AddedBy = Session["Pk_AdminId"].ToString();
            if (bannerid != 0)
            {

                try
                {
                    model.BannerId = bannerid;
                    DataSet ds = model.BannerDelete();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Banner"] = "Banner Details Deleted Successfully";
                            FormName = "BannerList";
                            Controller = "Master";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["Banner"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "BannerList";
                            Controller = "Master";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Banner"] = ex.Message;
                    FormName = "BannerList";
                    Controller = "Master";

                }
            }
            return RedirectToAction(FormName, Controller);
        }

        #endregion
        #region ProductTag
        public ActionResult ProductTagMaster()
        {
            return View();
        }
        #endregion
        #region ProductMultiPleImage
        public ActionResult ProductImage(Master model)
        {
            #region Bind SeviceTyoe
            int count = 0;
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = model.NFCList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Value = "0", Text = "Select Product" });
                    }
                    ddlProduct.Add(new SelectListItem { Value = r["PK_EventId"].ToString(), Text = r["EventName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlProduct = ddlProduct;
            }
            #endregion
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "btn_Product")]
        [ActionName("ProductImage")]
        public ActionResult SaveMultiPleImage(IEnumerable<HttpPostedFileBase> postedFile, Master model)
        {
            model.AddedBy = Session["Pk_AdminId"].ToString();
            try
            {
                DataTable dtImage = new DataTable();
                dtImage.Columns.Add("Image");
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        model.EventImage = "/images/ProductImage/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.BannerImage)));
                        dtImage.Rows.Add(model.EventImage);
                    }
                }

                  model.dtImage = dtImage;
                  DataSet ds = model.SaveProductImage();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["ProductImage"] = "Product Image Saved Successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["ProductImage"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
            }
            catch (Exception ex)
            {

                TempData["ProductImage"] = ex.Message;
            }
            return Redirect("ProductImage");
        }
        public ActionResult ProductImageList(Master model)
        {
            List<Master> lstbanner = new List<Master>();

            model.BannerId = model.BannerId == 0 ? null : model.BannerId;
            DataSet ds = model.BannerList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.BannerId = Convert.ToInt32(r["BannerId"].ToString());
                    obj.BannerImage = r["BannerImage"].ToString();
                    obj.BannerTitle = r["BannerTitle"].ToString();
                    obj.Trailer = r["TrailarLink"].ToString();
                    obj.Description = r["Description"].ToString();

                    obj.IsAddFav = Convert.ToBoolean(r["IsAddFav"].ToString());

                    if (obj.IsAddFav == false)
                    {
                        obj.FavStatus = "";
                    }
                    else
                    {
                        obj.FavStatus = "checked";
                    }
                    lstbanner.Add(obj);
                }
                model.lstBanner = lstbanner;
            }
            return View(model);
        }
        public ActionResult DeleteImage(int bannerid)
        {
            string FormName = "";
            string Controller = "";

            Master model = new Master();
            model.AddedBy = Session["Pk_AdminId"].ToString();
            if (bannerid != 0)
            {

                try
                {
                    model.BannerId = bannerid;
                    DataSet ds = model.BannerDelete();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Banner"] = "Banner Details Deleted Successfully";
                            FormName = "BannerList";
                            Controller = "Master";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["Banner"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "BannerList";
                            Controller = "Master";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Banner"] = ex.Message;
                    FormName = "BannerList";
                    Controller = "Master";

                }
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion
    }
}