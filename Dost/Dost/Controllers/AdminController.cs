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
       
    }
}