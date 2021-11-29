using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dost.Models;
using System.Data;
using Dost.Filter;

namespace Dost.Controllers
{
    public class AdminReportsController : AdminBaseController
    {
        // GET: AdminReports
        #region withdrawl
        public ActionResult WithdrawlRequestList()
        {
            Wallet objewallet = new Wallet();
            List<Wallet> lst = new List<Wallet>();
            DataSet ds = objewallet.GetWithdrawlWalletRequest();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Wallet Objload = new Wallet();
                    Objload.Pk_RequestId = dr["Pk_RequestId"].ToString();
                    Objload.LoginId = dr["LoginId"].ToString();
                    Objload.DisplayName = dr["Name"].ToString();
                    Objload.Amount = dr["Amount"].ToString();
                    Objload.Status = dr["Status"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    lst.Add(Objload);
                }
                objewallet.WalletRequestList = lst;
            }
            return View(objewallet);
        }
        public ActionResult Approve(string Id)
        {
            try
            {
                Wallet obj = new Wallet();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Pk_RequestId = Id;
                obj.Status = "Approved";
                DataSet ds = obj.ApproveWithdrawlwalletRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["EApproveRequest"] = "Withdrawl Request Approved Successfully";
                    }
                    else
                    {
                        TempData["EApproveRequest"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["EApproveRequest"] = ex.Message;
            }
            return RedirectToAction("WithdrawlRequestList", "AdminReports");
        }
        public ActionResult Decline(string Id)
        {
            try
            {
                Wallet obj = new Wallet();
                obj.AddedBy = Session["Pk_AdminId"].ToString();
                obj.Pk_RequestId = Id;
                obj.Status = "Declined";
                DataSet ds = obj.ApproveWithdrawlwalletRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["EApproveRequest"] = "Withdrawl Request Declined Successfully";
                    }
                    else
                    {
                        TempData["EApproveRequest"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["EApproveRequest"] = ex.Message;
            }
            return RedirectToAction("WithdrawlRequestList", "AdminReports");
        }
        #endregion
    }
}