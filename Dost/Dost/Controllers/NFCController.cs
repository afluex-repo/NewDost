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
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;

namespace Dost.Controllers
{
    public class NFCController : Controller
    {
        public ActionResult Profile(string id)
        {
            var RedirectionLink = "";
            //var enc = Crypto.EncryptNFC("DIGI946211");
            NFCProfileModel objProfile = new NFCProfileModel();
            try
            {

                if (id == null || id == "")
                {
                    return RedirectToAction("Login", "Home");
                }
                //if (Session["LoginId"] == null)
                //{
                //    return RedirectToAction("login_new", "home");
                //}
                NFCModel obj = new NFCModel();
                //DataSet ds11 = obj.GetNFCCodes();
                //if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
                //{

                //    for (int i = 0; i < ds11.Tables[0].Rows.Count; i++)
                //    {
                //        obj.LoginId = ds11.Tables[0].Rows[i]["Id"].ToString();
                //        obj.Code = Crypto.EncryptNFC(ds11.Tables[0].Rows[i]["Code"].ToString());
                //        obj.UpdateNFCEncCode();
                //    }
                //}
                id = id.Replace(" ", "+");

                //var enc = Crypto.EncryptNFC(id);
                var desc = Crypto.DecryptNFC(id);
                //if (Session["LoginId"] != null)
                //{
                //    //

                //}
                //else
                //{

                //List<Wallet> lst = new List<Wallet>();
                obj.Code = desc;
                DataSet ds = obj.CheckNFCCode();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["IsActivated"].ToString() != "" && Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActivated"].ToString()))
                    {
                        //get profile data for activated user
                        obj.Browser = HttpContext.Request.Browser.Browser;
                        obj.IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (string.IsNullOrEmpty(obj.IP))
                        {
                            obj.IP = Request.ServerVariables["REMOTE_ADDR"];
                        }
                        obj.Medium = "Web";
                        List<Location> locations = new List<Location>();
                        string APIKey = "391fd5bcd76d4745a6c883968b50a97124d416db45218ec201a58a6d049cbb9b";
                        string url = string.Format("http://api.ipinfodb.com/v3/ip-city/?key={0}&ip={1}&format=json", APIKey, obj.IP);
                        using (WebClient client = new WebClient())
                        {
                            string json = client.DownloadString(url);
                            Location location = new JavaScriptSerializer().Deserialize<Location>(json);
                            obj.ZipCode = location.ZipCode;
                            obj.Lat = location.Latitude;
                            obj.Long = location.Longitude;
                            obj.Location = location.CityName + ' ' + location.RegionName + ' ' + location.CountryName;
                            obj.Device = GetDevice();
                        }
                        DataSet ds1 = obj.GetNFCProfileData();
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToBoolean(ds1.Tables[0].Rows[0]["IsProfileTurnedOff"]) == true)
                            {
                                return Redirect("https://urdost.com/");
                            }
                            else
                            {
                                DataSet ds2 = obj.InsertLog();
                                if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
                                {
                                    if(ds2.Tables[0].Rows[0][0].ToString()=="1")
                                    {
                                        objProfile.LogId = ds2.Tables[0].Rows[0]["LogId"].ToString();
                                    }
                                }
                                objProfile.Name = ds1.Tables[0].Rows[0]["Name"].ToString();
                                objProfile.Email = ds1.Tables[0].Rows[0]["PrimaryEmail"].ToString();
                                objProfile.DOB = ds1.Tables[0].Rows[0]["DOB"].ToString();
                                objProfile.Mobile = ds1.Tables[0].Rows[0]["Mobile"].ToString();
                                objProfile.ProfilePic = ds1.Tables[0].Rows[0]["ProfilePic"].ToString();
                                objProfile.Gender = ds1.Tables[0].Rows[0]["Sex"].ToString();
                                objProfile.PK_UserId = ds1.Tables[0].Rows[0]["PK_UserId"].ToString();
                                objProfile.Summary = ds1.Tables[0].Rows[0]["Summary"].ToString();
                                objProfile.BusinessName = ds1.Tables[0].Rows[0]["BusinessName"].ToString();
                                objProfile.Designation = ds1.Tables[0].Rows[0]["Designation"].ToString();
                                objProfile.UserCode = ds1.Tables[0].Rows[0]["UserCode"].ToString();
                                objProfile.Leg = ds1.Tables[0].Rows[0]["NFCProfileLeg"].ToString();
                                Session["SponsorId"] = ds1.Tables[0].Rows[0]["LoginId"].ToString();
                                objProfile.ProfilePic = ds1.Tables[0].Rows[0]["ProfilePic"].ToString();


                                //IpInfo ipInfo = new IpInfo();
                                //string info = new WebClient().DownloadString("https://dost.click/" + myIP);
                                //ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                                //RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                                //ipInfo.Country = myRI1.EnglishName;
                            }
                        }
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                        {
                            List<NFCContent> NfcContentList = new List<NFCContent>();

                            foreach (DataRow row in ds1.Tables[1].Rows)
                            {
                                if (row["Type"].ToString() == "Email")
                                {
                                    objProfile.Email = row["Content"].ToString();
                                }
                                NfcContentList.Add(new NFCContent()
                                {
                                    Content = row["Content"].ToString(),
                                    Type = row["Type"].ToString(),
                                    IsWhatsApp = row["IsWhatsapp"].ToString(),
                                    IsPrimary = row["IsPrimary"].ToString(),
                                    IsDisplay= Convert.ToBoolean(row["Display"])
                                });
                            }

                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[2].Rows.Count > 0)
                            {
                                if (ds1.Tables[2].Rows[0]["IsRedirect"].ToString() == "True")
                                {
                                    RedirectionLink = ds1.Tables[2].Rows[0]["Content"].ToString();
                                }
                            }
                            objProfile.NfcContentList = NfcContentList;
                        }
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[3].Rows.Count > 0)
                        {
                            List<UserSkill> lstSkill = new List<UserSkill>();

                            foreach (DataRow row in ds1.Tables[3].Rows)
                            {
                                UserSkill objSkill = new UserSkill();
                                objSkill.Pk_SkillId = row["PK_SkillId"].ToString();
                                objSkill.Skill = row["Skill"].ToString();
                                lstSkill.Add(objSkill);
                            }
                            objProfile.lstSkill = lstSkill;
                        }
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[4].Rows.Count > 0)
                        {
                            List<UserLanguage> lstLanguage = new List<UserLanguage>();

                            foreach (DataRow row in ds1.Tables[4].Rows)
                            {
                                UserLanguage objLanguage = new UserLanguage();
                                objLanguage.Pk_LanguageId = row["PK_LanguageId"].ToString();
                                objLanguage.Language = row["Language"].ToString();
                                lstLanguage.Add(objLanguage);
                            }
                            objProfile.lstLanguage = lstLanguage;
                        }
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[5].Rows.Count > 0)
                        {
                            List<UserAchievement> lstAchievement = new List<UserAchievement>();

                            foreach (DataRow row in ds1.Tables[5].Rows)
                            {
                                UserAchievement objA = new UserAchievement();
                                objA.Pk_AchievementId = row["PK_AchievementId"].ToString();
                                objA.Achievement = row["Achievement"].ToString();
                                lstAchievement.Add(objA);
                            }
                            objProfile.lstAchievement = lstAchievement;
                        }
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[6].Rows.Count > 0)
                        {
                           
                            objProfile.Description = ds1.Tables[6].Rows[0]["Description"].ToString();
                            objProfile.BannerImage = ds1.Tables[6].Rows[0]["BannerImage"].ToString();
                        }
                        ViewBag.ISActivated = true;
                        Session["NFCCode"] = id;
                        Session["NFCActivated"] = "true";
                    }
                    else
                    {
                        //Not Activated login or activation required
                        ViewBag.ISActivated = false;
                        Session["NFCCode"] = id;
                        Session["NFCActivated"] = "false";
                        Session["SponsorId"] = null;
                        Session["Leg"] = null;
                    }
                }
                else
                {
                    ViewBag.NFCResopnse = "Wrong NFC Code";
                }
                //}
            }
            catch (System.Exception ex)
            {
                return RedirectToAction("Login", "Home");
            }
            if (RedirectionLink != "")
            {

                RedirectionLink = RedirectionLink.ToLower();
                if (!RedirectionLink.Contains("http://www.") && !RedirectionLink.Contains("https://www.") && !RedirectionLink.Contains("https://") && !RedirectionLink.Contains("http://"))
                {
                    RedirectionLink = RedirectionLink.Replace("www.", "");
                    RedirectionLink = "http://www." + RedirectionLink;
                }

                //return Content("<script>window.location = 'http://www.example.com';</script>");
                return Redirect(RedirectionLink);
            }
            return View(objProfile);
        }
        public string GetDevice()
        {
            string userAgent = Request.ServerVariables["HTTP_USER_AGENT"];
            Regex OS = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex device = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string device_info = string.Empty;
            if (OS.IsMatch(userAgent))
            {
                device_info = OS.Match(userAgent).Groups[0].Value;
            }
            if (device.IsMatch(userAgent.Substring(0, 4)))
            {
                device_info += device.Match(userAgent).Groups[0].Value;
            }

            if (device_info != "" && device_info != null)
            {

                int index2 = device_info.IndexOf(';');
                string[] authorInfo = device_info.Split(';');
                foreach (string info in authorInfo)
                {
                    device_info = info.Substring(0, info.IndexOf(")") + 1);
                    device_info = device_info.Replace(')', ' ');
                }
            }
            return device_info;
        }
        public ActionResult SaveVCF(string id, string LogId)
        {
            try
            {
                if (id == null || id == "")
                {
                    return RedirectToAction("Login_New", "Home");
                }
                NFCProfileModel objProfile = new NFCProfileModel();

                NFCModel obj = new NFCModel();
                var desc = Crypto.DecryptNFC(id);
                obj.Code = desc;
                DataSet ds = obj.CheckNFCCode();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["IsActivated"].ToString() != "" && Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActivated"].ToString()))
                    {
                        //get profile data for activated user
                        DataSet ds1 = obj.GetNFCProfileData();
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            objProfile.FirstName = ds1.Tables[0].Rows[0]["FirstName"].ToString();
                            objProfile.LastName = ds1.Tables[0].Rows[0]["LastName"].ToString();
                            objProfile.ProfileName = ds1.Tables[0].Rows[0]["ProfileName"].ToString();
                            objProfile.Name = ds1.Tables[0].Rows[0]["Name"].ToString();
                            objProfile.Email = ds1.Tables[0].Rows[0]["PrimaryEmail"].ToString();
                            objProfile.BusinessName = ds1.Tables[0].Rows[0]["BusinessName"].ToString();
                            objProfile.Designation = ds1.Tables[0].Rows[0]["Designation"].ToString();
                            objProfile.DOB = ds1.Tables[0].Rows[0]["DOB"].ToString();
                            objProfile.Mobile = ds1.Tables[0].Rows[0]["Mobile"].ToString();
                            objProfile.ProfilePic = ds1.Tables[0].Rows[0]["ProfilePic"].ToString();
                            objProfile.Gender = ds1.Tables[0].Rows[0]["Sex"].ToString();
                            objProfile.PK_UserId = ds1.Tables[0].Rows[0]["PK_UserId"].ToString();
                            objProfile.PinCode = ds1.Tables[0].Rows[0]["PinCode"].ToString();
                            objProfile.Address = ds1.Tables[0].Rows[0]["Address"].ToString();
                            objProfile.State = ds1.Tables[0].Rows[0]["State"].ToString();
                            objProfile.City = ds1.Tables[0].Rows[0]["City"].ToString();
                            objProfile.Summary = ds1.Tables[0].Rows[0]["Summary"].ToString();
                        }
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
                        {
                            List<NFCContent> NfcContentList = new List<NFCContent>();

                            foreach (DataRow row in ds1.Tables[1].Rows)
                            {
                                if (row["Content"].ToString().Contains("m.youtube.com"))
                                {
                                    row["Content"].ToString().Replace("www.youtube.com", "m.youtube.com");
                                }
                                NfcContentList.Add(new NFCContent()
                                {

                                    Content = row["Content"].ToString(),
                                    Type = row["Type"].ToString(),
                                    IsWhatsApp = row["IsWhatsapp"].ToString()
                                });
                            }

                            objProfile.NfcContentList = NfcContentList;
                        }
                        if (!string.IsNullOrEmpty(objProfile.Name))
                        {

                            var myCard = new VCard
                            {
                                FirstName = objProfile.FirstName,
                                LastName = objProfile.LastName,
                                ProfileName = objProfile.ProfileName,
                                BusinessName = objProfile.BusinessName,
                                Designation = objProfile.Designation,
                                Note = objProfile.Summary,
                                //Organization = "",
                                //JobTitle = "",
                                //Zip = objProfile.PinCode,
                                //StreetAddress = objProfile.Address,
                                //City = objProfile.City,
                                //CountryName = "India",
                                //Phone = objProfile.Mobile,
                                //Mobile = objProfile.Mobile,
                                //OfficialEmail = objProfile.Email

                            };
                            try
                            {
                                if (objProfile.NfcContentList != null && objProfile.NfcContentList.Count > 0)
                                {
                                    List<string> Contacts = new List<string>();
                                    List<string> OtherContacts = new List<string>();
                                    List<string> Email = new List<string>();
                                    List<string> Weblinks = new List<string>();
                                    List<string> SocialLinks = new List<string>();
                                    foreach (var item in objProfile.NfcContentList)
                                    {
                                        if (item.Type == "WebLink")
                                        {
                                            Weblinks.Add(item.Content);
                                        }
                                        else if (item.Type == "SocialMedia")
                                        {
                                            Weblinks.Add(item.Content);
                                        }
                                        else if (item.Type == "ContactNo" && Convert.ToBoolean(item.IsPrimary)==true)
                                        {
                                            Contacts.Add(item.Content);
                                        }
                                        else if (item.Type == "ContactNo" && Convert.ToBoolean(item.IsPrimary) == false)
                                        {
                                            OtherContacts.Add(item.Content);
                                        }
                                        else if (item.Type == "Email")
                                        {
                                            Email.Add(item.Content);
                                        }
                                    }
                                    if (Weblinks != null && Weblinks.Count > 0)
                                    {
                                        myCard.WebLinks = Weblinks;
                                    }
                                    if (Contacts != null && Contacts.Count > 0)
                                    {
                                        myCard.Contacts = Contacts;
                                    }
                                    if (OtherContacts != null && OtherContacts.Count > 0)
                                    {
                                        myCard.OtherContacts = OtherContacts;
                                    }
                                    if (Email != null && Email.Count > 0)
                                    {
                                        myCard.Emails = Email;
                                    }
                                    if (SocialLinks != null && SocialLinks.Count > 0)
                                    {
                                        myCard.SocialLinks = SocialLinks;
                                    }
                                }
                            }
                            catch (Exception exx)
                            {

                            }
                            try
                            {
                                if (!string.IsNullOrEmpty(objProfile.ProfilePic))
                                {
                                    string path = System.Web.HttpContext.Current.Server.MapPath(objProfile.ProfilePic);
                                    myCard.Image = System.IO.File.ReadAllBytes(path);
                                }
                            }
                            catch (Exception exx)
                            {

                            }
                            System.Web.HttpContext.Current.Response.Clear();
                            //System.Web.HttpContext.Current.Response.ContentType = "text/vcard";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-type", "text/x-vcard; charset=utf-8");
                            if (objProfile.FirstName == "")
                            {
                                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + objProfile.FirstName + "Contact.vcf");

                            }
                            else
                            {
                                System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + objProfile.FirstName + ".vcf");
                            }

                            var cardString = myCard.ToString();
                            var inputEncoding = Encoding.Default;
                            var outputEncoding = Encoding.GetEncoding("utf-8");
                            var cardBytes = inputEncoding.GetBytes(cardString);
                            var outputBytes = Encoding.Convert(inputEncoding,
                                                    outputEncoding, cardBytes);
                            System.Web.HttpContext.Current.Response.OutputStream.Write(outputBytes, 0, outputBytes.Length);
                            System.Web.HttpContext.Current.Response.End();
                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                }
                //}
            }
            catch (System.Exception ex)
            {

            }


            return RedirectToAction("Profile", new
            {
                id = id
            });
            //System.Web.HttpContext.Current.Request.QueryString["id"].ToString();
            //return Json("hi", JsonRequestBehavior.AllowGet);
        }
    }
}