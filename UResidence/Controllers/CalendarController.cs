using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class CalendarController : Controller
    {

        public JsonResult GetEvents()
        {
            List<SchedReservation> reservationList = UResidence.SchedReservationController.GetAllA();
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        //[HttpPost]
        //public JsonResult SaveEvent(SchedReservation e)
        //{
        //    var status = false;
        //    List<SchedReservation> reservationList = UResidence.SchedReservationController.GetAllA();
        //    {
        //        if (e.AmenityId > 0)
        //        {
        //            //UPDATE EVENT
        //            var v = reservationList.Where(a => a.AmenityId == e.AmenityId).FirstOrDefault();
        //            if (v != null)
        //            {
        //                v.Id = e.Id;
        //                v.AmenityId = e.AmenityId;
        //                v.StartTime = e.StartTime;
        //                v.EndTIme = e.EndTIme;
        //                v.Rate = e.Rate;
        //                v.Theme = e.Theme;
        //            }
        //        }
        //        else
        //        {
        //            reservationList.Add(e);
        //        }
        //     SchedReservationController.SaveChanges(e);
        //        status = true;
        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}




        //[HttpPost]
        //public JsonResult DeleteEvent(int amenityId)
        //{
        //    var status = false;
        //    List<SchedReservation> reservationList = UResidence.SchedReservationController.GetAllA();
        //    {
               
        //        var v = reservationList.Where(a => a.AmenityId == amenityId).FirstOrDefault();
        //        if (v != null)
        //        {
        //            reservationList.Remove(v);
        //            SchedReservationController.SaveChanges(v);
        //            status = true;
        //        }

        //    }
        //    return new JsonResult { Data = new { status = status } };
        //}






        // GET: Calendar
        public ActionResult CalendarView()
        {
            return View();
        }
    }
}