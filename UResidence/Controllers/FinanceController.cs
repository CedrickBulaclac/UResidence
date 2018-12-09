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
    public class FinanceController : Controller
    {
        bool status;
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
        public ActionResult Registration_Cashier()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
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
        public ActionResult Registration_Cashier(Admin adm)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
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
                        URL= "~/Content/AdminImages/user.png"
                    };

                    UResidence.AdminController.Insert(ad);

                    Admin admi = new Admin();
                    admi = UResidence.AdminController.GetEmailAdmin(adm.Email.ToString());
                    int adminid = admi.Id;

                    UserLogin ull = new UserLogin
                    {                     
                        Username = adm.Email,
                        Hash = hash,
                        CreatedBy = "",
                        ModifyBy = "",
                        DateCreated = DateTime.Now,
                        Level = 3,
                        Locked = 0,
                        LastLogin = DateTime.Now
                    };


                    UResidence.UserController.Insert(ull);
                    SendEmail(adm.Email, pass);
                    status = true;
                    ViewBag.AddMessage = status;
                    ViewCashier();
                    return View("ViewCashier");
                }
                else
                {
                    string script = "<script type = 'text/javascript'>alert('There is an Existing Admin!Please try Again.');</script>";
                    Response.Write(script);
                    ViewBag.ErrorMessage = FixMessages(err);
                    status = false;

                }

                ViewBag.AddMessage = status;

            }
            return View();

        }
        public ActionResult ViewCashier()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Admin> adminList = UResidence.AdminController.GetAllCashier();
            return View(adminList);
        }

        public ActionResult Delete(int id)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string delete = "1";
            Admin am = new Admin()
            {
                Id = id,
                Deleted = delete
            };
            status = UResidence.AdminController.UpdateDelete(am);
            if (status == true)
            {

                ViewCashier();
            }
            ViewBag.DeleteStatus = status;
            return View("ViewCashier");
        }
        public ActionResult CashierEdit()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult CashierEdit(int id)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
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
            return View("AdminEdit");
        }

        [HttpPost]
        public ActionResult CashierEdit(Admin adm)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string[] err = new string[] { };
            if (adm.Validate(out err))
            {
                status = UResidence.AdminController.Update(adm);
                if (status == true)
                {
                    ViewBag.UpdateMessage = status;
                    ViewCashier();
                    return View("AdminView");
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
                    string folderPath = Path.Combine(Server.MapPath("~/Content/FinanceImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                       
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/FinanceImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/FinanceImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/FinanceImages/" + fileName + extension;
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


        public JsonResult UpdateImageCashier(Admin adm)
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
                    string folderPath = Path.Combine(Server.MapPath("~/Content/CashierImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/CashierImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/CashierImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/CashierImages/" + fileName + extension;
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