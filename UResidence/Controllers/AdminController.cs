using System;
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
             int Id = Convert.ToInt32 (fc["Id"]);
             string Adno = fc["AdminNo"];
             string Fn = fc["Fname"];
             string Mn = fc["Mname"];
             string Ln = fc["Lname"];
             DateTime Bday =Convert.ToDateTime( fc["Bdate"]);
             string Cn = fc["CelNo"];
             string Tn = fc["TelNo"];
             int Gen = Convert.ToInt32(fc["Gender"]);
             int Ag = Convert.ToInt32(fc["Age"]);
             string mail = fc["Email"];


            Admin ad = new Admin()
            {
                Id = Id,
                AdminNo = Adno,
                Fname = Fn,
                Mname = Mn,
                Lname = Ln,
                Bdate = Bday,
                CelNo = Cn,
                TelNo = Tn,
                Gender = Gen,
                Age = Ag,
                Email = mail

            };

            status = UResidence.AdminController.Insert(ad);
            if (status==true)
            {
                ViewBag.Message = true;
            
            }
            else
            {
                ViewBag.Message = false;
            }


            return View();
        }


        public ActionResult AdminView ()
        {
            List<Admin> adminlist = default(List<Admin>);
            adminlist = UResidence.AdminController.GetAll();
            ViewBag.admin = adminlist;
            return View ();
            
        }
        
        [HttpGet]
        public ActionResult AdminView (int? Adno)
        {
            Admin adm = new Admin
            {
                AdminNo = Adno.ToString()
            };

            status = UResidence.AdminController.Delete(adm);
            if (status==true)
            {
                List<Admin> adminlist = default(List<Admin>);
                adminlist = UResidence.AdminController.GetAll();
                ViewBag.admin = adminlist;
                

            }
            return View();
        }
    }
}