using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace UResidence.Controllers
{
    public class SecurityGuardController : Controller
    {
        bool status;
        // GET: Admin
        private void SendEmail(string email1, string pass)
        {
            var fromAddress = new MailAddress("uresidence04@gmail.com", "URESIDENCE");
            var toAddress = new MailAddress(email1, "To Name");
            const string fromPassword = "uresidence";
            const string subject = "PalmDale Heights Condominium";

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
                Body = "Account Information" + "\n" + "Username :" + email1 + "\n"
                + "Password :" + pass
            })
            {

                smtp.Send(message);

            }
        }
        public ActionResult Registration()
        {
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            return View();
        }
        public ActionResult Home()
        {
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
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

            if (listUser.Count == 0)
            {
                string[] err = new string[] { };
                if (adm.Validate(out err))
                {
                    Admin ad = new Admin()
                    {
                        Fname = adm.Fname,
                        Mname = adm.Mname,
                        Lname = adm.Lname,
                        Bdate = adm.Bdate,
                        CelNo = adm.CelNo,
                        Email = adm.Email,
                        Deleted = "0",
                        URL = "~/Content/AdminImages/user.png"
                    };

                    UResidence.AdminController.Insert(ad);

                    Admin admi = new Admin();
                    admi = UResidence.AdminController.GetEmailAdmin(adm.Email.ToString());
                    int adminid = admi.Id;

                    UserLogin ull = new UserLogin
                    {
                        AdminId = adminid,
                        Username = adm.Email,
                        Hash = hash,
                        CreatedBy = "",
                        ModifyBy = "",
                        DateCreated = DateTime.Now,
                        Level = 7,
                        Locked = 0,
                        LastLogin = DateTime.Now
                    };


                    UResidence.UserController.InsertAdminId(ull);
                    SendEmail(adm.Email, pass);
                    status = true;
                    ViewBag.AddMessage = status;
                    SecurityGuardView();
                    return View("SecurityGuardView");
                }
                else
                {
                    string script = "<script type = 'text/javascript'>alert('There is an Existing Security!Please try Again.');</script>";
                    Response.Write(script);
                    ViewBag.ErrorMessage = FixMessages(err);
                    status = false;

                }

                ViewBag.AddMessage = status;

            }
            return View();

        }
        public ActionResult SecurityGuardView()
        {
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Admin> adminList = UResidence.AdminController.GetAllSecurity();
            return View(adminList);
        }
        public ActionResult Delete(int id)
        {
            string delete = "1";
            Admin am = new Admin()
            {
                Id = id,
                Deleted = delete
            };
            status = UResidence.AdminController.UpdateDelete(am);
            if (status == true)
            {

                SecurityGuardView();
            }
            ViewBag.DeleteStatus = status;
            return View("SecurityGuardView");
        }
        public ActionResult Edit()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            string i = id.ToString();
            if (ModelState.IsValid)
            {
                Admin adm = UResidence.AdminController.GetbyIDEdit(i);
                return View(adm);
            }
            return View("Edit");
        }

        [HttpPost]
        public ActionResult Edit(Admin adm)
        {
            string[] err = new string[] { };
            if (adm.Validate(out err))
            {
                status = UResidence.AdminController.Update(adm);
                if (status == true)
                {
                    ViewBag.UpdateMessage = status;
                    SecurityGuardView();
                    return View("SecurityGuardView");
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
            return View();
        }

        public JsonResult UpdateImage(Admin adm)
        {
            var image = adm.Image;
            bool status = false;
            int id = adm.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/OICImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/OICImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/OICImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/OICImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }

                    Admin a = new Admin
                    {
                        Id = id,
                        URL = finalpath
                    };


                    status = UResidence.AdminController.UpdateDP(a);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public JsonResult UpdateImageGuard(Admin adm)
        {
            var image = adm.Image;
            bool status = false;
            int id = adm.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/SecuGuardImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/SecuGuardImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/SecuGuardImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/SecuGuardImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }

                    Admin a = new Admin
                    {
                        Id = id,
                        URL = finalpath
                    };


                    status = UResidence.AdminController.UpdateDP(a);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


        public string FixMessages(string[] err)
        {
            string errors = "Please check the following: <br/>";
            foreach (string er in err) errors += (er + "<br/>");
            return errors;
        }
    }
}