using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class ReserveController : Controller
    {
        private bool status = false;
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
            string aname = Convert.ToString(fc["namea"]);


            Session["ID"] = aid;
            Session["RATE"] = arate;
            Session["NAME"] = aname;


            return RedirectToAction("Calendar", "Reserve");

        }
        public ActionResult Calendar()
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
            string drate = fc["tratee"];
            Session["drate"] = drate;

            List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll(sd, ed);
            if (schedList.Count > 0)
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
            string sd = (string)Session["sd"];
            string ed = (string)Session["ed"];
            List<Equipment> equipList = UResidence.EquipmentController.GetAll(sd, ed);
            List<Equipment> equip = UResidence.EquipmentController.GetAll();
            List<object> model = new List<object>();
            model.Add(equipList.ToList());
            model.Add(equip.ToList());
            return View(model);
        }
        [HttpPost]
        public void Choose_Equipment(int[] data, int[] datar, int[] eid)
        {
            string sd = (string)Session["sd"];
            string ed = (string)Session["ed"];


            if (data != null)
            {
                foreach (var i in data)
                {
                    Response.Write("<script>alert(" + i + ")</script>");
                }
                int[] quantity = data;
                Session["quantity"] = quantity;

                int[] ratee = datar;
                Session["ratee"] = ratee;
                int[] eqpid = eid;
                Session["eqpid"] = eqpid;
                Summary();

            }
            else
            {
                Choose_Equipment();

            }

        }
        public ActionResult Summary()
        {

            List<Equipment> equip = UResidence.EquipmentController.GetAll();
            string sd = (string)Session["sd"];
            string ed = (string)Session["ed"];
            ViewBag.start = sd;
            ViewBag.end = ed;
            ViewBag.ratea = Session["drate"];
            ViewBag.amenname = Session["NAME"];
            ViewBag.quan = Session["quantity"];
            ViewBag.rat = Session["ratee"];



            return View(equip);
        }

        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }


        [HttpPost]
        public ActionResult Summary(FormCollection fc)
        {
            string sd = (string)Session["sd"];
            string ed = (string)Session["ed"];
            string rate = (string)Session["drate"];
            string uid = (string)Session["UID"];
            string aid = (string)Session["ID"];
            SchedReservation a = new SchedReservation
            {
                AmenityId = Convert.ToInt32(aid),
                StartTime = Convert.ToDateTime(sd),
                EndTIme = Convert.ToDateTime(ed),
                Rate = Convert.ToInt32(rate),

            };
            status = UResidence.SchedReservationController.Insert(a);
            if (status == true)
            {
                SchedReservation b = new SchedReservation();
                b = UResidence.SchedReservationController.GetAmenityNo(aid, sd, ed);
                int sid = b.Id;
                string tor = (string)Session["TOR"];
                string UserId = (string)Session["UID"];
                string fname;
                string mname;
                string lname;
                string fullname = "";
                UResidence.Residence reside = new UResidence.Residence();
                UResidence.Owner own = new UResidence.Owner();
                UResidence.Tenant ten = new UResidence.Tenant();
                if (tor == "Owner")
                {
                    own = UResidence.OwnerController.GetIdOwner(UserId);
                    reside = UResidence.ResidenceController.GetOwnerNo(UserId);
                    fname = RemoveWhitespace(own.Fname);
                    mname = RemoveWhitespace(own.Mname);
                    lname = RemoveWhitespace(own.Lname);
                    fullname = fname + " " + mname + " " + lname;
                }
                else if (tor == "Tenant")
                {
                    ten = UResidence.TenantController.GetIdTenant(UserId);
                    reside = UResidence.ResidenceController.GetTenantNo(UserId);
                    fname = RemoveWhitespace(ten.Fname);
                    mname = RemoveWhitespace(ten.Mname);
                    lname = RemoveWhitespace(ten.Lname);
                    fullname = fname + " " + mname + " " + lname;
                }
                UResidence.Reservation r = new UResidence.Reservation
                {
                    Rid = Convert.ToInt32(reside.Id),
                    Sid = sid,
                    Status = "Pending",
                    Tor = tor,
                    AcknowledgeBy = "",
                    ReservedBy = fullname,
                };
                status = UResidence.ReservationController.Insert(r);
                if (status == true)
                {
                    UResidence.Reservation reserve = new UResidence.Reservation();
                    reserve = UResidence.ReservationController.GetId(sid);
                    int refno = reserve.Id;
                    int[] equantity = (Int32[])Session["quantity"];
                    int[] eid = (Int32[])Session["eqpid"];
                    int[] ratee = (Int32[])Session["ratee"];
                    int ctr = 0;
                    foreach (int ii in equantity)
                    {

                        if (ii != 0)
                        {
                            EquipReservation er = new EquipReservation
                            {
                                EquipNo = eid[ctr],
                                Quantity = ii,
                                RefNo = refno,
                                Rate = ratee[ctr],
                            };
                            status = UResidence.EquipReservationController.Insert(er);

                        }

                    }
                    if (status == true)
                    {
                        Response.Write("<script>alert('You can proceed to the Admin Office to give the Downpayment')</script>");
                    }

                }

            }
            return RedirectToAction("Home", "Reserve");

        }



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
       

      


      
      
        
      


     
    
    
      


 