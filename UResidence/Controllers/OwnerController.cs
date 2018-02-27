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
        public ActionResult OwnerAdd(Owner owe)
        {
            string[] err = new string[] { };
            if (owe.Validate(out err))
            {
                UResidence.OwnerController.Insert(owe);
                status = true;
                return RedirectToAction("OwnerView");
            }
            else
            {
                ViewBag.ErrorMessage = FixMessages(err);
               status = false;
               
            }
            ViewBag.AddMessage = status;
            return View();
        }
        public ActionResult OwnerView()
        {
            List<Owner> ownerList = UResidence.OwnerController.GetAll();
            return View(ownerList);
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
                ViewBag.DeleteStatus = status;
                OwnerView();
            }
            else
            {
                ViewBag.DeleteStatus = status;
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
                Owner ownerList = UResidence.OwnerController.GetIdOwner(id);
                return View(ownerList);
            }
            return View("OwnerEdit");
        }
        [HttpPost]
        public ActionResult OwnerEdit(Owner owe)
        {
            string[] err = new string[] { };
            if (owe.Validate(out err))
            {
                status = UResidence.OwnerController.Update(owe);
                if (status == true)
                {
                    ViewBag.UpdateMessage = status;
                    OwnerView();
                    return View("OwnerView");
                }
                else
                {
                    ViewBag.UpdateMessage = status;
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessages = FixMessages(err);
            }
            ViewBag.UpdateMessage = true;
            return View(owe);
        }
        public string FixMessages(string[] err)
        {
            string errors = "Please check the following: <br/>";
            foreach (string er in err) errors += (er + "<br/>");
            return errors;
        }
    }
}