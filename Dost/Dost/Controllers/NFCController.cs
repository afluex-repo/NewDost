using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dost.Models;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace Dost.Controllers
{
    public class NFCController : Controller
    {
        // GET: NFC
        public ActionResult ProfileUpdate()
        {
            #region SocialMedia
            List<SelectListItem> SocialMedia = Common.BindSocialMedia();
            ViewBag.SocialMedia = SocialMedia;
            #endregion SocialMedia
            #region WebLink
            List<SelectListItem> WebLink = Common.BindWebLink();
            ViewBag.WebLink = WebLink;
            #endregion WebLink
            if (Session["LoginId"] == null)
            {
                return RedirectToAction("login_new", "home");
            }

            NFCProfileModel model = new NFCProfileModel();
            //model.Code = desc;
            model.PK_UserId = Session["Pk_userId"].ToString();
            List<NFCProfileModel> lstUerProfile = new List<NFCProfileModel>();

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
                    obj.ProfileName = r["ProfileName"].ToString();
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
                if (Session["Pk_userId"] == null)
                {
                    return RedirectToAction("login_new", "home");
                }
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
                                    model.Message = "Profile updated Successfully";
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
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProfilePersonalData()
        {
            NFCProfileModel model = new NFCProfileModel();
            if (Session["LoginId"] == null)
            {
                return RedirectToAction("login_new", "home");
            }
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
            if (Session["LoginId"] == null)
            {
                return RedirectToAction("login_new", "home");
            }
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
            if (Session["LoginId"] == null)
            {
                return RedirectToAction("login_new", "home");
            }
            model.PK_UserId = Session["Pk_userId"].ToString();
            model.PK_ProfileId = PK_ProfileId;
            DataSet ds = model.GetBusinessProfileById();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Result = "Yes";
                var LinesStatusList = JsonConvert.SerializeObject(ds.Tables[0]);
                return Json(LinesStatusList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("0", JsonRequestBehavior.AllowGet);
            }
        }
    }
}