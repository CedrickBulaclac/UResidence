using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Views.Reservation
{
    public class ReserveController : Controller
    {
        bool status;
        // GET: Reserve
        public ActionResult SelectAmenity()
        {
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll();
            List<Equipment> equipList = UResidence.EquipmentController.GetAll();
            List<object> model = new List<object>();
            model.Add(amenityList.ToList());
            model.Add(schedList.ToList());
            model.Add(equipList.ToList());
            return View(model);
        }

        public ActionResult Amenity()
        {
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);
        }
        [HttpPost]
        public ActionResult Amenity(FormCollection fc)
        {
            int aid = Convert.ToInt32(fc["ida"]);
            int arate = Convert.ToInt32(fc["ratea"]);
            SchedReservation a = new SchedReservation
            {
                AmenityId = aid
            };
            Session["ID"] = aid;
            Session["RATE"] = arate;
            return RedirectToAction("Calendar", "Reserve");

        }
        public ActionResult Calendar()
        {
            return View();
        }
        public ActionResult Summary()
        {

            return View();
        }
        public ActionResult Choose_Date()
        {
            ViewBag.Message = Convert.ToInt32(Session["RATE"]);
            return View();
        }
        [HttpPost]
        public ActionResult Choose_Date(FormCollection fc)
        {
            string sd = fc["stime"];
            string ed = fc["etime"];
            Session["sd"] = sd;
            Session["ed"] = ed;
            List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll(sd, ed);
            if(schedList.Count > 0)
            {
                Response.Write("<script>alert('Your chosen date and time is not available')</script>");
                ViewBag.Message = Convert.ToInt32(Session["RATE"]);
            }
            else
            {
                Response.Write("<script>alert('Successful')</script>");
                return RedirectToAction("Choose_Equipment", "Reserve");
            }
           
            return View();
        }
        public ActionResult Choose_Equipment()
        {
            string sd= (string) Session["sd"];
            string ed = (string)Session["ed"];
            List<Equipment> equipList = UResidence.EquipmentController.GetAll(sd,ed);
            List<Equipment> equip = UResidence.EquipmentController.GetAll();
            List<object> model = new List<object>();
            model.Add(equipList.ToList());
            model.Add(equip.ToList());
            return View(model);
        }
        [HttpPost]
        public void Choose_Equipment(int[] data)
        {
            if (data != null)
            {
                foreach (var i in data)
                {
                    Response.Write("<script>alert(" + i + ")</script>");
                }
               

                string sd = (string)Session["sd"];
                string ed = (string)Session["ed"];
                Response.Write("<script>alert(" + sd + ")</script>");
                Summary();
            }
            else
            {
                Response.Write("<script>alert('Failed')</script>");
                Choose_Equipment();
            }
        }

        //[HttpPost]
        //public ActionResult SelectAmenity(FormCollection fc)
        //{
        //    int aid = Convert.ToInt32(fc["ida"]);
        //    DateTime startTime = Convert.ToDateTime(fc["sta"]);
        //    DateTime endTime = Convert.ToDateTime(fc["eta"]);
        //    int rate = Convert.ToInt32(fc["ap"]);
        //    string theme = Convert.ToString(fc["thm"]);
        //    SchedReservation a = new SchedReservation
        //    {
        //        AmenityId =aid,
        //        StartTime=startTime,
        //        EndTIme=endTime,
        //        Rate= rate,
        //        Theme=theme


        //    };
        //    status = UResidence.SchedReservationController.Insert(a);
        //    if (status == true)
        //    {
        //        Response.Write("<script>alert('Scheduled Successfully')</script>");
        //        List<Amenity> amenityList = UResidence.AmenityController.GetAll();
        //        List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll();
        //        List<Equipment> equipList = UResidence.EquipmentController.GetAll();
        //        List<object> model = new List<object>();
        //        model.Add(amenityList.ToList());
        //        model.Add(schedList.ToList());
        //        model.Add(equipList.ToList());
        //        return View(model);
        //    }
        //    else
        //    {
        //        Response.Write("<script>alert('Please try again!')</script>");
        //        List<Amenity> amenityList = UResidence.AmenityController.GetAll();
        //        List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll();
        //        List<Equipment> equipList = UResidence.EquipmentController.GetAll();
        //        List<object> model = new List<object>();
        //        model.Add(amenityList.ToList());
        //        model.Add(schedList.ToList());
        //        model.Add(equipList.ToList());
        //        return View(model);

        //    }
        //}
        public JsonResult GetEvents()
        {
            int AID = Convert.ToInt32(Session["ID"]);

            List<SchedReservation> schedList = UResidence.SchedReservationController.GetAllA(AID);
                var events = schedList.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
           
        }
        public ActionResult Home()
        {
            return View();
        }


        
    }
}