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
            NameValueCollection values =(NameValueCollection)fc;
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
        public ActionResult AdminView()
        {
            List<Admin> adminList = default(List<Admin>);
            adminList = UResidence.AdminController.GetAll();
            ViewBag.admin = adminList;
            return View();
        }
        public ActionResult Delete(int? id)
        {

            Admin am = new Admin()
            {
                AdminNo = id.ToString()
            };
            status = UResidence.AdminController.Delete(am);
            if (status == true)
            {
                AdminView();
            }
            ViewBag.DeleteStatus = status;
            return View("AdminView");
       }
        [HttpGet]
        public ActionResult AdminEdit(int id)
        {
            if (ModelState.IsValid)
            {
                List<Admin> adminList = default(List<Admin>);
                adminList = UResidence.AdminController.GetbyID(id);
                ViewBag.updateList = adminList;
                return View("AdminEdit");

            }
            return View();
        }
        [HttpPost]
        public ActionResult AdminEdit(FormCollection fc)
        {
            NameValueCollection values = (NameValueCollection)fc;
            Admin ad = Admin.CreateObject(values);
            if (ad.Validate())
            {
                status = UResidence.AdminController.Update(ad);
                if (status == true)
                {
                    AdminView();
                }
                ViewBag.UpdateMessage = status;
                
            }
            return View("AdminView");
        }
    }
}