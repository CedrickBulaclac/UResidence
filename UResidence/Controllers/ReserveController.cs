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
            return View(amenityList);
        }
    }
}