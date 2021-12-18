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
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult UserDashboard()
        {
            Dashboard obj = new Dashboard();
            List<Dashboard> lstinvestment = new List<Dashboard>();
            obj.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetAssociateDashboard();
            Dashboard model = new Dashboard();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.TotalDownline = ds.Tables[0].Rows[0]["TotalDownline"].ToString();
                ViewBag.TotalDirects = ds.Tables[0].Rows[0]["TotalDirect"].ToString();
                ViewBag.PayoutWalletBalance = string.Format("{0:0.00}", ds.Tables[0].Rows[0]["PayoutWalletBalance"]);
                ViewBag.TotalPayout = ds.Tables[0].Rows[0]["TotalPayout"].ToString();
                ViewBag.TotalDeduction = ds.Tables[0].Rows[0]["TotalDeduction"].ToString();
                ViewBag.TotalAdvance = ds.Tables[0].Rows[0]["TotalAdvance"].ToString();
                ViewBag.TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString();
                ViewBag.TotalInActive = ds.Tables[0].Rows[0]["TotalInActive"].ToString();
                ViewBag.Recognition = ds.Tables[0].Rows[0]["Recoginition"].ToString();
                ViewBag.TotalReseller = ds.Tables[0].Rows[0]["TotalReseller"].ToString();
                ViewBag.TotalDistributor = ds.Tables[0].Rows[0]["TotalDistributor"].ToString();
                ViewBag.TotalAirDrop = ds.Tables[0].Rows[0]["TotalAirDrop"].ToString();
                ViewBag.TotalIncome = ds.Tables[0].Rows[0]["TotalIncome"].ToString();
            }
            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                ViewBag.PaidBusinessLeft = ds.Tables[2].Rows[0]["PaidBusinessLeft"].ToString();
                ViewBag.PaidBusinessRight = ds.Tables[2].Rows[0]["PaidBusinessRight"].ToString();
                ViewBag.TotalBusinessLeft = ds.Tables[2].Rows[0]["TotalBusinessLeft"].ToString();
                ViewBag.TotalBusinessRight = ds.Tables[2].Rows[0]["TotalBusinessRight"].ToString();
                ViewBag.CarryLeft = ds.Tables[2].Rows[0]["CarryLeft"].ToString();
                ViewBag.CarryRight = ds.Tables[2].Rows[0]["CarryRight"].ToString();
                ViewBag.PaidBusinessLeftNew = ds.Tables[2].Rows[0]["PaidBusinessLeft"].ToString();
                ViewBag.PaidBusinessRightNew = ds.Tables[2].Rows[0]["PaidBusinessRight"].ToString();
                ViewBag.TotalBusinessLeftNew = ds.Tables[2].Rows[0]["TotalBusinessLeft"].ToString();
                ViewBag.TotalBusinessRightNew = ds.Tables[2].Rows[0]["TotalBusinessRight"].ToString();
                ViewBag.CarryLeftNew = ds.Tables[2].Rows[0]["CarryLeft"].ToString();
                ViewBag.CarryRightNew = ds.Tables[2].Rows[0]["CarryRight"].ToString();
            }

            if (ds != null && ds.Tables[3].Rows.Count > 0)
            {
                ViewBag.LoginId = ds.Tables[3].Rows[0]["LoginId"].ToString();
                ViewBag.DisplayName = ds.Tables[3].Rows[0]["Name"].ToString();
                ViewBag.JoiningDate = ds.Tables[3].Rows[0]["JoiningDate"].ToString();
                model.SponserId = ds.Tables[3].Rows[0]["SponsorId"].ToString();
                model.SponsorName = ds.Tables[3].Rows[0]["SponsorName"].ToString();
                //model.Leg = ds.Tables[3].Rows[0]["Leg"].ToString();
                model.Fk_SponsorId1 = ds.Tables[3].Rows[0]["FK_SponsorId"].ToString();
                Session["SponserId"] = ds.Tables[3].Rows[0]["SponsorId"].ToString();
                Session["SponsorId"] = ds.Tables[3].Rows[0]["SponsorId"].ToString();
                model.IsAcceptanceTNC = ds.Tables[3].Rows[0]["IsAcceptanceTNC"].ToString();
                Session["IsAcceptanceTNC"] = ds.Tables[3].Rows[0]["IsAcceptanceTNC"].ToString();
            }
            if (ds != null && ds.Tables[4].Rows.Count > 0)
            {

                ViewBag.PaidBusinessLeftNew = ds.Tables[4].Rows[0]["PaidBusinessLeft"].ToString();
                ViewBag.PaidBusinessRightNew = ds.Tables[4].Rows[0]["PaidBusinessRight"].ToString();
                ViewBag.TotalBusinessLeftNew = ds.Tables[4].Rows[0]["TotalBusinessLeft"].ToString();
                ViewBag.TotalBusinessRightNew = ds.Tables[4].Rows[0]["TotalBusinessRight"].ToString();
                ViewBag.CarryLeftNew = ds.Tables[4].Rows[0]["CarryLeft"].ToString();
                ViewBag.CarryRightNew = ds.Tables[4].Rows[0]["CarryRight"].ToString();
            }
            if (ds.Tables != null && ds.Tables.Count > 1 && ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
            {
                Session["UserNFCCode"] = Crypto.EncryptNFC(ds.Tables[5].Rows[0]["Code"].ToString());
            }
            else
            {
                Session["UserNFCCode"] = "na";
            }
            #region Messages

            model.Fk_UserId = Session["Pk_UserId"].ToString();

            List<Dashboard> lst1 = new List<Dashboard>();

            DataSet ds11 = model.GetAllMessagesfordashboard();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    Dashboard Obj = new Dashboard();
                    Obj.Pk_MessageId = r["Pk_MessageId"].ToString();
                    Obj.Fk_UserId = r["Fk_UserId"].ToString();
                    Obj.MemberName = r["Name"].ToString();
                    Obj.MessageTitle = r["MessageTitle"].ToString();
                    Obj.AddedOn = r["AddedOn"].ToString();
                    Obj.Message = r["Message"].ToString();
                    Obj.cssclass = r["cssclass"].ToString();
                    Obj.ProfilePic = r["ProfilePic"].ToString();
                    lst1.Add(Obj);
                }
                model.lstmessages = lst1;
            }
            #endregion Messages

            #region Investment
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[1].Rows)
                {
                    Dashboard Obj = new Dashboard();
                    Obj.ProductName = r["ProductName"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.Status = r["Status"].ToString();

                    lstinvestment.Add(Obj);
                }
                model.lstinvestment = lstinvestment;
            }
            #endregion Investment
            #region Walletledger
            List<Wallet> lst = new List<Wallet>();
            obj.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet dsledger = obj.EwalletLedger();
            if (dsledger.Tables != null && dsledger.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsledger.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["CurrentDate"].ToString();
                    Objload.EwalletBalance = dr["Balance"].ToString();

                    lst.Add(Objload);
                }
                model.lstewalletledger = lst;
            }
            #endregion
            #region Walletledger
            List<Dashboard> lstuser = new List<Dashboard>();
            obj.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet dsUser = obj.userlist();
            if (dsUser.Tables != null && dsUser.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsUser.Tables[0].Rows)
                {
                    Dashboard model1 = new Dashboard();
                    model1.DisplayName = dr["Name"].ToString();
                    model1.ProfilePic = dr["Profile"].ToString();
                    model1.Mobile = dr["LoginId"].ToString();
                    lstuser.Add(model1);
                }
                model.lstuser = lstuser;
            }
            #endregion
            return View(model);
        }
        public ActionResult UserProfile()
        {
            Profile objprofile = new Profile();

            List<Profile> lstprofile = new List<Profile>();
            objprofile.LoginId = Session["LoginId"].ToString();
            Profile obj = new Profile();
            DataSet ds = objprofile.GetUserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.MiddleName = ds.Tables[0].Rows[0]["MiddleName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString();
                obj.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.EmailId = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                obj.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                obj.AccountNumber = ds.Tables[0].Rows[0]["AccountNo"].ToString();
                obj.BankName = ds.Tables[0].Rows[0]["BankName"].ToString();
                obj.BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString();
                obj.IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString();
                obj.ProfilePicture = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                obj.Summary = ds.Tables[0].Rows[0]["Summary"].ToString();
                obj.AccountHolderName = ds.Tables[0].Rows[0]["BankHolderName"].ToString();
                obj.DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString();
                obj.UPIStatus = "Status : " + ds.Tables[0].Rows[0]["UPIStatus"].ToString();
                obj.PaytmStatus = "Status : " + ds.Tables[0].Rows[0]["PaytmStatus"].ToString();
                obj.PhonePeStatus = "Status : " + ds.Tables[0].Rows[0]["PhonePeStatus"].ToString();
                obj.GooglePayStatus = "Status : " + ds.Tables[0].Rows[0]["GooglePayStatus"].ToString();
                obj.UPI = ds.Tables[0].Rows[0]["UPI"].ToString();
                obj.PhonePe = ds.Tables[0].Rows[0]["PhonePe"].ToString();
                obj.Paytm = ds.Tables[0].Rows[0]["Paytm"].ToString();
                obj.GooglePay = ds.Tables[0].Rows[0]["GooglePay"].ToString();

            }
            obj.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet ds1 = obj.GetKYCDocuments();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                obj.AdharNumber = ds1.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.AdharImage = ds1.Tables[0].Rows[0]["AdharImage"].ToString();
                obj.AdharStatus = "Status : " + ds1.Tables[0].Rows[0]["AdharStatus"].ToString();
                obj.PanNumber = ds1.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.PanImage = ds1.Tables[0].Rows[0]["PanImage"].ToString();
                obj.PanStatus = "Status : " + ds1.Tables[0].Rows[0]["PanStatus"].ToString();
                obj.DocumentStatus = "Status : " + ds1.Tables[0].Rows[0]["DocumentStatus"].ToString();
                obj.MemberAccNo = ds1.Tables[1].Rows[0]["MemberAccNo"].ToString();
                obj.MemberBankName = ds1.Tables[1].Rows[0]["MemberBankName"].ToString();
                obj.IFSCCode = ds1.Tables[1].Rows[0]["IFSCCode"].ToString();
                obj.MemberBranch = ds1.Tables[1].Rows[0]["MemberBranch"].ToString();

            }
            //obj.Fk_UserId = Session["Pk_userId"].ToString();
            //DataSet dt2 = obj.GetCardDetail();
            //if (dt2.Tables[0].Rows.Count > 0 && dt2.Tables[0].Rows[0]["Msg"].ToString() == "1")
            //{
            //    obj.FirstName = dt2.Tables[0].Rows[0]["FirstName"].ToString();
            //    obj.Gender = dt2.Tables[0].Rows[0]["Gender"].ToString();
            //    obj.Mobile = dt2.Tables[0].Rows[0]["Mobile"].ToString();
            //    obj.Email = dt2.Tables[0].Rows[0]["Email"].ToString();
            //    obj.FatherName = dt2.Tables[0].Rows[0]["FatherName"].ToString();
            //    obj.Address = dt2.Tables[0].Rows[0]["Address"].ToString();
            //    obj.PinCode = dt2.Tables[0].Rows[0]["PinCode"].ToString();

            //    obj.DOB = dt2.Tables[0].Rows[0]["DOB"].ToString();
            //    obj.NameH = dt2.Tables[0].Rows[0]["NameH"].ToString();
            //    obj.FatherNameH = dt2.Tables[0].Rows[0]["FatherNameH"].ToString();
            //    obj.AddressH = dt2.Tables[0].Rows[0]["AddressH"].ToString();
            //}


           return View(obj);
        }
        [HttpPost]
        [ActionName("UserProfile")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePersonalInfo( Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.PK_UserID = Session["Pk_userId"].ToString();
                DataSet ds = obj.UpdatePersonalInfo();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Personal Details Updated successfully..";
                        TempData["Profile"] = "Yes";
                        FormName = "UserProfile";
                        Controller = "User";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "UserProfile";
                Controller = "User";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("UserProfile")]
        [OnAction(ButtonName = "btnBankUpdate")]
        public ActionResult UpdateBankDetails(HttpPostedFileBase postedCheque, Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                if (postedCheque != null)
                {
                    obj.DocumentImage = "/images/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(postedCheque.FileName);
                    postedCheque.SaveAs(Path.Combine(Server.MapPath(obj.DocumentImage)));
                }
                obj.PK_UserID = Session["Pk_userId"].ToString();
                DataSet ds = obj.UpdateBankInfo();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Bank Details Updated successfully..";
                        TempData["Profile"] = "Yes";
                        FormName = "UserProfile";
                        Controller = "User";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "UserProfile";
                Controller = "User";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("UserProfile")]
        [OnAction(ButtonName = "btnKYCUpdate")]
        public ActionResult KYCDocuments(IEnumerable<HttpPostedFileBase> postedAadhar, IEnumerable<HttpPostedFileBase> postedPan, Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                foreach (var file in postedAadhar)
                {
                    if (file != null && file.ContentLength > 0)
                    {

                        obj.AdharImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.AdharImage)));
                    }
                }
                foreach (var file in postedPan)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath(obj.PanImage)));
                    }
                }
           
                obj.Fk_UserId = Session["Pk_userId"].ToString();

                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                      
                        TempData["UpdateProfile"] = "Kyc upload successfully..";
                        TempData["Profile"] = "Yes";
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "UserProfile";
                Controller = "User";
            }
            return RedirectToAction(FormName, Controller);
        }
        [HttpPost]
        [ActionName("UserProfile")]
        [OnAction(ButtonName = "btnWalletUpdate")]
        public ActionResult UpdateWalletinfo(Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.Fk_UserId = Session["Pk_userId"].ToString();
                DataSet ds = obj.UpdateWalletDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Wallet details  updated successfully..";
                        TempData["Profile"] = "Yes";
                        FormName = "UserProfile";
                        Controller = "User";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "UserProfile";
                Controller = "User";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult SetTransactionPassword()
        {
            Password model = new Password();
            return View(model);
        }
        [HttpPost]
        public ActionResult SetTransactionPassword(Password model)
        {
            try
            {
                Utility util = new Utility();
                model.Fk_UserId = Session["Pk_userId"].ToString();
                DataSet ds = model.GetMobilenumber();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    //var Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Fk_UserId = ds.Tables[0].Rows[0]["Pk_Userid"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    model.OTP = util.GenerateAlphanumeric(6);
                 
                    string Message = "Dear " + model.Name + " Your 6 digit OTP to create/forget/change transactional PIN is " + model.OTP + ". DOST INC";
                    DataSet ds1 = model.GenerateOTPFORPassword();
                    if (ds1 != null && ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0 && ds1.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            Session["MobileNo"] = model.Mobile;
                            Session["UserId"] = model.Fk_UserId;
                            try
                            {
                                string msg = BLSMS.sendSMSUpdated(Message, model.Mobile);
                            }
                            catch (Exception ex)
                            {
                              TempData["ForgetPassword"] = ex.Message;
                            }
                            //Send Mail
                            try
                            {
                                string Subject = "Forgot Password";
                                string message = "Please Find OTP to create your password.<br>" + model.OTP + "<br><br>For any issue please reach us through email or through DOST helpline.";
                                BLMail.SendVerificationMail(model.Name, message, Subject, model.Email);
                            }
                            catch (Exception ex)
                            {
                                //TempData["ForgetPassword"] = ex.Message;
                            }
                            TempData["ForgetPassword"] = "OTP is sent to your mobile no.";
                            return RedirectToAction("ValidateOTP", "User");
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
   
        public ActionResult ValidateOTP()
        {
            Password model = new Password();
            return View(model);
        }
        [HttpPost]
        public ActionResult ValidateOTP(Password model)
        {
            try
            {
                model.Mobile = Session["MobileNo"].ToString();
                model.Fk_UserId = Session["UserId"].ToString();
                DataSet ds = model.ValidateOTP();
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
                                        TempData["ForgetPassword"] = "Transaction Password reset successfully.";
                                        return RedirectToAction("Validate", "User");
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
                        TempData["ForgetPassword"] = "Incorrect OTP. Please Re-enter";
                    }

                }
                else
                {
                    TempData["ForgetPassword"] = "OTP not matched";
                }
            }
            catch (Exception ex)
            {
                TempData["ForgetPassword"] = ex.Message;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("UserDashboard")]
        [OnAction(ButtonName = "btnTransfer")]
        public ActionResult TransferEwallet(Dashboard model)
        {
            try
            {
                Wallet obj = new Wallet();
                obj.MobileNo = model.Mobile;
                obj.TransferAmount = model.Amount;
                obj.AddedBy = Session["Pk_UserId"].ToString();
                DataSet ds = obj.TransferEwalletToEwallet();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        obj.ReceiverMobile = ds.Tables[0].Rows[0]["ToMobileNo"].ToString();
                        obj.SenderMobile = ds.Tables[0].Rows[0]["FromMobileNo"].ToString();
                        obj.ReceiverName = ds.Tables[0].Rows[0]["ToName"].ToString();
                        obj.SenderName = ds.Tables[0].Rows[0]["FromName"].ToString();
                        obj.SenderEmail = ds.Tables[0].Rows[0]["FromEmail"].ToString();
                        obj.ReceiverEmail = ds.Tables[0].Rows[0]["ToEmail"].ToString();
                        TempData["Ewallet"] = "wallet fund transfered successfully.";
                        TempData["Wallet"] = "Yes";
                        try
                        {
                            string str = "Rs. " + obj.Amount + " has been successfully transferred to the user " + obj.ReceiverName + " from your DOST Wallet. Thanks Team - DOST Inc.";
                            string str2 = "You have received Rs. " + obj.Amount + " from the user " + obj.SenderName + " in your DOST Wallet. Team - DOST INC";
                            BLSMS.sendSMSUpdated(str, obj.SenderMobile);
                            BLSMS.sendSMSUpdated(str2, obj.ReceiverMobile);
                        }
                        catch (Exception ex)
                        {

                        }
                        try
                        {
                            string Subject1 = "Wallet Transfer";
                            string Subject2 = "Fund Received";
                            string MailBody1 = "INR " + obj.Amount + " has been successfully transferred from your DOST wallet to user " + obj.ReceiverName + ".<br><br>For any issue please reach us through email or through DOST helpline.";
                            string MailBody2 = "INR " + obj.Amount + " has been successfully credited into your DOST wallet from the user " + obj.SenderName + "<br><br>For any issue please reach us through email or through DOST helpline.";
                            BLMail.SendTransactionMail(obj.SenderName, MailBody1, Subject1, obj.SenderEmail);
                            BLMail.SendTransactionMail(obj.ReceiverName, MailBody2, Subject2, obj.ReceiverEmail);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else
                    {
                        TempData["Ewallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Ewallet"] = ex.Message;

            }
            return RedirectToAction("UserDashboard", "User");

        }
        public JsonResult UpdateProfilePic(string UserId)
        {
            Profile obj = new Profile();
            bool msg = false;
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                HttpPostedFileBase file = files[0];

                string fileName = file.FileName;
                obj.PK_UserID = UserId;
                obj.ProfilePicture = "/images/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(Server.MapPath(obj.ProfilePicture)));
                DataSet ds = obj.UpdateProfilePic();

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        msg = true;
                        Session["ProfilePic"] = obj.ProfilePicture;
                    }
                    else
                    {
                        msg = false;
                    }
                }
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangePassword()
        {
            List<SelectListItem> ddlPasswordType = Common.BindPasswordType();
            ViewBag.ddlPasswordType = ddlPasswordType;
            return View();
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdatePassword(Password obj)
        {
            try
            {
                List<SelectListItem> ddlPasswordType = Common.BindPasswordType();
                ViewBag.ddlPasswordType = ddlPasswordType;
                obj.UpdatedBy = Session["Pk_userId"].ToString();
                obj.OldPassword = Crypto.Encrypt(obj.OldPassword);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["ChangePassword"] = "Password updated successfully..";
                    }
                    else
                    {
                        TempData["ChangePasswordError"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ChangePasswordError"] = ex.Message;
            }
            return View(obj);
        }

        #region Get CardDetails user profile
        public ActionResult GetCarrdDetails(string Fk_UserId)
        {
            Profile obj = new Profile();
            obj.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet dt2 = obj.GetCardDetail();
           if (dt2.Tables[0].Rows.Count > 0 && dt2.Tables[0].Rows[0]["Msg"].ToString() == "1")
            {
                obj.FirstName = dt2.Tables[0].Rows[0]["FirstName"].ToString();
                obj.Gender = dt2.Tables[0].Rows[0]["Gender"].ToString();
                obj.Mobile = dt2.Tables[0].Rows[0]["Mobile"].ToString();
                obj.Email = dt2.Tables[0].Rows[0]["Email"].ToString();
                obj.FatherName = dt2.Tables[0].Rows[0]["FatherName"].ToString();
                obj.Address = dt2.Tables[0].Rows[0]["Address"].ToString();
                obj.Pincode = dt2.Tables[0].Rows[0]["Pincode"].ToString();

                obj.DOB = dt2.Tables[0].Rows[0]["DOB"].ToString();
                obj.NameH = dt2.Tables[0].Rows[0]["NameH"].ToString();
                obj.FatherNameH = dt2.Tables[0].Rows[0]["FatherNameH"].ToString();
                obj.AddressH = dt2.Tables[0].Rows[0]["AddressH"].ToString();
                obj.CardNumber = dt2.Tables[0].Rows[0]["CardNumber"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "No"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region update CardDetails user profile

        [HttpPost]
        [ActionName("UserProfile")]
        [OnAction(ButtonName = "btnRegistration")]
        public  ActionResult Updatecarduserprofile(Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.PK_UserID = Session["Pk_userId"].ToString();
                DataSet ds = obj.Updatecarduserprofile();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Personal Details Updated successfully..";
                        TempData["Profile"] = "Yes";
                        FormName = "UserProfile";
                        Controller = "User";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "UserProfile";
                Controller = "User";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion




        #region Get PanCard user profile
        public ActionResult GetPanDetails(string Fk_UserId)
        {
            Profile obj = new Profile();
            obj.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet dt3 = obj.GetCardPanDetail();
            if (dt3.Tables[0].Rows.Count > 0 && dt3.Tables[0].Rows[0]["Msg"].ToString() == "1")
            {
                obj.FirstName = dt3.Tables[0].Rows[0]["FirstName"].ToString();
                obj.Gender = dt3.Tables[0].Rows[0]["Gender"].ToString();
                obj.Mobile = dt3.Tables[0].Rows[0]["Mobile"].ToString();
                obj.Email = dt3.Tables[0].Rows[0]["Email"].ToString();
                obj.FatherName = dt3.Tables[0].Rows[0]["FatherName"].ToString();
                obj.Address = dt3.Tables[0].Rows[0]["Address"].ToString();
                obj.Pincode = dt3.Tables[0].Rows[0]["Pincode"].ToString();

                obj.DOB = dt3.Tables[0].Rows[0]["DOB"].ToString();
                obj.NameH = dt3.Tables[0].Rows[0]["NameH"].ToString();
                obj.FatherNameH = dt3.Tables[0].Rows[0]["FatherNameH"].ToString();
                obj.AddressH = dt3.Tables[0].Rows[0]["AddressH"].ToString();
                obj.PanNumber = dt3.Tables[0].Rows[0]["CardNumber"].ToString();
               // obj.PanNumber = dt3.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "No"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region update pan card userprofile
        [HttpPost]
        [ActionName("UserProfile")]
        [OnAction(ButtonName = "btnpanupdate")]
        public ActionResult UpdatePancarduserprofile(Profile obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.PK_UserID = Session["Pk_userId"].ToString();
                DataSet ds1 = obj.UpdatePan_carduserprofile();
                if (ds1 != null && ds1.Tables.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateProfile"] = "Pan Details Updated successfully..";
                        TempData["Profile"] = "Yes";
                        FormName = "UserProfile";
                        Controller = "User";
                        //return View();
                    }
                    else
                    {
                        TempData["UpdateProfile"] = ds1.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "UserProfile";
                        Controller = "User";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateProfile"] = ex.Message;
                FormName = "UserProfile";
                Controller = "User";
            }
            return RedirectToAction(FormName, Controller);
        }
        #endregion



        #region
        public ActionResult ApplyFiterForWalletActivity(string TypeDate)
        {
            Dashboard model = new Dashboard();
            List<Wallet> lst = new List<Wallet>();
            model.Fk_UserId = Session["Pk_UserId"].ToString();
            model.TypeDate = TypeDate;
            DataSet dst = model.getdatetypefilter();
            if (dst.Tables != null && dst.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in dst.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["CurrentDate"].ToString();
                    Objload.EwalletBalance = dr["Balance"].ToString();

                    lst.Add(Objload);
                    model.Result = "Yes";
                }
                model.lstewalletledger = lst;

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}