using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;
namespace UResidence.Controllers
{
    public class LoginController : Controller
    {
        string i;
        string email1;
        bool status;
     
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
                    UResidence.UserController.UpdateLog(user.Id);
                    return RedirectToAction("Index", "Home");
                }
                else if (user.Level == 1)
                {
                    UResidence.UserController.UpdateLog(user.Id);
                    return RedirectToAction("Home", "Reserve");
                }
                else if (user.Level == 2)
                {
                    UResidence.UserController.UpdateLog(user.Id);
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
            email1 = email;
            Session["email"] = email1;
          List<UserLogin> listUser= UResidence.UserController.GetAll(email);
            if (listUser.Count == 1)
            {
                SendEmail();
                Session["code"] = i;
                return View("CodeVerification");
            }
            else
            {
                string script = "<script type = 'text/javascript'>alert('No existing email');</script>";
                Response.Write(script);
                return View();
            }
        }
        [HttpPost]
        public ActionResult CodeVerification(string code,string co)
        {
            var CODE = Session["code"];
            if(code==CODE.ToString())
            {
                return View("NewPassword");
            }
          else
            {
                string script = "<script type = 'text/javascript'>alert('Code doesn't match');</script>";
                Response.Write(script);
                return View();
            }
        }
        [HttpPost]
        public ActionResult NewPassword(string pw)
        {
            var e = Session["email"];
            string hash;
            hash = Hash(pw);
            UResidence.UserController.Update(e.ToString(), hash);     
                string script = "<script type = 'text/javascript'>alert('Password has been changed');</script>";
                Response.Write(script);
                return View("Index");
        }


        private string Hash(string p)
        {
            string hash="";          
                SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
                UTF8Encoding utf8 = new UTF8Encoding();
               hash = BitConverter.ToString(sh.ComputeHash(utf8.GetBytes(p.ToString())));             
            return hash;
        }

        public void SendEmail()
        {
                var fromAddress = new MailAddress("uresidence04@gmail.com", "URESIDENCE");
                var toAddress = new MailAddress(email1, "To Name");
                const string fromPassword = "uresidence";
                const string subject = "Reset Password";
                string body = Randomizer();
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                i = body;
                smtp.Send(message);
                   
                }
        }

          public string Randomizer()
        {
            char[] letter = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            Random rd = new Random();
            string Code = "";
            for (int i = 0; i <= 3; i++)
            {
                Code += letter[rd.Next(0, 62)].ToString();
            }

            return Code;
        }
}
    
}