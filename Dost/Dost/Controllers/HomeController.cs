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
using BusinessLayer;

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
                            obj.FormName = "Login";
                            obj.ControllerName = "Home";
                        }
                    }
                    else
                    {
                        TempData["LoginResponse"] = "Invalid Login Id Or Password";
                        obj.FormName = "Login";
                        obj.ControllerName = "Home";
                    }
                }
                else
                {
                    TempData["LoginResponse"] = "Invalid Login Id Or Password";
                    obj.FormName = "Login";
                    obj.ControllerName = "Home";
                }
            }
            catch (Exception ex)
            {
                TempData["LoginResponse"] = "Oops! Something went wrong, please try again later";
                obj.FormName = "Login";
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
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(Home model)
        {
            try
            {
                Utility util = new Utility();
                DataSet ds = model.GetMobilenumber();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    //var Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    model.FK_UserId = ds.Tables[0].Rows[0]["Pk_Userid"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    //Random random = new Random();
                    model.TempPassword = util.GenerateAlphanumeric(10);
                    //Response.Cookies["OTP"]["OTP"] = otp.ToString();
                    //Response.Cookies["UserId"]["UserId"] = UserId.ToString();
                    //Response.Cookies["Pk_Userid"]["Pk_Userid"] = ds.Tables[0].Rows[0]["Pk_Userid"].ToString();
                    string Message = "Dear " + model.Name + " , Your DOST Inc temporary password is " + model.TempPassword + " , use it to reset your password. Thank You.";
                    DataSet ds1 = model.GenerateTemporaryPassword();
                    if (ds1 != null && ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            Session["MobileNo"] = model.Mobile;
                            Session["UserId"] = model.FK_UserId;
                            try
                            {
                                string msg = BLSMS.sendSMSUpdated(Message, model.MobileNo);
                            }
                            catch (Exception ex)
                            {
                                //TempData["ForgetPassword"] = ex.Message;
                            }
                            //Send Mail
                            try
                            {
                                string Subject = "Forgot Password";
                                string message = "Please Find OTP to create your new password.<br>" + model.TempPassword + "<br><br>For any issue please reach us through email or through DOST helpline.";
                                BLMail.SendVerificationMail(model.Name, message, Subject, model.Email);
                            }
                            catch (Exception ex)
                            {
                                //TempData["ForgetPassword"] = ex.Message;
                            }
                            TempData["ForgetPassword"] = "Temporary Password is sent to your mobile no.";
                            return RedirectToAction("ResetPassword", "Home");
                        }
                    }
                }
                else
                {
                    TempData["ForgetPassword"] = "Error Occurred";
                }
            }
            catch (Exception ex)
            {

                TempData["ForgetPassword"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult ResetPassword()
        {
            Home model = new Home();
            return View(model);
        }
        [HttpPost]
        public ActionResult ResetPassword(Home model)
        {
            try
            {
                model.MobileNo = Session["MobileNo"].ToString();
                model.FK_UserId = Session["UserId"].ToString();
                DataSet ds = model.ValidateTemporaryPassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                        {
                            model.NewPassword = Crypto.Encrypt(model.NewPassword);
                            DataSet ds1 = model.ResetPassword();
                            if (ds1 != null && ds1.Tables.Count > 0)
                            {
                                if (ds1.Tables[0].Rows.Count > 0)
                                {
                                    if (ds1.Tables[0].Rows[0]["MSG"].ToString() == "1")
                                    {
                                        TempData["ForgetPassword"] = "Password reset successfully.";
                                        return RedirectToAction("Login", "Home");
                                    }
                                    else if (ds1.Tables[0].Rows[0]["MSG"].ToString() == "2")
                                    {
                                        TempData["ForgetPassword"] = ds1.Tables[0].Rows[0]["Message"].ToString();
                                    }
                                    else
                                    {
                                        TempData["ForgetPassword"] = "Error occured";
                                    }
                                }
                                else
                                {
                                    TempData["ForgetPassword"] = "Not Reset";
                                }
                            }
                            else
                            {
                                TempData["ForgetPassword"] = "Not Reset";
                            }
                        }
                        else
                        {
                            TempData["ForgetPassword"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["ForgetPassword"] = "Incorrect Temporary Password. Please Re-enter";
                    }

                }
                else
                {
                    TempData["ForgetPassword"] = "Temporary Password not matched";
                }
            }
            catch (Exception ex)
            {
                TempData["ForgetPassword"] = ex.Message;
            }
            return View(model);
        }
        public ActionResult SignUp(string PId)
        {
            ViewBag.PId = PId;
            Session["S_MobileNo"] = null;
            Session["S_OTP"] = null;
            Session["S_OTPValidity"] = null;
            Session["S_OTPSendCounter"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Home model)
        {
            try
            {
                DataSet ds = model.CheckMobileNo();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            Session["S_MobileNo"] = model.MobileNo;

                            return RedirectToAction("Login", "Home");
                        }
                        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "2")
                        {
                            Session["S_MobileNo"] = model.MobileNo;
                            string OTP = Common.GenerateRandom();
                            Session["S_OTP"] = OTP;
                            DateTime dt1 = DateTime.Now;
                            DateTime dt2 = dt1.AddMinutes(10);
                            Session["S_OTPValidity"] = dt2;
                            try
                            {
                                if (model.MobileNo != null && model.MobileNo != "")
                                {
                                    string str2 = "Dear User, Your DOST Inc Registration OTP is " + OTP + ". Thank You.";
                                    BLSMS.sendSMSUpdated(str2, model.MobileNo);
                                }
                            }
                            catch (Exception ex)
                            {

                            }
                            model.Response = "Success";
                            if (!string.IsNullOrEmpty(model.PId))
                            {
                                return RedirectToAction("Registration", "Home", new { PId = model.PId });
                            }
                            return RedirectToAction("Registration", "Home");
                        }
                        else
                        {
                            model.Response = "Error Occurred";
                            return RedirectToAction("SignUp", "Home");
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return View(model);
        }
        public ActionResult ResendOTP()
        {
            if (Session["S_MobileNo"] != null)
            {
                if (Session["S_OTPSentCounter"] == null)
                {
                    Session["S_OTPSentCounter"] = 0;
                }
                else
                {
                    Session["S_OTPSentCounter"] = Convert.ToInt32(Session["S_OTPSentCounter"]) + 1;
                }
                string MobileNo = Session["S_MobileNo"].ToString();
                string OTP = Common.GenerateRandom();
                Session["S_OTP"] = OTP;
                DateTime dt1 = DateTime.Now;
                DateTime dt2 = dt1.AddMinutes(10);
                Session["S_OTPValidity"] = dt2;
                string str2 = "Dear User, Your DOST Inc Registration OTP is " + OTP + ". Thank You.";
                string msgs = BLSMS.sendSMS(str2, MobileNo);
                msgs = "Success";
                return Json(msgs, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("SignUp", "Home");
            }
        }
        public ActionResult Registration(string PId)
        {
            try
            {
                if (Session["S_MobileNo"] == null)
                {
                    if (!string.IsNullOrEmpty(PId))
                    {
                        return RedirectToAction("SignUp", "Home", new { PId = PId });
                    }
                    return RedirectToAction("SignUp", "Home");
                }
                //Session.Abandon();
                if (!string.IsNullOrEmpty(PId))
                {
                    var d = Crypto.Decrypt(PId);
                    ViewBag.SponsorId = d.Split('|')[0];
                    //ViewBag.Leg = d.Split('|')[1];
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
        public ActionResult NewSaveSignup(string FirstName, string LastName, string Email, string Password, string Gender, string SponsorId, string Leg, string OTP)
        {

            Home obj = new Home();
            if (SponsorId != null && obj.Leg != null)
            {
                obj.SponsorId = "";
                obj.Leg = null;
            }
            else
            {
                obj.SponsorId = SponsorId;
                obj.Leg = Leg;
            }
            obj.FirstName = FirstName;
            obj.LastName = LastName;
            obj.Email = Email;
            obj.MobileNo = Session["S_MobileNo"].ToString();
            obj.RegistrationBy = "Web";
            obj.Gender = Gender;
            //obj.PinCode = Password;
            //string OTP = Common.GenerateRandom();
            obj.OTP = OTP;

            if (Session["S_OTP"].ToString() == OTP)
            {
                if (Convert.ToDateTime(Session["S_OTPValidity"]) < DateTime.Now)
                {
                    obj.Response = "OTP Expired";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                obj.Response = "OTP Mismatch";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            //string password = Common.GenerateRandom();
            obj.Password = Crypto.Encrypt(Password);
            try
            {
                DataSet ds = obj.SignUp_Web();

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        Session["Login_Id"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["DisplayName"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["PassWord"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["Transpassword"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        OTP = ds.Tables[0].Rows[0]["OTP"].ToString();
                        obj.Response = "Success";
                        Session["S_MobileNo"] = null;
                        Session["S_OTP"] = null;
                        Session["S_OTPValidity"] = null;
                        Session["S_OTPSendCounter"] = null;
                        /*try
                        {
                            //string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            string str2 = "Dear "+ ds.Tables[0].Rows[0]["Name"].ToString() + ", Your Digi Direct registration OTP is "+ OTP + ". Thank You. Team-RetraEx INC ";
                            BLSMS.sendSMS( str2, MobileNumber);
                            obj.Response = "Success";
                            return Json(obj, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception ex)
                        {
                            obj.Response = ex.Message;
                        }*/
                        //Send Mail
                        //try
                        //{
                        //    string str2 = "<p>Dear " + ds.Tables[0].Rows[0]["Name"].ToString() + ",</p>";
                        //    str2 += "<p>You are successfully registered in dost.</p><br><br>";
                        //    str2 += "<p>Thank You.</p>";
                        //    string sub = "Registration Mail";
                        //    BLMail.SendMail(ds.Tables[0].Rows[0]["Name"].ToString(), str2, sub, Email);
                        //    obj.Response = "Success";
                        //    return Json(obj, JsonRequestBehavior.AllowGet);
                        //}
                        //catch (Exception ex)
                        //{
                        //    obj.Response = ex.Message;
                        //}
                        // Login User for viewing profile and NFC Activation process 
                        try
                        {
                            //string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            //BLSMS.SendSMSNew(MobileNumber, str2);
                            obj.LoginId = Session["MobileNo"].ToString();
                            obj.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                            DataSet ds1 = obj.Login();
                            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                            {
                                obj.UserType = ds1.Tables[0].Rows[0]["UserType"].ToString();

                                if ((ds1.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                                {

                                    Session["LoginId"] = ds1.Tables[0].Rows[0]["LoginId"].ToString();
                                    Session["Pk_userId"] = ds1.Tables[0].Rows[0]["Pk_userId"].ToString();
                                    Session["UserType"] = ds1.Tables[0].Rows[0]["UserType"].ToString();
                                    Session["FullName"] = ds1.Tables[0].Rows[0]["FullName"].ToString();
                                    Session["Password"] = ds1.Tables[0].Rows[0]["Password"].ToString();
                                    Session["TransPassword"] = ds1.Tables[0].Rows[0]["TransPassword"].ToString();
                                    ViewBag.ProfilePic = ds1.Tables[0].Rows[0]["Profile"].ToString();
                                    Session["ProfilePic"] = ds1.Tables[0].Rows[0]["Profile"].ToString();
                                    Session["SponsorId"] = ds1.Tables[0].Rows[0]["SponsorId"].ToString();
                                    Session["SponsorLoginId"] = ds1.Tables[0].Rows[0]["SponsorLoginId"].ToString();
                                    Session["SponsorName"] = ds1.Tables[0].Rows[0]["SponsorName"].ToString();
                                    Session["Leg"] = ds1.Tables[0].Rows[0]["Leg"].ToString();
                                    Session["JoiningDate"] = ds1.Tables[0].Rows[0]["JoiningDate"].ToString();
                                    Session["UserCode"] = ds1.Tables[0].Rows[0]["UserCode"].ToString();
                                    Session["IsAcceptanceTNC"] = ds1.Tables[0].Rows[0]["IsAcceptanceTNC"].ToString();
                                    Session["OTPValidationForSignup"] = "true";
                                    //TempData["LoginResponse"] = "Success";
                                    //obj.FormName = "AssociateDashBoard";
                                    //obj.ControllerName = "User";

                                    if (ds1.Tables != null && ds1.Tables.Count > 1 && ds1.Tables[1] != null && ds1.Tables[1].Rows.Count > 0)
                                    {
                                        Session["UserNFCCode"] = Crypto.EncryptNFC(ds1.Tables[1].Rows[0]["Code"].ToString());
                                    }
                                    else
                                    {
                                        Session["UserNFCCode"] = "na";
                                    }


                                    if (Session["NFCCode"] != null && Session["NFCCode"].ToString() != "" && Session["NFCActivated"] != null && Session["NFCActivated"].ToString() == "false")
                                    {
                                        return RedirectToAction("NFCActivation");
                                    }
                                }
                            }
                            obj.Response = "Success";
                            return Json(obj, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception ex)
                        {
                            obj.Response = ex.Message;
                        }
                        // End of Viewing Profile
                        //return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        obj.Response = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(obj.Response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Response = ex.Message;

                return Json(obj, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult OTPValidate()
        {
            if (Session["MobileNo"] != null && Session["MobileNo"].ToString() != "")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Registration");
            }
        }


        public ActionResult VerifyOTP(string MobileNumber, string OTP)
        {
            Home model = new Home();
            //Session.Abandon();
            model.MobileNo = MobileNumber;
            model.OTP = OTP;
            DataSet ds = model.CheckOTP();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    model.Result = "OTP Validation Successfull.";
                    Session["Login_Id"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    Session["DisplayName"] = ds.Tables[0].Rows[0]["Name"].ToString();
                    Session["PassWord"] = "";// Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                    Session["Transpassword"] = "";// Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                    Session["MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                    //OTP = ds.Tables[0].Rows[0]["OTP"].ToString();
                    try
                    {
                        //string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                        //BLSMS.SendSMSNew(MobileNumber, str2);
                        model.LoginId = MobileNumber;
                        model.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        DataSet ds1 = model.Login();
                        if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                        {
                            model.UserType = ds1.Tables[0].Rows[0]["UserType"].ToString();

                            if ((ds1.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                            {

                                Session["LoginId"] = ds1.Tables[0].Rows[0]["LoginId"].ToString();
                                Session["Pk_userId"] = ds1.Tables[0].Rows[0]["Pk_userId"].ToString();
                                Session["UserType"] = ds1.Tables[0].Rows[0]["UserType"].ToString();
                                Session["FullName"] = ds1.Tables[0].Rows[0]["FullName"].ToString();
                                Session["Password"] = ds1.Tables[0].Rows[0]["Password"].ToString();
                                Session["TransPassword"] = ds1.Tables[0].Rows[0]["TransPassword"].ToString();
                                ViewBag.ProfilePic = ds1.Tables[0].Rows[0]["Profile"].ToString();
                                Session["ProfilePic"] = ds1.Tables[0].Rows[0]["Profile"].ToString();
                                Session["SponsorId"] = ds1.Tables[0].Rows[0]["SponsorId"].ToString();
                                Session["SponsorLoginId"] = ds1.Tables[0].Rows[0]["SponsorLoginId"].ToString();
                                Session["SponsorName"] = ds1.Tables[0].Rows[0]["SponsorName"].ToString();
                                Session["Leg"] = ds1.Tables[0].Rows[0]["Leg"].ToString();
                                Session["JoiningDate"] = ds1.Tables[0].Rows[0]["JoiningDate"].ToString();
                                Session["UserCode"] = ds1.Tables[0].Rows[0]["UserCode"].ToString();
                                Session["IsAcceptanceTNC"] = ds1.Tables[0].Rows[0]["IsAcceptanceTNC"].ToString();
                                Session["OTPValidationForSignup"] = "true";
                                //TempData["LoginResponse"] = "Success";
                                //obj.FormName = "AssociateDashBoard";
                                //obj.ControllerName = "User";

                                if (ds1.Tables != null && ds1.Tables.Count > 1 && ds1.Tables[1] != null && ds1.Tables[1].Rows.Count > 0)
                                {
                                    Session["UserNFCCode"] = Crypto.EncryptNFC(ds1.Tables[1].Rows[0]["Code"].ToString());
                                }
                                else
                                {
                                    Session["UserNFCCode"] = "na";
                                }


                                if (Session["NFCCode"] != null && Session["NFCCode"].ToString() != "" && Session["NFCActivated"] != null && Session["NFCActivated"].ToString() == "false")
                                {
                                    return RedirectToAction("NFCActivation");
                                }
                            }
                        }
                        model.Response = "Success";
                        return Json(model, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception ex)
                    {
                        model.Response = ex.Message;
                    }
                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "2")
                {
                    model.Result = "OTP Expired";

                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "3")
                {
                    model.Result = "Invalid OTP";

                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "4")
                {
                    model.Result = "OTP Mismatch";

                }
                else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "5")
                {
                    model.Result = "OTP Already Verified";

                }
            }
            else
            {
                model.Result = "No";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GenerateOTP(string MobileNumber)
        //{
        //    Home model1 = new Home();
        //    GenerateOTPModel model = new GenerateOTPModel();
        //    model.MobileNo = MobileNumber;
        //    string OTP = Common.GenerateRandom();
        //    DataSet ds = model.GenerateOTP(OTP);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //        {
        //            try
        //            {
        //                string str2 = "Digi Direct OTP is " + OTP;
        //                BLSMS.SendSMSNew(model.MobileNo, str2);
        //            }
        //            catch { }

        //            model1.Result = "OTP Generation Successfull.";
        //        }
        //        else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "4")
        //        {
        //            model1.Result = "Record not found";
        //        }
        //        else
        //        {
        //            model1.Result = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        model1.Result = "Error: ";
        //    }
        //    return Json(model1, JsonRequestBehavior.AllowGet);
        //}
        public ActionResult ConfirmationPage()
        {
            return View();
        }
        public ActionResult AdminLogin()
        {
            Home model = new Home();
            Session.Abandon();
            return View(model);
        }
        public ActionResult LoginAction_Admin(Home obj)
        {
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                    {
                        TempData["LoginResponse"] = "Success";
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                        Session["UsertypeName"] = ds.Tables[0].Rows[0]["UsertypeName"].ToString();
                        Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();

                        if (ds.Tables[0].Rows[0]["isFranchiseAdmin"].ToString() == "True")
                        {
                            Session["FranchiseAdminID"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            obj.FormName = "Registration";
                            obj.ControllerName = "FranchiseAdmin";
                        }
                        else
                        {
                            obj.FormName = "AdminDashBoard";
                            obj.ControllerName = "Admin";
                        }
                    }
                    else
                    {
                        TempData["LoginResponse"] = "Invalid Login Id Or Password";
                        obj.FormName = "adminlogin";
                        obj.ControllerName = "Home";
                    }
                }
                else
                {
                    TempData["LoginResponse"] = "Invalid Login Id Or Password";
                    obj.FormName = "adminlogin";
                    obj.ControllerName = "Home";
                }
            }
            catch (Exception ex)
            {
                TempData["LoginResponse"] = "Oops! Something went wrong, please try again later";
                obj.FormName = "adminlogin";
                obj.ControllerName = "Home";
            }
            return RedirectToAction(obj.FormName, obj.ControllerName);
        }
        public virtual ActionResult Menu()
        {
            Home Menu = null;

            if (Session["_Menu"] != null)
            {
                Menu = (Home)Session["_Menu"];
            }
            else
            {

                Menu = Home.GetMenus(Session["Pk_AdminId"].ToString(), Session["UserTypeName"].ToString()); // pass employee id here
                Session["_Menu"] = Menu;
            }
            return PartialView("_Menu", Menu);
        }
    }
}