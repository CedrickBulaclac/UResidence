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
    public class OwnerController : Controller
    {
        bool status;
         

        // GET: Owner
        public ActionResult OwnerAdd()
        {
            return View();
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
                CreatedBy="",
                ModifyBy="",
                DateCreated = DateTime.Now,
                Level=1,
                Locked=1,
                LastLogin=DateTime.Now                
            };

            Owner ow = new Owner() {
                
            BldgNo=owe.BldgNo,
            UnitNo= owe.UnitNo,
            Fname= owe.Fname,
            Mname=owe.Mname,
            Lname=owe.Lname,
            Bdate=owe.Bdate,
            CelNo=owe.CelNo,
            Email=owe.Email,
            Deleted="0"

            };

            if (listUser.Count == 0)
            {
                string[] err = new string[] { };
                if (owe.Validate(out err))
                {
                    UResidence.UserController.Insert(ul);
                    UResidence.OwnerController.Insert(ow);
                    status = true;
                    return RedirectToAction("OwnerView");
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
            Owner ten = new Owner()
            {
                Id = id
            };
            status = UResidence.OwnerController.Delete(ten);
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
        public string FixMessages(string[] err)
        {
            string errors = "Please check the following: <br/>";
            foreach (string er in err) errors += (er + "<br/>");
            return errors;
        }
    }
}