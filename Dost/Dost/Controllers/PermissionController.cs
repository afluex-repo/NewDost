using Dost.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dost.Controllers
{
    public class PermissionController : AdminBaseController
    {
        // GET: Permission
        public ActionResult SetPermission()
        {
            DataSet ds1 = new DataSet();

            #region ddlformtype
            Permission obj = new Permission();
            int count = 0;
            List<SelectListItem> ddlformtype = new List<SelectListItem>();
            ds1 = obj.BindFormTypeMaster();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlformtype.Add(new SelectListItem { Text = "Select ", Value = "0" });
                    }
                    ddlformtype.Add(new SelectListItem { Text = r["FormType"].ToString(), Value = r["PK_FormTypeId"].ToString() });
                    count = count + 1;
                }
            }
            else
            {
                ddlformtype.Add(new SelectListItem { Text = "Select ", Value = "0" });
            }
            ViewBag.ddlformtype = ddlformtype;

            #endregion
            #region ddlempid
           
            int count1 = 0;
            List<SelectListItem> ddlemplist = new List<SelectListItem>();
           DataSet ds = obj.Emplist();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlemplist.Add(new SelectListItem { Text = "Select ", Value = "0" });
                    }
                    ddlemplist.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_AdminId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            else
            {
                ddlemplist.Add(new SelectListItem { Text = "Select ", Value = "0" });
            }
            ViewBag.ddlemplist = ddlemplist;

            #endregion
            return View();
        }
    }
}