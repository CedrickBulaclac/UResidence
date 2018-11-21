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
            string StatusLogin = (string)Session["StatusLogin"];
            if (StatusLogin == "Logout")
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin c = new Admin();
                c = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = c.URL;
            }
            string pass =(string) Session["pass"];
            
            string RType =(string) Session["TOR"];
            int Aid = (int)Session["UID"];
            int lid = (int)Session["LID"];
            List<Admin> a = new List<Admin>();
                List<UserLogin> ul = new List<UserLogin>();
                
                a =UResidence.AdminController.Get(Aid);
                ul=UResidence.UserController.GetAll(lid);
                List<object> model = new List<object>();
                model.Add(a.ToList());
                model.Add(ul.ToList());
        
                return View(model);
            
        }
        public ActionResult ManagerAccountSetting()
        {
            string StatusLogin = (string)Session["StatusLogin"];
            if (StatusLogin == "Logout")
            {
                return Redirect("~/Login");
            }
            string pass = (string)Session["pass"];
            string RType = (string)Session["TOR"];
            int Aid = (int)Session["UID"];
            int lid = (int)Session["LID"];
            List<Admin> a = new List<Admin>();
            List<UserLogin> ul = new List<UserLogin>();
            a = UResidence.AdminController.Get(Aid);
            ul = UResidence.UserController.GetAll(lid);
            List<object> model = new List<object>();
            model.Add(a.ToList());
            model.Add(ul.ToList());
            return View(model);
        }
        [HttpPost]
        public ActionResult ManagerAccountSetting(FormCollection fc)
        {
            string StatusLogin = (string)Session["StatusLogin"];
            if (StatusLogin == "Logout")
            {
                return Redirect("~/Login");
            }
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
                Admin a = new Admin
                {
                    Id = Aid,
                    Fname = Fname,
                    Mname = Mname,
                    Lname = Lname,
                };
                status = UResidence.AdminController.AUpdate(a);
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
        [HttpPost]
        public ActionResult AdminAccountSetting(FormCollection fc)
        {
            string StatusLogin = (string)Session["StatusLogin"];
            if (StatusLogin == "Logout")
            {
                return Redirect("~/Login");
            }
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


            Admin user = UResidence.AdminController.GetbyID(lid);
            if (user.Email != username)
            {

                List<UserLogin> listUser = UResidence.UserController.GetAll(username);
                if (listUser.Count == 0)
                {
                    if (pass == cp)
                    {
                        Admin a = new Admin
                        {
                            Id = Aid,
                            Fname = Fname,
                            Mname = Mname,
                            Lname = Lname,
                            Email = username

                        };
                        status = UResidence.AdminController.AUpdate(a);
                        if (status == true)
                        {
                            if (np != "")
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
                            }
                            else
                            {
                                UserLogin ul = new UserLogin
                                {
                                    Id = lid,
                                    Username = username,
                                    ModifyBy = tor,
                                    Locked = locked,
                                };
                                status = UResidence.UserController.UpdateNoPass(ul);
                            }
                            if (status == true)
                            {

                                if (Convert.ToInt32(Session["Level"]) == 0)
                                {
                                    return RedirectToAction("AdminView", "Admin");
                                }
                                else if (Convert.ToInt32(Session["Level"]) == 2 || Convert.ToInt32(Session["Level"]) == 3)
                                {
                                    return RedirectToAction("CalendarView", "Calendar");
                                }
                                else if (Convert.ToInt32(Session["Level"]) == 4)
                                {
                                    return RedirectToAction("SelectOT", "ReservationA");
                                }
                                else if (Convert.ToInt32(Session["Level"]) == 5)
                                {
                                    return RedirectToAction("AdminView", "Admin");
                                }
                                else if (Convert.ToInt32(Session["Level"]) == 6 || Convert.ToInt32(Session["Level"]) == 7)
                                {
                                    return RedirectToAction("LogBookView", "LogBook");
                                }
                                else
                                {
                                    return RedirectToAction("Home", "Admin");
                                }
                                //return RedirectToAction("Home", "Admin");
                            }

                        }
                    }

                    else
                    {
                        Response.Write("<script>alert('Current Password is incorrect')</script>");

                    }
                }
                else
                {
                    Response.Write("<script type = 'text/javascript'>alert('The email address you have entered is already in used');</script>");
                }

            }
            else
            {
                if (pass == cp)
                {
                    Admin a = new Admin
                    {
                        Id = Aid,
                        Fname = Fname,
                        Mname = Mname,
                        Lname = Lname,
                        Email = username

                    };
                    status = UResidence.AdminController.AUpdate(a);
                    if (status == true)
                    {
                        if (np != "")
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
                        }
                        else
                        {
                            UserLogin ul = new UserLogin
                            {
                                Id = lid,
                                Username = username,
                                ModifyBy = tor,
                                Locked = locked,
                            };
                            status = UResidence.UserController.UpdateNoPass(ul);
                        }
                        if (status == true)
                        {

                            if (Convert.ToInt32(Session["Level"]) == 0)
                            {
                                return RedirectToAction("AdminView", "Admin");
                            }
                            else if (Convert.ToInt32(Session["Level"]) == 2 || Convert.ToInt32(Session["Level"]) == 3)
                            {
                                return RedirectToAction("CalendarView", "Calendar");
                            }
                            else if (Convert.ToInt32(Session["Level"]) == 4)
                            {
                                return RedirectToAction("SelectOT", "ReservationA");
                            }
                            else if (Convert.ToInt32(Session["Level"]) == 5)
                            {
                                return RedirectToAction("AdminView", "Admin");
                            }
                            else if (Convert.ToInt32(Session["Level"]) == 6 || Convert.ToInt32(Session["Level"]) == 7)
                            {
                                return RedirectToAction("LogBookView", "LogBook");
                            }
                            else
                            {
                                return RedirectToAction("Home", "Admin");
                            }
                            //return RedirectToAction("Home", "Admin");
                        }

                    }
                }

                else
                {
                    Response.Write("<script>alert('Current Password is incorrect')</script>");

                }
            }
            return AdminAccountSetting();


        }

            public ActionResult OwnerAccountSetting()
            {
            string StatusLogin = (string)Session["StatusLogin"];
            if (StatusLogin == "Logout")
            {
                return Redirect("~/Login");
            }
            Session["amenity"] = null;
            Session["calendar"] = null;
            Session["choose_date"] = null;
            Session["swimming"] = null;
            Session["choose_equipment"] = null;
            Session["summary"] = null;

            Session["drate"] = null;
            Session["NAME"] = null;
            Session["quantity"] = null;
            Session["ratee"] = null;
            Session["qa"] = null;
            Session["qc"] = null;
            Session["ar"] = null;
            Session["cr"] = null;
            Session["sd"] = null;
            Session["ed"] = null;
            string type = (Session["TOR"]).ToString();
            if (type == "Owner")
            {
                Owner b = new Owner();
                b = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                Session["URLL"] = b.URL;
            }

            else
            {
                Tenant t = new Tenant();
                t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                Session["URLL"] = t.URL;
            }
            Session["aa"] = 1;
            string RType = (string)Session["TOR"];
            ViewBag.Type = RType;
            int Oid = (int)Session["UID"];
            string i = Oid.ToString();
            int lid = (int)Session["LID"];
            List<Owner> a = new List<Owner>();
            List<UserLogin> ul = new List<UserLogin>();

            a = UResidence.OwnerController.GetById(i);
            ul = UResidence.UserController.GetAll(lid);
            List<object> model = new List<object>();
            model.Add(a.ToList());
            model.Add(ul.ToList());
            return View(model);
        }
        [HttpPost]
        public ActionResult OwnerAccountSetting(FormCollection fc)
        {
            string StatusLogin = (string)Session["StatusLogin"];
            if (StatusLogin == "Logout")
            {
                return Redirect("~/Login");
            }
            Session["aa"] = 1;
            //tbLogin
            int lid = (int)Session["LID"];
            bool status = false;
            //tbOwner
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


           
            Owner user = UResidence.OwnerController.GetIdOwner(Aid);
            if (user.Email != username)
            {
                List<UserLogin> listUser = UResidence.UserController.GetAll(username);
                if (listUser.Count == 0)
                {
                    if (pass == cp)
                    {
                        Owner a = new Owner
                        {
                            Id = Aid,
                            Fname = Fname,
                            Mname = Mname,
                            Lname = Lname,
                            Email = username

                        };
                        status = UResidence.OwnerController.OUpdate(a);
                        if (status == true)
                        {
                            if (np != "")
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
                            }
                            else
                            {
                                UserLogin ul = new UserLogin
                                {
                                    Id = lid,
                                    Username = username,
                                    ModifyBy = tor,
                                    Locked = locked,
                                };
                                status = UResidence.UserController.UpdateNoPass(ul);
                            }
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
                }
                else
                {
                    Response.Write("<script type = 'text/javascript'>alert('The email address you have entered is already in used');</script>");
                }
            }
            else
            {
                if (pass == cp)
                {
                    Owner a = new Owner
                    {
                        Id = Aid,
                        Fname = Fname,
                        Mname = Mname,
                        Lname = Lname,
                        Email = username

                    };
                    status = UResidence.OwnerController.OUpdate(a);
                    if (status == true)
                    {
                        if (np != "")
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
                        }
                        else
                        {
                            UserLogin ul = new UserLogin
                            {
                                Id = lid,
                                Username = username,
                                ModifyBy = tor,
                                Locked = locked,
                            };
                            status = UResidence.UserController.UpdateNoPass(ul);
                        }
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
            }



            return OwnerAccountSetting();
        }
            public ActionResult TenantAccountSetting()
        {
            string StatusLogin = (string)Session["StatusLogin"];
            if (StatusLogin == "Logout")
            {
                return Redirect("~/Login");
            }
            string type = (Session["TOR"]).ToString();
            if (type == "Owner")
            {
                Owner b = new Owner();
                b = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                Session["URLL"] = b.URL;
            }

            else
            {
                Tenant t = new Tenant();
                t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                Session["URLL"] = t.URL;
            }
            Session["aa"] = 1;
            string RType = (string)Session["TOR"];
            int Aid = (int)Session["UID"];
            int lid = (int)Session["LID"];
            List<Tenant> a = new List<Tenant>();
            List<UserLogin> ul = new List<UserLogin>();

            a = UResidence.TenantController.GetId(Aid);
            ul = UResidence.UserController.GetAll(lid);
            List<object> model = new List<object>();
            model.Add(a.ToList());
            model.Add(ul.ToList());
            return View(model);
        }
        [HttpPost]
        public ActionResult TenantAccountSetting(FormCollection fc)
        {
            string StatusLogin = (string)Session["StatusLogin"];
            if (StatusLogin == "Logout")
            {
                return Redirect("~/Login");
            }
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


            Tenant user = UResidence.TenantController.GetIdTenant(Aid.ToString());
            if (user.Email != username)
            {

                List<UserLogin> listUser = UResidence.UserController.GetAll(username);
                if (listUser.Count == 0)
                {


                    if (pass == cp)
                    {
                        Tenant a = new Tenant
                        {
                            Id = Aid,
                            Fname = Fname,
                            Mname = Mname,
                            Lname = Lname,
                            Email = username

                        };
                        status = UResidence.TenantController.TUpdate(a);
                        if (status == true)
                        {
                            if (np != "")
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
                            }
                            else
                            {
                                UserLogin ul = new UserLogin
                                {
                                    Id = lid,
                                    Username = username,
                                    ModifyBy = tor,
                                    Locked = locked,
                                };
                                status = UResidence.UserController.UpdateNoPass(ul);
                            }
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
                }
                else
                {
                    Response.Write("<script type = 'text/javascript'>alert('The email address you have entered is already in used');</script>");
                }
            }
            else
            {
                if (pass == cp)
                {
                    Tenant a = new Tenant
                    {
                        Id = Aid,
                        Fname = Fname,
                        Mname = Mname,
                        Lname = Lname,
                        Email = username

                    };
                    status = UResidence.TenantController.TUpdate(a);
                    if (status == true)
                    {
                        if (np != "")
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
                        }
                        else
                        {
                            UserLogin ul = new UserLogin
                            {
                                Id = lid,
                                Username = username,
                                ModifyBy = tor,
                                Locked = locked,
                            };
                            status = UResidence.UserController.UpdateNoPass(ul);
                        }
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