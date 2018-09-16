using System;
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


namespace UResidence.Controllers
{

    public class OwnerController : Controller
    {
        bool status;

        // GET: Owner
        public ActionResult OwnerAdd()
        {
            return View();
        }
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


        [HttpPost]
        public ActionResult OwnerAdd(Owner owe)
        {
           
            string hash;
            string pass = owe.Bdate.ToShortDateString();
            hash = Hash(pass);
            List<UserLogin> listUser = UResidence.UserController.GetAll(owe.Email);
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
                URL = "~/Content/WebImages/user.png"

            };

            if (listUser.Count == 0)
            {
                string[] err = new string[] { };
                if (owe.Validate(out err))
                {
            
                    UResidence.OwnerController.Insert(ow);

                    Owner b = new Owner();
                    b = UResidence.OwnerController.GetEmailOwner(owe.Email.ToString());
                    int ownerid = b.Id;

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



                    UResidence.UserController.InsertOwnerId(ull);
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


        public ActionResult OwnerView()
        {
            List<Owner> ownerList = UResidence.OwnerController.GetAll();
            return View(ownerList);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
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
            return View();
        }
        [HttpGet]
        public ActionResult OwnerEdit(int id)
        {
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

        public string FixMessages(string[] err)
        {
            string errors = "Please check the following: <br/>";
            foreach (string er in err) errors += (er + "<br/>");
            return errors;
        }
    }
}