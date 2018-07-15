using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class BillingController : Controller
    {
        // GET: Billing
        public ActionResult BillingView()
        {
            Billing model = default(Billing);
            int id = Convert.ToInt32(Session["UID"]);
            string tor = (Session["TOR"]).ToString();
            if (tor == "Owner")
            {
                model = UResidence.BillingController.GetOwner(id);
            }
            else if(tor=="Tenant")
            {
                model = UResidence.BillingController.GetTenant(id);
            }
            return View(model);
        }
    }
}