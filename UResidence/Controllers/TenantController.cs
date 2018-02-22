using System;
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
        public ActionResult TenantAdd(FormCollection fc)
        {
            DateTime start = Convert.ToDateTime(fc["LeaseStart"]);
            DateTime end = Convert.ToDateTime(fc["LeaseEnd"]);
            string gender = fc["Gender"].ToString();
            Tenant ten = new Tenant()
            {
                UnitNo = fc["UnitNo"],
                BldgNo=fc["BldgNo"],
               TenantNo=fc["TenantNo"],
               Fname=fc["Fname"],
               Mname=fc["Mname"],
                Lname = fc["Lname"],
                Gender =gender,
                Age = Convert.ToInt32(fc["Age"]),
                TelNo = fc["TelNo"],
                CelNo = fc["CelNo"],
                Email = fc["Email"],
                Citizenship = fc["Citizenship"],
                Status = fc["Status"],
                LeaseStart =start ,
                LeaseEnd = end
            };
            status = UResidence.TenantController.Insert(ten);
            if(status==true)
            {
                ViewBag.AddMessage = status;
            }
           else
            {
                ViewBag.AddMessage = status;
            }
            return View();
        }
        public ActionResult TenantView()
        {
            List<Tenant> tenantList = default(List<Tenant>);
            tenantList=UResidence.TenantController.GetAll();
            ViewBag.tenant = tenantList;
            return View();
         
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
                List<Tenant> tenantList = default(List<Tenant>);
                  tenantList = UResidence.TenantController.GetIdTenant(id);
                ViewBag.TenantList = tenantList;
            }
            return View("TenantEdit");
        }
        [HttpPost]
        public ActionResult TenantEdit(FormCollection fc)
        {
            DateTime start = Convert.ToDateTime(fc["LeaseStart"]);
            DateTime end = Convert.ToDateTime(fc["LeaseEnd"]);
            string gender = fc["Gender"].ToString();
            Tenant ten = new Tenant()
            {
                UnitNo = fc["UnitNo"],
                BldgNo = fc["BldgNo"],
                TenantNo = fc["TenantNo"],
                Fname = fc["Fname"],
                Mname = fc["Mname"],
                Lname = fc["Lname"],
                Gender = gender,
                Age = Convert.ToInt32(fc["Age"]),
                TelNo = fc["TelNo"],
                CelNo = fc["CelNo"],
                Email = fc["Email"],
                Citizenship = fc["Citizenship"],
                Status = fc["Status"],
                LeaseStart = start,
                LeaseEnd = end
            };
            status = UResidence.TenantController.Update(ten);
            if(status==true)
            {
                TenantView();
                ViewBag.UpdateMessage = status;
            }
            else
            {
                ViewBag.UpdateMessage = status;
            }
            return View("TenantView");
        }
    }
}