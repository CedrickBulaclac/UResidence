using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class OwnerController : Controller
    {
        bool status;
        // GET: Owner
        public ActionResult OwnerAdd()
        {
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
        public ActionResult OwnerView()
        {
            List<Owner> ownerList = default(List<Owner>);
            ownerList = UResidence.OwnerController.GetAll();
            ViewBag.owner = ownerList;
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Owner ten = new Owner()
            {
                Id = id
            };
            status = UResidence.OwnerController.Delete(ten);
            if (status == true)
            {
                ViewBag.DeleteMessage = status;
                OwnerView();
            }
            else
            {
                ViewBag.DeleteMessage = status;
            }
            return View("OwnerView");
        }
        public ActionResult OwnerEdit()
        {
            return View();
        }
        [HttpGet]
        public ActionResult OwnerEdit(int id)
        {
            if (ModelState.IsValid)
            {
                List<Owner> ownerList = default(List<Owner>);
                ownerList = UResidence.OwnerController.GetIdOwner(id);
                ViewBag.ownerList = ownerList;
            }
            return View("OwnerEdit");
        }
        [HttpPost]
        public ActionResult OwnerEdit(FormCollection fc)
        {
            NameValueCollection values = (NameValueCollection)fc;
            Owner ten = Owner.CreateObject(values);
            if (ten.Validate())
            {
                status = UResidence.OwnerController.Update(ten);

                if (status == true)
                {
                    OwnerView();
                    ViewBag.UpdateMessage = status;
                }
                else
                {
                    ViewBag.UpdateMessage = status;
                }
                return View("OwnerView");
            }
            else
            {
                return View("OwnerEdit");
            }
        }
    }
}