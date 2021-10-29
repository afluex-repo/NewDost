using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dost.Controllers
{
    public class WalletController : BaseController
    {
        // GET: Wallet
        public ActionResult AddFund()
        {
            return View();
        }
    }
}