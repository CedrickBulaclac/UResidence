using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;


namespace UResidence.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string chash;
            string hash= fc["Hash"];
            string username;
            username = fc["Username"];
            chash = Hash(hash);
            UserLogin user = new UserLogin();
            user = UResidence.UserController.Get(username, chash);
            if (user != default(UserLogin))
            {
                if (user.Level == 0)
                {

                    return RedirectToAction("Index", "Home");
                }
                else if (user.Level == 1)
                {
                    return RedirectToAction("Home", "Reserve");
                }
                else if (user.Level == 2)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
              string script = "<script type = 'text/javascript'>alert('Wrong Username or Password');</script>";
                Response.Write(script);

            }
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View(); 
        }
        public ActionResult CodeVerification()
        {
            return View();
        }
        public ActionResult NewPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            return View("CodeVerification");
        }
        [HttpPost]
        public ActionResult CodeVerification(string email)
        {
            return View("NewPassword");
        }
        [HttpPost]
        public ActionResult NewPassword(string nwpw)
        {
            return View("Index");
        }

        public string Hash(string p)
        {
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            string hash = BitConverter.ToString(sh.ComputeHash(utf8.GetBytes(p.ToString())));
            return hash;
        }

    }
}