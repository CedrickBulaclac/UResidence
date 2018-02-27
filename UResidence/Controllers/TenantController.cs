using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UResidence;
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
        [HttpPost]
        public ActionResult TenantAdd(Tenant ten)
        {
            string[] err = new string[] { };
            if (ten.Validate(out err))
            {
                status = UResidence.TenantController.Insert(ten);
                
                   
                    if(status==true)
                {
                    ViewBag.Message = status;
                    TenantView();
                    return View("TenantView");
                }
                    else
                {
                    ViewBag.Message = status;
                    return View(ten);
                }
               
            }
            else
            {
                ViewBag.Message = false;
                ViewBag.ErrorMessages = FixMessages(err);
                return View(ten);
            }
           
        }
        public ActionResult TenantView()
        {
            List<Tenant> tenantList = UResidence.TenantController.GetAll();
            return View(tenantList);      
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Tenant ten = new Tenant()
            {
                Id = id
            };
            status=UResidence.TenantController.Delete(ten);
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
            if(ModelState.IsValid)
            {
                Tenant tenantList = UResidence.TenantController.GetIdTenant(id);
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