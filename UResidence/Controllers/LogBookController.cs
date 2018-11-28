using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;
using System.Drawing;
namespace UResidence.Controllers
{
    public class LogBookController : Controller
    {
        // GET: LogBook
        public ActionResult LogBook()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Logbook> logbookList = new List<Logbook>();
            logbookList = UResidence.LogbookController.GET_ALL();
            List<Owner> ownerr = new List<Owner>();
            List<object> building = new List<object>();
            List<object> unit = new List<object>();
            var to = new TenantOwner();
            ownerr = UResidence.OwnerController.GetAll();
            if (ownerr.Count > 0)
            {
                for (int i = 0; i <= ownerr.Count - 1; i++)
                {
                    if (building.Contains(ownerr[i].BldgNo))
                    {

                    }
                    else
                    {
                        building.Add(ownerr[i].BldgNo);
                    }
                    if (unit.Contains(ownerr[i].UnitNo))
                    {

                    }
                    else
                    {
                        unit.Add(ownerr[i].UnitNo);
                    }
                }
            }
            to.UnitNoList = unit;
            to.BuildingList = building;
            to.LogbookList = logbookList;
            return View(to);
        }
        [HttpPost]
        public ActionResult LogBook(FormCollection fc, TenantOwner to)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            DateTime date= Convert.ToDateTime(fc["date"]);
            string visitor = Convert.ToString(fc["visitorname"]);
            string resident = Convert.ToString(fc["residentname"]);
            string purpose = Convert.ToString(fc["purpose"]);
            string buildingNo = Convert.ToString(to.tenant.BldgNo);
            string unitNo = Convert.ToString(to.tenant.UnitNo);

            DateTime timein = DateTime.Now.ToUniversalTime().AddHours(8);
            string url = Convert.ToString(fc["logbookpictext"]);

            bool status = false;
            if (url == "")
            {
                Logbook log = new Logbook
                {
                    date = date,
                    VisitorName = visitor,
                    ResidentName = resident,
                    Purpose = purpose,
                    BuildingNo = buildingNo,
                    UnitNo = unitNo,
                    Timein = timein,
                    Timeout = Convert.ToDateTime("00:00:00"),
                    URL = "~/Content/LogBookImages/user.png"
                };
                status = LogbookController.Insert(log);
            }
            else
            {
                Logbook log = new Logbook
                {
                    date = date,
                    VisitorName = visitor,
                    ResidentName = resident,
                    Purpose = purpose,
                    BuildingNo = buildingNo,
                    UnitNo = unitNo,
                    Timein = timein,
                    Timeout = Convert.ToDateTime("00:00:00"),
                    URL = url
                };
                status = LogbookController.Insert(log);
            }

            List<Logbook> logbookList = new List<Logbook>();
            logbookList = UResidence.LogbookController.GET_ALL();
            List<Owner> ownerr = new List<Owner>();
            List<object> building = new List<object>();
            List<object> unit = new List<object>();
            var to1 = new TenantOwner();
            ownerr = UResidence.OwnerController.GetAll();
            if (ownerr.Count > 0)
            {
                for (int i = 0; i <= ownerr.Count - 1; i++)
                {
                    if (building.Contains(ownerr[i].BldgNo))
                    {

                    }
                    else
                    {
                        building.Add(ownerr[i].BldgNo);
                    }
                    if (unit.Contains(ownerr[i].UnitNo))
                    {

                    }
                    else
                    {
                        unit.Add(ownerr[i].UnitNo);
                    }
                }
            }
            to1.UnitNoList = unit;
            to1.BuildingList = building;
            to1.LogbookList = logbookList;
            LogBook();
            return RedirectToAction("LogBook", "LogBook");
        }
        public ActionResult LogBookView()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
                ViewBag.ReservationModule = a.ReservationModule;
                ViewBag.RegistrationModule = a.RegistrationModule;
                ViewBag.LogBookModule = a.LogBookModule;
                ViewBag.PaymentModule = a.PaymentModule;
                ViewBag.ReversalModule = a.ReversalModule;
                Session["ReservationModule"] = ViewBag.ReservationModule;
                Session["RegistrationModule"] = ViewBag.RegistrationModule;
                Session["LogBookModule"] = ViewBag.LogBookModule;
                Session["PaymentModule"] = ViewBag.PaymentModule;
                Session["ReversalModule"] = ViewBag.ReversalModule;
            }
            List<Logbook> log = new List<Logbook>();
            log = UResidence.LogbookController.GET_ALL();
            return View(log);
        }

        [HttpPost]
        public ActionResult LogBookView(FormCollection fc)
        {
            if (Session["Level"] ==null)
            {
                return Redirect("~/Login");              
            }
           if(Convert.ToInt32(Session["Level"]) <=7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
                ViewBag.ReservationModule = a.ReservationModule;
                ViewBag.RegistrationModule = a.RegistrationModule;
                ViewBag.LogBookModule = a.LogBookModule;
                ViewBag.PaymentModule = a.PaymentModule;
                ViewBag.ReversalModule = a.ReversalModule;
                Session["ReservationModule"] = ViewBag.ReservationModule;
                Session["RegistrationModule"] = ViewBag.RegistrationModule;
                Session["LogBookModule"] = ViewBag.LogBookModule;
                Session["PaymentModule"] = ViewBag.PaymentModule;
                Session["ReversalModule"] = ViewBag.ReversalModule;
            }
            DateTime date = Convert.ToDateTime(fc["txtdate"]);
            List<Logbook> logbookList = new List<Logbook>();
            logbookList = LogbookController.GET_ALL(date);
         
            return View(logbookList);
        }

        public ActionResult LogBookViewing()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Logbook> logbookList = new List<Logbook>();
            List<Owner> ownerr = new List<Owner>();
            List<object> building = new List<object>();
            List<object> unit = new List<object>();
            var to = new TenantOwner();
            ownerr = UResidence.OwnerController.GetAll();
            logbookList = UResidence.LogbookController.GET_ALL();
            if (ownerr.Count > 0)
            {
                for (int i = 0; i <= ownerr.Count - 1; i++)
                {
                    if (building.Contains(ownerr[i].BldgNo))
                    {

                    }
                    else
                    {
                        building.Add(ownerr[i].BldgNo);
                    }
                    if (unit.Contains(ownerr[i].UnitNo))
                    {

                    }
                    else
                    {
                        unit.Add(ownerr[i].UnitNo);
                    }
                }
            }
            to.UnitNoList = unit;
            to.BuildingList = building;
            to.LogbookList = logbookList;
            return View(to);
        }
        [HttpPost]
        public ActionResult LogBookViewing(FormCollection fc, TenantOwner to1)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            DateTime date = Convert.ToDateTime(fc["gdate"]);
            string bn=Convert.ToString(to1.tenant.BldgNo);
            string un = Convert.ToString(to1.tenant.UnitNo);
            List<Logbook> logbookList = new List<Logbook>();
            logbookList = LogbookController.GET_ALL(date, bn, un);
            List<Owner> ownerr = new List<Owner>();
            List<object> building = new List<object>();
            List<object> unit = new List<object>();
            var to = new TenantOwner();
            ownerr = UResidence.OwnerController.GetAll();
            if (ownerr.Count > 0)
            {
                for (int i = 0; i <= ownerr.Count - 1; i++)
                {
                    if (building.Contains(ownerr[i].BldgNo))
                    {

                    }
                    else
                    {
                        building.Add(ownerr[i].BldgNo);
                    }
                    if (unit.Contains(ownerr[i].UnitNo))
                    {

                    }
                    else
                    {
                        unit.Add(ownerr[i].UnitNo);
                    }
                }
            }
            to.UnitNoList = unit;
            to.BuildingList = building;
            to.LogbookList = logbookList;
            return View(to);
        }

        public JsonResult Search(Logbook data)
        {
            try
            {
                List<Logbook> logbookList = new List<Logbook>();
                logbookList = UResidence.LogbookController.GET_ALL(data.date);
                var events = logbookList.ToList();
                return new JsonResult
                {
                    Data = events,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (System.Data.SqlTypes.SqlTypeException)
            {
                var events = false;
                return new JsonResult
                {
                    Data = events,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }
        }
        public JsonResult GetEvents()
        {
            List<Logbook> log = new List<Logbook>();
            log = UResidence.LogbookController.GET_ALL();
            var events = log.ToList();
            return new JsonResult
            {
                Data = events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult InsertImage(Logbook data)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            bool status=false;
            var image1 = data.Image;
            if (image1 != null)
            {
                if (image1.ContentLength > 0)
                {
                    
                    string imagefileName = Path.GetFileName(image1.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/LogBookImages"), imagefileName);
                    string folderpath1 = "~/Content/LogBookImages/" + imagefileName;
                   
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        image1.SaveAs(folderPath);
                    }
                    else
                    {
                        image1.SaveAs(folderPath);
                    }
                    Logbook log = new Logbook
                    {
                        Id=data.Id,
                        URL = folderpath1
                    };
                    if(Logbook.Validation(log)==true)
                    {
                        status = LogbookController.UpdateImage(log);
                       
                    }
                    else
                    {
                        status = false;
                    }
                }
            }
            return View("LogBook");
        }
        public JsonResult Insert(Logbook data, HttpPostedFileBase logbookpic)
        {
            bool status = false;
            var image = data.Image;

            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/LogBookImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                      
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/LogBookImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/LogBookImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        
                        string folderpath1 = "~/Content/LogBookImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }
                    status = true;
                    Logbook log = new Logbook
                    {
                        date = data.date,
                        ResidentName = data.ResidentName,
                        VisitorName = data.VisitorName,
                        Purpose = data.Purpose,
                        Timein = data.Timein,
                        Timeout = Convert.ToDateTime("00:00:00"),
                        URL = finalpath
                    };
                    status = LogbookController.Insert(log);
                }
            }

        
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };




            //        string folderpath1 = "~/Content/LogBookImages/" + data.URL;
            //string folderPath = Path.Combine(Server.MapPath("~/Content/LogBookImages"), data.URL);
        }
        public JsonResult UpdateTimein(Logbook data)
        {
            DateTime date = DateTime.Now.ToUniversalTime().AddHours(8);
            bool status = false;
            Logbook log = new Logbook
            {
                Id = data.Id,
                Timein = date
            };
            status = UResidence.LogbookController.UpdateTimein(log);


            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult Update(Logbook data)
        {
            DateTime date = DateTime.Now.ToUniversalTime().AddHours(8);
            bool status = false;
            Logbook log = new Logbook
            {
                Id = data.Id,
                Timeout = date
            };
            status = UResidence.LogbookController.Update(log);
         
          
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}