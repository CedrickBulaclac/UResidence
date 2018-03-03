using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class SchedViewController : Controller
    {
        // GET: SchedView
        public ActionResult ScheduleView()
        {
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll();
            List<object> model = new List<object>();
            model.Add(amenityList.ToList());
            model.Add(schedList.ToList());

            return View(model);
        }
    }
}