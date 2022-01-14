using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dost.Filter;
using Dost.Models;
using System.Data;
using System.Data.SqlClient;
namespace Dost.Controllers
{
    public class AdminController : AdminBaseController
    {
      // GET: Admin
        public ActionResult AdminDashboard()
        {
            return View();
        }
        public ActionResult GenerateNFCCode()
        {
            NFCData model = new NFCData();
            return View();
        }
        [HttpPost]
        public ActionResult GenerateNFCCode(Home model)
        {
            DataSet ds = new DataSet();
            model.AddedBy = Session["Pk_AdminId"].ToString();
            for (int i = 1; i <= model.Quantity; i++)
            {
                Random ran = new Random();
                model.Code = ran.Next(111111, 999999).ToString();
                model.Code = 'D' + model.Code;
                model.EncCode = Crypto.EncryptNFC(model.Code);
                ds = model.GenerateNFCCode();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        i = i - 1;
                    }
                }
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                {
                    TempData["Msg"] = "NFC Code generated successfully.";
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("AdminLogin", "Home");
        }
    }
}