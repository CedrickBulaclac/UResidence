using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace UResidence.Controllers
{
    public class AdminController : Controller
    {
        bool status;
        // GET: Admin
        public ActionResult Registration()
        {
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
                        Deleted = "0"
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
                        Locked = 1,
                        LastLogin = DateTime.Now
                    };


                    UResidence.UserController.InsertAdminId(ull);

                    status = true;
                    ViewBag.AddMessage = status;
                    AdminView();
                    return View("AdminView");
                }
                else
                {
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
                Deleted=delete
            };
            status=UResidence.AdminController.UpdateDelete(am);
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
    }
}