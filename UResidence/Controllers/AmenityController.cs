using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UResidence;
namespace UResidence.Controllers
{
    public class AmenityController : Controller
    {
        // GET: Amenity
        public ActionResult Reserve()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection fc)
        {
            Amenity am = new Amenity()
            {
                Description = fc["Description"],
            Capacity = Convert.ToInt32(fc["Capacity"]),
            AmenityNo = fc["AmenityNo"]
        };
            UResidence.AmenityController.Insert(am);         
            return View();
        }

      
    }
}