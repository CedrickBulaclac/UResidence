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
        public ActionResult Index()
        {
            Session["Level"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            try
            {
                Session["Level"] = null;
                string chash;
                string hash = fc["Hash"];
                string username;
                username = fc["Username"];
                Session["pass"] = hash;
                chash = Hash(hash);
                UserLogin user = new UserLogin();
                user = UResidence.UserController.Get(username, chash);
                if (user != default(UserLogin))
                {
                    if (user.Level <= 7)
                    {

                        Session["Level"] = user.Level;
                        Session["LID"] = user.Id;


                        Session["TOR"] = "Admin";
                        Admin a = new Admin();
                        a = UResidence.AdminController.GetbyID(user.Id);
                        Session["UID"] = a.Id;
                        string Fname = RemoveWhitespace(a.Fname);
                        string Mname = RemoveWhitespace(a.Mname);
                        string Lname = RemoveWhitespace(a.Lname);
                        Session["FullName"] = Fname + " " + Mname + " " + Lname;
                        UResidence.UserController.UpdateLog(user.Id);
                        if (user.Level == 2 || user.Level == 3)
                        {
                            return RedirectToAction("CalendarView", "Calendar");
                        }
                        else if (user.Level == 4)
                        {
                            return RedirectToAction("SelectOT", "ReservationA");
                        }
                        else if (user.Level == 5)
                        {
                            return RedirectToAction("AdminView", "Admin");
                        }
                        else if (user.Level == 6 || user.Level == 7)
                        {
                            return RedirectToAction("LogBookView", "LogBook");
                        }
                        else
                        {
                            return RedirectToAction("Home", "Admin");
                        }
                    }
                    else if (user.Level == 8)
                    {
                        Session["aa"] = 0;
                        Session["Level"] = user.Level;
                        Session["LID"] = user.Id;

                        Session["TOR"] = "Owner";
                        Owner a = new Owner();
                        a = UResidence.OwnerController.GetEmailOwner(user.Username);
                        Session["UID"] = a.Id;
                        Session["BDAY"] = a.Bdate.ToShortDateString();
                        Session["UNO"] = a.UnitNo;
                        Session["BLDG"] = a.BldgNo;
                        string Fname = RemoveWhitespace(a.Fname);
                        string Mname = RemoveWhitespace(a.Mname);
                        string Lname = RemoveWhitespace(a.Lname);
                        Session["FullName"] = Fname + " " + Mname + " " + Lname;
                        UResidence.UserController.UpdateLog(user.Id);
                        return RedirectToAction("Home", "Reserve");
                    }
                    else if (user.Level == 9)
                    {
                        Session["Level"] = user.Level;
                        Session["LID"] = user.Id;

                        Session["TOR"] = "Tenant";
                        Tenant a = new Tenant();
                        a = UResidence.TenantController.GetEmailTenant(user.Username);
                        Session["UID"] = a.Id;
                        if (a.LeaseEnd <= DateTime.Now)
                        {
                            Tenant t = new Tenant
                            {
                                Deleted = "1",
                                Id = a.Id
                            };
                           UResidence.TenantController.UpdateDelete(t);
                          
                           UResidence.UserController.UpdateLockout(user.Id);
                            string script = "<script type = 'text/javascript'>alert('Wrong Username or Password');</script>";
                            Response.Write(script);
                        }
                        else
                        {
                            string Fname = RemoveWhitespace(a.Fname);
                            string Mname = RemoveWhitespace(a.Mname);
                            string Lname = RemoveWhitespace(a.Lname);
                            Session["FullName"] = Fname + " " + Mname + " " + Lname;
                            Session["BDAY"] = a.Bdate.ToShortDateString();
                            Session["UNO"] = a.UnitNo;
                            Session["BLDG"] = a.BldgNo;
                            UResidence.UserController.UpdateLog(user.Id);
                            return RedirectToAction("Home", "Reserve");
                        }
                    }
                }
                else
                {
                    string script = "<script type = 'text/javascript'>alert('Wrong Username or Password');</script>";
                    Response.Write(script);
                }
            }
            catch(Exception)
            {
                string script = "<script type = 'text/javascript'>alert('Wrong Username or Password');</script>";
                Response.Write(script);
            }
 
            return View();
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
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

        private void SendEmail()
        {
            try
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
                    Body = "Your Reference Code is : " + body + "."
                })
                {
                    i = body;
                    smtp.Send(message);

                }
            }
            catch(Exception)
            {
                string script = "<script type = 'text/javascript'>alert('Submission Failed');</script>";
                Response.Write(script);
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