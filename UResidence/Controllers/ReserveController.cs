using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;


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
        public ActionResult Home()
        {
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            int balance = 0;
            List<Billing> billing = new List<Billing>();
            int uid = Convert.ToInt32(Session["UID"]);
            string type = (Session["TOR"]).ToString();
            if (type == "Owner")
            {
                Owner a = new Owner();
                a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                Session["URLL"] = a.URL;
                billing = UResidence.BillingController.GetOwner(uid);
            }

            else
            {

                Tenant t = new Tenant();
                t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                Session["URLL"] = t.URL;
                billing = UResidence.BillingController.GetTenant(uid);
            }

            for (int i = 0; i <= billing.Count - 1; i++)
            {

                balance += ((billing[i].Rate + billing[i].Charge + billing[i].ChairCost + billing[i].TableCost) - (billing[i].Totale - billing[i].Amount));
            }
            if (balance > 0)
            {
                bool ss = true;
                ViewBag.Bal = balance;
                ViewBag.s = ss;
                Session["status"] = true;
                //string hrtml = "<script>alert('You have an Outstanding Balance of ₱" + balance + " ')</script>";
                //Response.Write(hrtml);
               
                return View(amenityList);
                
            }
            else
            {
                bool ss = false;
                ViewBag.Bal = balance;
                ViewBag.s = ss ;
                return View(amenityList);

            }
        }

        public ActionResult Amenity()
        {
            int balance = 0;
            List<Billing> billing = new List<Billing>();
            int uid = Convert.ToInt32(Session["UID"]);
            string type = (Session["TOR"]).ToString();
            if (type == "Owner")
            {
                billing = UResidence.BillingController.GetOwner(uid);
            }
            else
            {
                billing = UResidence.BillingController.GetTenant(uid);
            }
            for(int i=0;i<=billing.Count-1;i++)
            {
                balance += ((billing[i].Rate + billing[i].Charge + billing[i].ChairCost + billing[i].TableCost) - (billing[i].Totale - billing[i].Amount));
            }
                if ( balance > 0)
                {
                Session["aa"] = 0;


                return RedirectToAction("Home", "Reserve");
                }
                else
                {
                    List<Amenity> amenityList = UResidence.AmenityController.GetAll();                 
                    return View(amenityList);
                }                 
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
            if(aname.ToUpper()=="SWIMMING POOL")
            {
                int child = Convert.ToInt32(fc["ratec"]);
                int adult = Convert.ToInt32(fc["rateaa"]);
                Session["child"] = child;
                Session["adult"] = adult;
            }
            Amenity amenity = UResidence.AmenityController.GetAmenityImage(aid);
            string amenitylink = amenity.Url.ToString();
            Session["AmenityURL"] = amenitylink;

            return RedirectToAction("Calendar", "Reserve");

        }
        public ActionResult Calendar()
        {
            Session["aa"] = 1;
            ViewBag.Amenity=(Session["NAME"]).ToString();
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);
        }
        public ActionResult Choose_Date()
        {
            ViewBag.name = (Session["NAME"]).ToString();
            ViewBag.Message = Convert.ToInt32(Session["RATE"]);
            return View();
        }
        [HttpPost]
        public ActionResult Choose_Date(FormCollection fc)
        {
            string nameamenity = (Session["NAME"]).ToString();
            if (nameamenity.ToUpper() != "BASKETBALL COURT")
            {
                string sd = Convert.ToString(fc["stime"]);
                string ed = Convert.ToString(fc["etime"]);
                Session["sd"] = sd;
                Session["ed"] = ed;
                string drate = fc["tratee"];
                Session["drate"] = drate;
                int aid = Convert.ToInt32(Session["ID"]);

                List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll(sd, ed, aid);
                if (schedList.Count > 0)
                {
                    Response.Write("<script>alert('Your chosen date and time is not available')</script>");
                    ViewBag.Message = Convert.ToInt32(Session["RATE"]);
                    return View();
                }
                else
                {
                    Response.Write("<script>alert('Successful')</script>");
                    return RedirectToAction("Choose_Equipment", "Reserve");
                }
            }
            else
            {
                string sd = Convert.ToString(fc["stime"]);
                string ed = Convert.ToString(fc["etime"]);
                Session["sd"] = sd;
                Session["ed"] = ed;
                string drate = fc["tratee"];
                Session["drate"] = drate;
                int aid = Convert.ToInt32(Session["ID"]);

                List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll(sd, ed, aid);
                if (schedList.Count > 0)
                {
                    Response.Write("<script>alert('Your chosen date and time is not available')</script>");
                    ViewBag.Message = Convert.ToInt32(Session["RATE"]);
                    return View();
                }
                else
                {
                    Response.Write("<script>alert('Successful')</script>");
                    return RedirectToAction("Summary", "Reserve");
                }
               
            }           
        }
        public ActionResult Choose_Equipment()
        {        
                ViewBag.Amenity = (Session["NAME"]).ToString();
                int[] eqpid;
                List<int> qid = new List<int>();
                string sd = (string)Session["sd"];
                string ed = (string)Session["ed"];
                List<Equipment> equipList = UResidence.EquipmentController.GetAll(sd, ed);
                List<Equipment> equip = UResidence.EquipmentController.GetAll();
                List<object> model = new List<object>();
                model.Add(equipList.ToList());
                model.Add(equip.ToList());
                foreach (Equipment eqp in equip)
                {
                    qid.Add(eqp.Id);
                }
                eqpid = qid.ToArray();
                Session["eqpid"] = eqpid;
                return View(model);                     
        }
        [HttpPost]
        public void Choose_Equipment(int[] data, int[] datar)
        {
            string sd = (string)Session["sd"];
            string ed = (string)Session["ed"];


            if (data != null)
            {

                int[] quantity = data;
                Session["quantity"] = quantity;

                int[] ratee = datar;
                Session["ratee"] = ratee;

                Summary();

            }
            else
            {
                Choose_Equipment();

            }

        }
        public ActionResult Summary()
        {
            ViewBag.Amenity = (Session["NAME"]).ToString();
            List<Equipment> equip = UResidence.EquipmentController.GetAll();
            string sd = (string)Session["sd"];
            string ed = (string)Session["ed"];
            ViewBag.start = sd;
            ViewBag.end = ed;
            ViewBag.ratea = Session["drate"];
            ViewBag.amenname = Session["NAME"];

            ViewBag.quan = Session["quantity"];
            ViewBag.rat = Session["ratee"];
            ViewBag.QA=Session["qa"] ;
            ViewBag.QC = Session["qc"];
            ViewBag.AR = Session["ar"];
            ViewBag.CR = Session["cr"];

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
            int uid = (int)Session["UID"];
            int aid = (int)Session["ID"];
            DateTime date = DateTime.Now;
           string sdate= String.Format("{0:d/M/yyyy HH:mm:ss}", date);
            Session["date"] = sdate;
            SchedReservation a = new SchedReservation
            {
                AmenityId = aid,
                StartTime = Convert.ToDateTime(sd),
                EndTIme = Convert.ToDateTime(ed),
                Rate = Convert.ToInt32(rate),
                Date = Convert.ToDateTime(sdate),

            };
            status = UResidence.SchedReservationController.Insert(a);
           
            if (status == true)
            {
                string amenityname = (Session["NAME"]).ToString();
                string qa = (string)Session["qa"];
                string qc = (string)Session["qc"];
                SchedReservation b = new SchedReservation();
                b = UResidence.SchedReservationController.GetAmenityNo(aid.ToString(), sd, ed,Convert.ToDateTime(sdate));
                
                int sid =b.Id;
                string tor = (string)Session["TOR"];
                int UserId = (int)Session["UID"];         
                string fullname = "";
                if (amenityname == "Swimming Pool" || amenityname == "SWIMMING POOL")
                {
                    Swimming swim = new Swimming
                    {
                        Adult = Convert.ToInt32(qa),
                        Child = Convert.ToInt32(qc),
                        SchedID =sid,
                    };
                    UResidence.SwimmingController.Insert(swim);

                }
                UResidence.Residence reside = new UResidence.Residence();
                UResidence.Owner own = new UResidence.Owner();
                UResidence.Tenant ten = new UResidence.Tenant();
                if (tor == "Owner")
                {                
                    reside = UResidence.ResidenceController.GetOwnerNo(UserId.ToString());                              
                }
                else if (tor == "Tenant")
                {                  
                    reside = UResidence.ResidenceController.GetTenantNo(UserId.ToString());
                }
                fullname = (Session["Fullname"]).ToString();
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
                    if (equantity != null)
                    {
                        for (int i = 0; i <= equantity.Count() - 1; i++)
                        {

                            if (Convert.ToInt32(equantity[i]) != 0)
                            {
                                EquipReservation er = new EquipReservation
                                {

                                    EquipId = Convert.ToInt32(eid[i]),
                                    Quantity = Convert.ToInt32(equantity[i]),
                                    RefNo = refno,
                                    Rate = Convert.ToInt32(ratee[i]) * Convert.ToInt32(equantity[i]),

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
            }
            return RedirectToAction("Home", "Reserve");

        }



        public JsonResult GetEvents()
        {
            string name = (Session["NAME"]).ToString();
            List<SchedReservation> schedList = UResidence.SchedReservationController.GetAllA(name);
            var events = schedList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult GetEventsA()
        {
            List<ReservationProcess> reservationList = ReservationProcessController.GET_ALL();
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }




        public ActionResult Swimming()
        {
            ViewBag.Message = Convert.ToInt32(Session["RATE"]);
            return View();
        }

        [HttpPost]
        public ActionResult Swimming(FormCollection fc)
        {
            string qa = fc["adult"];
            string qc = fc["child"];
            string ar= fc["rateadult"];
            string cr = fc["ratechild"];
            Session["qa"] = qa;
            Session["qc"] = qc;
            Session["ar"] = ar;
            Session["cr"] = cr;
            string sd = fc["stime"];
            string ed = fc["etime"];
            Session["sd"] = sd;
            Session["ed"] = ed;
            string drate = fc["rate"];
            Session["drate"] = drate;
            int aid = Convert.ToInt32(Session["ID"]);

            
                CheckSwimming cs = new CheckSwimming();
                cs = CheckSwimmingController.Get(sd, aid);
            if (cs == null)
            {
                return RedirectToAction("Choose_Equipment", "Reserve");
            }
            else
            {
                if (cs.Capacity > 0)
                {
                    Response.Write("<script>alert('Successful')</script>");
                    return RedirectToAction("Choose_Equipment", "Reserve");
                   
                }
                else
                {
                    Response.Write("<script>alert('Your chosen date and time is not available')</script>");
                    ViewBag.Message = Convert.ToInt32(Session["RATE"]);
                }
            }
            return View();
        }


        public ActionResult CalendarViewOT()
        {
            Session["aa"] = 1;
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);
        }
        public ActionResult DownloadReservation(int refno1)
        {
            bool events = false ;
            //int refno = Convert.ToInt32(fc["rfid"]);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Views/Report"), "ReservationForm.rpt"));        
            List<ReportReservationAmenity> data = default(List<ReportReservationAmenity>);
            data = UResidence.ReportReservationAmenityController.GET(refno1);
            rd.SetDataSource(data.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Reservation.pdf");
               
            }
            catch (Exception)
            {
                throw;              
            }
          
        }
    


        public ActionResult AboutUs()
        {
            Session["aa"] = 1;
            return View();
        }

        public JsonResult GetCount()
        {
            int level = Convert.ToInt32(Session["Level"]);
            int uid = Convert.ToInt32(Session["UID"]);

            List<Notification> oldContact = default(List<Notification>);
            if (level == 8)
            {
                oldContact = UResidence.NotificationController.GetCountO(uid);
            }
            else
            {
                oldContact = UResidence.NotificationController.GetCountT(uid);
            }
            var events = oldContact.Count();
            return new JsonResult
            {
                Data = events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetNotificationContacts()
        {

            int ctr = Convert.ToInt32(Session["oldcontactList"]);
            Session["oldcontactList"] = ctr;
            int level = Convert.ToInt32(Session["Level"]);
            int uid = Convert.ToInt32(Session["UID"]);
            List<Notification> notiList = default(List<Notification>);
            if (level == 8)
            {
                notiList = NotificationController.GetAllO(uid);
            }
            else if (level == 9)
            {
                notiList = NotificationController.GetAllT(uid);
            }
            var list = notiList.ToList();
            return new JsonResult { Data = list, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult UpdateNotif()
        {
            int level = Convert.ToInt32(Session["Level"]);
            int uid = Convert.ToInt32(Session["UID"]);
            List<Notification> notiList = default(List<Notification>);
            if (level == 8)
            {
                notiList = UResidence.NotificationController.GetCountO(uid);
            }
            else
            {
                notiList = UResidence.NotificationController.GetCountT(uid);
            }
            var liste = notiList.ToList();
            int oldlist = liste.Count;

            bool status = false;
            var events = default(List<Notification>);
            for (int ii = 0; ii < oldlist; ii++)
            {
                Notification noti = new Notification
                {
                    Id = liste[ii].Id,
                    Visit = 1
                };

                status = NotificationController.UpdateVisit(noti);
            }

            List<Notification> notList = default(List<Notification>);
            if (level == 8)
            {
                notList = NotificationController.GetAllO(uid);
            }
            else if (level == 9)
            {
                notList = NotificationController.GetAllT(uid);

            }
            events = notList.ToList();
            return new JsonResult
            {
                Data = events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}    
       

      


      
      
        
      


     
    
    
      


 