using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class AmenityMaintenanceController : Controller
    {
        // GET: AmenityMaintenance
        public ActionResult Home()
        {
            return View();
        }

        bool status;
        // GET: Reserve
        public ActionResult AmenityMaintenanceView()
        {
            List<Amenity> amenityList = UResidence.AmenityController.GetAllM();
            List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll();
            List<Equipment> equipList = UResidence.EquipmentController.GetAll();
            List<object> model = new List<object>();
            model.Add(amenityList.ToList());
            model.Add(schedList.ToList());
            model.Add(equipList.ToList());
            return View(model);
        }
        [HttpPost]
        public ActionResult AmenityMaintenanceView(FormCollection fc)
        {
            int aid = Convert.ToInt32(fc["ida"]);
            DateTime startTime = Convert.ToDateTime(fc["sta"]);
            DateTime endTime = Convert.ToDateTime(fc["eta"]);
            int rate = Convert.ToInt32(fc["ap"]);
            string theme = Convert.ToString(fc["thm"]);
            SchedReservation a = new SchedReservation
            {
                AmenityId = aid,
                StartTime = startTime,
                EndTIme = endTime,
                Rate = rate,
                Theme = theme


            };
            status = UResidence.SchedReservationController.Insert(a);
            if (status == true)
            {
                Response.Write("<script>alert('Scheduled Successfully')</script>");
                List<Amenity> amenityList = UResidence.AmenityController.GetAll();
                List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll();
                List<Equipment> equipList = UResidence.EquipmentController.GetAll();
                List<object> model = new List<object>();
                model.Add(amenityList.ToList());
                model.Add(schedList.ToList());
                model.Add(equipList.ToList());
                return View(model);
            }
            else
            {
                Response.Write("<script>alert('Please try again!')</script>");
                List<Amenity> amenityList = UResidence.AmenityController.GetAll();
                List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll();
                List<Equipment> equipList = UResidence.EquipmentController.GetAll();
                List<object> model = new List<object>();
                model.Add(amenityList.ToList());
                model.Add(schedList.ToList());
                model.Add(equipList.ToList());
                return View(model);

            }
        }
        public JsonResult GetEvents()
        {
            List<SchedReservation> reservationList = UResidence.SchedReservationController.GetAllM();
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

    }
}