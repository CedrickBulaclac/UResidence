using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class LogBookController : Controller
    {
        // GET: LogBook
        public ActionResult LogBook()
        {
            return View();
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
        public JsonResult Insert(Logbook data)
        {
            Logbook log = new Logbook {
                date = data.date,
                ResidentName = data.ResidentName,
                VisitorName = data.VisitorName,
                Purpose = data.Purpose,
                Timein = data.Timein,
                Timeout = Convert.ToDateTime("00:00:00")
               
                            };

            bool status = UResidence.LogbookController.Insert(log);
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