using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class AdminController : Controller
    {
        bool status;
        // GET: Admin
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(FormCollection fc)
        {
            NameValueCollection values = new NameValueCollection();
            Admin ad = Admin.CreateObject(values);
            if (ad.Validate() == true)
            {
                status = UResidence.AdminController.Insert(ad);
                if (status == true)
                {
                    ViewBag.Message = true;

                }
                else
                {
                    ViewBag.Message = false;
                }
            }

            return View();
        }

    }
}