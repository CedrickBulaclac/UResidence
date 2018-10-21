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
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Logbook> logbookList = new List<Logbook>();
            logbookList = UResidence.LogbookController.GET_ALL();
            return View(logbookList);
        }
        [HttpPost]
        public ActionResult LogBook(FormCollection fc,HttpPostedFileBase logbookpic)
        {
            DateTime date= Convert.ToDateTime(fc["date"]);
            string visitor = Convert.ToString(fc["visitorname"]);
            string resident = Convert.ToString(fc["residentname"]);
            string purpose = Convert.ToString(fc["purpose"]);
            DateTime timein = DateTime.Now;


            bool status = false;
            var image = logbookpic;

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
                        date = date,
                        ResidentName = resident,
                        VisitorName = visitor,
                        Purpose =purpose,
                        Timein =timein,
                        Timeout = Convert.ToDateTime("00:00:00"),
                        URL = finalpath
                    };
                    status = LogbookController.Insert(log);
                }
            }
            List<Logbook> logbookList = new List<Logbook>();
            logbookList = UResidence.LogbookController.GET_ALL();
            return View(logbookList);
        }
        public ActionResult LogBookView()
        {
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Logbook> log = new List<Logbook>();
            log = UResidence.LogbookController.GET_ALL();
            return View(log);
        }

        public ActionResult LogBookViewing()
        {
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Logbook> logbookList = new List<Logbook>();
            return View(logbookList);
        }
        [HttpPost]
        public ActionResult LogBookViewing(FormCollection fc)
        {
            DateTime date = Convert.ToDateTime(fc["gdate"]);
            string bn=Convert.ToString(fc["gbldg"]);
            string un = Convert.ToString(fc["gunit"]);
            List<Logbook> logbookList = new List<Logbook>();
            logbookList = LogbookController.GET_ALL(date, bn, un);
            return View(logbookList);
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
            bool status = false;
            Logbook log = new Logbook
            {
                Id = data.Id,
                Timein = data.Timein
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
            bool status = false;
            Logbook log = new Logbook
            {
                Id = data.Id,
                Timeout = data.Timeout
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