using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UResidence;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Reporting.WebForms;

namespace UResidence.Controllers
{
    public class TenantController : Controller
    {
        bool status;
        // GET: Tenant
        public ActionResult GetTenant()
        {
            List<Tenant> ret = new List<Tenant>();
            ret = UResidence.TenantController.GetAll();
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TenantAdd()
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

        public ActionResult Download()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            List<Tenant> data = default(List<Tenant>);
            data = UResidence.TenantController.GetAll();
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Views/Report/TenantList.rdlc");
            ReportDataSource rd = new ReportDataSource();
            rd.Name = "TenantList";
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
            Response.AddHeader("content-disposition", "attachment;filename=TenantList." + filenameExtension);
            return File(renderbyte, filenameExtension);
        }





        public JsonResult InsertMoving1(Tenant tenant)
        {
            int id = tenant.Id;
            var image1 = tenant.Image1;
            bool status = false;
            if (image1 != null)
            {
                if (image1.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image1.FileName);
                    var extension = Path.GetExtension(image1.FileName);
                    string imagefileName = Path.GetFileName(image1.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/TenantImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/TenantImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/TenantImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image1.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/TenantImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image1.SaveAs(folderPath);
                    }

                    Tenant a = new Tenant()
                    {
                        Id = id,
                        MovingIn = finalpath,
                    };
                    status = UResidence.TenantController.UpdateImage1(a);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }





        public JsonResult InsertMoving2(Tenant tenant)
        {
            int id = tenant.Id;
            var image2 = tenant.Image2;
            bool status = false;
            if (image2 != null)
            {
                if (image2.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image2.FileName);
                    var extension = Path.GetExtension(image2.FileName);
                    string imagefileName = Path.GetFileName(image2.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/TenantImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/TenantImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/TenantImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image2.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/TenantImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image2.SaveAs(folderPath);
                    }

                    Tenant a = new Tenant()
                    {
                        Id = id,
                        MovingOut = finalpath,
                    };
                    status = UResidence.TenantController.UpdateImage2(a);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

       
        [HttpPost]
        public ActionResult TenantAdd(Tenant ten, HttpPostedFileBase Image1)
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
                    string folderPath = Path.Combine(Server.MapPath("~/Content/TenantImages"), imagefileName);

                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/TenantImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/TenantImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/TenantImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);


                    }
                }
            }


            string hash;
            string pass = ten.Bdate.ToShortDateString();
            hash = Hash(pass);
            List<UserLogin> listUser = UResidence.UserController.GetAllT(ten.Email);
            if (listUser.Count == 0)
            {
                UserLogin ul = new UserLogin
                {
                    Username = ten.Email,
                    Hash = hash,
                    CreatedBy = "",
                    ModifyBy = "",
                    DateCreated = DateTime.Now,
                    Level = 9,
                    Locked = 1,
                    LastLogin = DateTime.Now
                };
                List<Owner> own = new List<Owner>(); ;
                own = UResidence.OwnerController.GetOwnerReserve(ten.BldgNo, ten.UnitNo);
                //string MoveIn = Session["MoveIn"].ToString();
                //string MoveOut = Session["MoveOut"].ToString();
                 if (own.Count != 0)
                 {
                  
                        Tenant tenn = new Tenant()
                        {

                            BldgNo = ten.BldgNo,
                            UnitNo = ten.UnitNo,
                            Fname = ten.Fname,
                            Mname = ten.Mname,
                            Lname = ten.Lname,
                            Bdate = ten.Bdate,
                            CelNo = ten.CelNo,
                            Email = ten.Email,
                            LeaseStart = ten.LeaseStart,
                            LeaseEnd = ten.LeaseEnd,
                            Deleted = "0",
                            URL = "~/Content/TenantImages/user.png",
                            MovingIn = "~/Content/TenantImages/Noimageavailable.jpeg",
                            MovingOut = "~/Content/TenantImages/Noimageavailable.jpeg"
                        };

                    
                        List<Tenant> listTen = default(List<Tenant>);
                                    listTen = UResidence.TenantController.Check(tenn);
                                    if (listTen.Count == 0)
                                    {
                                        string[] err = new string[] { };
                                        if (ten.Validate(out err))
                                        {
                            if (image != null)
                            {
                                Tenant tennn = new Tenant()
                                {

                                    BldgNo = ten.BldgNo,
                                    UnitNo = ten.UnitNo,
                                    Fname = ten.Fname,
                                    Mname = ten.Mname,
                                    Lname = ten.Lname,
                                    Bdate = ten.Bdate,
                                    CelNo = ten.CelNo,
                                    Email = ten.Email,
                                    LeaseStart = ten.LeaseStart,
                                    LeaseEnd = ten.LeaseEnd,
                                    Deleted = "0",
                                    URL = "~/Content/TenantImages/user.png",
                                    MovingIn = finalpath,
                                    MovingOut = "~/Content/TenantImages/Noimageavailable.jpeg"
                                };
                                status = UResidence.TenantController.Insert(tennn);
                            }
                            else
                            {
                                Tenant tennn = new Tenant()
                                {

                                    BldgNo = ten.BldgNo,
                                    UnitNo = ten.UnitNo,
                                    Fname = ten.Fname,
                                    Mname = ten.Mname,
                                    Lname = ten.Lname,
                                    Bdate = ten.Bdate,
                                    CelNo = ten.CelNo,
                                    Email = ten.Email,
                                    LeaseStart = ten.LeaseStart,
                                    LeaseEnd = ten.LeaseEnd,
                                    Deleted = "0",
                                    URL = "~/Content/TenantImages/user.png",
                                    MovingIn = "~/Content/TenantImages/Noimageavailable.jpeg",
                                    MovingOut = "~/Content/TenantImages/Noimageavailable.jpeg"
                                };
                                status = UResidence.TenantController.Insert(tennn);
                            }



                                           
                                            Tenant b = new Tenant();
                                            b = UResidence.TenantController.GetEmailTenant(ten.Email);
                                            int tenandID = b.Id;

                                            UserLogin ull = new UserLogin
                                            {
                                                TenantId = tenandID,
                                                Username = ten.Email,
                                                Hash = hash,
                                                CreatedBy = "",
                                                ModifyBy = "",
                                                DateCreated = DateTime.Now,
                                                Level = 9,
                                                Locked = 0,
                                                LastLogin = DateTime.Now

                                            };

                                            UResidence.UserController.InsertTenantId(ull);

                                            Residence red = new Residence
                                            {
                                                OwnerNo = own[0].Id,
                                                TenantNo = tenandID
                                            };

                                            ResidenceController.Insert(red);
                                            SendEmail(ten.Email, pass);
                                            status = true;
                                            ViewBag.AddMessage = status;
                                            TenantView();
                                            return View("TenantView");
                                        }
                                        else
                                        {
                                            ViewBag.ErrorMessage = FixMessages(err);
                                            status = false;

                                        }
                                    }
                                    else
                                    {
                                        string script = "<script type = 'text/javascript'>alert('There is an Existing Tenant!! Please try Again.Please try Again.');</script>";
                                        Response.Write(script);
                                        status = false;
                                    }
                                }////////////
                                else
                                {
                                    string script = "<script type = 'text/javascript'>alert('Wrong Building No or Unit No!! Please try Again.');</script>";
                                    Response.Write(script);
                                    status = false;
                                }
                            }
                            else
                            {
                                string script = "<script type = 'text/javascript'>alert('Email is already taken');</script>";
                                Response.Write(script);
                                status = false;
                            }
                            ViewBag.AddMessage = status;
                            return View();
                           }
                   
            
           
        
        public ActionResult TenantView()
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
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string delete = "1";
            Tenant ten = new Tenant()
            {
                Id = id,
                Deleted=delete
                
            };
            status=UResidence.TenantController.UpdateDelete(ten);
            if(status==true)
            {
                ViewBag.DeleteMessage = status;
                TenantView();
            }
           else
            {
                ViewBag.DeleteMessage = status;
            }
            return View("TenantView");
        }
        public ActionResult TenantEdit()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult TenantEdit(int id)
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
            if(ModelState.IsValid)
            {
                Tenant tenantList = UResidence.TenantController.GetId(i);
                return View(tenantList);
            }
            return View("TenantEdit");
        }
        [HttpPost]
        public ActionResult TenantEdit(Tenant ten)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string[] err = new string[] { };
            if (ten.Validate(out err))
            {
                status = UResidence.TenantController.Update(ten);
                if (status == true)
                {
                    ViewBag.UpdateMessage = status;
                    TenantView();
                    return View("TenantView");
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
           
            return View(ten);
        }


        public JsonResult UpdateImage(Tenant ten)
        {
            var image = ten.Image;
            bool status = false;
            int id = ten.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/TenantImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/TenantImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/TenantImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/TenantImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }

                    Tenant a = new Tenant
                    {
                        Id = id,
                        URL = finalpath
                    };


                    status = UResidence.TenantController.UpdateDP(a);
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
            string errors = "Please check the following: <br />";
            foreach (string er in err) errors += (er + "<br />");
            return errors;
        }
    }
}