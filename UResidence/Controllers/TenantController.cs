using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UResidence;
using System.Security.Cryptography;
using System.Text;

namespace UResidence.Controllers
{
    public class TenantController : Controller
    {
        bool status;
        // GET: Tenant
        public ActionResult TenantAdd()
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
        public ActionResult TenantAdd(Tenant ten)
        {
            string hash;
            string pass = ten.Bdate.ToShortDateString();
            hash = Hash(pass);
            List<UserLogin> listUser = UResidence.UserController.GetAll(ten.Email);
            UserLogin ul = new UserLogin
            {
                Username = ten.Email,
                Hash = hash,
                CreatedBy = "",
                ModifyBy = "",
                DateCreated = DateTime.Now,
                Level = 2,
                Locked = 1,
                LastLogin = DateTime.Now
            };



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
                Deleted = "0"

            };



            string[] err = new string[] { };
            if (ten.Validate(out err))
            {

             

                status = UResidence.TenantController.Insert(tenn);


                Tenant b = new Tenant();
                b = UResidence.TenantController.GetIdTenant(ten.Email.ToString());
                int tenandID = b.Id;

                UserLogin ull = new UserLogin
                {
                    TenantId = tenandID,
                    Username = ten.Email,
                    Hash = hash,
                    CreatedBy = "",
                    ModifyBy = "",
                    DateCreated = DateTime.Now,
                    Level = 2,
                    Locked = 1,
                    LastLogin = DateTime.Now
                    
                };

                UResidence.UserController.InsertTenantId(ull);


          
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

            ViewBag.AddMessage = status;
            return View();
        }
        public ActionResult TenantView()
        {
            List<Tenant> tenantList = UResidence.TenantController.GetAll();
            return View(tenantList);      
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
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
            return View();
        }
        [HttpGet]
        public ActionResult TenantEdit(int id)
        {
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

        public string FixMessages(string[] err)
        {
            string errors = "Please check the following: <br />";
            foreach (string er in err) errors += (er + "<br />");
            return errors;
        }
    }
}