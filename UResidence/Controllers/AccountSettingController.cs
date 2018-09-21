using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Security.Cryptography;
namespace UResidence.Controllers
{
    public class AccountSettingController : Controller
    {
        // GET: AccountSetting
        public ActionResult AdminAccountSetting()
        {
            string pass =(string) Session["pass"];
            
            string RType =(string) Session["TOR"];
            int Aid = (int)Session["UID"];
           
                List<Admin> a = new List<Admin>();
                List<UserLogin> ul = new List<UserLogin>();
                
                a=UResidence.AdminController.Get(Aid);
                ul=UResidence.UserController.AGetAll(Aid);
                List<object> model = new List<object>();
                model.Add(a.ToList());
                model.Add(ul.ToList());
            return View(model);
        }
        [HttpPost]
        public ActionResult AdminAccountSetting(FormCollection fc)
        {
            int lid = (int)Session["LID"];
            bool status = false;
            int Aid = (int)Session["UID"];
            string pass = (string)Session["pass"];
            string cp = fc["cp"];
            string Fname = fc["fname"];
            string Mname = fc["mname"];
            string Lname = fc["lname"];
            string np = fc["np"];
            string npass = Hash(np);
            string username= fc["username"];
            string tor =(string) Session["TOR"];
            int locked= 0;
            if (pass==cp)
            {
                Admin a = new Admin
                {
                    Id= Aid,
                    Fname =Fname,
                    Mname=Mname,
                    Lname=Lname,

                };
                status = UResidence.AdminController.AUpdate(a);
                if(status==true)
                {
                    UserLogin ul = new UserLogin
                    {
                        Id=lid,
                        Username=username,
                        Hash=npass,
                        ModifyBy= tor,
                        Locked=locked,
                    };
                    status=UResidence.UserController.Update(ul);
                    if(status==true)
                    {

                        return RedirectToAction("Home", "Admin");
                    }

                }              
            }
            else
            {
                Response.Write("<script>alert('Current Password is incorrect')</script>");
               
            }
            return AdminAccountSetting();


        }

            public ActionResult OwnerAccountSetting()
        {
            Session["aa"] = 1;
            string RType = (string)Session["TOR"];
            ViewBag.Type = RType;
            int Oid = (int)Session["UID"];
            string i = Oid.ToString();
            List<Owner> a = new List<Owner>();
            List<UserLogin> ul = new List<UserLogin>();

            a = UResidence.OwnerController.GetById(i);
            ul = UResidence.UserController.OGetAll(Oid);
            List<object> model = new List<object>();
            model.Add(a.ToList());
            model.Add(ul.ToList());
            return View(model);
        }
        [HttpPost]
        public ActionResult OwnerAccountSetting(FormCollection fc)
        {
            Session["aa"] = 1;
            int lid = (int)Session["LID"];
            bool status = false;
            int Aid = (int)Session["UID"];
            string pass = (string)Session["pass"];
            string cp = fc["cp"];
            string Fname = fc["fname"];
            string Mname = fc["mname"];
            string Lname = fc["lname"];
            string np = fc["np"];
            string npass = Hash(np);
            string username = fc["username"];
            string tor = (string)Session["TOR"];
            int locked = 0;
            if (pass == cp)
            {
                Owner a = new Owner
                {
                    Id = Aid,
                    Fname = Fname,
                    Mname = Mname,
                    Lname = Lname,

                };
                status = UResidence.OwnerController.OUpdate(a);
                if (status == true)
                {
                    UserLogin ul = new UserLogin
                    {
                        Id = lid,
                        Username = username,
                        Hash = npass,
                        ModifyBy = tor,
                        Locked = locked,
                    };
                    status = UResidence.UserController.Update(ul);
                    if (status == true)
                    {

                        return RedirectToAction("Home", "Reserve");
                    }

                }
            }
            else
            {
                Response.Write("<script>alert('Current Password is incorrect')</script>");
                
            }
            return OwnerAccountSetting();
        }
            public ActionResult TenantAccountSetting()
        {
            Session["aa"] = 1;
            string RType = (string)Session["TOR"];
            int Aid = (int)Session["UID"];

            List<Tenant> a = new List<Tenant>();
            List<UserLogin> ul = new List<UserLogin>();

            a = UResidence.TenantController.GetId(Aid);
            ul = UResidence.UserController.TGetAll(Aid);
            List<object> model = new List<object>();
            model.Add(a.ToList());
            model.Add(ul.ToList());
            return View(model);
        }
        [HttpPost]
        public ActionResult TenantAccountSetting(FormCollection fc)
        {
            Session["aa"] = 1;
            int lid = (int)Session["LID"];
            bool status = false;
            int Aid = (int)Session["UID"];
            string pass = (string)Session["pass"];
            string cp = fc["cp"];
            string Fname = fc["fname"];
            string Mname = fc["mname"];
            string Lname = fc["lname"];
            string np = fc["np"];
            string npass = Hash(np);
            string username = fc["username"];
            string tor = (string)Session["TOR"];
            int locked = 0;
            if (pass == cp)
            {
                Tenant a = new Tenant
                {
                    Id = Aid,
                    Fname = Fname,
                    Mname = Mname,
                    Lname = Lname,

                };
                status = UResidence.TenantController.TUpdate(a);
                if (status == true)
                {
                    UserLogin ul = new UserLogin
                    {
                        Id = lid,
                        Username = username,
                        Hash = npass,
                        ModifyBy = tor,
                        Locked = locked,
                    };
                    status = UResidence.UserController.Update(ul);
                    if (status == true)
                    {

                        return RedirectToAction("Home", "Reserve");
                    }

                }
            }
            else
            {
                Response.Write("<script>alert('Current Password is incorrect')</script>");

            }
            return TenantAccountSetting();
        }



        private string Hash(string p)
        {
            string hash = "";
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            hash = BitConverter.ToString(sh.ComputeHash(utf8.GetBytes(p.ToString())));
            return hash;
        }
    }
}