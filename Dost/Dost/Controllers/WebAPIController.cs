using BusinessLayer;
using dost;
using Dost.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Linq;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace dost.Controllers
{
    public class WebAPIController : ApiController
    {


        [HttpPost]
        public HttpResponseMessage CheckMobile(CheckMobileModel model)
        {
            try
            {
                string OTP = Common.GenerateRandom();
                DataSet ds = model.CheckMobileNo(OTP);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        try
                        {
                            string str2 = "Dear User, Your DOST Inc Registration OTP is " + OTP + ". Thank You.";
                            BLSMS.sendSMSUpdated(str2, model.MobileNo);
                        }
                        catch { }
                        return Request.CreateResponse(HttpStatusCode.OK,
                           new
                           {
                               StatusCode = HttpStatusCode.OK,
                               Message = "OTP Generated Successfully.",
                               Data = new
                               {

                               }
                           });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "2")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "User already exist with same Mobile No.",
                              Data = new
                              {

                              }
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error: ",

                         });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error: " + ex.Message,

                    });
            }

        }
        [HttpPost]
        public HttpResponseMessage VerifyOTP(CheckOTPModel model)
        {
            try
            {
                DataSet ds = model.CheckOTP();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                           new
                           {
                               StatusCode = HttpStatusCode.OK,
                               Message = "OTP Validation Successfull.",
                               Data = new
                               {

                               }
                           });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "2")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = "OTP Expired",
                              Data = new
                              {

                              }
                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "3")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = "Invalid OTP",
                              Data = new
                              {

                              }
                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "4")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = "OTP Mismatch",
                              Data = new
                              {

                              }
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error: ",

                         });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error: " + ex.Message,

                    });
            }

        }
        [HttpPost]
        public HttpResponseMessage GenerateOTP(GenerateOTPModel model)
        {
            try
            {
                string OTP = Common.GenerateRandom();
                DataSet ds = model.GenerateOTP(OTP);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        try
                        {
                            string str2 = "Dear User, Your DOST Inc Registration OTP is " + OTP + ". Thank You.";
                            BLSMS.sendSMSUpdated(str2, model.MobileNo);
                        }
                        catch { }
                        return Request.CreateResponse(HttpStatusCode.OK,
                           new
                           {
                               StatusCode = HttpStatusCode.OK,
                               Message = "OTP Generation Successfull.",
                               Data = new
                               {

                               }
                           });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "4")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.NotFound,
                              Message = "Record not found",
                              Data = new
                              {

                              }
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error: ",

                         });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error: " + ex.Message,

                    });
            }

        }
        [HttpPost]
        public HttpResponseMessage Register(RegistrationModel model)
        {
            try
            {
                //string password = Common.GenerateRandom();
                model.Password = Crypto.Encrypt(model.Password);
                DataSet ds = model.Registration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        try
                        {
                            //string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            //BLSMS.SendSMSNew(MobileNo, str2);
                        }
                        catch { }
                        return Request.CreateResponse(HttpStatusCode.OK,
                           new
                           {
                               StatusCode = HttpStatusCode.OK,
                               Message = "Registration Successfull",
                               Data = new
                               {
                                   PK_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString(),
                                   LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString(),
                                   Name = ds.Tables[0].Rows[0]["Name"].ToString(),
                                   Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()),
                                   MobileNo = ds.Tables[0].Rows[0]["LoginId"].ToString(),
                                   SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString(),
                                   UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString()
                               }
                           });
                    }
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "2")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Already Registered",
                              Data = new
                              {
                                  PK_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString(),
                                  LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString(),
                                  Name = ds.Tables[0].Rows[0]["Name"].ToString(),
                                  Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()),
                                  MobileNo = ds.Tables[0].Rows[0]["LoginId"].ToString(),
                                  SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString(),
                                  UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString()
                              }
                          });
                    }
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "4")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.NotFound,
                              Message = "Record Not found",
                              Data = new
                              {

                              }
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error: ",

                         });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error: " + ex.Message,

                    });
            }

        }
        [HttpPost]
        public HttpResponseMessage Login(LoginModel model)
        {
            try
            {
                model.Password = Crypto.Encrypt(model.Password);
                DataSet ds = model.Login();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        try
                        {
                            //string str2 = BLSMS.Registration(ds.Tables[0].Rows[0]["Name"].ToString(), ds.Tables[0].Rows[0]["LoginId"].ToString(), Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()));
                            //BLSMS.SendSMSNew(MobileNo, str2);
                        }
                        catch { }
                        return Request.CreateResponse(HttpStatusCode.OK,
                           new
                           {
                               StatusCode = HttpStatusCode.OK,
                               Message = "Login Successful",
                               Data = new
                               {
                                   PK_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString(),
                                   LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString(),
                                   Name = ds.Tables[0].Rows[0]["Name"].ToString(),
                                   Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()),
                                   MobileNo = ds.Tables[0].Rows[0]["LoginId"].ToString(),
                                   UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString(),
                                   UserType = ds.Tables[0].Rows[0]["UserType"].ToString(),
                                   IsDistributor = ds.Tables[0].Rows[0]["IsDistributor"].ToString(),
                                   SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString(),
                                   IsAcceptanceTNC = Convert.ToBoolean(ds.Tables[0].Rows[0]["UserType"])
                               }
                           });
                    }
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "4")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.Unauthorized,
                              Message = "Invalid Credentials",
                              Data = new
                              {

                              }
                          });
                    }

                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error: ",

                         });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error: " + ex.Message,

                    });
            }

        }
        [HttpPost]
        public HttpResponseMessage AssociateDashBoard(DashBoardMobile obj)
        {
            try
            {
                List<DashboardInvestment> lstinvestment = new List<DashboardInvestment>();
                DashboardResponse model = new DashboardResponse();

                model = obj.GetAllMessages();

                DataSet ds = obj.GetAssociateDashboard();
                DashboardInvestment dsInvest = new DashboardInvestment();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    #region Investment
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds.Tables[1].Rows)
                        {
                            DashboardInvestment Obj = new DashboardInvestment();
                            Obj.ProductName = r["ProductName"].ToString();
                            Obj.Amount = r["Amount"].ToString();
                            Obj.Status = r["Status"].ToString();
                            lstinvestment.Add(Obj);
                        }
                        model.lstInvestment = lstinvestment;
                    }
                    #endregion Investment
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        Data = new
                        {
                            TotalDownline = ds.Tables[0].Rows[0]["TotalDownline"].ToString(),
                            TotalDirects = ds.Tables[0].Rows[0]["TotalDirect"].ToString(),
                            PayoutWalletBalance = ds.Tables[0].Rows[0]["PayoutWalletBalance"],
                            TotalPayout = ds.Tables[0].Rows[0]["TotalPayout"].ToString(),
                            TotalDeduction = ds.Tables[0].Rows[0]["TotalDeduction"].ToString(),
                            TotalAdvance = ds.Tables[0].Rows[0]["TotalAdvance"].ToString(),
                            TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString(),
                            TotalInActive = ds.Tables[0].Rows[0]["TotalInActive"].ToString(),
                            Recognition = ds.Tables[0].Rows[0]["Recoginition"].ToString(),
                            PaidBusinessLeft = ds.Tables[2].Rows[0]["PaidBusinessLeft"].ToString(),
                            PaidBusinessRight = ds.Tables[2].Rows[0]["PaidBusinessRight"].ToString(),
                            TotalBusinessLeft = ds.Tables[2].Rows[0]["TotalBusinessLeft"].ToString(),
                            TotalBusinessRight = ds.Tables[2].Rows[0]["TotalBusinessRight"].ToString(),
                            CarryLeft = ds.Tables[2].Rows[0]["CarryLeft"].ToString(),
                            CarryRight = ds.Tables[2].Rows[0]["CarryRight"].ToString(),
                            LoginId = ds.Tables[3].Rows[0]["LoginId"].ToString(),
                            DisplayName = ds.Tables[3].Rows[0]["Name"].ToString(),
                            JoiningDate = ds.Tables[3].Rows[0]["JoiningDate"].ToString(),
                            ProfilePic = ds.Tables[3].Rows[0]["ProfilePic"].ToString(),
                            Name = ds.Tables[3].Rows[0]["Name"].ToString(),
                            SponserId = ds.Tables[3].Rows[0]["SponsorId"].ToString(),
                            CurrentPaidBusinessLeft = ds.Tables[4].Rows[0]["PaidBusinessLeft"].ToString(),
                            CurrentPaidBusinessRight = ds.Tables[4].Rows[0]["PaidBusinessRight"].ToString(),
                            CurrentTotalBusinessLeft = ds.Tables[4].Rows[0]["TotalBusinessLeft"].ToString(),
                            CurrentTotalBusinessRight = ds.Tables[4].Rows[0]["TotalBusinessRight"].ToString(),
                            CurrentCarryLeft = ds.Tables[4].Rows[0]["CarryLeft"].ToString(),
                            CurrentCarryRight = ds.Tables[4].Rows[0]["CarryRight"].ToString(),
                        },
                        lstMessages = model.lst,

                        lstInvestment = model.lstInvestment,
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.InternalServerError,
                          Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                      });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        public HttpResponseMessage ViewProfile(ProfileMobile objprofile)
        {
            try
            {
                string ProfilePicture = "";
                string PassbookImage = "";
                DataSet ds = objprofile.GetUserProfile();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["ProfilePic"].ToString() != "")
                    {
                        ProfilePicture = "https://dost.click" + ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                    }
                    if (ds.Tables[0].Rows[0]["DocumentImage"].ToString() != "")
                    {
                        PassbookImage = "https://dost.click" + ds.Tables[0].Rows[0]["DocumentImage"].ToString();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        Data = new
                        {
                            LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString(),
                            FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString(),
                            LastName = ds.Tables[0].Rows[0]["LastName"].ToString(),
                            JoiningDate = ds.Tables[0].Rows[0]["JoiningDate"].ToString(),
                            Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString(),
                            AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString(),
                            EmailId = ds.Tables[0].Rows[0]["Email"].ToString(),
                            SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString(),
                            SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString(),
                            AccountNumber = ds.Tables[0].Rows[0]["AccountNo"].ToString(),
                            BankName = ds.Tables[0].Rows[0]["BankName"].ToString(),
                            BankBranch = ds.Tables[0].Rows[0]["BankBranch"].ToString(),
                            IFSC = ds.Tables[0].Rows[0]["IFSC"].ToString(),
                            ProfilePicture = ProfilePicture,
                            PassbookImage = PassbookImage,
                            Summary = ds.Tables[0].Rows[0]["Summary"].ToString(),
                            UPI = ds.Tables[0].Rows[0]["UPI"].ToString(),
                            UPIStatus = ds.Tables[0].Rows[0]["UPIStatus"].ToString()
                        }
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        public HttpResponseMessage UpdateProfile(ProfileAPI obj)
        {
            try
            {
                obj.FirstName = string.IsNullOrEmpty(obj.FirstName) ? null : obj.FirstName;
                obj.LastName = string.IsNullOrEmpty(obj.LastName) ? null : obj.LastName;
                obj.Mobile = string.IsNullOrEmpty(obj.Mobile) ? null : obj.Mobile;
                obj.EmailId = string.IsNullOrEmpty(obj.EmailId) ? null : obj.EmailId;
                obj.AccountNumber = string.IsNullOrEmpty(obj.AccountNumber) ? null : obj.EmailId;
                obj.BankName = string.IsNullOrEmpty(obj.BankName) ? null : obj.BankName;
                obj.BankBranch = string.IsNullOrEmpty(obj.BankBranch) ? null : obj.BankBranch;
                obj.IFSC = string.IsNullOrEmpty(obj.IFSC) ? null : obj.IFSC;
                obj.ProfilePicture = string.IsNullOrEmpty(obj.ProfilePicture) ? null : obj.ProfilePicture;
                obj.AdharNo = string.IsNullOrEmpty(obj.AdharNo) ? null : obj.AdharNo;
                obj.Summary = string.IsNullOrEmpty(obj.Summary) ? null : obj.Summary;
                obj.Password = string.IsNullOrEmpty(obj.Password) ? null : obj.Password;
                if (obj.Password != null)
                {
                    obj.Password = Crypto.Encrypt(obj.Password);
                }
                obj.Image = string.IsNullOrEmpty(obj.Image) ? null : "";
                obj.UPIId = string.IsNullOrEmpty(obj.UPIId) ? null : obj.UPIId;
                DataSet ds = obj.UpdateProfile();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Profile Updated Successful",
                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                        });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = "Error: " + ex.Message,

                  });
            }
        }
        //[HttpPost]
        //public HttpResponseMessage SaveMessages(DashboardMessage obj)
        //{

        //    try
        //    {
        //        DataSet ds = obj.SaveMessage();
        //        if (ds != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //               new
        //               {
        //                   StatusCode = HttpStatusCode.OK,
        //                   Message = "Message sent Successfully",
        //               });
        //            }
        //            else
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //                new
        //                {
        //                    StatusCode = HttpStatusCode.InternalServerError,
        //                    Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
        //                });
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK,
        //                   new
        //                   {
        //                       StatusCode = HttpStatusCode.InternalServerError,
        //                       Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
        //                   }); ;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK,
        //          new
        //          {
        //              StatusCode = HttpStatusCode.InternalServerError,
        //              Message = "Error: " + ex.Message,

        //          });
        //    }
        //}
        public HttpResponseMessage UpdateSponsor(Sponsor obj)
        {
            try
            {
                DataSet ds = obj.UpdateSponsor();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Sponsor Updated Successfully",
                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                       });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = "Error: " + ex.Message,

                  });
            }
        }
        [HttpPost]
        public HttpResponseMessage ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                string Email = "";
                string Name = "";
                string OTP = Common.GenerateRandom();
                DataSet ds = model.ForgetPassword(OTP);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        try
                        {
                            string Message = "Dear " + Name + " , Your DOST Inc temporary password is " + OTP + " , use it to reset your password. Thank You.";
                            BLSMS.sendSMSUpdated(Message, model.MobileNo);
                        }
                        catch { }
                        try
                        {
                            string Subject = "Forgot Password";
                            string message = "Please Find OTP to create your new password.<br>" + OTP + "<br><br>For any issue please reach us through email or through DOST helpline.";
                            BLMail.SendVerificationMail(Name, message, Subject, Email);
                        }
                        catch (Exception ex)
                        {
                            //TempData["ForgetPassword"] = ex.Message;
                        }
                        return Request.CreateResponse(HttpStatusCode.OK,
                           new
                           {
                               StatusCode = HttpStatusCode.OK,
                               Message = "Temporary Password Generation Successfull.",
                               Data = new
                               {

                               }
                           });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "4")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.NotFound,
                              Message = "Record not found",
                              Data = new
                              {

                              }
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error: ",

                         });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error: " + ex.Message,

                    });
            }

        }
        [HttpPost]
        public HttpResponseMessage ResetPassword(ResetPasswordModel model)
        {
            try
            {
                model.Password = Crypto.Encrypt(model.Password);
                DataSet ds = model.ResetPassword();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                           new
                           {
                               StatusCode = HttpStatusCode.OK,
                               Message = "Password Reset Successfully.",
                               Data = new
                               {

                               }
                           });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "2")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.Accepted,
                              Message = "Temporary Password Expired",
                              Data = new
                              {

                              }
                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "3")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.Unauthorized,
                              Message = "Temporary Password not Mismatch",
                              Data = new
                              {

                              }
                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "4")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.NotFound,
                              Message = "Record not found",
                              Data = new
                              {

                              }
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error: ",

                         });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error: " + ex.Message,

                    });
            }

        }
        [HttpPost]
        public HttpResponseMessage GetSponserDetails(SponsorDetail obj)
        {
            DataSet ds = obj.GetMemberDetails();
            DataSet ds1 = obj.GetLegDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.OK,
                      Message = "Record Found",
                      Data = new
                      {
                          DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString(),

                      }
                  });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                 new
                 {
                     StatusCode = HttpStatusCode.InternalServerError,
                     Message = "Invalid Sponsor Id",

                 });
            }

        }
        [HttpPost]
        public HttpResponseMessage GetLegDetails(SponsorDetail obj)
        {
            DataSet ds1 = obj.GetLegDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {

                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",

                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error : " + ds1.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                    });
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ds1.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                });
            }
        }
        [HttpPost]
        public HttpResponseMessage UnPaidIncomes(Reports objreports)
        {
            List<Reports> lst = new List<Reports>();
            DataSet ds = objreports.GetUnPaidIncomes();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.FromLoginId = r["FromLoginId"].ToString();
                    obj.FromUserName = r["FromUserName"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.IncomeType = (r["IncomeType"].ToString());
                    obj.Date = (r["CurrentDate"].ToString());
                    lst.Add(obj);
                }
                objreports.lstunpaidincomes = lst;
                return Request.CreateResponse(HttpStatusCode.OK,
              new
              {
                  StatusCode = HttpStatusCode.OK,
                  Message = "Record Found",
                  Data = objreports.lstunpaidincomes
              });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
             new
             {
                 StatusCode = HttpStatusCode.InternalServerError,
                 Message = "Record Not Found",

             });
            }
        }
        public HttpResponseMessage GuestDashboard()
        {
            Master model = new Master();
            List<EventDetail> lstUpcomingEvent = new List<EventDetail>();
            List<GuestDashboardDetail> lstbanner = new List<GuestDashboardDetail>();
            List<EventDetail> lstEvent = new List<EventDetail>();
            DataSet ds = model.BannerList();
            DataSet dsEvent = model.GetRunningEvents();
            DataSet dsUpcoming = model.GetUpcomingEvents();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    GuestDashboardDetail obj = new GuestDashboardDetail();
                    obj.BId = Crypto.Encrypt(r["BannerId"].ToString());
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
                //model.lstBanner = lstbanner;
            }
            #region Event
            if (dsEvent != null && dsEvent.Tables.Count > 0 && dsEvent.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsEvent.Tables[0].Rows)
                {
                    EventDetail obj = new EventDetail();
                    obj.PK_EventId = Convert.ToInt32(r["PK_EventId"].ToString());
                    obj.EventName = r["EventName"].ToString();
                    obj.CategoryId = r["FK_CategoryId"].ToString();
                    obj.EventImage = r["EventImage"].ToString();
                    lstEvent.Add(obj);
                }
                //model.lst = lstEvent;
            }
            if (dsUpcoming != null && dsUpcoming.Tables.Count > 0 && dsUpcoming.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsUpcoming.Tables[0].Rows)
                {
                    EventDetail obj = new EventDetail();
                    obj.PK_EventId = Convert.ToInt32(r["PK_EventId"].ToString());
                    obj.EventName = r["EventName"].ToString();
                    obj.EventImage = r["EventImage"].ToString();
                    obj.CategoryId = r["FK_CategoryId"].ToString();
                    lstUpcomingEvent.Add(obj);
                }
                //model.lstUpcoming = lstUpcomingEvent;
            }
            #endregion
            if (ds != null || dsEvent != null || dsUpcoming != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
            new
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Record Found",
                Data = new
                {
                    lstBanner = lstbanner,
                    lstCurrentEvents = lstEvent,
                    lstUpcomingEvents = lstUpcomingEvent
                }
            });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
            new
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = "Record Not Found",
                Data = new
                {
                    lstBanner = lstbanner,
                    lstCurrentEvents = lstEvent,
                    lstUpcomingEvents = lstUpcomingEvent
                }
            });
            }
        }
        public HttpResponseMessage EventDetails(EventDetailRequest obj)
        {
            try
            {
                List<RelatedEvent> lstEvent = new List<RelatedEvent>();
                DataSet dsEvent = obj.GetAllRelatedEvent();
                DataSet ds = obj.EventList();
                #region Related Event

                if (dsEvent != null && dsEvent.Tables.Count > 0 && dsEvent.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in dsEvent.Tables[0].Rows)
                    {
                        RelatedEvent obj1 = new RelatedEvent();
                        obj1.PK_EventId = Convert.ToInt32(r["PK_EventId"].ToString());
                        obj1.EventName = r["EventName"].ToString();
                        obj1.EventLocation = r["EventLocation"].ToString();
                        obj1.EventDescription = r["EventDescription"].ToString();
                        obj1.StartDate = r["StartDate"].ToString();
                        obj1.EndDate = r["EndDate"].ToString();
                        obj1.EventImage = r["EventImage"].ToString();
                        obj1.OfferName = r["OfferName"].ToString();
                        obj1.NoOfSeats = r["NoOfSeats"].ToString();
                        obj1.CategoryId = r["FK_CategoryId"].ToString();
                        lstEvent.Add(obj1);
                    }
                }
                #endregion

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Not Found",
                        Data = new
                        {
                            PK_EventId = Convert.ToInt32(ds.Tables[0].Rows[0]["PK_EventId"]),
                            EventName = ds.Tables[0].Rows[0]["EventName"].ToString(),
                            EventLocation = ds.Tables[0].Rows[0]["EventLocation"].ToString(),
                            EventDescription = ds.Tables[0].Rows[0]["EventDescription"].ToString(),
                            StartDate = ds.Tables[0].Rows[0]["StartDate"].ToString(),
                            EndDate = ds.Tables[0].Rows[0]["EndDate"].ToString(),
                            EventImage = ds.Tables[0].Rows[0]["EventImage"].ToString(),
                            NoOfSeats = ds.Tables[0].Rows[0]["NoOfSeats"].ToString(),
                            OfferId = Convert.ToInt32(ds.Tables[0].Rows[0]["FK_OfferId"]),
                            CategoryId = ds.Tables[0].Rows[0]["FK_CategoryId"].ToString(),
                        },
                        lstRelatedEvent = lstEvent,
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Record Not Found",
                       Data = new
                       {

                       },
                       lstRelatedEvent = lstEvent
                   });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ex.Message,

                     });
            }
        }
        [HttpPost]
        public HttpResponseMessage CheckEventDetails(TicketRequest model)
        {
            if (model.UserId == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "UserId is required.",
                         Data = new
                         {

                         }
                     });
            }
            else
            {
                DataSet ds = model.GetEventDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        Data = new
                        {
                            ProductPrice = ds.Tables[0].Rows[0]["Price"].ToString(),
                            NoOfSeats = ds.Tables[0].Rows[0]["RemainingSeats"].ToString(),
                            Balance = ds.Tables[0].Rows[0]["Balance"].ToString()
                        }
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Record Not Found",
                       Data = new
                       {

                       }
                   });
                }
            }
        }
        [HttpPost]
        public HttpResponseMessage BookTicket(SaveNFC model)
        {
            DataSet ds = model.SaveEventDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Event booked successfully",
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error : " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                    });
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error : Invalid Details",
                    });
            }
        }

        [HttpPost]
        public HttpResponseMessage KYCDocuments(RequestMobile objKYC)
        {
            DataSet ds = objKYC.GetKYCDocuments();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.OK,
                       Message = "Record Found",
                       Data = new
                       {
                           AdharNumber = ds.Tables[0].Rows[0]["AdharNumber"].ToString(),
                           AdharImage = ds.Tables[0].Rows[0]["AdharImage"].ToString(),
                           AdharStatus = "Status : " + ds.Tables[0].Rows[0]["AdharStatus"].ToString(),
                           PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString(),
                           PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString(),
                           PanStatus = "Status : " + ds.Tables[0].Rows[0]["PanStatus"].ToString(),
                           DocumentNumber = ds.Tables[0].Rows[0]["DocumentNumber"].ToString(),
                           DocumentImage = ds.Tables[0].Rows[0]["DocumentImage"].ToString(),
                           DocumentStatus = "Status : " + ds.Tables[0].Rows[0]["DocumentStatus"].ToString(),
                           MemberAccNo = ds.Tables[1].Rows[0]["MemberAccNo"].ToString(),
                           MemberBankName = ds.Tables[1].Rows[0]["MemberBankName"].ToString(),
                           IFSCCode = ds.Tables[1].Rows[0]["IFSCCode"].ToString(),
                           MemberBranch = ds.Tables[1].Rows[0]["MemberBranch"].ToString(),
                       }
                   });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Record Not Found",
                       Data = new
                       {

                       }
                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage UploadKYCDocuments(IEnumerable<HttpPostedFileBase> postedFile, KYCDocument obj)
        {
            int count = 0;
            try
            {
                foreach (var file in postedFile)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        if (count == 0)
                        {
                            obj.AdharImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath(obj.AdharImage)));
                        }
                        if (count == 1)
                        {
                            obj.PanImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath(obj.PanImage)));
                        }
                        if (count == 2)
                        {
                            obj.DocumentImage = "/KYCDocuments/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath(obj.DocumentImage)));
                        }
                    }
                    count++;
                }
                DataSet ds = obj.UploadKYCDocuments();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Document uploaded successfully",
                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                        });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error : Not Uploaded",
                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                });
            }
        }

        public HttpResponseMessage ChangePassword(PasswordMobile obj)
        {
            try
            {
                obj.OldPassword = Crypto.Encrypt(obj.OldPassword);
                obj.NewPassword = Crypto.Encrypt(obj.NewPassword);
                DataSet ds = obj.UpdatePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Password updated successfully..",
                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                        });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error occurred",
                    });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message,
                });
            }
        }
        [HttpPost]
        public HttpResponseMessage SaveEwalletRequest()
        {
            try
            {
                WalletMobile obj = new WalletMobile();
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                obj.LoginId = HttpContext.Current.Request.Params["LoginId"];
                obj.Amount = HttpContext.Current.Request.Params["Amount"];
                obj.PaymentMode = HttpContext.Current.Request.Params["PaymentMode"];
                obj.DDChequeNo = HttpContext.Current.Request.Params["DDChequeNo"];
                obj.DDChequeDate = HttpContext.Current.Request.Params["DDChequeDate"];
                obj.DDChequeDate = string.IsNullOrEmpty(obj.DDChequeDate) ? null : Common.ConvertToSystemDate(obj.DDChequeDate, "dd/MM/yyyy");
                obj.BankBranch = HttpContext.Current.Request.Params["BankBranch"];
                obj.BankName = HttpContext.Current.Request.Params["BankName"];
                obj.AddedBy = HttpContext.Current.Request.Params["AddedBy"];
                obj.RequestCode = HttpContext.Current.Request.Params["RequestCode"];

                //for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                //{
                var file = HttpContext.Current.Request.Files[0];
                //var fileimagename = file.FileName.Split('/')[0].Split('.')[0];
                //var fileExt = file.FileName.Split('/')[0].Split('.')[1];
                var thisFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                string folderPath = HttpContext.Current.Server.MapPath("~/KYCDocuments/");
                //Check whether Directory(Folder) exists.
                if (!Directory.Exists(folderPath))
                {
                    //If Directory (Folder) does not exists. Create it.
                    Directory.CreateDirectory(folderPath);
                }
                //Save the File to the Directory (Folder).
                file.SaveAs(folderPath + thisFileName);

                DataSet ds = obj.SaveEWalletRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.OK,
                             Message = "Wallet Request Saved Successfully.",
                         });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                         });

                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error occurred",
                         });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                 new
                 {
                     StatusCode = HttpStatusCode.InternalServerError,
                     Message = ex.Message,
                 });
            }
        }
        [HttpPost]
        public HttpResponseMessage EwalletRequestList(RequestMobile objewallet)
        {
            List<WalletMobile> lst = new List<WalletMobile>();
            DataSet ds = objewallet.GetEWalletRequest();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WalletMobile Objload = new WalletMobile();
                    Objload.Amount = dr["Amount"].ToString();
                    Objload.RequestCode = dr["RequestCode"].ToString();
                    Objload.PaymentMode = dr["PaymentMode"].ToString();
                    Objload.Status = dr["Status"].ToString();
                    Objload.DocumentImg = dr["ImageURL"].ToString();
                    Objload.BankName = dr["BankName"].ToString();
                    Objload.BankBranch = dr["BankBranch"].ToString();
                    Objload.DDChequeNo = dr["ChequeDDNo"].ToString();
                    Objload.DDChequeDate = dr["ChequeDDDate"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    lst.Add(Objload);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Record Found",
                    lstEwallet = lst,
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Record not Found",
                });
            }
        }
        [HttpPost]
        public HttpResponseMessage TransferEwallet(TransferEwallet obj)
        {
            try
            {
                string ReceiverMobile = "";
                string SenderMobile = "";
                string ReceiverName = "";
                string SenderName = "";
                string SenderEmail = "";
                string ReceiverEmail = "";
                DataSet ds = obj.TransferEwalletToEwallet();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
                    {
                        ReceiverMobile = ds.Tables[0].Rows[0]["ToMobileNo"].ToString();
                        SenderMobile = ds.Tables[0].Rows[0]["FromMobileNo"].ToString();
                        ReceiverName = ds.Tables[0].Rows[0]["ToName"].ToString();
                        SenderName = ds.Tables[0].Rows[0]["FromName"].ToString();
                        SenderEmail = ds.Tables[0].Rows[0]["FromEmail"].ToString();
                        ReceiverEmail = ds.Tables[0].Rows[0]["ToEmail"].ToString();
                        try
                        {
                            string str = "Rs. " + obj.Amount + " has been successfully transferred to the user " + ReceiverName + " from your DOST Wallet. Thanks Team - DOST Inc.";
                            string str2 = "You have received Rs. " + obj.Amount + " from the user " + SenderName + " in your DOST Wallet. Team - DOST INC";
                            BLSMS.sendSMSUpdated(str, SenderMobile);
                            BLSMS.sendSMSUpdated(str2, ReceiverMobile);
                        }
                        catch (Exception ex)
                        {

                        }
                        //try
                        //{
                        //    string Subject1 = "Wallet Transfer";
                        //    string Subject2 = "Fund Received";
                        //    string MailBody1 = "INR " + obj.Amount + " has been successfully transferred from your DOST wallet to user " + ReceiverName + ".<br><br>For any issue please reach us through email or through DOST helpline.";
                        //    string MailBody2 = "INR " + obj.Amount + " has been successfully credited into your DOST wallet from the user " + SenderName + "<br><br>For any issue please reach us through email or through DOST helpline.";
                        //    BLMail.SendPasswordMail(SenderName, MailBody1, Subject1, SenderEmail);
                        //    BLMail.SendPasswordMail(ReceiverName, MailBody2, Subject2, ReceiverEmail);
                        //}
                        //catch (Exception ex)
                        //{

                        //}
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Wallet transferred Successfully.",
                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                        });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error Occurred"
                    });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = ex.Message
                });
            }
        }
        [HttpPost]
        public HttpResponseMessage EwalletLedger(RequestMobile objewallet)
        {
            List<WalletLedgerMobile> lst = new List<WalletLedgerMobile>();
            int i = 1;
            //if(objewallet.FromDate =="")
            //{
            //    objewallet.FromDate = null;
            //}
            //if (objewallet.ToDate == "")
            //{
            //    objewallet.ToDate = null;
            //}
            objewallet.FromDate = string.IsNullOrEmpty(objewallet.FromDate) ? null : Common.ConvertToSystemDate(objewallet.FromDate, "dd/MM/yyyy");
            objewallet.ToDate = string.IsNullOrEmpty(objewallet.ToDate) ? null : Common.ConvertToSystemDate(objewallet.ToDate, "dd/MM/yyyy");
            DataSet ds = objewallet.EwalletLedger();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WalletLedgerMobile Objload = new WalletLedgerMobile();
                    Objload.SrNo = i;
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["CurrentDate"].ToString();
                    Objload.EwalletBalance = dr["Balance"].ToString();
                    lst.Add(Objload);
                    i++;
                }
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Record Found",
                    lstWallet = lst
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
               new
               {
                   StatusCode = HttpStatusCode.InternalServerError,
                   Message = "Record not found"
               });
            }
        }
        [HttpPost]
        public HttpResponseMessage DirectList(DirectListRequest model)
        {
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            if (model.Name == "")
            {
                model.Name = null;
            }
            if (model.FromActivationDate == "")
            {
                model.FromActivationDate = null;
            }
            if (model.ToActivationDate == "")
            {
                model.ToActivationDate = null;
            }
            if (model.Status == "")
            {
                model.Status = null;
            }
            List<DirectListMobile> lst = new List<DirectListMobile>();
            DataSet ds = model.GetDirectList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DirectListMobile obj = new DirectListMobile();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.SponsorId = (r["SponsorId"].ToString());
                    obj.SponsorName = (r["SponsorName"].ToString());
                    obj.Name = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Record found",
                    lstDirect = lst
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Record not found",
                });
            }
        }
        [HttpPost]
        public HttpResponseMessage DirectList2(DirectListRequest model)
        {

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<DirectListMobile> lst = new List<DirectListMobile>();
            if (model.Name == "")
            {
                model.Name = null;
            }
            if (model.FromActivationDate == "")
            {
                model.FromActivationDate = null;
            }
            if (model.ToActivationDate == "")
            {
                model.ToActivationDate = null;
            }
            if (model.Status == "")
            {
                model.Status = null;
            }
            DataSet ds = model.GetDirectListL2();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DirectListMobile obj = new DirectListMobile();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.Name = (r["Name"].ToString());
                    obj.SponsorName = (r["SponsorName"].ToString());
                    obj.SponsorId = (r["SponsorId"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Record found",
                    lstDirect = lst
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Record Not found"
                });
            }
        }
        [HttpPost]
        public HttpResponseMessage DownLineList(DirectListRequest model)
        {
            List<DirectListMobile> lst = new List<DirectListMobile>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : string.Format("{0:MMMM dd, yyyy}", model.FromDate);
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : string.Format("{0:MMMM dd, yyyy}", model.ToDate);
            DataSet ds = model.GetDownlineList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DirectListMobile obj = new DirectListMobile();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Record found",
                    lstDownline = lst
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
               new
               {
                   StatusCode = HttpStatusCode.InternalServerError,
                   Message = "Record Not found"
               });
            }
        }
        [HttpPost]
        public HttpResponseMessage PayoutLedger(Payout obj)
        {
            List<WalletLedgerMobile> lst = new List<WalletLedgerMobile>();
            DataSet ds = obj.PayoutLedger();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WalletLedgerMobile Objload = new WalletLedgerMobile();
                    Objload.Narration = dr["Narration"].ToString();
                    Objload.DrAmount = dr["DrAMount"].ToString();
                    Objload.CrAmount = dr["CrAmount"].ToString();
                    Objload.AddedOn = dr["TransactionDate"].ToString();
                    Objload.EwalletBalance = dr["Balance"].ToString();
                    lst.Add(Objload);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Record found",
                    lstPayout = lst
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Record Not found",
                });
            }
        }
        [HttpPost]
        public HttpResponseMessage PayoutReport(PayoutReport obj)
        {
            List<PayoutReportData> lst1 = new List<PayoutReportData>();
            DataSet ds11 = obj.GetPayoutReport();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    PayoutReportData Obj = new PayoutReportData();
                    Obj.EncryptLoginID = Crypto.Encrypt(r["LoginId"].ToString());
                    Obj.EncryptPayoutNo = Crypto.Encrypt(r["PayoutNo"].ToString());

                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.PayoutNo = r["PayoutNo"].ToString();
                    Obj.ClosingDate = r["ClosingDate"].ToString();
                    Obj.BinaryIncome = r["BinaryIncome"].ToString();
                    Obj.DirectIncome = r["DirectIncome"].ToString();
                    Obj.GrossAmount = r["GrossAmount"].ToString();
                    Obj.TDSAmount = r["TDSAmount"].ToString();
                    Obj.ProcessingFee = r["ProcessingFee"].ToString();
                    Obj.NetAmount = r["NetAmount"].ToString();
                    Obj.LeadershipBonus = r["DirectLeaderShipBonus"].ToString();
                    Obj.ProductWallet = r["ProductWallet"].ToString();
                    lst1.Add(Obj);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
               new
               {
                   StatusCode = HttpStatusCode.OK,
                   Message = "Record found",
                   lstPayout = lst1
               });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
               new
               {
                   StatusCode = HttpStatusCode.InternalServerError,
                   Message = "Record Not found"
               });
            }
        }
        [HttpPost]
        public HttpResponseMessage BusinessReport(BusinessRequest model)
        {
            List<BusinessReport> lst1 = new List<BusinessReport>();
            model.Leg = string.IsNullOrEmpty(model.Leg) ? null : model.Leg;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds11 = model.BusinessReport();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    BusinessReport Obj = new BusinessReport();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["FirstName"].ToString();
                    Obj.Leg = r["Leg"].ToString();
                    Obj.ClosingDate = r["CalculationDate"].ToString();
                    Obj.NetAmount = r["AMount"].ToString();
                    Obj.LeadershipBonus = r["BV"].ToString();
                    lst1.Add(Obj);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
              new
              {
                  StatusCode = HttpStatusCode.OK,
                  Message = "Record found",
                  lstAsscoaite = lst1,
                  TotalNetAmount = double.Parse(ds11.Tables[0].Compute("sum(AMount)", "").ToString()).ToString("n2"),
                  TotalBV = double.Parse(ds11.Tables[0].Compute("sum(BV)", "").ToString()).ToString("n2")
              });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
             new
             {
                 StatusCode = HttpStatusCode.InternalServerError,
                 Message = "Record Not found",
             });
            }
        }
        [HttpPost]
        public HttpResponseMessage PaidPayoutDetails(PaidPayout model)
        {
            List<PaidPayoutDetail> lst1 = new List<PaidPayoutDetail>();
            DataSet ds11 = model.PaidPayoutDetailsList();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    PaidPayoutDetail Obj = new PaidPayoutDetail();

                    Obj.Amount = r["Amount"].ToString();
                    Obj.Description = r["Description"].ToString();
                    Obj.PaymentDate = r["Paymentdate"].ToString();
                    Obj.TransactionDate = r["TransactionDate"].ToString();
                    Obj.TransactionNo = r["TransactionNo"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Name = r["Name"].ToString();
                    lst1.Add(Obj);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Record found",
                    lstPaidPayout = lst1
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Record not found"
                });
            }
        }
        [HttpPost]
        public HttpResponseMessage TopupReport(TopUp newdata)
        {
            List<TopUpDataMobile> lst1 = new List<TopUpDataMobile>();
            newdata.Package = newdata.Package == "0" ? null : newdata.Package;
            newdata.FromDate = string.IsNullOrEmpty(newdata.FromDate) ? null : Common.ConvertToSystemDate(newdata.FromDate, "dd/MM/yyyy");
            newdata.ToDate = string.IsNullOrEmpty(newdata.ToDate) ? null : Common.ConvertToSystemDate(newdata.ToDate, "dd/MM/yyyy");
            DataSet ds11 = newdata.GetTopupReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    TopUpDataMobile Obj = new TopUpDataMobile();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.PK_CategoryId = r["PK_CategoryId"].ToString();
                    Obj.PK_EventId = r["PK_EventId"].ToString();
                    Obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    Obj.CategoryName = r["CategoryName"].ToString();
                    Obj.EventName = r["EventName"].ToString();
                    Obj.BookingCode = r["BookingCode"].ToString();
                    Obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    Obj.TopUpAmount = r["TopUpAmt"].ToString();
                    Obj.UsedFor = r["UsedFor"].ToString();
                    Obj.ActivationStatus = r["ActivationStatus"].ToString();
                    lst1.Add(Obj);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
               new
               {
                   StatusCode = HttpStatusCode.OK,
                   Message = "Record not found",
                   lstTopUp = lst1
               });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
               new
               {
                   StatusCode = HttpStatusCode.InternalServerError,
                   Message = "Record not found"
               });
            }
        }

        [HttpPost]
        public HttpResponseMessage DCMIReport(RequestMobile model)
        {
            List<DCMIReport> lst1 = new List<DCMIReport>();
            DataSet ds11 = model.GetDCMIReportForAssociate();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    DCMIReport Obj = new DCMIReport();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.Pk_DCMIId = r["Pk_DCMIId"].ToString();
                    Obj.Month = r["Month"].ToString();
                    Obj.Name = r["Name"].ToString();
                    Obj.TotalMatching = r["TotalMatching"].ToString();
                    Obj.TransactionDate = r["TransactionDate"].ToString();
                    Obj.DCMIIncome = r["DCMIIncome"].ToString();
                    Obj.TotalBV = r["TotalBV"].ToString();
                    Obj.DCMIGrossIncome = r["DCMIGrossIncome"].ToString();
                    Obj.TDS = r["TDS"].ToString();
                    Obj.AdminCharge = r["AdminCharge"].ToString();
                    lst1.Add(Obj);
                }
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Record found",
                    lstDCMI = lst1
                });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Record Not found"
                });
            }
        }
        [HttpPost]
        public HttpResponseMessage SubscriptionList(SubscriptionRequest model)
        {

            List<SubscriptionResponse> lst1 = new List<SubscriptionResponse>();
            try
            {
                DataSet ds11 = model.GetBookingEventDetails();
                if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds11.Tables[0].Rows)
                    {
                        SubscriptionResponse Obj = new SubscriptionResponse();
                        Obj.LoginId = r["LoginId"].ToString();
                        Obj.SubscriptionId = r["PK_EventId"].ToString();
                        Obj.SubscriptionName = r["EventName"].ToString();
                        Obj.SubscriptionImage = r["EventImage"].ToString();
                        Obj.SubscriptionDescription = r["EventDescription"].ToString();
                        Obj.Name = r["Name"].ToString();
                        Obj.Email = r["Email"].ToString();
                        Obj.StartDate = r["StartDate"].ToString();
                        Obj.EndDate = r["EndDate"].ToString();
                        Obj.MobileNo = r["Mobile"].ToString();
                        Obj.NoOfSeats = r["NoOfSeats"].ToString();
                        Obj.Price = r["EventPrice"].ToString();
                        Obj.offername = r["offername"].ToString();
                        Obj.CouponCode = r["CouponCode"].ToString();
                        Obj.OfferValidity = r["OfferValidity"].ToString();
                        Obj.IGST = r["IGST"].ToString();
                        Obj.CGST = r["CGST"].ToString();
                        Obj.SGST = r["SGST"].ToString();
                        Obj.BinaryPercentage = r["BinaryPercentage"].ToString();
                        Obj.ReferralPercentage = r["ReferralPercentage"].ToString();
                        Obj.OfferPrice = r["OfferPrice"].ToString();
                        Obj.Discount = r["Discount"].ToString();
                        Obj.DeliveryCharges = r["DeliveryCharges"].ToString();

                        lst1.Add(Obj);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record found",
                        lstSubscription = lst1
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Record Not found"
                    });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error : " + ex.Message
                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage UnusedCoupon(UnsuseCoupon obj)
        {
            try
            {
                DataSet ds = obj.GetUsedUnUsedcouponsAssociate();
                List<WalletResponse> lst = new List<WalletResponse>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        WalletResponse model = new WalletResponse();
                        model.CouponCode = dr["CouponCode"].ToString();
                        model.CategoryName = dr["CategoryName"].ToString();
                        model.CreatedDate = dr["CreatedDate"].ToString();
                        model.CuopnStatus = dr["CuopnStatus"].ToString();
                        model.EventName = dr["EventName"].ToString();
                        model.CouponPrice = dr["CouponPrice"].ToString();
                        lst.Add(model);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.OK,
                       Message = "Record Found",
                       lstunusedcoupons = lst
                   });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Record Not Found",

                   });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }

        public HttpResponseMessage UsedCoupon(UseCoupon objewallet)
        {
            objewallet.Status = "Used";
            List<WalletResponseUse> lst = new List<WalletResponseUse>();
            DataSet ds = objewallet.GetUsedcouponsAssociate();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    WalletResponseUse Objload = new WalletResponseUse();
                    Objload.CouponCode = dr["CouponCode"].ToString();
                    Objload.CategoryName = dr["CategoryName"].ToString();
                    Objload.CreatedDate = dr["CreatedDate"].ToString();
                    Objload.CuopnStatus = dr["CuopnStatus"].ToString();
                    Objload.EventName = dr["EventName"].ToString();
                    Objload.CouponPrice = dr["CouponPrice"].ToString();
                    Objload.RegisteredTo = dr["RegisteredTo"].ToString();
                    lst.Add(Objload);
                }
                objewallet.lstunusedpins = lst;
                return Request.CreateResponse(HttpStatusCode.OK,
               new
               {
                   StatusCode = HttpStatusCode.OK,
                   Message = "Record not found",
                   lstTopUp = lst
               });
            }

            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,
               new
               {
                   StatusCode = HttpStatusCode.InternalServerError,
                   Message = "Record not found"
               });
            }
        }
        public HttpResponseMessage ActivateUserId(ActivateUserid model)
        {
            try
            {
                model.TopupDate = Common.ConvertToSystemDate(model.TopupDate, "dd/MM/yyyy"); ;
                DataSet ds = model.ActivateUserId();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
              new
              {
                  StatusCode = HttpStatusCode.OK,
                  Message = "User Activated"
              });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
               new
               {
                   StatusCode = HttpStatusCode.InternalServerError,
                   Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
               });
                    }

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
               new
               {
                   StatusCode = HttpStatusCode.InternalServerError,
                   Message = "Error Occured"
               });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
              new
              {
                  StatusCode = HttpStatusCode.InternalServerError,
                  Message = "Error: " + ex.Message
              });
            }
        }

        #region Tree
        public HttpResponseMessage Tree(TreeAPI model)
        {

            TreeAPIResponse obj = new TreeAPIResponse();
            if (model.LoginId == "" || model.LoginId == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
              new
              {
                  Status = "1",
                  Message = "Please enter LoginId"
              });
            }
            if (model.Fk_headId == "" || model.Fk_headId == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
             new
             {
                 Status = "1",
                 Message = "Please enter headId"
             });
            }
            try
            {
                DataSet ds = model.GetTree();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["msg"].ToString() == "0")
                    {

                        List<Tree> GetGenelogy = new List<Tree>();
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Tree obj1 = new Tree();
                            obj1.Fk_UserId = r["Fk_UserId"].ToString();
                            obj1.Fk_ParentId = r["Fk_ParentId"].ToString();
                            obj1.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                            obj1.SponsorId = r["SponsorId"].ToString();
                            obj1.LoginId = r["LoginId"].ToString();
                            obj1.TeamPermanent = r["TeamPermanent"].ToString();
                            obj1.MemberName = r["MemberName"].ToString();
                            obj1.MemberLevel = r["MemberLevel"].ToString();
                            obj1.Leg = r["Leg"].ToString();
                            obj1.Id = r["Id"].ToString();

                            obj1.ActivationDate = r["ActivationDate"].ToString();
                            obj1.ActiveLeft = r["ActiveLeft"].ToString();
                            obj1.ActiveRight = r["ActiveRight"].ToString();
                            obj1.InactiveLeft = r["InactiveLeft"].ToString();
                            obj1.InactiveRight = r["InactiveRight"].ToString();
                            obj1.BusinessLeft = r["BusinessLeft"].ToString();
                            obj1.BusinessRight = r["BusinessRight"].ToString();
                            obj1.ImageURL = r["ImageURL"].ToString();
                            GetGenelogy.Add(obj1);
                        }
                        obj.GetGenelogy = GetGenelogy;
                        obj.Message = "Tree";
                        obj.Status = "0";
                        obj.LoginId = model.LoginId;
                        obj.Fk_headId = model.Fk_headId;

                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            Status = "1",
                            Message = "No Data Found"
                        });
                    }

                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           Status = "1",
                           Message = "No Data Found"
                       });
                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            Status = "1",
                            Message = ex.Message
                        });
            }
            return Request.CreateResponse(HttpStatusCode.OK,
                       obj);
        }
        #endregion

        [HttpPost]
        public HttpResponseMessage GetSponsor(SponsorMobile obj)
        {
            try
            {
                DataSet ds = obj.GetSponsor();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          Data = new
                          {
                              SponsorName = ds.Tables[0].Rows[0]["FullName"].ToString(),
                              PK_UserId = ds.Tables[0].Rows[0]["SponsorId"].ToString(),
                          }
                      });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = ds.Tables[0].Rows[0]["Message"].ToString(),
                     });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Invalid Sponsor Id",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = ex.Message,

                     });
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAllEvent()
        {
            GetData obj = new GetData();
            try
            {
                DataSet ds = obj.GetAllEvent();
                List<SubCategory> lst = new List<SubCategory>();
                List<Event> lst1 = new List<Event>();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            SubCategory obj1 = new SubCategory();
                            obj1.SubCategoryId = dr["Pk_SubCategoryId"].ToString();
                            obj1.SubCategoryName = dr["SubcatogryName"].ToString();
                            obj1.SubCategoryIcon = dr["SubCatogryIcon"].ToString();
                            lst.Add(obj1);
                        }
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            Event obj2 = new Event();
                            obj2.EventId = dr["PK_EventId"].ToString();
                            obj2.EventName = dr["EventName"].ToString();
                            obj2.StartDate = dr["StartDate"].ToString();
                            obj2.EndDate = dr["EndDate"].ToString();
                            obj2.EventLocation = dr["EventLocation"].ToString();
                            obj2.EventImage = dr["EventImage"].ToString();
                            lst1.Add(obj2);
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstSubCategory = lst,
                          lstEvent = lst1
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAllSubscription()
        {
            GetData obj = new GetData();
            try
            {
                DataSet ds = obj.GetAllSubscription();
                List<Subscription> lst = new List<Subscription>();
                List<Subscription> lst1 = new List<Subscription>();
                List<SubCategory> lst2 = new List<SubCategory>();
                if (ds != null && ds.Tables.Count > 0)
                {

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Subscription obj1 = new Subscription();
                            obj1.SubscriptionId = dr["Pk_SubscriptionsId"].ToString();
                            obj1.SubscriptionName = dr["Subscriptions"].ToString();
                            obj1.CreatedDate = dr["CreatedDate"].ToString();
                            obj1.SubscriptionImage = dr["SubscriptionImage"].ToString();
                            obj1.SubCategory = dr["SubCatogry"].ToString();
                            lst1.Add(obj1);
                        }
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            Subscription obj2 = new Subscription();
                            obj2.SubscriptionId = dr["Pk_SubscriptionsId"].ToString();
                            obj2.SubscriptionName = dr["Subscriptions"].ToString();
                            obj2.SubscriptionImage = dr["SubscriptionImage"].ToString();
                            lst.Add(obj2);
                        }
                    }
                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[2].Rows)
                        {
                            SubCategory obj3 = new SubCategory();
                            obj3.SubCategoryName = dr["SubcatogryName"].ToString();
                            obj3.SubCategoryIcon = dr["SubCatogryIcon"].ToString();
                            lst2.Add(obj3);
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstSubscription = lst1,
                          lstSubCategory = lst2,
                          lstSub = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }


        [HttpPost]
        public HttpResponseMessage GetTermsAndConditions()
        {

            return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Success",
                    Data = "<h5> DOST Terms of Use</h5>" +

                           " DOST provides a personalized subscription service that allows our members to access movies and TV shows (“DOST content”) streamed over the Internet to certain Internet-connected TVs, computers and other devices (“DOST ready devices”).These Terms of Use govern your use of our service. As used in these Terms of Use, “DOST service”, “our service” or “the service” means the personalized service provided by DOST for discovering and watching DOST content, including all features and functionalities, recommendations and reviews, the website, and user interfaces, as well as all content and software associated with our service. " +
                            "<br />" +
                            "<b>1.Subscription/Products </b>" +
                            "<br />" +
                            "1.1. Your DOST Partner’s Subscription will continue until terminated. To use the DOST service you must have Internet access and a DOST ready device, and provide us with one or more Payment Methods. “Payment Method” means a current, valid, accepted method of payment, as may be updated from time to time, and which may include payment through your account with a third party. Unless you cancel your membership before your billing date, you authorize us to charge the subscription fee for the next billing cycle to your Payment Method (see “Cancellation” below)." +
                            "<br />" +
                            "1.2. We may offer a number of membership plans, including memberships offered by third parties in conjunction with the provision of their own products and services. We are not responsible for the products and services provided by such third parties. Some membership plans may have differing conditions and limitations, which will be disclosed at your sign-up or in other communications made available to you. You can find specific details regarding your DOST membership by visiting our website and clicking on the “Account” link available at the top of the pages under your profile name." +
                            "<br />" +
                            "2.Promotional Offers. We may from time to time offer special promotional offers, plans or memberships (“Offers”). Offer eligibility is determined by DOST at its sole discretion and we reserve the right to revoke an Offer and put your account on hold in the event that we determine you are not eligible. Members of households with an existing or recent DOST membership may not be eligible for certain introductory Offers. We may use information such as device ID, method of payment or an account email address used with an existing or recent DOST membership to determine Offer eligibility. The eligibility requirements and other limitations and conditions will be disclosed when you sign-up for the Offer or in other communications made available to you." +
                            "<br />" +
                            "<b>3.Renewal and Purchase </b>" +
                            "<br />" +
                            "3.1. Billing Cycle. The membership fee for the DOST service and any other charges you may incur in connection with your use of the service, such as taxes and possible transaction fees, will be charged to your Payment Method on the specific payment date indicated on the “Account” page. The length of your billing cycle will depend on the type of subscription that you choose when you signed up for the service. In some cases your payment date may change, for example if your Payment Method has not successfully settled, when you change your subscription plan or if your paid membership began on a day not contained in a given month. Visit our website and click on the “Billing details” link on the “Account” page to see your next payment date. We may authorize your Payment Method in anticipation of membership or service-related charges through various methods, including authorizing it for up to approximately one month of service as soon as you register. If you signed up for DOST using your account with a third party as a Payment Method, you can find the billing information about your DOST membership by visiting your account with the applicable third party." +
                            "<br />" +
                            "3.2. Payment Methods. To use the DOST service you must provide one or more Payment Methods. You authorize us to charge any Payment Method associated to your account in case your primary Payment Method is declined or no longer available to us for payment of your subscription fee. You remain responsible for any uncollected amounts. If a payment is not successfully settled, due to expiration, insufficient funds, or otherwise, and you do not cancel your account, we may suspend your access to the service until we have successfully charged a valid Payment Method. For some Payment Methods, the issuer may charge you certain fees, such as foreign transaction fees or other fees relating to the processing of your Payment Method. Local tax charges may vary depending on the Payment Method used. Check with your Payment Method service provider for details." +
                            "<br />" +
                            "3.3. Updating your Payment Methods. You can update your Payment Methods by going to the “Account” page. We may also update your Payment Methods using information provided by the payment service providers. Following any update, you authorize us to continue to charge the applicable Payment Method(s)." +
                            "< br />" +
                            "3.4. Cancellation. You can cancel your DOST membership at any time, and you will continue to have access to the DOST service through the end of your billing period. To the extent permitted by the applicable law, payments are non-refundable and we do not provide refunds or credits for any partial membership periods or unwatched DOST content. To cancel, go to the “Account” page and follow the instructions for cancellation. If you cancel your membership, your account will automatically close at the end of your current billing period. To see when your account will close, click “Billing details” on the “Account” page. If you signed up for DOST using your account with a third party as a Payment Method and wish to cancel your DOST membership, you may need to do so through such third party, for example by visiting your account with the applicable third party and turning off auto-renew, or unsubscribing from the DOST service through that third party." +
                            "<br />" +
                            "3.5. Changes to the Price and Subscription Plans. We may change our subscription plans and the price of our service from time to time; however, any price changes or changes to your subscription plans will apply no earlier than 30 days following notice to you." +
                            "<br />" +
                            "<b>4.DOST Service </b>" +
                            "<br />" +
                            "4.1. You must be at least 18 years of age to become a member of the DOST service. Minors may only use the service under the supervision of an adult." +
                            "<br />" +
                            "4.2. The DOST service and any content viewed through the service are for your personal and non-commercial use only and may not be shared with individuals beyond your household. During your DOST membership we grant you a limited, non-exclusive, non-transferable right to access the DOST service and view DOST content. Except for the foregoing, no right, title or interest shall be transferred to you. You agree not to use the service for public performances." +
                            "<br />" +
                            "4.3. You may view the DOST content primarily within the country in which you have established your account and only in geographic locations where we offer our service and have licensed such content. The content that may be available to watch will vary by geographic location and will change from time to time. The number of devices on which you may simultaneously watch depends on your chosen subscription plan and is specified on the “Account” page." +
                            "<br />" +
                            "4.4. The DOST service, including the content library, is regularly updated. In addition, we continually test various aspects of our service, including our website, user interfaces, promotional features and availability of DOST content. You can turn off tests participation at any time by visiting the “Account” page and changing the “Test participation” settings." +
                            "<br />" +
                            "4.5. Some DOST content is available for temporary download and offline viewing on certain supported devices (“Offline Titles”). Limitations apply, including restrictions on the number of Offline Titles per account, the maximum number of devices that can contain Offline Titles, the time period within which you will need to begin viewing Offline Titles and how long the Offline Titles will remain accessible. Some Offline Titles may not be playable in certain countries and if you go online in a country where you would not be able to stream that Offline Title, the Offline Title will not be playable while you are in that country." +
                            "<br />" +
                            "4.6. You agree to use the DOST service, including all features and functionalities associated therewith, in accordance with all applicable laws, rules and regulations, or other restrictions on use of the service or content therein. You agree not to archive, reproduce, distribute, modify, display, perform, publish, license, create derivative works from, offer for sale, or use (except as explicitly authorized in these Terms of Use) content and information contained on or obtained from or through the DOST service. You also agree not to: circumvent, remove, alter, deactivate, degrade or thwart any of the content protections in the DOST service; use any robot, spider, scraper or other automated means to access the DOST service; decompile, reverse engineer or disassemble any software or other products or processes accessible through the DOST service; insert any code or product or manipulate the content of the DOST service in any way; or use any data mining, data gathering or extraction method. In addition, you agree not to upload, post, e-mail or otherwise send or transmit any material designed to interrupt, destroy or limit the functionality of any computer software or hardware or telecommunications equipment associated with the DOST service, including any software viruses or any other computer code, files or programs. We may terminate or restrict your use of our service if you violate these Terms of Use or are engaged in illegal or fraudulent use of the service." +
                            "<br />" +
                            "4.7. The quality of the display of the DOST content may vary from device to device, and may be affected by a variety of factors, such as your location, the bandwidth available through and/or speed of your Internet connection. HD, Ultra HD and HDR availability is subject to your Internet service and device capabilities. Not all content is available in all formats, such as HD, Ultra HD or HDR and not all subscription plans allow you to receive content in all formats. Default playback settings on cellular networks exclude HD, Ultra HD and HDR content. The minimum connection speed for SD quality is 1.0 Mbps; however, we recommend a faster connection for improved video quality. A download speed of at least 3.0 Mbps per stream is recommended to receive HD content (defined as a resolution of 720p or higher). A download speed of at least 15.0 Mbps per stream is recommended to receive Ultra HD (defined as a resolution of 4K or higher). You are responsible for all Internet access charges. Please check with your Internet provider for information on possible Internet data usage charges. The time it takes to begin watching DOST content will vary based on a number of factors, including your location, available bandwidth at the time, the content you have selected and the configuration of your DOST ready device." +
                            "<br />" +
                            "4.8. DOST software is developed by, or for, DOST and may solely be used for authorized streaming and viewing of DOST content through DOST ready devices. This software may vary by device and medium, and functionalities and features may also differ between devices. You acknowledge that the use of the service may require third party software that is subject to third party licenses. You agree that you may automatically receive updated versions of the DOST software and related third-party software." +
                            "<br />" +
                            "5.Passwords and Account Access. The member who created the DOST account and whose Payment Method is charged (the “Account Owner”) is responsible for any activity that occurs through the DOST account. To maintain control over the account and to prevent anyone from accessing the account (which would include information on viewing history for the account), the Account Owner should maintain control over the DOST ready devices that are used to access the service and not reveal the password or details of the Payment Method associated with the account to anyone. You are responsible for updating and maintaining the accuracy of the information you provide to us relating to your account. We can terminate your account or place your account on hold in order to protect you, DOST or our partners from identity theft or other fraudulent activity." +
                            "<br />" +
                            "6.Warranties and Limitations on Liability. The DOST service is provided “as is” and without warranty or condition. In particular, our service may not be uninterrupted or error-free. You waive all special, indirect and consequential damages against us. These terms will not limit any non-waivable warranties or mandatory consumer protection rights that apply to you." +
                            "<br />" +
                            "7.Class Action Waiver. WHERE PERMITTED UNDER THE APPLICABLE LAW, YOU AND DOST AGREE THAT EACH MAY BRING CLAIMS AGAINST THE OTHER ONLY IN YOUR OR ITS INDIVIDUAL CAPACITY, AND NOT AS A PLAINTIFF OR CLASS MEMBER IN ANY PURPORTED CLASS OR REPRESENTATIVE PROCEEDING. Further, where permitted under the applicable law, unless both you and DOST agree otherwise, the court may not consolidate more than one person's claims with your claims, and may not otherwise preside over any form of a representative or class proceeding." +
                            "<br />" +
                            "<b> 8.Miscellaneous </b>" +
                            "<br />" +
                            "8.1. Governing Law. These Terms of Use shall be governed by and construed in accordance with the laws of India." +
                            "<br />" +
                            "8.2. Unsolicited Materials. DOST does not accept unsolicited materials or ideas for DOST content and is not responsible for the similarity of any of its content or programming in any media to materials or ideas transmitted to DOST." +
                            "<br />" +
                            "8.3. Customer Support. To find more information about our service and its features or if you need assistance with your account, please visit the DOST Help Center on our website. In certain instances, Customer Service may best be able to assist you by using a remote access support tool through which we have full access to your computer. If you do not want us to have this access, you should not consent to support through the remote access tool, and we will assist you through other means. In the event of any conflict between these Terms of Use and information provided by Customer Support or other portions of our website, these Terms of Use will control." +
                           "<br />" +
                           "8.4. Survival. If any provision or provisions of these Terms of Use shall be held to be invalid, illegal, or unenforceable, the validity, legality and enforceability of the remaining provisions shall remain in full force and effect." +
                            "<br />" +
                            "8.5. Changes to Terms of Use and Assignment. DOST may, from time to time, change these Terms of Use. We will notify you at least 30 days before such changes apply to you. We may assign or transfer our agreement with you including our associated rights and obligations at any time and you agree to cooperate with us in connection with such an assignment or transfer." +
                            "<br />" +
                            "8.6. Electronic Communications. We will send you information relating to your account (e.g. payment authorizations, invoices, changes in password or Payment Method, confirmation messages, notices) in electronic form only, for example via emails to your email address provided during registration"

                });

        }

        public HttpResponseMessage JoinReferalProgram(ReferalProgram model)
        {
            try
            {
                DataSet ds = model.UpdateSponsor();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Successfully joined referal program",

                    });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error Occurred",

                   });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error Occurred",

                   });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = ex.Message,

                   });
            }
        }

        public HttpResponseMessage GetKnowMore()
        {

            return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "Success",
                    Data = " <h3> WHY DOST ?</h3> <br /> <p> By choosing to work with Dost, you can be reassured that your revenue and network just grows , because we are committed for things that are trust , innovation , quality & customer satisfaction. Our products are innovative and right in demand and our research and development team never let us turn out of focus.</p> <b>START SELLING IN A FEW DAYS</b><p>Each Dost Product includes a set of appropriate tools to support sales and promotion. This means that as part of our distribution network, we will help you find clients. All you have to do is to distribute the products you have purchased from us.</p><br /><b>WHAT YOU GET?</b> <b> GREAT NUMBER OF BENEFITS</b><ul><li>Reliable business partner</li><li>Complete Training & Support</li><li>Top quality service</li><li>Warranty</li><li>Marketing support</li><li>Start-up package</li><li>Margins/Commission at best</li></ul><br /><b> BEGINNING OF Dost</b><p> From the very first day of initiation our presence and approach towards market has been rewarding and this we can only be thankful about but our commitment and work towards quality and innovation will ensure this continues.</p><b>DD PRODUCTS WELL RECEIVED IN THE DEMANDING INDIAN MARKET</b><p>Our products/services are very handy and in demand because it relates to the ne’s life style and professional needs therefore being in limelight is our tradition.</p><b>DYNAMIC DEVELOPMENT</b><p>DD not only stands for Dost but it also is synonymous for Dynamic Development and our public participation strategy is solid and allows everyone to become the part of our growing network financially as well as socially.</p>"

                });
        }

        [HttpPost]
        public HttpResponseMessage AddDistributor(Distributor model)
        {
            try
            {
                DataSet ds = model.SaveDistrubutorRecord();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        try
                        {
                            string ApplicationNo = "DDIN000" + ds.Tables[0].Rows[0]["ApplicationRef"].ToString();
                            string msg = "Dear " + model.YourName + " , We have received your DOST Inc Distributor Application , Your application reference number is " + ApplicationNo + ", we will update you once it is approved. Team - DOST INC";
                            BLSMS.sendSMSUpdated(msg, model.Mobile);
                        }
                        catch (Exception ex)
                        {

                        }
                        //Send Mail
                        //try
                        //{
                        //    string msg = "";
                        //    msg = "We have received your application to be a part of our supply chain network. Your application is being reviewed soon and you will receive communication from one of your DOST.<br><br>For any issue please reach us through email or through DOST helpline.";
                        //    string Subject = "Distributor Application";
                        //    BLMail.SendDistributorMail(model.YourName, msg, Subject, model.Email);
                        //}
                        //catch (Exception ex)
                        //{

                        //}
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Ditributor Saved Successfully",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }

        [HttpGet]
        public HttpResponseMessage GetDigiDashBoardData()
        {
            DigiDashBoardData obj = new DigiDashBoardData();
            try
            {
                DataSet ds = obj.GetDashBoardDataForMobile();
                List<SubCategory> lst = new List<SubCategory>();
                List<BannerDetails> lst1 = new List<BannerDetails>();
                List<BannerDetails> lst2 = new List<BannerDetails>();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[1].Rows)
                        {
                            SubCategory obj1 = new SubCategory();
                            obj1.SubCategoryId = dr["Pk_SubCategoryId"].ToString();
                            obj1.SubCategoryName = dr["SubcatogryName"].ToString();
                            obj1.SubCategoryIcon = dr["SubCatogryIcon"].ToString();
                            if (ds.Tables[2].Rows.Count > 0)
                            {
                                List<ProductItem> ProductList = new List<ProductItem>();
                                foreach (DataRow dr2 in ds.Tables[2].Rows)
                                {
                                    ProductItem pr = new ProductItem();
                                    if (obj1.SubCategoryId == dr2["FK_SubCategoryId"].ToString())
                                    {
                                        pr.Name = dr2["ProductName"].ToString();
                                        pr.OfferPrice = dr2["OfferPrice"].ToString();
                                        pr.DeliveryCharges = dr2["DeliveryCharges"].ToString();
                                        pr.IGST = dr2["IGST"].ToString();
                                        pr.CGST = dr2["CGST"].ToString();
                                        pr.SGST = dr2["SGST"].ToString();
                                        pr.Image = dr2["Images"].ToString();
                                        pr.ShortDescription = dr2["ShortDescription"].ToString();
                                        pr.Price = dr2["Price"].ToString();
                                        pr.PK_SubCategoryId = dr2["FK_SubCategoryId"].ToString();
                                        pr.Pk_EventId = dr2["PK_EventId"].ToString();
                                        ProductList.Add(pr);
                                    }
                                }
                                obj1.Products = ProductList;
                            }
                            lst.Add(obj1);
                        }
                    }
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        List<BannerDetails> objterms1 = new List<BannerDetails>();
                        foreach (DataRow row1 in (ds.Tables[0].Rows))
                        {
                            lst1.Add(new BannerDetails

                            {
                                BannerImage = EncKey.ProfilePicURL + row1["BannerImage"].ToString().Replace("../", ""),

                            });
                        }
                        List<BannerDetails> objterms2 = new List<BannerDetails>();
                        foreach (DataRow row1 in (ds.Tables[3].Rows))
                        {
                            lst2.Add(new BannerDetails

                            {
                                BannerImage = EncKey.ProfilePicURL + row1["BannerImage"].ToString().Replace("../", ""),

                            });
                        }
                        //lst1.Add(new BannerDetails
                        //{

                        //    lstbanner = objterms1

                        //});
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstBanner = lst1,
                          lstBanner1 = lst2,
                          lstSubCategory = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage UserDashboardProfile(DashBoardData DashUserProfile)
        {
            //DashBoardData DashUserProfile = new DashBoardData();
            try
            {
                //DashUserProfile.Pk_UserId = Pk_UserId;
                DataSet dsResult = DashUserProfile.GetUserDashboardProfile();
                if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        Data = new
                        {
                            LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString(),
                            Name = dsResult.Tables[0].Rows[0]["Name"].ToString(),
                            DOJ = dsResult.Tables[0].Rows[0]["DOJ"].ToString(),
                            Mobile = dsResult.Tables[0].Rows[0]["Mobile"].ToString(),
                            UnclearedBalance = dsResult.Tables[0].Rows[0]["UnclearedBalance"].ToString(),
                            TeamSize = dsResult.Tables[0].Rows[0]["TeamSize"].ToString(),
                            Commission = dsResult.Tables[0].Rows[0]["Commission"].ToString(),
                            KYCStatus = dsResult.Tables[0].Rows[0]["KYCStatus"].ToString(),
                            LastLogin = dsResult.Tables[0].Rows[0]["LastLogin"].ToString(),
                            DOA = dsResult.Tables[0].Rows[0]["DOA"].ToString(),
                            ProfilePic = EncKey.ProfilePicURL + dsResult.Tables[0].Rows[0]["ProfilePic"].ToString(),
                            TodaysPrimeId = dsResult.Tables[0].Rows[0]["TodaysPrimeId"].ToString(),
                            TodaysId = dsResult.Tables[0].Rows[0]["TodaysId"].ToString(),
                            ReferralCode = dsResult.Tables[0].Rows[0]["ReferalCode"].ToString()

                        }
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetPassbook(GetPassbook obj)
        {
            //DashBoardData DashUserProfile = new DashBoardData();
            try
            {
                //DashUserProfile.Pk_UserId = Pk_UserId;
                DataSet dsResult = obj.GetPassBookBalance();
                if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        Data = new
                        {
                            WalletBalance = dsResult.Tables[0].Rows[0]["Balance"].ToString(),
                            Coin = dsResult.Tables[1].Rows[0]["Balance"].ToString(),
                            FranchiseeBalance = dsResult.Tables[2].Rows[0]["Balance"].ToString()


                        }
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage WithDrawal(WithdrawalMobile model)
        {
            try
            {
                DataSet ds = model.SaveWithDrawalRequest();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Withdrawal Request Save Successfully",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }
        [HttpPost]
        public HttpResponseMessage GetDirectCommision(DirectCommission obj)
        {
            try
            {
                DataSet ds = obj.GetDirectCommision();
                List<DirectCommisionList> lst = new List<DirectCommisionList>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.InternalServerError,
                          Message = "No Record Found"
                      });
                    }
                    else
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DirectCommisionList obj1 = new DirectCommisionList();
                            obj1.FromLoginId = dr["FromLoginId"].ToString();
                            obj1.FromUserName = dr["FromUserName"].ToString();
                            obj1.Amount = dr["Amount"].ToString();
                            obj1.IncomeType = dr["IncomeType"].ToString();
                            obj1.Date = dr["CurrentDate"].ToString();
                            lst.Add(obj1);
                        }
                        return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstDirectCommission = lst
                      });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found"


                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetPaidCommission(PaidCommision obj)
        {
            try
            {
                DataSet ds = obj.GetPaidCommission();
                List<PaidCommisionList> lst = new List<PaidCommisionList>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        PaidCommisionList Obj1 = new PaidCommisionList();
                        Obj1.Amount = r["Amount"].ToString();
                        Obj1.Description = r["Description"].ToString();
                        Obj1.PaymentDate = r["Paymentdate"].ToString();
                        Obj1.TransactionDate = r["TransactionDate"].ToString();
                        Obj1.TransactionNo = r["TransactionNo"].ToString();
                        Obj1.LoginId = r["LoginId"].ToString();
                        Obj1.Name = r["Name"].ToString();
                        lst.Add(Obj1);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstPaidCommission = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetStockStatement(StaockStatement obj)
        {
            try
            {
                DataSet ds = obj.GetStockStatement();
                List<StaockStatementList> lst = new List<StaockStatementList>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        StaockStatementList Objload = new StaockStatementList();
                        Objload.Narration = dr["Narration"].ToString();
                        Objload.DrAmount = dr["DrAMount"].ToString();
                        Objload.CrAmount = dr["CrAmount"].ToString();
                        Objload.AddedOn = dr["CurrentDate"].ToString();
                        Objload.EwalletBalance = dr["Balance"].ToString();
                        lst.Add(Objload);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstStockStatement = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage AddFranciseeFund(FranchiseFund model)
        {
            try
            {
                DataSet ds = model.SaveFranchiseeFund();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Franchisee fund requested Successfully",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }
        [HttpPost]
        public HttpResponseMessage FranchiseWalletStatement(FranchiseStatement obj)
        {
            try
            {
                DataSet ds = obj.GetFranchiseeWalletLedger();
                List<FranchiseStatementList> lst = new List<FranchiseStatementList>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        FranchiseStatementList Objload = new FranchiseStatementList();
                        Objload.Narration = dr["Narration"].ToString();
                        Objload.DrAmount = dr["DrAMount"].ToString();
                        Objload.CrAmount = dr["CrAmount"].ToString();
                        Objload.AddedOn = dr["CurrentDate"].ToString();
                        Objload.EwalletBalance = dr["Balance"].ToString();
                        lst.Add(Objload);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstFranchiseWalletStatement = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage DistributorCommissionHistory(DistributorCommissionHistory obj)
        {
            try
            {
                DataSet ds = obj.getDistributorcommisionHistory();
                List<DistributorCommissionHistoryList> lst = new List<DistributorCommissionHistoryList>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DistributorCommissionHistoryList Objload = new DistributorCommissionHistoryList();
                        Objload.Amount = dr["Amount"].ToString();
                        Objload.date = dr["CurrentDate"].ToString();
                        Objload.Narration = dr["Narration"].ToString();
                        lst.Add(Objload);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstDistributorCommissionHistory = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage NFCPersonalInformation(NFCProfile objprofile)
        {
            try
            {
                DataSet ds = objprofile.GetNFCProfileData();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        Data = new
                        {
                            FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString(),
                            LastName = ds.Tables[0].Rows[0]["LastName"].ToString(),
                            ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString(),
                            Summary = ds.Tables[0].Rows[0]["Mobile"].ToString(),
                            Leg = ds.Tables[0].Rows[0]["Leg"].ToString()
                        }
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetNFCContactDetails(NFCProfile obj)
        {
            try
            {
                DataSet ds = obj.GetFfcContactDetails();
                List<ContactList> lst = new List<ContactList>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ContactList Objload = new ContactList();
                        Objload.Pk_NfcProfileId = dr["Pk_NfcProfileId"].ToString();
                        Objload.Contact = dr["Content"].ToString();
                        Objload.Type = dr["Type"].ToString();
                        Objload.IsWhatsup = Convert.ToBoolean(dr["IsWhatsapp"].ToString());
                        Objload.IsIncluded = Convert.ToBoolean(dr["IsIncluded"]);
                        //Objload.WhatsupNo = dr["WhatsappNo"].ToString();
                        Objload.Fk_UserId = dr["Fk_UserId"].ToString();
                        lst.Add(Objload);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstContact = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetNFCWebLinkDetails(NFCProfile obj)
        {
            try
            {
                DataSet ds = obj.GetFfcContactDetails();
                List<WebLink> lst = new List<WebLink>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[2].Rows)
                    {
                        WebLink Objload = new WebLink();
                        Objload.Pk_NfcProfileId = dr["Pk_NfcProfileId"].ToString();
                        Objload.Link = dr["Content"].ToString();
                        Objload.Type = dr["Type"].ToString();
                        Objload.IsRedirect = Convert.ToBoolean(dr["IsRedirect"].ToString());
                        Objload.IsIncluded = Convert.ToBoolean(dr["IsIncluded"]);
                        Objload.Redirection = dr["Redirection"].ToString();
                        Objload.Fk_UserId = dr["Fk_UserId"].ToString();
                        lst.Add(Objload);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstWeb = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetNFCEmailDetails(NFCProfile obj)
        {
            try
            {
                DataSet ds = obj.GetFfcContactDetails();
                List<EmailList> lst = new List<EmailList>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        EmailList Objload = new EmailList();
                        Objload.Pk_NfcProfileId = dr["Pk_NfcProfileId"].ToString();
                        Objload.Email = dr["Content"].ToString();
                        Objload.IsIncluded = Convert.ToBoolean(dr["IsIncluded"]);
                        Objload.Type = dr["Type"].ToString();
                        Objload.Fk_UserId = dr["Fk_UserId"].ToString();
                        lst.Add(Objload);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstEmail = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetNFCSocialMediaDetails(NFCProfile obj)
        {
            try
            {
                DataSet ds = obj.GetFfcContactDetails();
                List<SocialMediaLink> lst = new List<SocialMediaLink>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[3].Rows)
                    {
                        SocialMediaLink Objload = new SocialMediaLink();
                        Objload.Pk_NfcProfileId = dr["Pk_NfcProfileId"].ToString();
                        Objload.Link = dr["Content"].ToString();
                        Objload.Type = dr["Type"].ToString();
                        Objload.IsIncluded = Convert.ToBoolean(dr["IsIncluded"]);
                        Objload.IsRedirect = Convert.ToBoolean(dr["IsRedirect"].ToString());
                        Objload.Redirection = dr["Redirection"].ToString();
                        Objload.Fk_UserId = dr["Fk_UserId"].ToString();
                        lst.Add(Objload);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstEmail = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage AddNFCContactDetails(NFCContact model)
        {
            try
            {
                DataSet ds = model.SaveUpdateContactNoNfc();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Contact No saved successfully",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }
        [HttpPost]
        public HttpResponseMessage AddNFCEmailDetails(NFCEmail model)
        {
            try
            {
                DataSet ds = model.SaveUpdateContactNoNfc();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Email saved successfully",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }
        [HttpPost]
        public HttpResponseMessage AddNFCSocialMediaDetails(NFCSocialMedia model)
        {
            try
            {
                DataSet ds = model.SaveUpdateContactNoNfc();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Social media saved successfully",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }
        [HttpPost]
        public HttpResponseMessage UpdateNFCProfileDetails(UPdateNFCProfile model)
        {
            try
            {


                DataSet ds = model.UpdateProfileInfo();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Profile update successfully",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }
        [HttpPost]
        public HttpResponseMessage DeletNFCDetails(DeleteNFC model)
        {
            try
            {
                DataSet ds = model.DeleteNFCProfileData();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "NFC Details Deleted successfully",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error Occured",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }
        [HttpPost]
        public HttpResponseMessage AddNFCWebLinkDetails(NFCWebLink model)
        {
            try
            {
                DataSet ds = model.SaveUpdateContactNoNfc();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "WebLink saved successfully",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }
        [HttpGet]
        public HttpResponseMessage GetNFC()
        {
            try
            {
                NFC2 obj = new NFC2();
                DataSet ds = obj.GetNFC();
                List<NFCList> lst = new List<NFCList>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        NFCList model = new NFCList();
                        model.PK_EventId = r["PK_EventId"].ToString();
                        model.EventName = r["EventName"].ToString();
                        model.BinaryBV = r["BinaryPercentage"].ToString();
                        model.ReferalBV = r["ReferralPercentage"].ToString();
                        model.EventImage = r["EventImage"].ToString();
                        model.ProductPrice = r["PlanName"].ToString();
                        model.Brand = r["Brand"].ToString();
                        // model.Price = r["ProductPrice"].ToString();
                        //  model.OfferPrice = r["OfferPrice"].ToString();
                        model.EventDescription = r["EventDescription"].ToString();
                        model.ProductCode = r["ProductCode"].ToString();
                        model.instock = r["NoOfSeats"].ToString();
                        model.Discount = r["Discount"].ToString();
                        lst.Add(model);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstnfc = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetMyNFC(MyNFC obj)
        {
            try
            {
                DataSet ds = obj.GetNFCReport();
                List<MyNFCList> lst = new List<MyNFCList>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        MyNFCList model = new MyNFCList();
                        model.PK_EventId = r["PK_EventId"].ToString();
                        model.EventName = r["EventName"].ToString();
                        model.Offername = r["offername"].ToString();
                        model.EventPrice = r["EventPrice"].ToString();
                        model.OfferValidity = r["OfferValidity"].ToString();
                        model.CouponCode = r["CouponCode"].ToString();
                        model.NoOfSeats = r["NoOfSeats"].ToString();
                        model.EventImage = r["EventImage"].ToString();
                        model.Description = r["EventDescription"].ToString();
                        lst.Add(model);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstmynfc = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }

        [HttpPost]
        public HttpResponseMessage GetNFCDetailsForPurchase(NFCDetailsPurchase1 obj1)
        {
            try
            {
                NFCDetailsPurchase obj = new NFCDetailsPurchase();
                obj.Fk_EventId = obj1.Fk_EventId;
                obj.EventId = obj.EventId;
                obj.PlanId = 0;
                DataSet ds = obj.SubscriptionList();
                List<NFCProduct> lstproduct = new List<NFCProduct>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        NFCProduct model = new NFCProduct();
                        model.Pk_PlanId = r["Pk_PlanId"].ToString();
                        model.PlanName = r["PlanName"].ToString();
                        model.ProductPrice = r["PlanPrice"].ToString();
                        model.Validity = r["Validity"].ToString();
                        lstproduct.Add(model);
                        model.PlanId = r["Pk_PlanId"].ToString();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstproduct = lstproduct
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }

            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAddress(NFCDetailsPurchase objprofile)
        {
            try
            {
                DataSet dsAddress = objprofile.GetUserAddressBook();
                List<AddressBook> lstAddress = new List<AddressBook>();
                if (dsAddress != null && dsAddress.Tables.Count > 0 && dsAddress.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsAddress.Tables[0].Rows)
                    {
                        lstAddress.Add(new AddressBook()
                        {
                            FirstName = row["FirstName"].ToString(),
                            LastName = row["LastName"].ToString(),
                            Email = row["Email"].ToString(),
                            MobileNo = row["MobileNo"].ToString(),
                            Address = row["Address"].ToString(),
                            City = row["City"].ToString(),
                            State = row["State"].ToString(),
                            Pincode = row["Pincode"].ToString(),
                            PK_AddressId = row["PK_AddressId"].ToString(),
                            CompleteAddress = row["CompleteAddress"].ToString(),
                            IsRecentlyUsed = row["IsRecentlyUsed"].ToString(),
                        });
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                           new
                           {
                               StatusCode = HttpStatusCode.OK,
                               Message = "Record Found",
                               lstproduct = lstAddress
                           });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + dsAddress.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetCheckCategory(NFCDetailsPurchase1 obj1)
        {
            try
            {
                NFCDetailsPurchase objprofile = new NFCDetailsPurchase();
                objprofile.PK_EventId = Convert.ToInt32(obj1.EventId);
                DataSet dsCheck_Category = objprofile.CheckCategory();
                if (dsCheck_Category != null && dsCheck_Category.Tables.Count > 0 && dsCheck_Category.Tables[0].Rows.Count > 0)
                {
                    var categoryId = dsCheck_Category.Tables[0].Rows[0]["FK_CategoryId"].ToString();
                    objprofile.Category = dsCheck_Category.Tables[0].Rows[0]["FK_CategoryId"].ToString();
                    if (categoryId == "1")
                    {
                        objprofile.EventType = "Event";
                    }
                    else if (categoryId == "2")
                    {
                        objprofile.EventType = "Subscription";
                    }
                    else if (categoryId == "3")
                    {

                        objprofile.EventType = "NFC";
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                           new
                           {
                               StatusCode = HttpStatusCode.OK,
                               Message = "Record Found",
                               EventType = objprofile.EventType
                           });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + dsCheck_Category.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetEventDetails(EventDetailAPI obj1)
        {
            try
            {
                NFCDetailsPurchase objprofile = new NFCDetailsPurchase();
                DataSet ds = objprofile.EventList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        Data = new
                        {
                            PK_EventId = Convert.ToInt32(ds.Tables[0].Rows[0]["PK_EventId"]),
                            EventName = ds.Tables[0].Rows[0]["EventName"].ToString(),
                            EventLocation = ds.Tables[0].Rows[0]["EventLocation"].ToString(),
                            EventDescription = ds.Tables[0].Rows[0]["EventDescription"].ToString(),
                            StartDate = ds.Tables[0].Rows[0]["StartDate"].ToString(),
                            EndDate = ds.Tables[0].Rows[0]["EndDate"].ToString(),
                            EventImage = ds.Tables[0].Rows[0]["EventImage"].ToString(),
                            NoOfSeats = ds.Tables[0].Rows[0]["NoOfSeats"].ToString(),
                            ProductPrice = ds.Tables[0].Rows[0]["Price"].ToString(),
                            OfferPrice = ds.Tables[0].Rows[0]["OfferPrice"].ToString(),
                            Discount = ds.Tables[0].Rows[0]["Discount"].ToString(),
                            DeliverCharge = ds.Tables[0].Rows[0]["DeliveryCharges"].ToString(),
                            ReferralPercent = ds.Tables[0].Rows[0]["ReferralPercentage"].ToString(),
                            GST = ds.Tables[0].Rows[0]["IGST"].ToString()
                        }
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage SaveNFCDetails(SaveNFC model)
        {
            try
            {

                DataSet ds = model.SaveEventDetails();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "NFC details  saved successfully",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }
        [HttpPost]
        public HttpResponseMessage GetCouponForTransfer(TransferCoupon obj)
        {
            try
            {
                DataSet ds = obj.GetcouponsAssociate();
                List<TransferCouponList> lst = new List<TransferCouponList>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        TransferCouponList model = new TransferCouponList();
                        model.CouponCode = dr["CouponCode"].ToString();
                        model.CategoryName = dr["CategoryName"].ToString();
                        model.CreatedDate = dr["CreatedDate"].ToString();
                        model.CuopnStatus = dr["CuopnStatus"].ToString();
                        model.EventName = dr["EventName"].ToString();
                        model.CouponPrice = dr["CouponPrice"].ToString();
                        lst.Add(model);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lsttransfercoupon = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage TransferCoupon(SaveTransferCoupon model)
        {
            try
            {

                DataSet ds = model.TransferCoupon();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Coupon Transfered !!",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }
        [HttpPost]
        public HttpResponseMessage GetStateCity(state model)
        {

            try
            {
                DataSet dsStateCity = model.GetStateCity();
                if (dsStateCity != null && dsStateCity.Tables.Count > 0 && dsStateCity.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        Data = new
                        {
                            State = dsStateCity.Tables[0].Rows[0]["State"].ToString(),
                            City = dsStateCity.Tables[0].Rows[0]["City"].ToString(),
                        }

                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {

                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "No Record Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage WalletBalance(Users model)
        {
            try
            {
                DataSet ds = model.GetWalletMoney();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        WalletBalance = ds.Tables[0].Rows[0][0].ToString()
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage getNFCProfileList(NFCProfileById obj)
        {
            NFCProfileModelMobile objProfile = new NFCProfileModelMobile();
            try
            {
                DataSet ds1 = obj.getNFCProfileData();
                List<NFCProfileModelMobile> lst = new List<NFCProfileModelMobile>();
                List<NFcType> NFClst = new List<NFcType>();
                List<NFcType> SocialMedialst = new List<NFcType>();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {

                        NFCProfileModelMobile obj1 = new NFCProfileModelMobile();
                        obj1.Name = ds1.Tables[0].Rows[0]["Name"].ToString();
                        obj1.Email = ds1.Tables[0].Rows[0]["PrimaryEmail"].ToString();
                        obj1.DOB = ds1.Tables[0].Rows[0]["DOB"].ToString();
                        obj1.Mobile = ds1.Tables[0].Rows[0]["Mobile"].ToString();
                        obj1.ProfilePic = ds1.Tables[0].Rows[0]["ProfilePic"].ToString();
                        obj1.Gender = ds1.Tables[0].Rows[0]["Sex"].ToString();
                        obj1.Pk_UserId = ds1.Tables[0].Rows[0]["PK_UserId"].ToString();
                        obj1.Summary = ds1.Tables[0].Rows[0]["Summary"].ToString();
                        obj1.UserCode = ds1.Tables[0].Rows[0]["UserCode"].ToString();
                        obj1.Leg = ds1.Tables[0].Rows[0]["NFCProfileLeg"].ToString();
                        obj1.LoginId = ds1.Tables[0].Rows[0]["LoginId"].ToString();
                        obj1.IsProfileTurnedOff = Convert.ToBoolean(ds1.Tables[0].Rows[0]["IsProfileTurnedOff"]);
                        lst.Add(obj1);

                    }
                    if (ds1.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds1.Tables[1].Rows)
                        {
                            NFcType nfcobj = new NFcType();
                            nfcobj.TypeName = dr["Type"].ToString();
                            if (nfcobj.TypeName != "SocialMedia")
                            {
                                if (ds1.Tables[2].Rows.Count > 0)
                                {
                                    List<TypeList> NFCList = new List<TypeList>();
                                    foreach (DataRow dr2 in ds1.Tables[2].Rows)
                                    {
                                        TypeList pr = new TypeList();
                                        if (nfcobj.TypeName == dr2["Type"].ToString())
                                        {
                                            pr.Content = dr2["Content"].ToString();
                                            pr.Type = dr2["Type"].ToString();
                                            pr.IsWhatsApp = dr2["IsWhatsapp"].ToString();
                                            if (dr2["IsRedirect"].ToString() == "True")
                                            {
                                                pr.RedirectionLink = dr2["Content"].ToString();
                                            }
                                            NFCList.Add(pr);
                                        }
                                    }
                                    nfcobj.TypeItem = NFCList;
                                }
                                NFClst.Add(nfcobj);
                            }
                            if (nfcobj.TypeName == "SocialMedia")
                            {
                                if (ds1.Tables[2].Rows.Count > 0)
                                {
                                    List<TypeList> SocialMedia = new List<TypeList>();
                                    foreach (DataRow dr2 in ds1.Tables[2].Rows)
                                    {
                                        TypeList pr = new TypeList();
                                        if (nfcobj.TypeName == dr2["Type"].ToString())
                                        {
                                            pr.Content = dr2["Content"].ToString();
                                            pr.Type = dr2["Type"].ToString();
                                            pr.IsWhatsApp = dr2["IsWhatsapp"].ToString();
                                            if (dr2["IsRedirect"].ToString() == "True")
                                            {
                                                pr.RedirectionLink = dr2["Content"].ToString();
                                            }
                                            SocialMedia.Add(pr);
                                        }
                                    }
                                    nfcobj.TypeItem = SocialMedia;
                                }
                                SocialMedialst.Add(nfcobj);
                            }
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          Profile = lst,
                          NFCList = NFClst,
                          SocialMediaList = SocialMedialst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }

        public HttpResponseMessage BusinessDashboard(BusinessDashboardRequest obj)
        {
            BusinessDashboard model = new BusinessDashboard();
            try
            {
                string LoginTime = "";
                DataSet ds = obj.GetBusinessDashboard();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        if (ds.Tables[2].Rows.Count > 0)
                        {
                            LoginTime = ds.Tables[2].Rows[0]["LoginDateTime"].ToString();
                        }
                        return Request.CreateResponse(HttpStatusCode.OK, new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Record Found",
                            UserId = ds.Tables[1].Rows[0]["UserCode"].ToString(),
                            MobileNo = ds.Tables[1].Rows[0]["Mobile"].ToString(),
                            Name = ds.Tables[1].Rows[0]["Name"].ToString(),
                            SponsorName = ds.Tables[1].Rows[0]["SponsorName"].ToString(),
                            DOJ = ds.Tables[1].Rows[0]["JoiningDate"].ToString(),
                            DOA = ds.Tables[1].Rows[0]["PermanentDate"].ToString(),
                            LastLogin = LoginTime,
                            TotalPayout = ds.Tables[0].Rows[0]["TotalPayout"].ToString(),
                            TotalIncome = ds.Tables[0].Rows[0]["TotalIncome"].ToString(),
                            TotalTeam = ds.Tables[0].Rows[0]["TotalDownline"].ToString(),
                            TotalDirect = ds.Tables[0].Rows[0]["TotalDirect"].ToString(),
                            TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString(),
                            TotalReseller = ds.Tables[0].Rows[0]["TotalReseller"].ToString(),
                            TotalDistributor = ds.Tables[0].Rows[0]["TotalDistributor"].ToString(),
                            TotalAirDrop = ds.Tables[0].Rows[0]["TotalAirDrop"].ToString(),
                            TodaysEarning = ds.Tables[0].Rows[0]["TodaysEarning"].ToString(),
                            TodaysTeam = ds.Tables[0].Rows[0]["TodaysTeam"].ToString(),
                            PaidBusinessLeft = ds.Tables[3].Rows[0]["PaidBusinessLeft"].ToString(),
                            PaidBusinessRight = ds.Tables[3].Rows[0]["PaidBusinessRight"].ToString(),
                            TotalBusinessLeft = ds.Tables[3].Rows[0]["TotalBusinessLeft"].ToString(),
                            TotalBusinessRight = ds.Tables[3].Rows[0]["TotalBusinessRight"].ToString(),
                            CarryLeft = ds.Tables[3].Rows[0]["CarryLeft"].ToString(),
                            CarryRight = ds.Tables[3].Rows[0]["CarryRight"].ToString(),
                            CurrentPaidBusinessLeft = ds.Tables[4].Rows[0]["PaidBusinessLeft"].ToString(),
                            CurrentPaidBusinessRight = ds.Tables[4].Rows[0]["PaidBusinessRight"].ToString(),
                            CurrentTotalBusinessLeft = ds.Tables[4].Rows[0]["TotalBusinessLeft"].ToString(),
                            CurrentTotalBusinessRight = ds.Tables[4].Rows[0]["TotalBusinessRight"].ToString(),
                            CurrentCarryLeft = ds.Tables[4].Rows[0]["CarryLeft"].ToString(),
                            CurrentCarryRight = ds.Tables[4].Rows[0]["CarryRight"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new
                        {
                            StatusCode = HttpStatusCode.NotFound,
                            Message = "No Record Found"
                        });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.InternalServerError,
                          Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                      });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        #region NFCProfile
        [HttpPost]
        public HttpResponseMessage UpdateBusinessInformation(BusinessInformation model)
        {
            try
            {
                DataSet ds = model.InsertBusinessInfo();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Profile inserted successfully",

                          });
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                  new
                  {
                      StatusCode = HttpStatusCode.InternalServerError,
                      Message = ex.Message,
                  });
            }

        }
        [HttpPost]
        public HttpResponseMessage GetNFCBusinessInformation(NFCProfile objprofile)
        {
            try
            {
                List<BusinessProfile> lst = new List<BusinessProfile>();
                DataSet ds = objprofile.GetBusinessInfo();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        BusinessProfile obj = new BusinessProfile();
                        obj.PK_ProfileId = r["PK_ProfileId"].ToString();
                        obj.FK_UserId = r["PK_UserId"].ToString();
                        obj.ProfileName = r["ProfileName"].ToString();
                        obj.ProfileType = r["ProfileType"].ToString();
                        obj.Name = r["Name"].ToString();
                        obj.Designation = r["Designation"].ToString();
                        if (r["Status"].ToString() == "Active")
                        {
                            obj.IsActive = true;
                        }
                        else
                        {
                            obj.IsActive = false;
                        }
                        obj.ProfilePic = r["ProfilePic"].ToString();
                        obj.Checked = r["IsChecked"].ToString();
                        obj.BusinessName = r["BusinessName"].ToString();
                        obj.Description = r["Description"].ToString();
                        obj.IsProfileTurnedOff = Convert.ToBoolean(r["IsProfileTurnedOff"]);

                        lst.Add(obj);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        lst = lst
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "No Record Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetNFCBusinessInfoById(Profile1 objprofile)
        {
            try
            {
                DataSet ds = objprofile.GetBusinessProfileById();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        PK_ProfileId = ds.Tables[0].Rows[0]["PK_ProfileId"].ToString(),
                        FK_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString(),
                        ProfileName = ds.Tables[0].Rows[0]["ProfileName"].ToString(),
                        FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString(),
                        LastName = ds.Tables[0].Rows[0]["LastName"].ToString(),
                        Designation = ds.Tables[0].Rows[0]["Designation"].ToString(),
                        Status = ds.Tables[0].Rows[0]["Status"].ToString(),
                        ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString(),
                        BusinessName = ds.Tables[0].Rows[0]["BusinessName"].ToString(),
                        Description = ds.Tables[0].Rows[0]["Description"].ToString(),

                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage EditBusinessInformation(UpdateBusinessProfile objprofile)
        {
            try
            {
                DataSet ds = objprofile.UpdateBusinessInfo();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Profile Updated Successfully"
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateProfileSkills(ContactList model)
        {
            UpdateBusinessProfile obj = new UpdateBusinessProfile();
            try
            {
                string Type = "";
                string Status = "";
                string Content = "";
                var contactList = new List<ContactList>();
                DataSet ds = obj.UpdateBusinessInfo();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Profile Updated Successfully"
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ex.Message,

                     });
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateProfileStatus(ProfileStatus objprofile)
        {
            try
            {
                DataSet ds = objprofile.UpdateProfileStatus();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Status Updated Successfully"
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage Updateleg(Leg1 objprofile)
        {
            try
            {
                DataSet ds = objprofile.UpdateLeg();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Leg Updated Successfully"
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }

        [HttpPost]
        #endregion
        public HttpResponseMessage UsedNFC(NFCRequestAPI model)
        {
            try
            {
                List<UsedNFCAPI> lst = new List<UsedNFCAPI>();
                if (model.Code == "")
                {
                    model.Code = null;
                }
                DataSet ds = model.GetUsedNFC();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        UsedNFCAPI obj = new UsedNFCAPI();
                        obj.SrNo = i;
                        obj.PK_NFCId = r["PK_NFCId"].ToString();
                        obj.Name = r["Name"].ToString();
                        obj.MobileNo = r["Mobile"].ToString();
                        obj.Email = r["Email"].ToString();
                        obj.Code = r["Code"].ToString();
                        obj.ActivatedOn = r["ActivatedOn"].ToString();
                        obj.NFCStatus = r["NFCStatus"].ToString();
                        lst.Add(obj);
                        i++;
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Record Found",
                              lstUsedNFC = lst
                          });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }

        }
        [HttpPost]
        public HttpResponseMessage UnusedNFC(NFCRequestAPI model)
        {
            try
            {
                if (model.Code == "")
                {
                    model.Code = null;
                }
                List<UnusedNFCAPI> lst = new List<UnusedNFCAPI>();
                DataSet ds = model.GetUnusedNFC();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int i = 1;
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        UnusedNFCAPI obj = new UnusedNFCAPI();
                        obj.SrNo = i;
                        obj.PK_NFCId = r["PK_NFCId"].ToString();
                        obj.Code = r["Code"].ToString();
                        obj.AllotedOn = r["AssignedDate"].ToString();
                        obj.AssignedBy = r["AssignedBy"].ToString();
                        obj.NFCStatus = r["NFCStatus"].ToString();
                        lst.Add(obj);
                        i++;
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Record Found",
                              lstUnusedNFC = lst
                          });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }

        }

        [HttpPost]
        public HttpResponseMessage InventoryRequest(InventoryRequest model)
        {
            try
            {
                DataSet ds1 = model.RequestForNFC();
                if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                {
                    if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Requested successfully !!",

                          });
                    }
                    else if (ds1.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = ds1.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                        });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occured",

                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error: " + ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateNFCProfileOnOffStatus(NFCstatus model)
        {
            try
            {
                DataSet ds = model.UpdateProfileOnOffStatus();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if (model.IsProfileTurnedOff == true)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Your profile turned off successfully!!",

                          });
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Your profile turned on successfully!!",

                        });
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = "Error Occured",

                         });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error: " + ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetProduct(Product model)
        {
            List<ProductResponse> lst = new List<ProductResponse>();
            try
            {
                DataSet ds = model.GetNFCForInventory();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ProductResponse obj = new ProductResponse();
                        obj.ProductId = r["PK_EventId"].ToString();
                        obj.ProductName = r["EventName"].ToString();
                        lst.Add(obj);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.OK,
                             Message = "Record Found",
                             lst = lst
                         });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "No Record Found",
                        });
                }

            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error: " + ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetInventoryCount(InventoryData model)
        {
            try
            {
                DataSet ds = model.GetNFCForInventory();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.OK,
                             Message = "Record Found",
                             InventoryRequest = ds.Tables[0].Rows[0]["InventoryRequest"].ToString(),
                             AvailableInventory = ds.Tables[0].Rows[0]["AvailableInventory"].ToString(),
                             DeliveredInventory = ds.Tables[0].Rows[0]["DeliveredInventory"].ToString(),
                         });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "No Record Found",
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = "Error: " + ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage EmailVerification(securityDetail model)
        {
            try
            {
                DataSet ds = model.VerifyEmailCode();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Verified Successfully",
                          });
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.InternalServerError,
                             Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                         });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "Error Occurred",
                        });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "Error Occurred",
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage UploadAadhar()
        {
            Aadhar model = new Aadhar();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                model.PK_UserId = HttpContext.Current.Request.Params["PK_UserId"];
                model.AadharNo = HttpContext.Current.Request.Params["AadharNo"];
                var file = HttpContext.Current.Request.Files[0];
                model.AadharImage = rn.Next(10, 100) + "_aadhar_" + file.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/KYCDocuments/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                file.SaveAs(folderPath + model.AadharImage);
                model.AadharImage = "/KYCDocuments/" + model.AadharImage;
                DataSet ds = model.UploadAdhar();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.OK,
                           Message = "Aadhar Uploaded Successfully",
                       });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.OK,
                           Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                       });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occurred",
                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = ex.Message,
                       });
            }
        }
        [HttpPost]
        public HttpResponseMessage UploadPan()
        {
            Pan model = new Pan();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                model.PK_UserId = HttpContext.Current.Request.Params["PK_UserId"];
                model.PanNo = HttpContext.Current.Request.Params["PanNo"];
                var file = HttpContext.Current.Request.Files[0];
                model.PanImage = rn.Next(10, 100) + "_pan_" + file.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/KYCDocuments/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                file.SaveAs(folderPath + model.PanImage);
                model.PanImage = "/KYCDocuments/" + model.PanImage;
                DataSet ds = model.UploadPan();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.OK,
                           Message = "Pan Uploaded Successfully",
                       });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.OK,
                           Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                       });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occurred",
                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = ex.Message,
                       });
            }
        }
        [HttpPost]
        public HttpResponseMessage UploadDocument()
        {
            Document model = new Document();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                model.PK_UserId = HttpContext.Current.Request.Params["PK_UserId"];
                model.DocumentNo = HttpContext.Current.Request.Params["DocumentNo"];
                var file = HttpContext.Current.Request.Files[0];
                model.DocumentImage = rn.Next(10, 100) + "_document_" + file.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/KYCDocuments/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                file.SaveAs(folderPath + model.DocumentImage);
                model.DocumentImage = "/KYCDocuments/" + model.DocumentImage;
                DataSet ds = model.UploadDocument();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.OK,
                           Message = "Document Uploaded Successfully",
                       });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.OK,
                           Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                       });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occurred",
                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = ex.Message,
                       });
            }
        }
        [HttpPost]
        public HttpResponseMessage UploadProfilePic()
        {
            Photo model = new Photo();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                model.PK_UserId = HttpContext.Current.Request.Params["PK_UserId"];
                var file = HttpContext.Current.Request.Files[0];
                model.ProfilePic = rn.Next(10, 100) + "_photo_" + file.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/images/ProfilePicture/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                file.SaveAs(folderPath + model.ProfilePic);
                model.ProfilePic = "/images/ProfilePicture/" + model.ProfilePic;
                DataSet ds = model.UploadProfilePic();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.OK,
                           Message = "Profile Pic Uploaded Successfully",
                       });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.OK,
                           Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                       });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occurred",
                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = ex.Message,
                       });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetNFCActivationStatus(BusinessDashboardRequest model)
        {
            try
            {
                DataSet ds = model.GetNFCActivationStatus();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          IsActivated = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsActivated"]),
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            IsActivated = false,
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage CheckResellerStatus(BusinessDashboardRequest model)
        {
            try
            {
                DataSet ds = model.CheckResellerStatus();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          IsAcceptanceTNC = Convert.ToBoolean(ds.Tables[0].Rows[0]["IsAcceptanceTNC"]),
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            IsAcceptanceTNC = false,
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        //[HttpPost]
        //public HttpResponseMessage SaveNFCProfileLog(NFCProfileLog model)
        //{
        //    try
        //    {
        //        DataSet ds = model.InsertLog();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //             new
        //             {
        //                 StatusCode = HttpStatusCode.OK,
        //                 Message = "Log saved successfully",
        //             });
        //            }
        //            else
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //           new
        //           {
        //               StatusCode = HttpStatusCode.InternalServerError,
        //               Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
        //           });
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK,
        //                new
        //                {
        //                    StatusCode = HttpStatusCode.InternalServerError,
        //                    IsAcceptanceTNC = "Error occurred",
        //                });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK,
        //           new
        //           {
        //               StatusCode = HttpStatusCode.InternalServerError,
        //               Message = "Error: " + ex.Message,

        //           });
        //    }
        //}
        [HttpPost]
        public HttpResponseMessage GetSponsorForReseller()
        {
            SponsorForReseller obj = new SponsorForReseller();
            try
            {
                DataSet ds = obj.GetSponsorForReseller();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.OK,
                         Message = "Record Found",
                         SponsorName = ds.Tables[0].Rows[0]["FullName"].ToString(),
                         FK_SponosrId = ds.Tables[0].Rows[0]["SponsorId"].ToString(),
                         SponsorCode = ds.Tables[0].Rows[0]["UserCode"].ToString(),
                     });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "No Record Found",
                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error : " + ex.Message,
                     });
            }
        }
        public HttpResponseMessage RecentAnalytics(NFCProfile model)
        {
            try
            {
                List<NFCModelAPI> lst = new List<NFCModelAPI>();
                DataSet ds = model.GetRecentAnalytics();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        NFCModelAPI obj = new NFCModelAPI();
                        obj.Device = r["Device"].ToString();
                        obj.Browser = r["Browser"].ToString();
                        obj.IP = r["IP"].ToString();
                        obj.ViewDateTime = r["ViewDateTime"].ToString();
                        obj.Location = r["Location"].ToString();
                        lst.Add(obj);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.OK,
                    Message = "Record found",
                    lst = lst
                });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = "No Recent Exchanges available",
                });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
               new
               {
                   StatusCode = HttpStatusCode.OK,
                   Message = "Error : " + ex.Message,
               });
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateRedirectionStatus(SocialMediaRedirection model)
        {
            try
            {
                DataSet ds = model.UpdateStatus();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Status changed successfully"
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateContactAsWhatsapp(Whatsapp model)
        {
            try
            {
                DataSet ds = model.EditWhatsappNo();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Contact updated successfully"
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        public HttpResponseMessage InvoiceList(NFCProfile model)
        {
            try
            {
                List<InvoiceAPI> lst = new List<InvoiceAPI>();
                DataSet ds = model.GetListForInvoice();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        InvoiceAPI obj = new InvoiceAPI();
                        obj.InvoiceNo = r["Pk_InvestmentId"].ToString();
                        obj.PK_BookingId = r["PK_EventBookingId"].ToString();
                        obj.EventName = r["Title"].ToString();
                        obj.Description = r["Description"].ToString();
                        obj.Fk_EventId = r["FK_EventId"].ToString();
                        obj.ProductPrice = r["EventPrice"].ToString();
                        obj.Fk_UserId = r["Fk_UserId"].ToString();
                        obj.ActivationDate = r["ActivatedOn"].ToString();
                        lst.Add(obj);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                            new
                            {
                                StatusCode = HttpStatusCode.OK,
                                Message = "Record Found",
                                lst = lst
                            });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                            new
                            {
                                StatusCode = HttpStatusCode.InternalServerError,
                                Message = "No Record Found"
                            });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        public HttpResponseMessage PrintInvoice(Invoice model)
        {
            try
            {
                DataSet ds = model.PrintInvoice();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.OK,
                        Message = "Record Found",
                        DisplayName = ds.Tables[0].Rows[0]["Name"].ToString(),
                        UserCode = ds.Tables[0].Rows[0]["UserCode"].ToString(),
                        Address = ds.Tables[0].Rows[0]["Address"].ToString(),
                        Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString(),
                        Email = ds.Tables[0].Rows[0]["Email"].ToString(),
                        BusinessName = ds.Tables[0].Rows[0]["BusinessName"].ToString(),
                        InvoiceNo = ds.Tables[0].Rows[0]["PK_InvestmentId"].ToString(),
                        ActivationDate = ds.Tables[0].Rows[0]["ActivatedOn"].ToString(),
                        EventName = ds.Tables[0].Rows[0]["Title"].ToString(),
                        Description = ds.Tables[0].Rows[0]["Description"].ToString(),
                        Quantity = ds.Tables[0].Rows[0]["Quantity"].ToString(),
                        ProductPrice = ds.Tables[0].Rows[0]["EventPrice"].ToString(),
                        SGST = ds.Tables[1].Rows[0]["SGST"].ToString(),
                        CGST = ds.Tables[1].Rows[0]["CGST"].ToString(),
                        Total = ds.Tables[0].Rows[0]["EventPrice"].ToString(),
                        TotalFinalAmount = ds.Tables[1].Rows[0]["TotalFinalAmount"].ToString(),
                        TotalFinalAmountWords = ds.Tables[1].Rows[0]["TotalFinalAmountWords"].ToString(),
                        ValueBeforeTax = ds.Tables[1].Rows[0]["Taxable"].ToString(),
                        TaxAdded = ds.Tables[1].Rows[0]["TaxAmount"].ToString(),
                    });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "No Record Found",
                   });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateIncludedStatus(Include model)
        {
            try
            {
                DataSet ds = model.UpdateIncludedStatus();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Included Status changed successfully"
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage SaveProfileName(AddProfile model)
        {
            try
            {
                DataSet ds = model.InsertProfileName();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Profile added successfully",
                              PK_ProfileId = ds.Tables[0].Rows[0]["PK_ProfileId"].ToString()
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                              PK_ProfileId = ""
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "Error occurred",
                            PK_ProfileId = ""
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,
                       PK_ProfileId = ""
                   });
            }
        }
        //[HttpPost]
        //public HttpResponseMessage SaveUPIId(UPIId model)
        //{
        //    try
        //    {
        //        DataSet ds = model.SaveUPI();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["MSG"].ToString() == "1")
        //            {

        //                return Request.CreateResponse(HttpStatusCode.OK,
        //                  new
        //                  {
        //                      StatusCode = HttpStatusCode.OK,
        //                      Message = "UPI added successfully",
        //                  });
        //            }
        //            else
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //                  new
        //                  {
        //                      StatusCode = HttpStatusCode.InternalServerError,
        //                      Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
        //                  });
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK,
        //                new
        //                {
        //                    StatusCode = HttpStatusCode.OK,
        //                    Message = "Error occurred"
        //                });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK,
        //           new
        //           {
        //               StatusCode = HttpStatusCode.InternalServerError,
        //               Message = "Error: " + ex.Message,

        //           });
        //    }
        //}
        [HttpPost]
        public HttpResponseMessage UploadPassbookImage()
        {
            Passbook model = new Passbook();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                model.PK_UserId = HttpContext.Current.Request.Params["PK_UserId"];
                var file = HttpContext.Current.Request.Files[0];
                model.Image = rn.Next(10, 100) + "_photo_" + file.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/KYCDocuments/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                file.SaveAs(folderPath + model.Image);
                model.Image = "/KYCDocuments/" + model.Image;
                DataSet ds = model.UploadPassbookImage();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.OK,
                           Message = "Image Uploaded Successfully",
                       });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                       });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occurred",
                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = ex.Message,
                       });
            }
        }
        [HttpPost]
        public HttpResponseMessage SaveHealthCardDetails(HealthCardDetail model)
        {
            try
            {
                DataSet ds = model.SaveHealtCardDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Details added successfully",
                              HealthCardNo = ds.Tables[0].Rows[0]["CardNumber"].ToString()
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetHealthCardDetails(Users model)
        {
            try
            {
                string Photo = "";
                DataSet ds = model.GetHealthCardDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Photo"].ToString() != "")
                    {
                        Photo = "https://dost.click/" + ds.Tables[0].Rows[0]["Photo"].ToString();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.OK,
                             Message = "Record Found",
                             PK_CardId = ds.Tables[0].Rows[0]["PK_CardId"].ToString(),
                             Name = ds.Tables[0].Rows[0]["FirstName"].ToString(),
                             Gender = ds.Tables[0].Rows[0]["Gender"].ToString(),
                             Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString(),
                             DOB = ds.Tables[0].Rows[0]["DOB"].ToString(),
                             Photo = Photo,
                             HealthCardNo = ds.Tables[0].Rows[0]["CardNumber"].ToString(),
                             QrImage = Photo,
                         });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.InternalServerError,
                          Message = "No Record Found",
                          PK_CardId = ""
                      });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage UploadPicForCard()
        {
            CardPic model = new CardPic();
            try
            {
                Random rn = new Random();
                string Results = string.Empty;
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }
                model.PK_CardId = HttpContext.Current.Request.Params["PK_CardId"];
                model.PK_UserId = HttpContext.Current.Request.Params["PK_UserId"];
                var file = HttpContext.Current.Request.Files[0];
                model.Photo = rn.Next(10, 100) + "_photo_" + file.FileName;
                string folderPath = HttpContext.Current.Server.MapPath("~/images/HealthProfileImage/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                file.SaveAs(folderPath + model.Photo);
                model.Photo = "/images/HealthProfileImage/" + model.Photo;
                DataSet ds = model.UploadPhoto();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.OK,
                           Message = "Photo Uploaded Successfully",
                       });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.OK,
                           Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),
                       });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = "Error Occurred",
                       });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                       new
                       {
                           StatusCode = HttpStatusCode.InternalServerError,
                           Message = ex.Message,
                       });
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateHealthCardDetails(UpdateHealthCardDetail model)
        {
            try
            {
                DataSet ds = model.UpdateHealthCardDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Details updated successfully",
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage SaveAdharCardDetails(AdharCardDetail model)
        {
            try
            {
                DataSet ds = model.SaveAdharCardDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Details added successfully",
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAdharCardDetails(Users model)
        {
            try
            {
                string Photo = "";
                DataSet ds = model.GetAdharCardDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Photo"].ToString() != "")
                    {
                        Photo = "https://dost.click/" + ds.Tables[0].Rows[0]["Photo"].ToString();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.OK,
                             Message = "Record Found",
                             PK_CardId = ds.Tables[0].Rows[0]["PK_CardId"].ToString(),
                             Name = ds.Tables[0].Rows[0]["FirstName"].ToString(),
                             NameH = ds.Tables[0].Rows[0]["NameH"].ToString(),
                             FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString(),
                             FatherNameH = ds.Tables[0].Rows[0]["FatherNameH"].ToString(),
                             Gender = ds.Tables[0].Rows[0]["Gender"].ToString(),
                             GenderH = ds.Tables[0].Rows[0]["GenderH"].ToString(),
                             Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString(),
                             DOB = ds.Tables[0].Rows[0]["DOB"].ToString(),
                             Photo = Photo,
                             AdharNo = ds.Tables[0].Rows[0]["CardNumber"].ToString(),
                             Email = ds.Tables[0].Rows[0]["Email"].ToString(),
                             Address = ds.Tables[0].Rows[0]["Address"].ToString(),
                             AddressH = ds.Tables[0].Rows[0]["AddressH"].ToString(),
                             PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString(),
                             QrImage = Photo,
                         });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.InternalServerError,
                          Message = "No Record Found",
                          PK_CardId = ""
                      });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateAdharCardDetails(UpdateAdharCardDetail model)
        {
            try
            {
                DataSet ds = model.UpdateAdharCardDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Details updated successfully",
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage SavePanCardDetails(PanCardDetail model)
        {
            try
            {
                DataSet ds = model.SavePanCardDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Details added successfully",
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage GetPanCardDetails(Users model)
        {
            try
            {
                string Photo = "";
                DataSet ds = model.GetPanCardDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Photo"].ToString() != "")
                    {
                        Photo = "https://dost.click/" + ds.Tables[0].Rows[0]["Photo"].ToString();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.OK,
                             Message = "Record Found",
                             PK_CardId = ds.Tables[0].Rows[0]["PK_CardId"].ToString(),
                             Name = ds.Tables[0].Rows[0]["FirstName"].ToString(),
                             FatherName = ds.Tables[0].Rows[0]["FatherName"].ToString(),
                             Gender = ds.Tables[0].Rows[0]["Gender"].ToString(),
                             Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString(),
                             DOB = ds.Tables[0].Rows[0]["DOB"].ToString(),
                             Photo = Photo,
                             PanNo = ds.Tables[0].Rows[0]["CardNumber"].ToString(),
                             Email = ds.Tables[0].Rows[0]["Email"].ToString(),
                             Address = ds.Tables[0].Rows[0]["Address"].ToString(),
                             QrImage = Photo,
                         });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.InternalServerError,
                          Message = "No Record Found",
                          PK_CardId = ""
                      });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdatePanCardDetails(UpdatePanCardDetail model)
        {
            try
            {
                DataSet ds = model.UpdatePanCardDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Details updated successfully",
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        #region updatebusinessprofile
        public HttpResponseMessage GetServiceType()
        {
            Menu obj = new Menu();
            try
            {
                List<Data1> datalist = new List<Data1>();
                List<menuDetails> datalist1 = new List<menuDetails>();
                DataSet dsResult = obj.getserviceType();
                if (dsResult != null && dsResult.Tables.Count > 0)
                {

                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        obj.Status = "0";
                        foreach (DataRow row0 in (dsResult.Tables[0].Rows))
                        {
                            obj.menu = datalist1;
                        }
                    }
                    if (dsResult.Tables[0].Rows.Count > 0)
                    {
                        List<menuDetails> objrecentjoined = new List<menuDetails>();
                        {
                            #region menuDetails
                            for (int i = 0; i <= dsResult.Tables[0].Rows.Count - 1; i++)
                            {
                                List<menuData> objsub = new List<menuData>();
                                objrecentjoined.Add(new menuDetails

                                {

                                    MainServiceType = dsResult.Tables[0].Rows[i]["MainServiceType"].ToString(),
                                    Preority = dsResult.Tables[0].Rows[i]["Preority"].ToString(),
                                    InputType = dsResult.Tables[0].Rows[i]["InputType"].ToString(),



                                });
                                for (int j = 0; j <= dsResult.Tables[1].Rows.Count - 1; j++)
                                {

                                    if (dsResult.Tables[0].Rows[i]["Pk_MainServiceTypeId"].ToString() == dsResult.Tables[1].Rows[j]["Fk_MainServiceTypeId"].ToString())
                                    {
                                        objsub.Add(new menuData

                                        {
                                            Service = dsResult.Tables[1].Rows[j]["Service"].ToString(),
                                            ServiceUrl = dsResult.Tables[1].Rows[j]["ServiceUrl"].ToString(),
                                            ServiceIcon = dsResult.Tables[1].Rows[j]["ServiceIcon"].ToString(),


                                        });
                                    }
                                }
                                objrecentjoined[i].menuData = objsub;
                            }
                            datalist.Add(new Data1
                            {

                                menu = objrecentjoined,


                            });
                            #endregion
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lstservice = obj.menu = datalist[0].menu
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateBusinessProfile(UpdateBusinessProfileForMobile model)
        {
            try
            {
                UpdateBusinessProfileForMobile para = new UpdateBusinessProfileForMobile();
                int FK_NFCProfileId = 0;
                int IsIncluded = 0;
                DataTable dtcontact = new DataTable();
                dtcontact.Columns.Add("FK_NFCProfileId");
                dtcontact.Columns.Add("IsIncluded");

                if (model.ContactList != null)
                {

                    int i = 0;
                    for (i = 0; i < model.ContactList.Count; i++)
                    {
                        FK_NFCProfileId = model.ContactList[i].Pk_NfcProfileId;
                        IsIncluded = model.ContactList[i].IsIncluded;
                        dtcontact.Rows.Add(FK_NFCProfileId, IsIncluded);
                    }
                }
                DataTable dtEmail = new DataTable();
                dtEmail.Columns.Add("FK_NFCProfileId");
                dtEmail.Columns.Add("IsIncluded");
                if (model.EmailList != null)
                {

                    int i = 0;
                    for (i = 0; i < model.EmailList.Count; i++)
                    {
                        FK_NFCProfileId = model.EmailList[i].Pk_NfcProfileId;
                        IsIncluded = model.EmailList[i].IsIncluded;
                        dtEmail.Rows.Add(FK_NFCProfileId, IsIncluded);
                    }
                }
                DataTable dtWebLink = new DataTable();
                dtWebLink.Columns.Add("FK_NFCProfileId");
                dtWebLink.Columns.Add("IsIncluded");
                if (model.WebLinkList != null)
                {

                    int i = 0;
                    for (i = 0; i < model.WebLinkList.Count; i++)
                    {
                        FK_NFCProfileId = model.WebLinkList[i].Pk_NfcProfileId;
                        IsIncluded = model.WebLinkList[i].IsIncluded;
                        dtWebLink.Rows.Add(FK_NFCProfileId, IsIncluded);
                    }
                }
                DataTable dtSocialMedia = new DataTable();
                dtSocialMedia.Columns.Add("FK_NFCProfileId");
                dtSocialMedia.Columns.Add("IsIncluded");
                if (model.SocialLink != null)
                {

                    int i = 0;
                    for (i = 0; i < model.SocialLink.Count; i++)
                    {
                        FK_NFCProfileId = model.SocialLink[i].Pk_NfcProfileId;
                        IsIncluded = model.SocialLink[i].IsIncluded;
                        dtSocialMedia.Rows.Add(FK_NFCProfileId, IsIncluded);
                    }
                }
                model.dtcontact = dtcontact;
                model.dtemail = dtEmail;
                model.dtweblink = dtWebLink;
                model.dtsocial = dtSocialMedia;
                DataSet ds = model.UpdateBusinessInfo();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Profile Updated successfully",
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Error: " + ds.Tables[0].Rows[0]["ErrorMessage"].ToString(),

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        [HttpPost]

        public HttpResponseMessage GetProductImageList(GetProductImage obj)
        {
            try
            {
                DataSet ds = obj.ProductImageList();
                List<List> lst = new List<List>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        List Objload = new List();
                        Objload.Fk_ProductId = dr["Fk_ProductId"].ToString();
                        Objload.ProductImage = dr["ProductImage"].ToString();
                        Objload.Fk_ProductImageid = dr["Pk_ProductImageId"].ToString();
                        lst.Add(Objload);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          lst = lst
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                     new
                     {
                         StatusCode = HttpStatusCode.InternalServerError,
                         Message = "Record Not Found",

                     });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    new
                    {
                        StatusCode = HttpStatusCode.InternalServerError,
                        Message = ex.Message,

                    });
            }
        }
        #endregion

        #region addtocard for mobile
        [HttpPost]
        public HttpResponseMessage Addtocard(Addtocard model)
        {

            try
            {
                DataSet ds = model.AddToCard();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Product Add in Cart successfully",
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        #endregion
        [HttpPost]
        public HttpResponseMessage GetCartList(Cart obj)
        {
            try
            {
                List<ShopMobile> lstproduct = new List<ShopMobile>();
                DataSet ds = obj.AddToCartList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        ShopMobile model = new ShopMobile();
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

                        model.IGST = dr["IGST"].ToString();
                        lstproduct.Add(model);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                         new
                         {
                             StatusCode = HttpStatusCode.OK,
                             Message = "Record Found",
                             Cartlst = lstproduct,
                             TotalItem = ds.Tables[1].Rows[0]["TotalItem"].ToString(),
                             TotalReferalBV = double.Parse(ds.Tables[0].Compute("sum(TotalReferalBV)", "").ToString()).ToString("n2"),
                             TotalPrice = double.Parse(ds.Tables[0].Compute("sum(TotalPrice)", "").ToString()).ToString(""),
                             GST = double.Parse(ds.Tables[0].Compute("sum(IGST)", "").ToString()).ToString("n2"),
                             TotalDeliveryCharge = ds.Tables[0].Rows[0]["DeliveryCharges"].ToString()

                         });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.InternalServerError,
                          Message = "No Record Found",

                      });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        //[HttpPost]
        //#region update cardlist
        //public HttpResponseMessage updateAddtocard(updateAddtocard model)
        //{

        //    try
        //    {
        //        DataSet ds = model.updateAddToCard();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
        //            {

        //                return Request.CreateResponse(HttpStatusCode.OK,
        //                  new
        //                  {
        //                      StatusCode = HttpStatusCode.OK,
        //                      Message = "Product Add in Cart successfully",
        //                  });
        //            }
        //            else
        //            {
        //                return Request.CreateResponse(HttpStatusCode.OK,
        //                  new
        //                  {
        //                      StatusCode = HttpStatusCode.InternalServerError,
        //                      Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
        //                  });
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK,
        //                new
        //                {
        //                    StatusCode = HttpStatusCode.OK,
        //                    Message = "Error occurred"
        //                });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK,
        //           new
        //           {
        //               StatusCode = HttpStatusCode.InternalServerError,
        //               Message = "Error: " + ex.Message,

        //           });
        //    }
        //}
        //#endregion
        [HttpPost]
        #region Deletecardlist
        public HttpResponseMessage DeleteCardLIst(DeleteLIst model)
        {

            try
            {
                DataSet ds = model.DeleteLIstitem();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Delete Item successfully",
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        #endregion

        [HttpPost]
        public HttpResponseMessage AddAddress(SaveAddress model)
        {
            try
            {
                DataSet ds = model.SaveUserAddress();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Address saved successfully",
                              PK_AddressId = ds.Tables[0].Rows[0]["PK_AddressId"].ToString(),
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }

        #region update addree
        [HttpPost]
        public HttpResponseMessage UpdateAddress(updateAddress model)
        {
            try
            {
                DataSet ds = model.updateUserAddress();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.OK,
                              Message = "Address Changed successfully",
                              // PK_AddressId = ds.Tables[0].Rows[0]["PK_AddressId"].ToString(),
                          });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                          new
                          {
                              StatusCode = HttpStatusCode.InternalServerError,
                              Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString()
                          });
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.OK,
                            Message = "Error occurred"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
        #endregion
        [HttpPost]
        public HttpResponseMessage GetItemDetailsForCheckout(Balance model)
        {
            try
            {
                List<Shop> lstproduct = new List<Shop>();
                DataSet ds = model.AddToCartList();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Shop obj = new Shop();
                        obj.ProductName = dr["EventName"].ToString();
                        obj.OfferPrice = dr["OfferPrice"].ToString();
                        //obj.BV = dr["TotalReferalBV"].ToString();
                        obj.ProductCode = dr["productcode"].ToString();
                        obj.EventDescription = dr["EventDescription"].ToString();
                        obj.EventImage = dr["EventImage"].ToString();
                        obj.NoOfSeats = dr["TotalItem"].ToString();
                        obj.Brand = dr["Brand"].ToString();
                        obj.TotalPrice = dr["TotalPrice"].ToString();
                        obj.PK_EventId = dr["Pk_Eventid"].ToString();
                        //obj.IGST = dr["IGST"].ToString();
                        lstproduct.Add(obj);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK,
                      new
                      {
                          StatusCode = HttpStatusCode.OK,
                          Message = "Record Found",
                          WalletBalance = Convert.ToDecimal(ds.Tables[2].Rows[0]["WalletMoney"]).ToString(),
                          TotalItem = ds.Tables[1].Rows[0]["TotalItem"].ToString(),
                          OrderId = ds.Tables[3].Rows[0]["OrderId"].ToString(),
                          Discount = double.Parse(ds.Tables[0].Compute("sum(Discount)", "").ToString()).ToString("n2"),
                          TotalReferalBV = double.Parse(ds.Tables[0].Compute("sum(TotalReferalBV)", "").ToString()).ToString("n2"),
                          TotalPrice = double.Parse(ds.Tables[0].Compute("sum(TotalPrice)", "").ToString()).ToString("n2"),
                          GST = double.Parse(ds.Tables[0].Compute("sum(IGST)", "").ToString()).ToString("n2"),
                          DeliveryCharge = ds.Tables[0].Rows[0]["DeliveryCharges"].ToString(),
                          lstItems = lstproduct,
                      });
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK,
                        new
                        {
                            StatusCode = HttpStatusCode.InternalServerError,
                            Message = "No Record Found"
                        });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                   new
                   {
                       StatusCode = HttpStatusCode.InternalServerError,
                       Message = "Error: " + ex.Message,

                   });
            }
        }
    }
}
