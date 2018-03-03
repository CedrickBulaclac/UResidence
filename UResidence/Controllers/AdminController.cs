using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

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

        public static string Hash(string p)
        {
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            string hash = BitConverter.ToString(sh.ComputeHash(utf8.GetBytes(p.ToString())));
            return hash;
        }
        [HttpPost]
        public ActionResult Registration(Admin adm)
        {
            string hash;
            string pass = adm.Bdate.ToShortDateString();
            hash = Hash(pass);
            List<UserLogin> listUser = UResidence.UserController.GetAll(adm.Email);
            UserLogin ul = new UserLogin
            {
                Username = adm.Email,
                Hash = hash,
                CreatedBy = "",
                ModifyBy = "",
                DateCreated = DateTime.Now,
                Level = 0,
                Locked = 1,
                LastLogin = DateTime.Now
            };

            string[] err = new string[] { };
            if (adm.Validate(out err))
            {
                UResidence.UserController.Insert(ul);
                UResidence.AdminController.Insert(adm);          
                    ViewBag.Message = true;       
            }
            else
            {
                ViewBag.ErrorMessage =FixMessages(err);
                ViewBag.Message = false;
            }
            return View();
        }
        public ActionResult AdminView()
        {
            List<Admin> adminList = UResidence.AdminController.GetAll();
            return View(adminList);
        }
        public ActionResult Delete(int id)
        {

            Admin am = new Admin()
            {
                Id = id
            };
            status=UResidence.AdminController.Delete(am);
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
              Admin adm=UResidence.AdminController.GetbyID(id);                     
            return View(adm);
        }
        [HttpPost]
        public ActionResult AdminEdit(Admin adm)
        {
            string[] err = new string[] { };
            if (adm.Validate(out err))
            {
                status = UResidence.AdminController.Update(adm);
                if (status == true)
                {                   
                    return RedirectToAction("AdminView");
                }
            }
            else
            {
                ViewBag.ErrorMessages = FixMessages(err);
            }
            ViewBag.UpdateMessage = true;
            return View(adm);
        }
        public string FixMessages(string[] err)
        {
            string errors = "Please check the following: <br/>";
            foreach (string er in err) errors += (er + "<br/>");
            return errors;
        }
    }
}