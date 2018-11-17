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
using Microsoft.Reporting.WebForms;
using System.IO;



namespace UResidence.Controllers
{
    public class AdminController : Controller
    {
        
        bool status;
        // GET: Admin
        public ActionResult GetAdmin()
        {
            List<Admin> ret = new List<Admin>();
            ret = UResidence.AdminController.GetAll();
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        private bool SendEmail(string email1, string pass)
        {
            try
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
                return true;
            }
            catch (Exception)
            {
                string script = "<script type = 'text/javascript'>alert('The email account that you tried to reach does not exist');</script>";
                Response.Write(script);
                return false;
            }
        }
        public ActionResult Registration()
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
            ViewBag.Level = level;

            return View();
        }
        public ActionResult Home()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);      
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            ViewBag.ReservationModule = a.ReservationModule;
            ViewBag.RegistrationModule = a.RegistrationModule;
            ViewBag.LogBookModule = a.LogBookModule;
            ViewBag.PaymentModule = a.PaymentModule;
            ViewBag.ReversalModule = a.ReversalModule;
            Session["ReservationModule"] = ViewBag.ReservationModule;
            Session["RegistrationModule"] = ViewBag.RegistrationModule;
            Session["LogBookModule"] = ViewBag.LogBookModule;
            Session["PaymentModule"] = ViewBag.PaymentModule;
            Session["ReversalModule"] = ViewBag.ReversalModule;
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
        public ActionResult Registration(Admin adm,int typeadmin)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string hash;
            string pass = adm.Bdate.ToShortDateString();
            int typea = Convert.ToInt32(typeadmin);
            hash = Hash(pass);
            List<UserLogin> listUser = UResidence.UserController.GetAll(adm.Email);

            int level = Convert.ToInt32(Session["Level"]);
            if (level == 1)
            {
                if (listUser.Count == 0)
                {
                    string[] err = new string[] { };
                    if (adm.Validate(out err))
                    {
                        status = SendEmail(adm.Email, pass);
                        if (status == true)
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
                                URL = "~/Content/AdminImages/user.png",
                                ReservationModule = adm.ReservationModule,
                                RegistrationModule = adm.RegistrationModule,
                                PaymentModule = adm.PaymentModule,
                                ReversalModule = adm.ReversalModule,
                                LogBookModule = adm.LogBookModule
                            };

                            UResidence.AdminController.InsertBoss(ad);

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
                                Level = typea,
                                Locked = 0,
                                LastLogin = DateTime.Now
                            };
                            status = UResidence.UserController.Insert(ull);

                            if (status == true)
                            {
                                List<UserLogin> ul = new List<UserLogin>();
                                ul = UserController.GetAll(adm.Email);
                                if (ul.Count > 0)
                                {
                                    status = UResidence.AdminController.Update(ul[0].Id, adm.Email);
                                }
                            }



                            ViewBag.AddMessage = status;
                            AdminView();
                            return View("AdminView");
                        }
                        else
                        {
                            string script = "<script type = 'text/javascript'>alert('The email account that you tried to reach does not exist');</script>";
                            Response.Write(script);
                            
                        }
                    }
                    else
                    {
                        string script = "<script type = 'text/javascript'>alert('Error.');</script>";
                        Response.Write(script);
                        ViewBag.ErrorMessage = FixMessages(err);
                        status = false;

                    }                 
                }
                else
                {
                    string script = "<script type = 'text/javascript'>alert('There is an Existing Admin!Please try Again.');</script>";
                    Response.Write(script);
                   
                    status = false;

                }
            }
            else
            {
                if (listUser.Count == 0)
                {
                    string[] err = new string[] { };
                    if (adm.Validate(out err))
                    {
                        status=SendEmail(adm.Email, pass);
                        if (status == true)
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
                                URL = "~/Content/AdminImages/user.png",
                                ReservationModule = adm.ReservationModule,
                                RegistrationModule = adm.RegistrationModule,
                                PaymentModule = adm.PaymentModule,
                                ReversalModule = adm.ReversalModule,
                                LogBookModule = adm.LogBookModule
                            };

                            UResidence.AdminController.InsertBoss(ad);

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
                                Level = typea,
                                Locked = 0,
                                LastLogin = DateTime.Now
                            };


                            status = UResidence.UserController.Insert(ull);

                            if (status == true)
                            {
                                List<UserLogin> ul = new List<UserLogin>();
                                ul = UserController.GetAll(adm.Email);
                                if (ul.Count > 0)
                                {
                                    status = UResidence.AdminController.Update(ul[0].Id, adm.Email);
                                }
                            }

                            ViewBag.AddMessage = status;
                            AdminView();
                            return View("AdminView");
                        }
                        else
                        {
                            string script = "<script type = 'text/javascript'>alert('The email account that you tried to reach does not exist');</script>";
                            Response.Write(script);
                        }
                    }
                    else
                    {
                        string script = "<script type = 'text/javascript'>alert('Error.');</script>";
                        Response.Write(script);
                        ViewBag.ErrorMessage = FixMessages(err);
                        status = false;

                    }

                    ViewBag.AddMessage = status;

                }
                else
                {
                    string script = "<script type = 'text/javascript'>alert('There is an Existing Admin!Please try Again.');</script>";
                    Response.Write(script);
                   
                    status = false;

                }
            }
            AdminView();
            return View("AdminView");

        }
        public ActionResult AdminView()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);
          
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            ViewBag.Level = level;
            ViewBag.ReservationModule = a.ReservationModule;
            ViewBag.RegistrationModule = a.RegistrationModule;
            ViewBag.LogBookModule = a.LogBookModule;
            ViewBag.PaymentModule = a.PaymentModule;
            ViewBag.ReversalModule = a.ReversalModule;
            Session["ReservationModule"] = ViewBag.ReservationModule;
            Session["RegistrationModule"] = ViewBag.RegistrationModule;
            Session["LogBookModule"] = ViewBag.LogBookModule;
            Session["PaymentModule"] = ViewBag.PaymentModule;
            Session["ReversalModule"] = ViewBag.ReversalModule;

            return View();
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
           
            Admin a = new Admin();
            a = UResidence.AdminController.GetbyID(id);
          
                UserLogin ul = new UserLogin();
                ul = UResidence.UserController.Get(a.Email);
                status = UResidence.UserController.UpdateLockout(ul.Id);
            if (status == true)
            {
                status = UResidence.AdminController.UpdateDelete(am);
                AdminView();
            }
            ViewBag.DeleteStatus = status;
            return View("AdminView");
        }
        public ActionResult AdminEdit()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);
            ViewBag.Level = level;
            return View();
        }
        [HttpGet]
        public ActionResult AdminEdit(int id)
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
        public ActionResult AdminEdit(Admin adm)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string[] err = new string[] { };
            if (adm.Validate(out err))
            {
                int level = Convert.ToInt32(Session["Level"]);
                if (level == 1)
                {
                    status = UResidence.AdminController.UpdateBoss(adm);
                }
                else
                {
                    status = UResidence.AdminController.Update(adm);
                }
                if (status == true)
                {
                    ViewBag.UpdateMessage = status;
                    AdminView();
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
         
            return View();
        }

        public string FixMessages(string[] err)
        {
            string errors = "Please check the following: <br/>";
            foreach (string er in err) errors += (er + "<br/>");
            return errors;
        }

        public ActionResult Download()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            List<Admin> data = default(List<Admin>);
            data = UResidence.AdminController.GetAll();
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Views/Report/AdminList.rdlc");
            ReportDataSource rd = new ReportDataSource();
            rd.Name = "AdminList";
            rd.Value = data.ToList();
            localreport.DataSources.Add(rd);
            string reportType = "PDF";
            string mimetype;
            string encoding;
            string filenameExtension = "pdf";
            string[] streams;
            Warning[] warnings;
            byte[] renderbyte;
            string deviceInfo = "<DeviceInfo><OutputFormat>PDF</OutputFormat><PageWidth>8.5in</PageWidth><PageHeight>11in</PageHeight><MarginTop>0.5in</MarginTop><MarginLeft>11in</MarginLeft><MarginRight>11in</MarginRight><MarginBottom>0.5in</MarginBottom></DeviceInfo>";
            renderbyte = localreport.Render(reportType, deviceInfo, out mimetype, out encoding, out filenameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename=AdminList." + filenameExtension);
            return File(renderbyte, filenameExtension);
        }
     
    }
}