using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
namespace UResidence.Controllers
{
    public class OwnerController : Controller
    {
        bool status;
        // GET: Owner
        public ActionResult OwnerView()
        {
            List<Owner> ownerList = default(List<Owner>);
            ownerList = UResidence.OwnerController.GetAll();
            ViewBag.ownerList = ownerList;
            return View();
        }
        [HttpPost]
        public ActionResult OwnerAdd(FormCollection fc)
        {
            NameValueCollection values = (NameValueCollection)fc;
            Owner ten = Owner.CreateObject(values);
            if (ten.Validate())
            {
                status = UResidence.OwnerController.Insert(ten);
                if (status == true)
                {
                    ViewBag.AddMessage = status;
                }
                else
                {
                    ViewBag.AddMessage = status;
                }
            }
            return View();
        }
    }
}