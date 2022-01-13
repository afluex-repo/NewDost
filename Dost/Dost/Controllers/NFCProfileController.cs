using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dost.Models;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Net;
//using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.IO;

namespace Dost.Controllers
{
    public class NFCProfileController : BaseController
    {
        // GET: NFCProfile
        // GET: NFC
        public ActionResult ProfileUpdate()
        {
            #region ddlgender
            List<SelectListItem> ddlgender = Common.BindGender();
            ViewBag.ddlgender = ddlgender;
            #endregion ddlgender
            NFCProfileModel model = new NFCProfileModel();
            //model.Code = desc;
            model.PK_UserId = Session["Pk_userId"].ToString();
            List<NFCProfileModel> lstUerProfile = new List<NFCProfileModel>();
            #region ddlEmailList
            int count = 0;
            List<SelectListItem> ddlEmail = new List<SelectListItem>();
            DataSet ds1 = model.GetmailList();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlEmail.Add(new SelectListItem { Text = "Select Email", Value = "0" });
                    }
                    ddlEmail.Add(new SelectListItem { Text = r["Content"].ToString(), Value = r["Pk_NfcProfileId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlEmail = ddlEmail;
            #endregion
            #region Mobile
            int count1 = 0;
            List<SelectListItem> Mobile = new List<SelectListItem>();
            DataSet ds2 = model.GetContactList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        Mobile.Add(new SelectListItem { Text = "Select Mobile No.", Value = "0" });
                    }
                    Mobile.Add(new SelectListItem { Text = r["Content"].ToString(), Value = r["Pk_NfcProfileId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.Mobile = Mobile;

            #endregion
            #region SocialMedia
            List<SelectListItem> SocialMedia = new List<SelectListItem>();
            DataSet ds3 = model.GetSocialMediaList();
            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds3.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        SocialMedia.Add(new SelectListItem { Text = "Select Social Media", Value = "0" });
                    }
                    SocialMedia.Add(new SelectListItem { Text = r["Content"].ToString(), Value = r["Pk_NfcProfileId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.SocialMedia = SocialMedia;
            #endregion SocialMedia
            #region WebLink
            List<SelectListItem> WebLink = new List<SelectListItem>();
            DataSet ds4 = model.GetWebLinkList();
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds4.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        WebLink.Add(new SelectListItem { Text = "Select Web Link", Value = "0" });
                    }
                    WebLink.Add(new SelectListItem { Text = r["Content"].ToString(), Value = r["Pk_NfcProfileId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.WebLink = WebLink;
            #endregion WebLink
            string[] color = { "#FF4C41", "#68CF29", "#51A6F5", "#eb8153", "#FFAB2D", "#eb8153", "#6418C3", "#FF4C90", "#68CF90", "#90A6F9", "#FFAB8D" };
            var i = 0;
            DataSet ds = model.GetNFCProfileData();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.IsProfileTurnedOff = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsProfileTurnedOff"]);
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    NFCProfileModel obj = new NFCProfileModel();
                    obj.PK_ProfileId = r["PK_ProfileId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.LastName = r["LastName"].ToString();
                    obj.ProfilePic = r["ProfilePic"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.BusinessName = r["BusinessName"].ToString();
                    obj.Designation = r["Designation"].ToString();
                    obj.Description = r["Description"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.ProfileType = r["ProfileType"].ToString();

                    if (obj.Status == "Active")
                    {
                        model.PK_ProfileId = obj.PK_ProfileId;
                        model.Leg = obj.Leg;
                    }
                    obj.CardImage = r["CardImage"].ToString();
                    obj.ProfileName = r["ProfileName"].ToString();
                    obj.ActiveStatus = r["IsChecked"].ToString();
                    obj.ColorCode = color[i];
                    lstUerProfile.Add(obj);
                }
                model.lst = lstUerProfile;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ProfileUpdate(NFCProfileModel model)
        {
            try
            {
                #region ddlgender
                List<SelectListItem> ddlgender = Common.BindGender();
                ViewBag.ddlgender = ddlgender;
                #endregion ddlgender
                model.PK_UserId = Session["Pk_userId"].ToString();

                if (model.IsRedirectWeb == true)
                {
                    model.IsRedirect = true;
                }
                if (model.IsRedirectSocial == true)
                {
                    model.IsRedirect = true;
                }
                if (model.IsIncludedContact == true)
                {
                    model.IsIncluded = true;
                }
                if (model.IsIncludedEmail == true)
                {
                    model.IsIncluded = true;
                }
                if (model.IsIncludedWeb == true)
                {
                    model.IsIncluded = true;
                }
                if (model.IsIncludedSocial == true)
                {
                    model.IsIncluded = true;
                }
                if (model.Leg == "undefined")
                {
                    model.Leg = null;
                }
                if (model.PK_ProfileId != null && model.Flag == null)
                {
                    UpdateBusinessProfileForMobile para = new UpdateBusinessProfileForMobile();
                    int FK_NFCProfileId = 0;
                    int IsIncluded = 0;
                    DataTable dtcontact = new DataTable();
                    dtcontact.Columns.Add("FK_NFCProfileId");
                    dtcontact.Columns.Add("IsIncluded");

                    if (model.ContactList != null)
                    {
                        model.ContactList = model.ContactList[0].Split(',');
                        int i = 0;
                        for (i = 0; i < model.ContactList.Length; i++)
                        {
                            if (model.ContactList[i] != "")
                            {
                                FK_NFCProfileId = Convert.ToInt32(model.ContactList[i]);
                                IsIncluded = 1;
                                dtcontact.Rows.Add(FK_NFCProfileId, IsIncluded);
                            }
                        }
                    }
                    DataTable dtEmail = new DataTable();
                    dtEmail.Columns.Add("FK_NFCProfileId");
                    dtEmail.Columns.Add("IsIncluded");
                    if (model.EmailList != null)
                    {
                        model.EmailList = model.EmailList[0].Split(',');
                        int i = 0;
                        for (i = 0; i < model.EmailList.Length; i++)
                        {
                            if (model.EmailList[i] != "")
                            {
                                FK_NFCProfileId = Convert.ToInt32(model.EmailList[i]);
                                IsIncluded = 1;
                                dtEmail.Rows.Add(FK_NFCProfileId, IsIncluded);
                            }
                        }
                    }
                    DataTable dtWebLink = new DataTable();
                    dtWebLink.Columns.Add("FK_NFCProfileId");
                    dtWebLink.Columns.Add("IsIncluded");
                    if (model.WebLinkList != null)
                    {
                        model.WebLinkList = model.WebLinkList[0].Split(',');
                        int i = 0;
                        for (i = 0; i < model.WebLinkList.Length; i++)
                        {
                            if (model.WebLinkList[i] != "")
                            {
                                FK_NFCProfileId = Convert.ToInt32(model.WebLinkList[i]);
                                IsIncluded = 1;
                                dtWebLink.Rows.Add(FK_NFCProfileId, IsIncluded);
                            }
                        }
                    }
                    DataTable dtSocialMedia = new DataTable();
                    dtSocialMedia.Columns.Add("FK_NFCProfileId");
                    dtSocialMedia.Columns.Add("IsIncluded");
                    if (model.SocialLink != null)
                    {
                        model.SocialLink = model.SocialLink[0].Split(',');
                        int i = 0;

                        for (i = 0; i < model.SocialLink.Length; i++)
                        {
                            if (model.SocialLink[i] != "")
                            {
                                FK_NFCProfileId = Convert.ToInt32(model.SocialLink[i]);
                                IsIncluded = 1;
                                dtSocialMedia.Rows.Add(FK_NFCProfileId, IsIncluded);
                            }

                        }
                    }
                    model.dtcontact = dtcontact;
                    model.dtemail = dtEmail;
                    model.dtweblink = dtWebLink;
                    model.dtsocial = dtSocialMedia;
                    model.FK_UserId = Session["Pk_userId"].ToString();
                    DataSet ds = model.UpdateProfileInfo();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {

                            model.Result = "Success";
                            model.Message = "Profile Updated Successfully";
                        }
                        else
                        {
                            model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                }
                if (model.Flag != null && model.Flag != "")
                {
                    if (model.Flag == "Contact")
                    {
                        TempData["FlagResponse"] = "Contact";
                        model.Type = "ContactNo";
                        if (model.Status == "U")
                        {
                            DataSet ds = model.UpdateNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Contact No updated successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Result = "Success";
                                model.Message = "Profile updated successfully";
                            }
                        }
                        else
                        {

                            DataSet ds = model.SaveUpdateContactNoNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Contact No saved successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Contact No not saved successfully";
                            }
                        }
                    }
                    if (model.Flag == "Email")
                    {
                        TempData["FlagResponse"] = "Email";
                        model.Type = "Email";
                        model.Content = model.ContentEmail;
                        if (model.Status == "U")
                        {
                            DataSet ds = model.UpdateNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Email updated successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Email not updated successfully";
                            }
                        }
                        else
                        {
                            model.Content = model.ContentEmail;
                            DataSet ds = model.SaveUpdateContactNoNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Email saved successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Profile not updated successfully";
                            }
                        }
                    }
                    if (model.Flag == "WebLink")
                    {
                        TempData["FlagResponse"] = "WebLink";
                        model.Type = "WebLink";
                        model.Content = model.WebLink + model.ContentWebLinks;
                        if (model.Status == "U")
                        {
                            DataSet ds = model.UpdateNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "WebLink updated successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "WebLink not updated successfully";
                            }
                        }
                        else
                        {
                            model.Content = model.WebLink + model.ContentWebLinks;
                            DataSet ds = model.SaveUpdateContactNoNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "WebLink saved successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "WebLink not saved successfully";
                            }
                        }
                    }
                    if (model.Flag == "SocialMedia")
                    {
                        TempData["FlagResponse"] = "SocialMedia";
                        model.Type = "SocialMedia";
                        model.Content = model.SocialMedia + model.ContentSocialMedia;
                        if (model.Status == "U")
                        {
                            DataSet ds = model.UpdateNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Social Media Link updated Successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {
                                model.Message = "Social Media Link not updated Successfully";
                            }
                        }
                        else
                        {
                            model.Content = model.SocialMedia + model.ContentSocialMedia;
                            DataSet ds = model.SaveUpdateContactNoNfc();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Social Media Link saved successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                            else
                            {

                                model.Message = "Social Media Link not saved successfully";
                            }
                        }
                    }
                    if (model.Flag == "Profile")
                    {
                        if (model.PK_ProfileId == null || model.PK_ProfileId == "")
                        {
                            DataSet ds = model.InsertProfileName();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "New Contact card save Successfully";
                                    model.PK_ProfileId = ds.Tables[0].Rows[0]["PK_ProfileId"].ToString();
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                        }
                    }
                    if (model.Flag == "Business")
                    {
                        TempData["FlagResponse"] = "Business";
                        if (model.PK_ProfileId == null || model.PK_ProfileId == "")
                        {
                            DataSet ds = model.InsertBusinessInfo();
                            if (ds != null && ds.Tables.Count > 0)
                            {
                                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                {
                                    model.Result = "Success";
                                    model.Message = "Profile updated Successfully";
                                }
                                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                {
                                    model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                }
                            }
                        }
                        else
                        {
                            if (Request.Files.Count > 0)
                            {
                                HttpFileCollectionBase files = Request.Files;
                                HttpPostedFileBase fileUploaderControl = null;
                                for (int i = 0; i < files.Count; i++)
                                {
                                    fileUploaderControl = files[i];
                                }
                                if (fileUploaderControl != null)
                                {
                                    string filename = System.IO.Path.GetFileName(fileUploaderControl.FileName);
                                    fileUploaderControl.SaveAs(Server.MapPath("~/images/ProfilePicture/" + filename));
                                    string filepathtosave = "/images/ProfilePicture/" + filename;
                                    model.Name = model.Name;
                                    model.ProfilePic = filepathtosave;
                                    DataSet ds = model.UpdateBusinessInfo();
                                    if (ds != null && ds.Tables.Count > 0)
                                    {
                                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                        {
                                            model.Result = "Success";
                                            model.Message = "Profile Updated Successfully";
                                        }
                                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                        {
                                            model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        model.Message = "Profile Updated Successfully";
                                    }
                                }
                            }
                            else
                            {
                                DataSet ds = model.UpdateBusinessInfo();
                                if (ds != null && ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                    {
                                        model.Result = "Success";
                                        model.Message = "Profile Updated Successfully";
                                    }
                                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                    {
                                        model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                    }
                                }
                                else
                                {
                                    model.Message = "Profile Updated Successfully";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
            }

            return Json(model.Result, model.Message, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProfilePersonalData()
        {
            NFCProfileModel model = new NFCProfileModel();
            model.PK_UserId = Session["Pk_userId"].ToString();
            TempData["ReDesc"] = model.Code;
            DataSet ds = model.GetNFCProfileData();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var LinesStatusList = JsonConvert.SerializeObject(ds.Tables[0]);
                return Json(LinesStatusList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateProfileStatus(string ProfileId, bool IsChecked)
        {
            NFCProfileModel model = new NFCProfileModel();
            model.PK_UserId = Session["Pk_userId"].ToString();
            model.PK_ProfileId = ProfileId;
            DataSet ds = model.UpdateProfileStatus(IsChecked);
            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
            {
                model.Result = "Yes";
            }
            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
            {
                model.Result = "No";
            }
            else
            {
                model.Result = "No";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBusinessProfile(string PK_ProfileId)
        {
            NFCProfileModel model = new NFCProfileModel();
            model.PK_UserId = Session["Pk_userId"].ToString();
            model.PK_ProfileId = PK_ProfileId;
            DataSet ds = model.GetBusinessProfileById();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Result = "Yes";
                var LinesStatusList = JsonConvert.SerializeObject(ds.Tables);
                return Json(LinesStatusList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult GetProfileData(string Type, string PK_ProfileId)
        {
            NFCProfileModel model = new NFCProfileModel();

            model.PK_UserId = Session["Pk_userId"].ToString();
            model.Type = Type;
            if (PK_ProfileId == null)
            {
                model.PK_ProfileId = "0";
            }
            else
            {
                model.PK_ProfileId = PK_ProfileId;
            }
            TempData["ReDesc"] = model.Code;
            DataSet ds = model.GetProfileDataForNFC();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var LinesStatusList = JsonConvert.SerializeObject(ds.Tables[0]);
                return Json(LinesStatusList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteNFCProfileData(string Pk_NfcProfileId)
        {
            NFCProfileModel model = new NFCProfileModel();
            model.PK_UserId = Session["Pk_userId"].ToString();
            model.Pk_NfcProfileId = Convert.ToInt32(Pk_NfcProfileId);
            DataSet ds = model.DeleteNFCProfileData();
            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
            {
                model.Result = "Yes";
                TempData["Update"] = "Success";
            }
            else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
            {
                model.Result = "No";
                TempData["Update"] = "Error";
            }
            else
            {
                model.Result = "No";
                TempData["Update"] = "Error";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NFCDevice()
        {
            Master model = new Master();
            try
            {
                List<Master> lst = new List<Master>();
                model.Fk_MainServiceTypeId = "3";
                model.Fk_UserId= Session["Pk_userId"].ToString();
                // string[] color = { "#FF4C41", "#68CF29", "#51A6F5", "#eb8153", "#FFAB2D", "#eb8153", "#6418C3", "#FF4C90", "#68CF90", "#90A6F9", "#FFAB8D" };
                DataSet ds = model.GetService();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Master obj = new Master();
                        obj.Color = r["ColorCode"].ToString();
                        obj.EncCode = r["ColorCode"].ToString();
                        obj.Pk_ServiceId = r["Pk_ServiceId"].ToString();
                        obj.Fk_MainServiceTypeId = r["Fk_MainServiceTypeId"].ToString();
                        obj.ServiceIcon = r["ServiceIcon"].ToString();
                        obj.Service = r["Service"].ToString();
                        obj.ServiceUrl = r["ServiceUrl"].ToString();
                        obj.Category = r["Category"].ToString();
                        obj.IsActive = r["IsActive"].ToString();
                        obj.IsActiveDeactiveDate = r["IsActiveDeactiveDate"].ToString();
                        lst.Add(obj);
                        i++;
                    }
                    model.lst = lst;
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        public ActionResult SetActionDashboard()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            model.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet ds = model.GetActivatedNFC();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Service = r["Service"].ToString();
                    obj.Color = r["ColorCode"].ToString();
                    obj.ServiceIcon = r["ServiceIcon"].ToString();
                    obj.Pk_ServiceId = r["PK_ServiceId"].ToString();
                    obj.PK_NFcId = Convert.ToInt32(r["PK_NFCId"]);
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult SaveCompanyDetails(NFCProfileModel model)
        {
            return View(model);
        }

        public JsonResult UpdateProfilePicForContactCard(string PK_ProfileId)
        {
            NFCProfileModel obj = new NFCProfileModel();
            bool msg = false;
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];

                string fileName = file.FileName;
                obj.PK_ProfileId = PK_ProfileId;
                obj.ProfilePic = "/images/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePic)));
                DataSet ds = obj.UpdateProfilePic();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Result = "true";
                        obj.Message = "Picture Uploaded succesfully ! ";
                    }
                    else
                    {
                        obj.Result = "false";
                        obj.Message = "Not Uploaded. Kindly try after some times. !! ";
                    }
                }
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUrlForRedirection(string Type)
        {
            NFCProfileModel obj = new NFCProfileModel();
            obj.Type = Type;
            obj.FK_UserId = Session["Pk_userId"].ToString();
            DataSet ds = obj.GetUrlForRedirection();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var JsonList = JsonConvert.SerializeObject(ds.Tables[0]);
                return Json(JsonList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult UpdateRedirectionUrl(string PK_ProfileId, string PK_NFCProfileId, string func)
        {
            NFCProfileModel obj = new NFCProfileModel();
            obj.PK_ProfileId = PK_ProfileId;
            obj.Pk_NfcProfileId = Convert.ToInt32(PK_NFCProfileId);
            obj.FK_UserId = Session["Pk_userId"].ToString();
            if (func == "Update")
            {
                obj.IsRedirect = true;
                obj.IsIncluded = true;
            }
            else if (func == "Remove")
            {
                obj.IsRedirect = false;
                obj.IsIncluded = false;
            }
            else
            {

            }
            DataSet ds = obj.UpdateRedirectionUrl();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    obj.Result = "Success";
                    obj.Message = "Profile updated successfully";
                }
                else
                {
                    obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}