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
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using CrystalDecisions.Shared;

namespace UResidence.Controllers
{
    public class AdminController : Controller
    {
        bool status;
        // GET: Admin
        private void SendEmail(string email1, string pass)
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
            }
            catch (Exception)
            {
                string script = "<script type = 'text/javascript'>alert('Submission Failed');</script>";
                Response.Write(script);
            }
        }
        public ActionResult Registration()
        {
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
        public ActionResult Registration(Admin adm, int typeadmin)
        {
            string hash;
            string pass = adm.Bdate.ToShortDateString();
            int typea = typeadmin;
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
                        Level = typea,
                        Locked = 0,
                        LastLogin = DateTime.Now
                    };


                    UResidence.UserController.InsertAdminId(ull);
                    SendEmail(adm.Email, pass);
                    status = true;
                    ViewBag.AddMessage = status;
                    AdminView();
                    return View("AdminView");
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
        public ActionResult AdminView()
        {
            List<Admin> adminList = UResidence.AdminController.GetAll();
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

                AdminView();
            }
            ViewBag.DeleteStatus = status;
            return View("AdminView");
        }
        public ActionResult AdminEdit()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AdminEdit(int id)
        {
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
            string[] err = new string[] { };
            if (adm.Validate(out err))
            {
                status = UResidence.AdminController.Update(adm);
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
            ViewBag.UpdateMessage = true;
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
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Views/Report"), "AdminList.rpt"));
            List<Admin> data = default(List<Admin>);
            data = UResidence.AdminController.GetAll();
            rd.SetDataSource(data.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "AdminList.pdf");

            }
            catch (Exception)
            {
                throw;
            }

        }

     
    }
}