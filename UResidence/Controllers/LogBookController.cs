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
    }
}