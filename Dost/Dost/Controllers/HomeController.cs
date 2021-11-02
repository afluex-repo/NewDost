using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dost.Models;
using System.Data;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace Dost.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> index()
        {
            try
            {
                var bank = new BankDetails();
                var IFSC = "PUNB0043100";
                using (var client = new WebClient())
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.GetAsync($"https://ifsc.razorpay.com/{IFSC}"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            bank = JsonConvert.DeserializeObject<BankDetails>(apiResponse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View();
        }
        public ActionResult Login()
        {
            Home model = new Home();
            if (Session["S_MobileNo"] != null)
            {
                model.LoginId = Session["S_MobileNo"].ToString();
            }
            //Session.Abandon();
            return View(model);
        }
        public ActionResult LoginAction(Home obj)
        {
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    obj.UserType = ds.Tables[0].Rows[0]["UserType"].ToString();

                    if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                    {
                        if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                            Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                            Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                            Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                            Session["TransPassword"] = ds.Tables[0].Rows[0]["TransPassword"].ToString();
                            ViewBag.ProfilePic = ds.Tables[0].Rows[0]["Profile"].ToString();
                            Session["ProfilePic"] = ds.Tables[0].Rows[0]["Profile"].ToString();
                            Session["SponsorId"] = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                            Session["SponsorLoginId"] = ds.Tables[0].Rows[0]["SponsorLoginId"].ToString();
                            Session["SponsorName"] = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                            Session["Leg"] = ds.Tables[0].Rows[0]["Leg"].ToString();
                            Session["UserCode"] = ds.Tables[0].Rows[0]["UserCode"].ToString();
                            Session["User_Code"] = ds.Tables[0].Rows[0]["UserCode"].ToString().Remove(0, 2);
                            Session["JoiningDate"] = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                            Session["IsAcceptanceTNC"] = ds.Tables[0].Rows[0]["IsAcceptanceTNC"].ToString();
                            Session["IsDistributor"] = ds.Tables[0].Rows[0]["IsDistributor"].ToString();
                            Session["IsDistributorAplied"] = ds.Tables[0].Rows[0]["IsDistributorAplied"].ToString();
                            Session["IsInvoiceGenerated"] = ds.Tables[0].Rows[0]["IsInvoiceGenerated"].ToString();
                            TempData["LoginResponse"] = "Success";
                            obj.FormName = "UserDashBoard";
                            obj.ControllerName = "User";

                            if (ds.Tables != null && ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                Session["UserNFCCode"] = Crypto.EncryptNFC(ds.Tables[1].Rows[0]["Code"].ToString());
                            }
                            else
                            {
                                Session["UserNFCCode"] = "na";
                            }
                            //NFC Activation Method
                            if (Session["NFCCode"] != null && Session["NFCCode"].ToString() != "" && Session["NFCActivated"] != null && Session["NFCActivated"].ToString() == "false")
                            {
                                return RedirectToAction("NFCActivation");
                            }
                      
                        }
                        else
                        {
                            TempData["LoginResponse"] = "Incorrect Password";
                            obj.FormName = "Login_New";
                            obj.ControllerName = "Home";
                        }
                    }
                    else
                    {
                        TempData["LoginResponse"] = "Invalid Login Id Or Password";
                        obj.FormName = "Login_New";
                        obj.ControllerName = "Home";
                    }
                }
                else
                {
                    TempData["LoginResponse"] = "Invalid Login Id Or Password";
                    obj.FormName = "Login_New";
                    obj.ControllerName = "Home";
                }
            }
            catch (Exception ex)
            {
                TempData["LoginResponse"] = "Oops! Something went wrong, please try again later";
                obj.FormName = "Login_New";
                obj.ControllerName = "Home";
            }
            return RedirectToAction(obj.FormName, obj.ControllerName);
        }
        public async Task<ActionResult> GetBranchByIFSCAsync(string ifsc)
        {
            var bank = new BankDetails();
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync($"https://ifsc.razorpay.com/{ifsc}"))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        bank = JsonConvert.DeserializeObject<BankDetails>(apiResponse);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.ToString()

                });

            }
            return Json(new
            {
                success = true,
                bank

            });
        }

    }
}