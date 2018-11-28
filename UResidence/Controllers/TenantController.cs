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
            catch(Exception)
            {
                string script = "<script type = 'text/javascript'>alert('The email account that you tried to reach does not exist');</script>";
                Response.Write(script);
                return false;
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
        public ActionResult TenantAdd(TenantOwner ten, HttpPostedFileBase Image1)
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
            string pass = ten.tenant.Bdate.ToShortDateString();
            hash = Hash(pass);
            List<UserLogin> listUser = UResidence.UserController.GetAll(ten.tenant.Email);
            if (listUser.Count == 0)
            {
                UserLogin ul = new UserLogin
                {
                    Username = ten.tenant.Email,
                    Hash = hash,
                    CreatedBy = "",
                    ModifyBy = "",
                    DateCreated = DateTime.Now,
                    Level = 9,
                    Locked = 1,
                    LastLogin = DateTime.Now
                };
                List<Owner> own = new List<Owner>(); ;
                own = UResidence.OwnerController.GetOwnerReserve(ten.tenant.BldgNo, ten.tenant.UnitNo);             
                 if (own.Count != 0)
                 {
                  
                        Tenant tenn = new Tenant()
                        {

                            BldgNo = ten.tenant.BldgNo,
                            UnitNo = ten.tenant.UnitNo,
                            Fname = ten.tenant.Fname,
                            Mname = ten.tenant.Mname,
                            Lname = ten.tenant.Lname,
                            Bdate = ten.tenant.Bdate,
                            CelNo = ten.tenant.CelNo,
                            Email = ten.tenant.Email,
                            LeaseStart = ten.tenant.LeaseStart,
                            LeaseEnd = ten.tenant.LeaseEnd,
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
                             if (ten.tenant.Validate(out err))
                              {
                            status=SendEmail(ten.tenant.Email, pass);
                            if (status == true)
                            {
                                if (image != null)
                                {
                                    Tenant tennn = new Tenant()
                                    {

                                        BldgNo = ten.tenant.BldgNo,
                                        UnitNo = ten.tenant.UnitNo,
                                        Fname = ten.tenant.Fname,
                                        Mname = ten.tenant.Mname,
                                        Lname = ten.tenant.Lname,
                                        Bdate = ten.tenant.Bdate,
                                        CelNo = ten.tenant.CelNo,
                                        Email = ten.tenant.Email,
                                        LeaseStart = ten.tenant.LeaseStart,
                                        LeaseEnd = ten.tenant.LeaseEnd,
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

                                        BldgNo = ten.tenant.BldgNo,
                                        UnitNo = ten.tenant.UnitNo,
                                        Fname = ten.tenant.Fname,
                                        Mname = ten.tenant.Mname,
                                        Lname = ten.tenant.Lname,
                                        Bdate = ten.tenant.Bdate,
                                        CelNo = ten.tenant.CelNo,
                                        Email = ten.tenant.Email,
                                        LeaseStart = ten.tenant.LeaseStart,
                                        LeaseEnd = ten.tenant.LeaseEnd,
                                        Deleted = "0",
                                        URL = "~/Content/TenantImages/user.png",
                                        MovingIn = "~/Content/TenantImages/Noimageavailable.jpeg",
                                        MovingOut = "~/Content/TenantImages/Noimageavailable.jpeg"
                                    };
                                    status = UResidence.TenantController.Insert(tennn);
                                }
                                Tenant b = new Tenant();
                                b = UResidence.TenantController.GetEmailTenant(ten.tenant.Email);
                                int tenandID = b.Id;

                                UserLogin ull = new UserLogin
                                {
                                    Username = ten.tenant.Email,
                                    Hash = hash,
                                    CreatedBy = "",
                                    ModifyBy = "",
                                    DateCreated = DateTime.Now,
                                    Level = 9,
                                    Locked = 0,
                                    LastLogin = DateTime.Now

                                };

                                status = UResidence.UserController.Insert(ull);
                                if (status == true)
                                {
                                    List<UserLogin> ul2 = new List<UserLogin>();
                                    ul2 = UserController.GetAll(ten.tenant.Email);
                                    if (ul2.Count > 0)
                                    {
                                        status = UResidence.TenantController.Update(ul2[0].Id, ten.tenant.Email);
                                    }

                                }
                                Residence red = new Residence
                                {
                                    OwnerNo = own[0].Id,
                                    TenantNo = tenandID
                                };

                                status = ResidenceController.Insert(red);
                            }
                            else
                            {
                                string script = "<script type = 'text/javascript'>alert('The email account that you tried to reach does not exist');</script>";
                                Response.Write(script);
                                ViewBag.Alert = true;
                                return RedirectToAction("TenantView", "Tenant", new { id = "" });
                            }
                                                       
                             }
                          else
                                        {
                                            ViewBag.ErrorMessage = FixMessages(err);
                                            status = false;
                            ViewBag.Alert = true;
                            return RedirectToAction("TenantView", "Tenant", new { id = "" });
                        }
                          }
                         else
                         {
                          string script = "<script type = 'text/javascript'>alert('The unit number is still occupied. Kindly check your specified details.');</script>";
                          Response.Write(script);
                          status = false;
                          ViewBag.Alert = true;
                        return RedirectToAction("TenantView", "Tenant", new { id = "" });
                    }
                 }
                 else
                 {
                 string script = "<script type = 'text/javascript'>alert('Invalid building number and unit number. Kindly check your specified details.');</script>";
                 Response.Write(script);
                 status = false;
                 ViewBag.Alert = true;
                    return RedirectToAction("TenantView", "Tenant", new { id = "" });
                }
            }
            else
            {
            string script = "<script type = 'text/javascript'>alert('The email address you have entered is already in used');</script>";
            Response.Write(script);
            status = false;
                ViewBag.Alert = true;
                return RedirectToAction("TenantView", "Tenant", new { id = "" });
            }
            Session["AddMessage"] = status;
            return RedirectToAction("TenantView", "Tenant");
        }
        [HttpGet]
        public ActionResult TenantView(int? id)
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
            if (Session["UpdateMess"] != null)
            {
                ViewBag.UpdateMessage = Session["UpdateMess"];
                Session["UpdateMess"] = null;
            }
            if (Session["AddMessage"] != null)
            {
                ViewBag.AddMessage = Session["AddMessage"];
                Session["AddMessage"] = null;
            }
            if (Session["DeleteStatus"] != null)
            {
                ViewBag.DeleteMessage = Session["DeleteStatus"];
                Session["DeleteStatus"] = null;
            }
            if (id == null)
            {
                List<Tenant> ten = new List<Tenant>();
                List<object> building = new List<object>();
                List<object> unit = new List<object>();
                ten = UResidence.TenantController.GetAll();
                if (ten.Count > 0)
                {
                    for (int i = 0; i <= ten.Count - 1; i++)
                    {
                        if (ten[i].LeaseEnd <= DateTime.Now)
                        {
                            Tenant tenmodel = new Tenant
                            {
                                Deleted = "1",
                                Id = ten[i].Id
                            };
                            UResidence.TenantController.UpdateDelete(tenmodel);
                        }
                    }
                }
                var ownerr = UResidence.OwnerController.GetAll();
                if (ownerr.Count > 0)
                {
                    for (int i = 0; i <= ownerr.Count - 1; i++)
                    {
                        if (building.Contains(ownerr[i].BldgNo))
                        {

                        }
                        else
                        {
                            building.Add(ownerr[i].BldgNo);
                        }
                        if (unit.Contains(ownerr[i].UnitNo))
                        {

                        }
                        else
                        {
                            unit.Add(ownerr[i].UnitNo);
                        }
                    }
                }
                var to = new TenantOwner();
                to.UnitNoList = unit;
                to.BuildingList = building;
                to.tenant = new Tenant();
                return View(to);
            }
            else
            {
                string i = id.ToString();
                int ii = 0;
                Tenant tenantList = UResidence.TenantController.GetId(i);
                var ownerr = UResidence.OwnerController.GetAll();
                var to = new TenantOwner();
                List<object> building = new List<object>();
                List<object> unit = new List<object>();
                ownerr = UResidence.OwnerController.GetAll();
                if (ownerr.Count > 0)
                {
                    for (ii = 0; ii <= ownerr.Count - 1; ii++)
                    {
                        if (building.Contains(ownerr[ii].BldgNo))
                        {

                        }
                        else
                        {
                            building.Add(ownerr[ii].BldgNo);
                        }
                        if (unit.Contains(ownerr[ii].UnitNo))
                        {

                        }
                        else
                        {
                            unit.Add(ownerr[ii].UnitNo);
                        }
                    }
                }
                to.UnitNoList = unit;
                to.BuildingList = building;
                to.tenant = tenantList;
                ViewBag.ModalView = 1;
                return View(to);
            }
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
            Tenant o = new Tenant();
            o = UResidence.TenantController.GetId(id.ToString());
            status = UResidence.UserController.UpdateLockout(o.LoginId);
            if (status == true)
            {             
                status = UResidence.TenantController.UpdateDelete(ten);                           
            }
            Session["DeleteStatus"] = status;
            return RedirectToAction("TenantView", "Tenant");
        }
        public ActionResult TenantEdit()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            List<Tenant> ten = new List<Tenant>();
            ten = UResidence.TenantController.GetAll();
            if (ten.Count > 0)
            {
                for (int i = 0; i <= ten.Count - 1; i++)
                {
                    if (ten[i].LeaseEnd <= DateTime.Now)
                    {
                        Tenant tenmodel = new Tenant
                        {
                            Deleted = "1",
                            Id = ten[i].Id
                        };
                        UResidence.TenantController.UpdateDelete(tenmodel);
                    }
                }
            }
            List<Owner> ownerr = new List<Owner>();
            List<object> building = new List<object>();
            List<object> unit = new List<object>();
            var to = new TenantOwner();
            ownerr = UResidence.OwnerController.GetAll();
            if (ownerr.Count > 0)
            {
                for (int i = 0; i <= ownerr.Count - 1; i++)
                {
                    if (building.Contains(ownerr[i].BldgNo))
                    {

                    }
                    else
                    {
                        building.Add(ownerr[i].BldgNo);
                    }
                    if (unit.Contains(ownerr[i].UnitNo))
                    {

                    }
                    else
                    {
                        unit.Add(ownerr[i].UnitNo);
                    }
                }
            }
            to.UnitNoList = unit;
            to.BuildingList = building;

            return View(to);
        }
        [HttpGet]
        public ActionResult TenantEdit(int? id)
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
            int ii = 0;
            if (ModelState.IsValid)
            {
               
                Tenant tenantList = UResidence.TenantController.GetId(i);
                var ownerr = UResidence.OwnerController.GetAll();
                var to = new TenantOwner();             
                List<object> building = new List<object>();
                List<object> unit = new List<object>();                
                ownerr = UResidence.OwnerController.GetAll();
                if (ownerr.Count > 0)
                {
                    for (ii = 0; ii <= ownerr.Count - 1; ii++)
                    {
                        if (building.Contains(ownerr[ii].BldgNo))
                        {

                        }
                        else
                        {
                            building.Add(ownerr[ii].BldgNo);
                        }
                        if (unit.Contains(ownerr[ii].UnitNo))
                        {

                        }
                        else
                        {
                            unit.Add(ownerr[ii].UnitNo);
                        }
                    }
                }
                to.UnitNoList = unit;
                to.BuildingList = building;
                to.tenant = tenantList;
                return View(to);
               
            }
            return View("TenantEdit");
        }
        [HttpPost]
        public ActionResult TenantView(TenantOwner ten1)
        {
            Tenant ten = ten1.tenant;
            List<Owner> ownerr = new List<Owner>();
            List<object> building = new List<object>();
            List<object> unit = new List<object>();
            TenantOwner to = new TenantOwner();
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string[] err = new string[] { };
            if (ten.Validate(out err))
            {
                Tenant ten2 = new Tenant();
                ten2 = UResidence.TenantController.GetIdTenant(ten.Id.ToString());
                if (ten.Email != ten2.Email)
                {
                    List<UserLogin> listUser = UResidence.UserController.GetAll(ten.Email);
                    if (listUser.Count == 0)
                    {
                        List<Owner> own = new List<Owner>(); ;
                        own = UResidence.OwnerController.GetOwnerReserve(ten.BldgNo, ten.UnitNo);
                        if (own.Count != 0)
                        {
                            status = UResidence.TenantController.Update(ten);
                            if (status == true)
                            {
                                status = UResidence.UserController.UpdateEmail(ten.Email, ten.LoginId);
                                Session["UpdateMess"] = status;
                                return RedirectToAction("TenantView", "Tenant", new { id=""});
                            }
                            else
                            {

                                ViewBag.UpdateMessage = status;                              
                                ownerr = UResidence.OwnerController.GetAll();
                                if (ownerr.Count > 0)
                                {
                                    for (int i = 0; i <= ownerr.Count - 1; i++)
                                    {
                                        if (building.Contains(ownerr[i].BldgNo))
                                        {

                                        }
                                        else
                                        {
                                            building.Add(ownerr[i].BldgNo);
                                        }
                                        if (unit.Contains(ownerr[i].UnitNo))
                                        {

                                        }
                                        else
                                        {
                                            unit.Add(ownerr[i].UnitNo);
                                        }
                                    }
                                }                               
                                to.UnitNoList = unit;
                                to.BuildingList = building;                             
                                to.tenant = ten;
                                return View(to);                             
                            }
                        }
                        else
                        {
                            string script = "<script type = 'text/javascript'>alert('Invalid building number and unit number. Kindly check your specified details.');</script>";
                            Response.Write(script);
                        }
                    }
                    else
                    {
                        string script = "<script type = 'text/javascript'>alert('The email address you have entered is already in used');</script>";
                        Response.Write(script);
                    }
                }
                else
                {
                    List<Owner> own = new List<Owner>(); ;
                    own = UResidence.OwnerController.GetOwnerReserve(ten.BldgNo, ten.UnitNo);
                    if (own.Count != 0)
                    {
                        status = UResidence.TenantController.Update(ten);
                        if (status == true)
                        {
                            status = UResidence.UserController.UpdateEmail(ten.Email, ten.LoginId);
                            Session["UpdateMess"] = status;
                            return RedirectToAction("TenantView", "Tenant", new { id = "" });
                        }
                        else
                        {
                            ViewBag.UpdateMessage = status;
                            ownerr = UResidence.OwnerController.GetAll();
                            if (ownerr.Count > 0)
                            {
                                for (int i = 0; i <= ownerr.Count - 1; i++)
                                {
                                    if (building.Contains(ownerr[i].BldgNo))
                                    {

                                    }
                                    else
                                    {
                                        building.Add(ownerr[i].BldgNo);
                                    }
                                    if (unit.Contains(ownerr[i].UnitNo))
                                    {

                                    }
                                    else
                                    {
                                        unit.Add(ownerr[i].UnitNo);
                                    }
                                }
                            }
                            to.UnitNoList = unit;
                            to.BuildingList = building;
                            to.tenant = ten;
                            return View(to);
                        }
                    }
                    else
                    {
                        string script = "<script type = 'text/javascript'>alert('Invalid building number and unit number. Kindly check your specified details.');</script>";
                        Response.Write(script);
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessages = FixMessages(err);
            }
            ownerr = UResidence.OwnerController.GetAll();
            if (ownerr.Count > 0)
            {
                for (int i = 0; i <= ownerr.Count - 1; i++)
                {
                    if (building.Contains(ownerr[i].BldgNo))
                    {

                    }
                    else
                    {
                        building.Add(ownerr[i].BldgNo);
                    }
                    if (unit.Contains(ownerr[i].UnitNo))
                    {

                    }
                    else
                    {
                        unit.Add(ownerr[i].UnitNo);
                    }
                }
            }
            to.UnitNoList = unit;
            to.BuildingList = building;
            to.tenant = ten;
            return View(to);
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