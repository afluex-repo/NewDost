using System;
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
        public ActionResult Category(string Id)
        {
            clsCategory model = new clsCategory();
            if (Id != null)
            {
                try
                {
                    model.Pk_SubCategoryId = Convert.ToInt32(Id);
                    model.Pk_SubCategoryId = model.Pk_SubCategoryId == 0 ? null : model.Pk_SubCategoryId;
                    if (model.Pk_SubCategoryId != 0)
                    {
                        DataSet ds = model.CategoryList();
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            //Pk_CategoryId,Icon,Name,IsDeleted	,AddedBy,AddedOn
                            model.Pk_SubCategoryId = Convert.ToInt32(ds.Tables[0].Rows[0]["Pk_SubCategoryId"]);
                            model.Icon = ds.Tables[0].Rows[0]["Icon"].ToString();
                            model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                            TempData["Image"] = ds.Tables[0].Rows[0]["Icon"].ToString();
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
        public ActionResult Category(HttpPostedFileBase fileUploaderControl, clsCategory model)
        {
            model.AddedBy = Convert.ToInt32(Session["Pk_AdminId"]);
            try
            {
                model.Pk_SubCategoryId = model.Pk_SubCategoryId;
                if (Convert.ToInt32(model.Pk_SubCategoryId) == 0 || model.Pk_SubCategoryId.ToString() == null)
                {
                    //Get the file name
                    if (fileUploaderControl != null)
                    {
                        string filename = System.IO.Path.GetFileName(fileUploaderControl.FileName);
                        //Save the file in server Images folder
                        fileUploaderControl.SaveAs(Server.MapPath("~/UploadCategoryIcon/" + filename));
                        string filepathtosave = "/UploadCategoryIcon/" + filename;
                        model.Icon = filepathtosave;
                    }
                    DataSet ds = model.SaveCategory();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            model.Name = "";
                            TempData["Subscription"] = "Sub Category Saved Successfully";
                            ModelState.Clear();
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["Subscription"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            ModelState.Clear();
                        }
                    }
                }
                else
                {
                    if (fileUploaderControl != null)
                    {
                        string filename = System.IO.Path.GetFileName(fileUploaderControl.FileName);
                        //Save the file in server Images folder
                        fileUploaderControl.SaveAs(Server.MapPath("~/UploadCategoryIcon/" + filename));
                        string filepathtosave = "/UploadCategoryIcon/" + filename;
                        model.Icon = filepathtosave;
                    }
                    model.UpdatedBy = Convert.ToInt32(Session["Pk_AdminId"]);
                    DataSet ds = model.UpdateCategory();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            model.Name = "";
                            TempData["Subscription"] = "Sub Category Update Successfully";
                            ModelState.Clear();
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["Subscription"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
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

        public ActionResult CategoryList(clsCategory model)
        {
            List<clsCategory> lstoffer = new List<clsCategory>();
            model.Pk_SubCategoryId = model.Pk_SubCategoryId == 0 ? null : model.Pk_SubCategoryId;
            DataSet ds = model.CategoryList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    clsCategory obj = new clsCategory();
                    obj.Pk_SubCategoryId = Convert.ToInt32(r["Pk_SubCategoryId"]);
                    obj.Name = r["Name"].ToString();
                    obj.Icon = r["Icon"].ToString();
                    obj.AddedOn = r["AddedOn"].ToString();
                    lstoffer.Add(obj);
                }
                model.lst = lstoffer;
            }
            return View(model);
        }

        public ActionResult DeletSubCategory(int Id)
        {
            string FormName = "";
            string Controller = "";

            clsCategory model = new clsCategory();
            model.DeletedBy = Convert.ToInt32(Session["Pk_AdminId"]);
            if (Id != 0)
            {
                try
                {
                    model.Pk_SubCategoryId = Id;
                    DataSet ds = model.DeleteSubCategory();
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Subcategory"] = "Sub Category Deleted Successfully";
                            FormName = "CategoryList";
                            Controller = "Master";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["Subcategory"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            FormName = "CategoryList";
                            Controller = "Master";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["NFC"] = ex.Message;
                    FormName = "CategoryList";
                    Controller = "Master";

                }
            }
            return RedirectToAction(FormName, Controller);
        }

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
                    //obj.Catalyst = r["Catalyst"].ToString();
                   // obj.Blaze = r["Blaze"].ToString();
                    //obj.Maverick = r["Maverick"].ToString();
                    //obj.Maverick = r["Maverick"].ToString();
                    //obj.Symphony = r["Symphony"].ToString();
                    //obj.Brand = r["Brand"].ToString();
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
                        file.SaveAs(Path.Combine(Server.MapPath(model.EventImage)));
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

        public ActionResult CouponMaster(string Id)
        {
            Master model = new Master();
            if (Id != null)
            {
                model.PK_CouponId = Id;
                DataSet ds = model.SelectCouponList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.PK_CouponId = ds.Tables[0].Rows[0]["PK_CouponId"].ToString();
                    model.Pk_CouponTypeId = ds.Tables[0].Rows[0]["FK_CouponTypeId"].ToString();
                    model.Coupon = ds.Tables[0].Rows[0]["Coupon"].ToString();
                    model.Price = ds.Tables[0].Rows[0]["Price"].ToString();
                    model.ValidityDate = ds.Tables[0].Rows[0]["ValidityDate"].ToString();
                    model.RangeFrom = ds.Tables[0].Rows[0]["RangeFrom"].ToString();
                    model.RangeTo = ds.Tables[0].Rows[0]["RangeTo"].ToString();
                    model.CouponCode = ds.Tables[0].Rows[0]["CouponCode"].ToString();
                }

            }

            #region Bind Coupon Type
            int count = 0;
            List<SelectListItem> ddlCouponType = new List<SelectListItem>();
            DataSet ds1 = model.GetCouponTypeDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlCouponType.Add(new SelectListItem { Value = "0", Text = "Select" });
                    }
                    ddlCouponType.Add(new SelectListItem { Value = r["PK_CouponTypeId"].ToString(), Text = r["CouponType"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlCouponType = ddlCouponType;
            }
            #endregion

            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "btnCoupon")]
        [ActionName("CouponMaster")]
        public ActionResult CouponMaster(Master model)
        {
            try
            {
                if (model.PK_CouponId == null)
                {
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    model.ValidityDate = string.IsNullOrEmpty(model.ValidityDate) ? null : Common.ConvertToSystemDate(model.ValidityDate, "dd/MM/yyyy");
                    DataSet ds = model.SaveCoupon();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Coupon"] = "Coupon  saved successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["Coupon"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                else
                {

                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    model.ValidityDate = string.IsNullOrEmpty(model.ValidityDate) ? null : Common.ConvertToSystemDate(model.ValidityDate, "dd/MM/yyyy");
                    DataSet ds = model.UpdateCoupon();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["Coupon"] = "Coupon  update successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["Coupon"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                TempData["Coupon"] = ex.Message;
            }
            return RedirectToAction("CouponMaster", "Master");
        }

        public ActionResult CouponList()
        {
           Master model = new Master(); 
            //List<Master> lstCoupon = new List<Master>();
            //DataSet ds = model.SelectCouponList();
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        Master obj = new Master();
            //        obj.PK_CouponId = r["PK_CouponId"].ToString();
            //        obj.CouponType = r["CouponType"].ToString();
            //        obj.Coupon = r["Coupon"].ToString();
            //        obj.Price = r["Price"].ToString();
            //        obj.ValidityDate = r["ValidityDate"].ToString();
            //        obj.RangeFrom =r["RangeFrom"].ToString();
            //        obj.RangeTo = r["RangeTo"].ToString();
            //        obj.CouponCode = r["CouponCode"].ToString();
            //        lstCoupon.Add(obj);
            //    }
            //    model.lstCoupon = lstCoupon;
            //}
            List<Master> lstCoupon = new List<Master>();
            DataSet ds = model.SelectCouponList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_CouponId = r["PK_CouponId"].ToString();
                    obj.CouponType = r["CouponType"].ToString();
                    obj.Coupon = r["Coupon"].ToString();
                    obj.Price = r["Price"].ToString();
                    obj.ValidityDate = r["ValidityDate"].ToString();
                    obj.RangeFrom = r["RangeFrom"].ToString();
                    obj.RangeTo = r["RangeTo"].ToString();
                    obj.CouponCode = r["CouponCode"].ToString();
                    lstCoupon.Add(obj);
                }
                model.lstCoupon = lstCoupon;
            }
            return View(model);
           
        }


        [HttpPost]
        [ActionName("CouponList")]
        public ActionResult CouponList(Master model)
        {
            List<Master> lstCoupon = new List<Master>();
            DataSet ds = model.SelectCouponList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_CouponId = r["PK_CouponId"].ToString();
                    obj.CouponType = r["CouponType"].ToString();
                    obj.Coupon = r["Coupon"].ToString();
                    obj.Price = r["Price"].ToString();
                    obj.ValidityDate = r["ValidityDate"].ToString();
                    obj.RangeFrom = r["RangeFrom"].ToString();
                    obj.RangeTo = r["RangeTo"].ToString();
                    obj.CouponCode = r["CouponCode"].ToString();
                    lstCoupon.Add(obj);
                }
                model.lstCoupon = lstCoupon;
            }
            return View(model);
        }



        public ActionResult DeleteCoupon(string Id)
        {
            try
            {
                Master model = new Master();
                model.PK_CouponId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteCoupon();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Coupon"] = "Coupon deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["Coupon"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Coupon"] = ex.Message;
            }
            return RedirectToAction("CouponList", "Master");
        }
        #region ServiceTypeMaster
        public ActionResult ServiceTypeMaster(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.ServiceTypeMasterList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_MainServiceTypeId = r["Pk_MainServiceTypeId"].ToString();
                    obj.MainServiceType = r["MainServiceType"].ToString();
                    obj.Preority = r["Preority"].ToString();
                    obj.InputType = r["InputType"].ToString();
                    obj.IsDeleted = r["IsDeleted"].ToString();
                    obj.DeletedDate = r["DeletedDate"].ToString();
                    lst.Add(obj);
                }
                model.ListServiceTypeMaster = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ServiceTypeMaster")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveServiceTypeMaster(Master model)

        {

            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SavingServiceTypeMaster();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ServiceTypeMaster"] = "Saved Successfully";


                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["ServiceTypeMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ServiceTypeMaster"] = ex.Message;
            }

            return RedirectToAction("ServiceTypeMaster");
        }

        public ActionResult ServiceInActive(string Pk_MainServiceTypeId)
        {
            Master model = new Master();
            model.Pk_MainServiceTypeId = Pk_MainServiceTypeId;
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ServiceInActive();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ServiceTypeMaster"] = "Service InActive Successfully";


                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["ServiceTypeMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ServiceTypeMaster"] = ex.Message;
            }

            return RedirectToAction("ServiceTypeMaster");
        }

        public ActionResult ServiceActive(string Pk_MainServiceTypeId)
        {
            Master model = new Master();
            model.Pk_MainServiceTypeId = Pk_MainServiceTypeId;
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ServiceActive();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ServiceTypeMaster"] = "Service Active Successfully";


                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["ServiceTypeMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ServiceTypeMaster"] = ex.Message;
            }

            return RedirectToAction("ServiceTypeMaster");
        }

        public ActionResult UpdateServiceTypeMaster(string Pk_MainServiceTypeId, string MainServiceType, string Preority)
        {
            Master model = new Master();
            try
            {
                model.MainServiceType = MainServiceType;
                model.Pk_MainServiceTypeId = Pk_MainServiceTypeId;
                model.Preority = Preority;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateServiceTypeMaster();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        model.Result = "Updated Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        model.Result = ds.Tables[0].Rows[0]["ERROR"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Result = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Service
        public ActionResult ServiceMaster(Master model)
        {
            #region Bind SeviceTyoe
            Master obj1 = new Master();

            int count = 0;
            List<SelectListItem> ddlServiceType = new List<SelectListItem>();
            DataSet ds1 = obj1.ServiceTypeMasterList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlServiceType.Add(new SelectListItem { Value = "0", Text = "Select Sevice Type Master" });
                    }
                    ddlServiceType.Add(new SelectListItem { Value = r["Pk_MainServiceTypeId"].ToString(), Text = r["MainServiceType"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlServiceType = ddlServiceType;
            }
            #endregion


            List<Master> lst = new List<Master>();
            DataSet ds = model.ServiceMasterList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_ServiceId = r["Pk_ServiceId"].ToString();
                    obj.Fk_MainServiceTypeId = r["Fk_MainServiceTypeId"].ToString();
                    obj.ServiceIcon = r["ServiceIcon"].ToString();
                    obj.Service = r["Service"].ToString();
                    obj.ServiceUrl = r["ServiceUrl"].ToString();
                    obj.Category = r["Category"].ToString();
                    obj.IsActive = r["IsActive"].ToString();
                    obj.MainServiceType = r["MainServiceType"].ToString();
                    obj.IsActiveDeactiveDate = r["IsActiveDeactiveDate"].ToString();
                    lst.Add(obj);
                }
                model.ListServiceMaster = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ServiceMaster")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveServiceMaster(Master model, IEnumerable<HttpPostedFileBase> postedFile3)

        {
            #region Bind SeviceTyoe
            Master obj1 = new Master();
            model.AddedBy = Session["Pk_AdminId"].ToString();
            int count = 0;
            List<SelectListItem> ddlServiceType = new List<SelectListItem>();
            DataSet ds1 = obj1.ServiceTypeMasterList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlServiceType.Add(new SelectListItem { Value = "0", Text = "Select Sevice Type Master" });
                    }
                    ddlServiceType.Add(new SelectListItem { Value = r["Pk_MainServiceTypeId"].ToString(), Text = r["MainServiceType"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlServiceType = ddlServiceType;
            }
            #endregion

            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();

                foreach (var file in postedFile3)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        model.ServiceIcon = "/images/ServiceIcon/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.ServiceIcon)));
                    }
                }


                DataSet ds = model.SavingServiceMaster();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ServiceMaster"] = "Saved Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["ServiceMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ServiceMaster"] = ex.Message;
            }

            return RedirectToAction("ServiceMaster");
        }
        public ActionResult CouponTypeMaster(string Id)
        {
            Master model = new Master();
            if (Id != null)
            {
                model.CouponTypeId = Id;
                DataSet ds = model.SelectCouponTypeList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.CouponType = ds.Tables[0].Rows[0]["CouponType"].ToString();
                }
            }

            return View(model);
        }
        [HttpPost]
        [ActionName("CouponTypeMaster")]
        public ActionResult CouponTypeMaster(Master model)
        {
            try
            {
                if (model.CouponTypeId == null)
                {
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.SaveCouponType();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["CouponType"] = "Coupon type saved successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["CouponType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                else
                {
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.UpdateCouponType();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["CouponType"] = "Coupon type updated successfully";
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                        {
                            TempData["CouponType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                TempData["CouponType"] = ex.Message;
            }
            return RedirectToAction("CouponTypeMaster", "Master");
        }



        public ActionResult CouponTypeList()
        {
            //Master model = new Master();
            //List<Master> lstCouponType = new List<Master>();
            //DataSet ds = model.SelectCouponTypeList();
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow r in ds.Tables[0].Rows)
            //    {
            //        Master obj = new Master();
            //        obj.CouponTypeId = r["PK_CouponTypeId"].ToString();
            //        obj.CouponType = r["CouponType"].ToString();
            //        lstCouponType.Add(obj);
            //    }
            //    model.lstCouponType = lstCouponType;
            //}
            return View();
        }

        [HttpPost]
        [ActionName("CouponTypeList")]
        public ActionResult CouponTypeList(Master model)
        {
            List<Master> lstCouponType = new List<Master>();

            DataSet ds = model.SelectCouponTypeList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.CouponTypeId = r["PK_CouponTypeId"].ToString();
                    obj.CouponType = r["CouponType"].ToString();
                    lstCouponType.Add(obj);
                }
                model.lstCouponType = lstCouponType;
            }
            return View(model);
        }



        public ActionResult DeleteCouponType(string Id)
        {
            try
            {
                Master model = new Master();
                model.CouponTypeId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteCouponType();
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["CouponType"] = "Coupon type deleted successfully";
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                {
                    TempData["CouponType"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["CouponType"] = ex.Message;
            }
            return RedirectToAction("CouponTypeList", "Master");
        }


        public ActionResult InActive(string Pk_ServiceId)
        {
            Master model = new Master();
            model.Pk_ServiceId = Pk_ServiceId;
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.InActive();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ServiceMaster"] = "Service InActive Successfully";


                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["ServiceMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ServiceMaster"] = ex.Message;
            }

            return RedirectToAction("ServiceMaster");
        }

        public ActionResult Active(string Pk_ServiceId)
        {
            Master model = new Master();
            model.Pk_ServiceId = Pk_ServiceId;
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.Active();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ServiceMaster"] = "Service Active Successfully";


                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["ServiceMaster"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ServiceMaster"] = ex.Message;
            }

            return RedirectToAction("ServiceMaster");
        }

        [HttpPost]
        [OnAction(ButtonName = "btnUpdate")]
        [ActionName("ServiceMaster")]
        public ActionResult UpdateServiceMaster(Master model, IEnumerable<HttpPostedFileBase> postedFile3)
        {
            #region Bind SeviceTyoe
            Master obj1 = new Master();

            int count = 0;
            List<SelectListItem> ddlServiceType = new List<SelectListItem>();
            DataSet ds1 = obj1.ServiceTypeMasterList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlServiceType.Add(new SelectListItem { Value = "0", Text = "Select Sevice Type Master" });
                    }
                    ddlServiceType.Add(new SelectListItem { Value = r["Pk_MainServiceTypeId"].ToString(), Text = r["MainServiceType"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlServiceType = ddlServiceType;
            }
            #endregion
 try
            {

                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                foreach (var file in postedFile3)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        model.ServiceIcon = "/ServiceIcon/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(model.ServiceIcon)));
                    }
                }
                DataSet ds = model.UpdateServiceMaster();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    TempData["ServiceMaster"] = "Updated Successfully";
                }
                else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    TempData["ServiceMaster"] = ds.Tables[0].Rows[0]["ERROR"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["ServiceMaster"] = ex.Message;
            }
            return RedirectToAction("ServiceMaster");
        }


        public ActionResult TransactionLimit(string Id)
        {
            #region Bind ddlTransaction
            Master obj1 = new Master();

            if (Id != null)
            {
                obj1.TransactionLimitId = Id;
                DataSet ds = obj1.GetTransactionLimitDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj1.TransactionLimitId = ds.Tables[0].Rows[0]["PK_TransactionLimitId"].ToString();
                    obj1.FK_TransactionId = ds.Tables[0].Rows[0]["FK_TransactionId"].ToString();
                    obj1.Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                    obj1.Percentage = ds.Tables[0].Rows[0]["Percentage"].ToString();
                }
            }

            int count = 0;
            List<SelectListItem> ddlTransaction = new List<SelectListItem>();
            DataSet ds1 = obj1.GetTransactionNameDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlTransaction.Add(new SelectListItem { Value = "0", Text = "Select Transaction" });
                    }
                    ddlTransaction.Add(new SelectListItem { Value = r["PK_TransactionId"].ToString(), Text = r["TransactionName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlTransaction = ddlTransaction;
            }
            #endregion
            return View(obj1);
        }

        [HttpPost]
        [OnAction(ButtonName = "BtnTransactionLimit")]
        [ActionName("TransactionLimit")]
        public ActionResult TransactionLimit(Master model)
        {
            try
            {
                if (model.TransactionLimitId==null)
                {
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.SaveTransactionLimit();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        TempData["TransactionLimit"] = "Transaction limit save successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["TransactionLimit"] = ds.Tables[0].Rows[0]["ERROR"].ToString();
                    }
                }
                else
                {
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.UpdateTransactionLimit();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        TempData["TransactionLimit"] = "Transaction limit update successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                    {
                        TempData["TransactionLimit"] = ds.Tables[0].Rows[0]["ERROR"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["TransactionLimit"] = ex.Message;
            }
            return RedirectToAction("TransactionLimit", "Master");
        }




        //[HttpPost]
        //[OnAction(ButtonName = "BtnUpdate")]
        //[ActionName("TransactionLimit")]
        //public ActionResult UpdateTransactionLimit(Master model)
        //{
        //    try
        //    {
        //        model.AddedBy = Session["Pk_AdminId"].ToString();
        //        DataSet ds = model.UpdateTransactionLimit();
        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            TempData["TransactionLimit"] = "Transaction limit update successfully";
        //        }
        //        else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
        //        {
        //            TempData["TransactionLimit"] = ds.Tables[0].Rows[0]["ERROR"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["TransactionLimit"] = ex.Message;
        //    }
        //    return RedirectToAction("TransactionLimit", "Master");
        //}






        public ActionResult TransactionLimitList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetTransactionLimitDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.TransactionLimitId = r["PK_TransactionLimitId"].ToString();
                    obj.TransactionName = r["TransactionName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.Percentage = r["Percentage"].ToString();
                    lst.Add(obj);
                }
                model.lstTransactionLimit = lst;
            }
            #region Bind ddlTransaction
            Master obj1 = new Master();

            int count = 0;
            List<SelectListItem> ddlTransaction = new List<SelectListItem>();
            DataSet ds1 = obj1.GetTransactionNameDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlTransaction.Add(new SelectListItem { Value = "0", Text = "Select Transaction" });
                    }
                    ddlTransaction.Add(new SelectListItem { Value = r["PK_TransactionId"].ToString(), Text = r["TransactionName"].ToString() });
                    count = count + 1;
                }
                ViewBag.ddlTransaction = ddlTransaction;
            }
            #endregion
            return View(model);
        }

        //[HttpPost]
        //[OnAction(ButtonName = "btnSearch")]
        //[ActionName("TransactionLimitList")]
        //public ActionResult TransactionLimitList(Master model)
        //{
        //    List<Master> lst = new List<Master>();
        //    DataSet ds = model.GetTransactionLimitDetails();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds.Tables[0].Rows)
        //        {
        //            Master obj = new Master();
        //            obj.TransactionLimitId = r["PK_TransactionLimitId"].ToString();
        //            obj.TransactionName = r["TransactionName"].ToString();
        //            obj.Amount = r["Amount"].ToString();
        //            obj.Percentage = r["Percentage"].ToString();
        //            lst.Add(obj);
        //        }
        //        model.lstTransactionLimit = lst;
        //    }
        //    #region Bind ddlTransaction
        //    Master obj1 = new Master();

        //    int count = 0;
        //    List<SelectListItem> ddlTransaction = new List<SelectListItem>();
        //    DataSet ds1 = obj1.GetTransactionNameDetails();
        //    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds1.Tables[0].Rows)
        //        {
        //            if (count == 0)
        //            {
        //                ddlTransaction.Add(new SelectListItem { Value = "0", Text = "Select Transaction" });
        //            }
        //            ddlTransaction.Add(new SelectListItem { Value = r["PK_TransactionId"].ToString(), Text = r["TransactionName"].ToString() });
        //            count = count + 1;
        //        }
        //        ViewBag.ddlTransaction = ddlTransaction;
        //    }
        //    #endregion
        //    return View(model);
        //}


        public ActionResult Deletetransactionlimit(string Id)
        {
            try
            {
                Master model = new Master();
                model.TransactionLimitId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteTransactionLimit();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    TempData["TransactionLimit"] = "Transaction limit deleted successfully";
                }
                else if (ds.Tables[0].Rows[0]["MSG"].ToString() == "0")
                {
                    TempData["TransactionLimit"] = ds.Tables[0].Rows[0]["ERROR"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["TransactionLimit"] = ex.Message;
            }
            return RedirectToAction("TransactionLimitList", "Master");
        }

    }

    #endregion


}