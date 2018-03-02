using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Views.Reservation
{
    public class ReserveController : Controller
    {
        // GET: Reserve
        public ActionResult SelectAmenity()
        {
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll();
            List<object> model = new List<object>();
            model.Add(amenityList.ToList());
            model.Add(schedList.ToList());

            return View(model);
        }
        public JsonResult GetEvents()
        {
            List<SchedReservation> reservationList = UResidence.SchedReservationController.GetAllA();
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public ActionResult Home()
        {
            return View();
        }


        
    }
}