using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Dost.Models
{
    public class Common
    {
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string Fk_SponsorId1 { get; set; }
        public string ReferBy { get; set; }
        public string Result { get; set; }
        public string Pincode { get; set; }
        public string DisplayName { get; set; }
        public string AddedOn { get; set; }
        public string Leg1 { get; set; }
        public string IsAcceptanceTNC { get; set; }
        public static string GenerateRandom()
        {
            Random r = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                s = string.Concat(s, r.Next(10).ToString());
            }
            return s;
        }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            DateTime Dt;
            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);
            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];
                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }
            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }
        }
        public static string ConvertDigitToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + ConvertDigitToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertDigitToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertDigitToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertDigitToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
        public DataSet GetStateCity()
        {
            SqlParameter[] para = { new SqlParameter("@Pincode", Pincode) };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);
            return ds;
        }
        public DataSet BindUserTypeForRegistration()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetUserTypeForRegistration");

            return ds;

        }
        public DataSet BindFormMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = DBHelper.ExecuteQuery("FormMasterManage", para);

            return ds;

        }
        public DataSet BindFormTypeMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = DBHelper.ExecuteQuery("FormTypeMasterManage", para);

            return ds;

        }
        #region Form Permissions By User
        public DataSet FormPermissions(string FormName, string AdminId)
        {
            try
            {
                SqlParameter[] para = {
                                          new SqlParameter("@FormName", FormName) ,
                                          new SqlParameter("@AdminId", AdminId)
                                      };

                DataSet ds = DBHelper.ExecuteQuery("PermissionsOfForm", para);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Id", ReferBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberName", para);

            return ds;
        }
        public DataSet GetSponsorForReseller()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetSponsorForReseller");
            return ds;
        }
        public DataSet GetMemberDetailsByMobile()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Id", ReferBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberNameByMobileNo", para);

            return ds;
        }
        public DataSet GetMemberDetailsForFEwallet()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Id", ReferBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberNameforFEwallet", para);

            return ds;
        }
        public DataSet GetSponsor()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Id", ReferBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetSponsor", para);

            return ds;
        }
        public DataSet GetMobileNo()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@MobileNo", ReferBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMobileNo", para);

            return ds;
        }
        public DataSet GetMemberDetailsForSale()
        {
            SqlParameter[] para = { new SqlParameter("@Id", ReferBy), };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberNameForSale", para);
            return ds;
        }
        public DataSet GetMemberDetailsForFranchiseSale()
        {
            SqlParameter[] para = { new SqlParameter("@Id", ReferBy), };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberDetailsForFranchiseSale", para);
            return ds;
        }
        public DataSet BindProduct()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductList");
            return ds;
        }
        public DataSet BindEventCategory()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetCategoryList");
            return ds;
        }

        public DataSet BindProductForFranchisee()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductListForFranchisee");
            return ds;
        }
        public DataSet BindFranchiseType()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetFranchiseType");
            return ds;
        }
        public static List<SelectListItem> BindPaymentMode()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            PaymentMode.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            PaymentMode.Add(new SelectListItem { Text = "Cheque", Value = "Cheque" });
            PaymentMode.Add(new SelectListItem { Text = "NEFT", Value = "NEFT" });
            PaymentMode.Add(new SelectListItem { Text = "UPI", Value = "UPI" });
            PaymentMode.Add(new SelectListItem { Text = "RTGS", Value = "RTGS" });
            PaymentMode.Add(new SelectListItem { Text = "Demand Draft", Value = "DD" });
            return PaymentMode;
        }
        public static List<SelectListItem> BindPaymentModeForList()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "All", Value = null });
            PaymentMode.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            PaymentMode.Add(new SelectListItem { Text = "Supplier", Value = "Supplier" });


            return PaymentMode;
        }

        public static List<SelectListItem> BindReligion()
        {
            List<SelectListItem> Religion = new List<SelectListItem>();
            Religion.Add(new SelectListItem { Text = "Select Religion", Value = null });
            Religion.Add(new SelectListItem { Text = "Hindu", Value = "Hindu" });
            Religion.Add(new SelectListItem { Text = "Muslim", Value = "Muslim" });
            Religion.Add(new SelectListItem { Text = "Christian", Value = "Christian" });

            return Religion;
        }


        public static List<SelectListItem> BindCategory()
        {
            List<SelectListItem> Category = new List<SelectListItem>();
            Category.Add(new SelectListItem { Text = "Select Category", Value = null });
            Category.Add(new SelectListItem { Text = "General", Value = "General" });
            Category.Add(new SelectListItem { Text = "OBC", Value = "OBC" });
            Category.Add(new SelectListItem { Text = "SC", Value = "SC" });
            Category.Add(new SelectListItem { Text = "ST", Value = "ST" });
            return Category;
        }

        public static List<SelectListItem> BindGender()
        {
            List<SelectListItem> Gender = new List<SelectListItem>();
            Gender.Add(new SelectListItem { Text = "Male", Value = "M" });
            Gender.Add(new SelectListItem { Text = "Female", Value = "F" });

            return Gender;
        }
        public static List<SelectListItem> BindPasswordType()
        {
            List<SelectListItem> PasswordType = new List<SelectListItem>();
            //PasswordType.Add(new SelectListItem { Text = "Select", Value = "0" });
            PasswordType.Add(new SelectListItem { Text = "Profile Password", Value = "P" });
            //PasswordType.Add(new SelectListItem { Text = "Transaction Password", Value = "T" });

            return PasswordType;
        }
        public static List<SelectListItem> TransactionType()
        {
            List<SelectListItem> TransactionType = new List<SelectListItem>();
            TransactionType.Add(new SelectListItem { Text = "Select", Value = "0" });
            TransactionType.Add(new SelectListItem { Text = "Credit", Value = "Credit" });
            TransactionType.Add(new SelectListItem { Text = "Debit", Value = "Debit" });

            return TransactionType;
        }
        public static List<SelectListItem> BindKYCStatus()
        {
            List<SelectListItem> PasswordType = new List<SelectListItem>();
            PasswordType.Add(new SelectListItem { Text = "Select", Value = "0" });
            PasswordType.Add(new SelectListItem { Text = "Not Uploaded", Value = "N" });
            PasswordType.Add(new SelectListItem { Text = "Pending", Value = "P" });
            PasswordType.Add(new SelectListItem { Text = "Approved", Value = "A" });
            PasswordType.Add(new SelectListItem { Text = "Rejected", Value = "R" });
            return PasswordType;
        }
        public static List<SelectListItem> AssociateStatus()
        {
            List<SelectListItem> AssociateStatus = new List<SelectListItem>();
            AssociateStatus.Add(new SelectListItem { Text = "All", Value = null });
            AssociateStatus.Add(new SelectListItem { Text = "Active", Value = "P" });
            AssociateStatus.Add(new SelectListItem { Text = "Inactive", Value = "T" });

            return AssociateStatus;
        }
        public static List<SelectListItem> Leg()
        {
            List<SelectListItem> Leg = new List<SelectListItem>();
            Leg.Add(new SelectListItem { Text = "All", Value = null });
            Leg.Add(new SelectListItem { Text = "Left", Value = "L" });
            Leg.Add(new SelectListItem { Text = "Right", Value = "R" });

            return Leg;
        }
        public static List<SelectListItem> BindTopupStatus()
        {
            List<SelectListItem> IncomeStatus = new List<SelectListItem>();
            IncomeStatus.Add(new SelectListItem { Text = "All", Value = null });
            IncomeStatus.Add(new SelectListItem { Text = "Calculated", Value = "1" });
            IncomeStatus.Add(new SelectListItem { Text = "Pending", Value = "0" });

            return IncomeStatus;
        }
        public static List<SelectListItem> BindRealation()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "S/O", Value = "S/O" });
            PaymentMode.Add(new SelectListItem { Text = "D/O", Value = "D/O" });
            PaymentMode.Add(new SelectListItem { Text = "W/O", Value = "W/O" });

            return PaymentMode;
        }
        public static List<SelectListItem> PaidStatus()
        {
            List<SelectListItem> PaidStatus = new List<SelectListItem>();
            PaidStatus.Add(new SelectListItem { Text = "All", Value = "null" });
            PaidStatus.Add(new SelectListItem { Text = "Paid", Value = "1" });
            PaidStatus.Add(new SelectListItem { Text = "Unpaid", Value = "0" });

            return PaidStatus;
        }

        public static List<SelectListItem> KycStatus()
        {
            List<SelectListItem> PaidStatus = new List<SelectListItem>();
            PaidStatus.Add(new SelectListItem { Text = "All", Value = "null" });
            PaidStatus.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
            PaidStatus.Add(new SelectListItem { Text = "Approved", Value = "Approved" });
            PaidStatus.Add(new SelectListItem { Text = "Rejected", Value = "Rejected" });
            return PaidStatus;
        }
        public static List<SelectListItem> BindSocialMedia()
        {
            List<SelectListItem> SocialMedia = new List<SelectListItem>();
            SocialMedia.Add(new SelectListItem { Text = "Select", Value = "0" });
            SocialMedia.Add(new SelectListItem { Text = "Facebook", Value = "https://www.facebook.com/" });
            SocialMedia.Add(new SelectListItem { Text = "Instagram", Value = "https://www.instagram.com/" });
            SocialMedia.Add(new SelectListItem { Text = "Twitter", Value = "https://twitter.com/" });
            SocialMedia.Add(new SelectListItem { Text = "LinkedIn", Value = "https://www.linkedin.com/in/" });
            SocialMedia.Add(new SelectListItem { Text = "YouTube", Value = "https://m.youtube.com/channel/" });
            return SocialMedia;
        }
        public static List<SelectListItem> BindWebLink()
        {
            List<SelectListItem> BindWebLink = new List<SelectListItem>();
            BindWebLink.Add(new SelectListItem { Text = "Select", Value = "0" });
            BindWebLink.Add(new SelectListItem { Text = "HTTP", Value = "http:" });
            BindWebLink.Add(new SelectListItem { Text = "HTTPs", Value = "https:" });
            return BindWebLink;
        }

        public string Fk_UserId { get; set; }
        //public string Address { get; set; }
        ////public string PinCode { get; set; }

        public string City { get; set; }

        public string State { get; set; }
        //public string FK_UserId { get; set; }

        //public string FK_CardTypeId { get; set; }

        public int GenerateRandomNo()
        {
            int _min = 0000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }


        public DataSet GetLegDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@Id", ReferBy),
                                        new SqlParameter("@Leg", Leg1),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetLegDetails", para);

            return ds;
        }
        public DataSet BindOffer()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetOffer");

            return ds;

        }
        public static void SendEmail(string Message, string ToMailAddress)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("info@afluex.com");
                message.To.Add(new MailAddress(ToMailAddress));
                message.Subject = "Validate Email";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = Message;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("info@afluex.com", "afluex@9919");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SMSCredential
    {
        public static string UserName = "dreamuser";
        public static string Password = "jUED8rm9rTJMBPE";
        public static string SenderId = "DRMCRS";
    }

    public class SoftwareDetails
    {
        public static string CompanyName = "Dost";
        public static string CompanyAddress = "Khasra No 937, Patwari ka Purwa, Devkali, Faizabad.";
        public static string Pin1 = "224001";
        public static string State1 = "UP";
        public static string City1 = "Faizabad";
        public static string ContactNo = "7275065834";
        public static string LandLine = "7275065834";
        public static string GSTNO = "09AAHCD3782E1ZJ";
        public static string Website = "www.dreamcrushers.in";
        public static string EmailID = "support@dreamcrushers.in";
    }


}
