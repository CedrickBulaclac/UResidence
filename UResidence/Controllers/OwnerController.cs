﻿using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Reporting.WebForms;

namespace UResidence.Controllers
{

    public class OwnerController : Controller
    {
        bool status;

        // GET: Owner
        public ActionResult OwnerAdd()
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
            catch(Exception)
            {
                string script = "<script type = 'text/javascript'>alert('The email account that you tried to reach does not exist');</script>";
                Response.Write(script);
            }
        }


        [HttpPost]
        public ActionResult OwnerAdd(Owner owe, HttpPostedFileBase Image1)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string finalpath = "";
            var image = Image1;
            bool status = false;        
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/OwnerImages"), imagefileName);
                  
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/OwnerImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/OwnerImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/OwnerImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                       

                    }
                }
            }
            
                    string hash;
            string pass = owe.Bdate.ToShortDateString();
            hash = Hash(pass);
            List<UserLogin> listUser = UResidence.UserController.GetAllO(owe.Email);
            UserLogin ul = new UserLogin
            {
                Username = owe.Email,
                Hash = hash,
                CreatedBy = "",
                ModifyBy = "",
                DateCreated = DateTime.Now,
                Level = 1,
                Locked = 1,
                LastLogin = DateTime.Now
            };

       
           
            if (listUser.Count == 0)
            {
                string[] err = new string[] { };
                if (owe.Validate(out err))
                {
                    if (image != null)
                    {
                        Owner ow = new Owner()
                        {

                            BldgNo = owe.BldgNo,
                            UnitNo = owe.UnitNo,
                            Fname = owe.Fname,
                            Mname = owe.Mname,
                            Lname = owe.Lname,
                            Bdate = owe.Bdate,
                            CelNo = owe.CelNo,
                            Email = owe.Email,
                            Deleted = "0",
                            URL = "~/Content/OwnerImages/user.png",
                            Form = finalpath

                        };

                        UResidence.OwnerController.Insert(ow);
                    }
                    else
                    {
                        Owner ow = new Owner()
                        {

                            BldgNo = owe.BldgNo,
                            UnitNo = owe.UnitNo,
                            Fname = owe.Fname,
                            Mname = owe.Mname,
                            Lname = owe.Lname,
                            Bdate = owe.Bdate,
                            CelNo = owe.CelNo,
                            Email = owe.Email,
                            Deleted = "0",
                            URL = "~/Content/OwnerImages/user.png",
                            Form = "~/Content/OwnerImages/noimage.jpeg"

                        };

                        UResidence.OwnerController.Insert(ow);
                    }

                  
                    string email=owe.Email;
                    Owner owee = UResidence.OwnerController.GetEmailOwner(email);
                    Residence r = new Residence()
                    {
                        OwnerNo = owee.Id,
                        TenantNo = 0
                    };
                     status = UResidence.ResidenceController.Insert(r);
                   
                    int ownerid = owee.Id;

                    UserLogin ull = new UserLogin
                    {
                        OwnerId=ownerid,
                        Username = owe.Email,
                        Hash = hash,
                        CreatedBy = "",
                        ModifyBy = "",
                        DateCreated = DateTime.Now,
                        Level = 8,
                        Locked = 0,
                        LastLogin = DateTime.Now
                    };



                    status = UResidence.UserController.InsertOwnerId(ull);
                    SendEmail(owe.Email,pass);
                    status = true;
                    ViewBag.AddMessage = status;
                    OwnerView();
                    return View("OwnerView");
                }
                else
                {
                    ViewBag.ErrorMessage = FixMessages(err);
                    status = false;

                }
            
                ViewBag.AddMessage = status;
            }
            else
            {
                Response.Write("<script type = 'text/javascript'>alert('Email is already exist');</script>");
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

        public ActionResult Download()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            List<Owner> data = default(List<Owner>);
            data = UResidence.OwnerController.GetAll();
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Views/Report/OwnerList.rdlc");
            ReportDataSource rd = new ReportDataSource();
            rd.Name = "OwnerList";
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
            Response.AddHeader("content-disposition", "attachment;filename=OwnerList." + filenameExtension);
            return File(renderbyte, filenameExtension);
        }
        public ActionResult OwnerView()
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
            List<Owner> ownerList = UResidence.OwnerController.GetAll();
            return View(ownerList);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string delete = "1";
            Owner ten = new Owner()
            {
                Id = id,
                Deleted=delete
            };
            status = UResidence.OwnerController.UpdateDelete(ten);
            if (status == true)
            {
                ViewBag.DeleteStatus = status;
                OwnerView();
            }
            else
            {
                ViewBag.DeleteStatus = status;
            }
            return View("OwnerView");
        }
        public ActionResult OwnerEdit()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult OwnerEdit(int id)
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
                Owner ownerList = UResidence.OwnerController.GetIdOwner(i);
                return View(ownerList);
            }
            return View("OwnerEdit");
        }
        [HttpPost]
        public ActionResult OwnerEdit(Owner owe)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string[] err = new string[] { };
            if (owe.Validate(out err))
            {
                status = UResidence.OwnerController.Update(owe);
                if (status == true)
                {
                    ViewBag.UpdateMessage = status;
                    OwnerView();
                    return View("OwnerView");
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
            return View(owe);
        }


        public JsonResult UpdateImage(Owner own)
        {
            var image = own.Image;
            bool status = false;
            int id = own.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/OwnerImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/OwnerImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/OwnerImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/OwnerImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }

                    Owner a = new Owner
                    {
                        Id = id,
                        URL = finalpath
                    };


                    status = UResidence.OwnerController.UpdateDP(a);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public JsonResult UpdateImageForm(Owner own)
        {
            var image = own.Image1;
            bool status = false;
            int id = own.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/OwnerImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/OwnerImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/OwnerImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/OwnerImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }

                    Owner a = new Owner
                    {
                        Id = id,
                        Form = finalpath
                    };


                    status = UResidence.OwnerController.UpdateForm(a);
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