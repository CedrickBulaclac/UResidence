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
            List<Logbook> logbookList = new List<Logbook>();
            logbookList = UResidence.LogbookController.GET_ALL();
            return View(logbookList);
        }
        public ActionResult LogBookView()
        {
            return View();
        }
        public JsonResult Search(Logbook data)
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
        public JsonResult Insert(Logbook data)
        {
            bool status = false;
            string folderpath1 = "~/Content/LogBookImages/" + data.URL;
            string folderPath = Path.Combine(Server.MapPath("~/Content/LogBookImages"), data.URL);


            Logbook log = new Logbook
                    {
                        date = data.date,
                        ResidentName = data.ResidentName,
                        VisitorName = data.VisitorName,
                        Purpose = data.Purpose,
                        Timein = data.Timein,
                        Timeout = Convert.ToDateTime("00:00:00"),
                        URL = folderpath1
            };
            status = LogbookController.Insert(log);
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