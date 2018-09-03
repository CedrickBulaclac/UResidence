﻿using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UResidence;
using System.Security.Cryptography;
using System.Text;
using System.IO;

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
            if (listUser.Count == 0)
            {
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
                List<Owner> own = new List<Owner>(); ;
                own = UResidence.OwnerController.GetOwnerReserve(ten.BldgNo, ten.UnitNo);

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
                        URL = "~/Content/WebImages/user.png"

                    };
                    List<Tenant> listTen = default(List<Tenant>);
                    listTen = UResidence.TenantController.Check(tenn);
                    if (listTen.Count == 0)
                    {
                        string[] err = new string[] { };
                        if (ten.Validate(out err))
                        {
                            status = UResidence.TenantController.Insert(tenn);
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
                                Level = 5,
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
                }
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


        public JsonResult UpdateImage(Tenant tenant)
        {
            var image = tenant.Image;
            bool status = false;
            int id = tenant.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), imagefileName);
                    string folderpath1 = "~/Content/AmenityImages/" + imagefileName;
                    if (System.IO.File.Exists(folderPath))
                    {
                        System.IO.File.Delete(folderPath);
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        image.SaveAs(folderPath);
                    }
                    Tenant t = new Tenant
                    {
                        Id = id,
                        URL = folderpath1
                    };


                    status = UResidence.TenantController.UpdateDP(t);
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