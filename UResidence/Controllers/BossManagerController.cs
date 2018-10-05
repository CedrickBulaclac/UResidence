using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace UResidence.Controllers
{

    public class BossManagerController : Controller
    {
        public JsonResult AmenityCount()
        {
            List<Amenity> data1 = new List<Amenity>();
            data1 = UResidence.AmenityController.GetAll();

            var data = data1.Count();
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult TenantCount()
        {
            List<Tenant> data1 = new List<Tenant>();
            data1 = UResidence.TenantController.GetAll();

            var data = data1.Count();
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult OwnerCount()
        {
            List<Owner> data1 = new List<Owner>();
            data1 = UResidence.OwnerController.GetAll();

            var data = data1.Count();
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult AdminCount()
        {
            List<Admin> data1 = new List<Admin>();
            data1 = UResidence.AdminController.GetAll();

            var data = data1.Count();
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetMonthly(int month,int year)
        {
            double total=0;
            double compute = 0 ;
            List<Dashboard> ret = default(List<Dashboard>);
            ret = DashboardController.GetAllm(month,year);
            List<object> data = new List<object>();         
            for (int i = 0; i <= ret.Count - 1; i++)
            {                           
                total += ret[i].Number;
            }
            for (int i=0; i<=ret.Count-1;i++)
            {
                compute = (Convert.ToDouble(ret[i].Number) / total) * 100;
                object[] d = { ret[i].AmenityName, compute };
                data.AddRange(d);
               
            }
            var final=data.ToList();

            return new JsonResult { Data=final,JsonRequestBehavior=JsonRequestBehavior.AllowGet };
        }
        public JsonResult GetYearly(int year)
        {
            double total = 0;
            double compute = 0;
            List<Dashboard> ret = default(List<Dashboard>);
            ret = DashboardController.GetAlly(year);
            List<object> data = new List<object>();
            for (int i = 0; i <= ret.Count - 1; i++)
            {
                total += ret[i].Number;
            }
            for (int i = 0; i <= ret.Count - 1; i++)
            {
                compute = (Convert.ToDouble(ret[i].Number) / total) * 100;
                object[] d = { ret[i].AmenityName, compute };
                data.AddRange(d);

            }
            var final = data.ToList();
            return new JsonResult { Data = final, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

            public JsonResult UpdateImage(Admin adm)
        {
            var image = adm.Image;
            bool status = false;
            int id = adm.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/BossManagerImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/BossManagerImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/BossManagerImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/BossManagerImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }

                    Admin a = new Admin
                    {
                        Id = id,
                        URL = finalpath
                    };


                    status = UResidence.AdminController.UpdateDP(a);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult AmenityLoad()
        {
            List<Amenity> amn = new List<Amenity>();
            amn = UResidence.AmenityController.GetAll();
            var data=amn.ToList();
            return new JsonResult { Data =data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult AdminLoad()
        {
            List<Admin> admin = new List<Admin>();
            admin = UResidence.AdminController.GetAll();
            var data = admin.ToList();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult OwnerLoad()
        {
            List<Owner> owner = new List<Owner>();
            owner = UResidence.OwnerController.GetAll();
            var data = owner.ToList();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult TenantLoad()
        {
            List<Tenant> tenant = new List<Tenant>();
            tenant = UResidence.TenantController.GetAll();
            var data = tenant.ToList();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}