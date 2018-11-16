using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.Reporting.WebForms;

namespace UResidence.Controllers
{
    public class ReserveController : Controller
    {
        private bool status = false;    
        public ActionResult SelectAmenity()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
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
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            Session["amenity"] = null;
            Session["calendar"] = null;
            Session["choose_date"] = null;
            Session["swimming"] = null;
            Session["choose_equipment"] = null;
            Session["summary"] = null;



            Session["drate"] = null;
            Session["NAME"] = null;
            Session["quantity"] = null;
            Session["ratee"] = null;
            Session["qa"] = null;
            Session["qc"] = null;
            Session["ar"] = null;
            Session["cr"] = null;
            Session["sd"] = null;
            Session["ed"] = null;
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            decimal balance = 0;
           
            List<ReservationList> revlist = new List<ReservationList>();
            int uid = Convert.ToInt32(Session["UID"]);
            string type = (Session["TOR"]).ToString();
            if (type == "Owner")
            {
                Owner a = new Owner();
                a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                Session["URLL"] = a.URL;
                revlist = UResidence.ReservationListController.GetAllO(a.Id);
            }
            else
            {
                Tenant t = new Tenant();
                t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                Session["URLL"] = t.URL;
                revlist = UResidence.ReservationListController.GetAllT(t.Id);
            }
            //bool successModal = false;
            try
            {
                bool m = (bool)Session["sessmodal"];
                ViewBag.successModal = m ;
                Session["sessmodal"] = null;
            }
                catch(Exception)
            {
                ViewBag.successModal = false;
            }
         
            if (revlist.Count>0)
            {
                for (int i = 0; i <= revlist.Count - 1; i++)
                {
                    balance += revlist[i].Outstanding;
                }
                if (balance > 0)
                {
                    bool ss = true;
                    ViewBag.Bal = balance;
                    ViewBag.s = ss;
                    Session["status"] = true;
                }
                else
                {
                    bool ss = false;
                    ViewBag.Bal = balance;
                    ViewBag.s = ss;
                }
            }
            else
            {
                bool ss = false;
                ViewBag.Bal = balance;
                ViewBag.s = ss;              
            }
            return View(amenityList);
        }

        public ActionResult Amenity()
        {
           
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            Session["amenity"] = null;
            Session["calendar"] = null;
            Session["choose_date"] = null;
            Session["swimming"] = null;
            Session["choose_equipment"] = null;
            Session["summary"] = null;
            decimal balance = 0;
            List<ReservationList> revlist = new List<ReservationList>();
            int uid;
            if (Convert.ToInt32(Session["Level"]) == 8)
            {
                uid = Convert.ToInt32(Session["UID"]);
            }
            else if (Convert.ToInt32(Session["Level"]) == 9)
            {
                uid = Convert.ToInt32(Session["UID"]);
            }
            else
            {
                uid = Convert.ToInt32(Session["UIDA"]);
            }
       
            string type;
            if (Convert.ToInt32(Session["Level"]) == 8)
            {
                 type = (Session["TOR"]).ToString();
            }
            else if (Convert.ToInt32(Session["Level"]) == 9)
            {
                 type = (Session["TOR"]).ToString();
            }
            else
            {
                 type = (Session["TORA"]).ToString();
            }
        
            if (type == "Owner")
            {
                Owner a = new Owner();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UIDA"].ToString());
                }
               
                Session["URLL"] = a.URL;
                revlist = UResidence.ReservationListController.GetAllO(a.Id);
            }

            else
            {
                Tenant t = new Tenant();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UIDA"].ToString());
                }
               
                Session["URLL"] = t.URL;
                revlist = UResidence.ReservationListController.GetAllO(t.Id);
            }
            
            if (revlist.Count>0)
            {
                for (int i = 0; i <= revlist.Count - 1; i++)
                {
                    balance += revlist[i].Outstanding;
                }
                if (balance > 0)
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
            else
            {
                List<Amenity> amenityList = UResidence.AmenityController.GetAll();
                return View(amenityList);
            }
        }

        [HttpPost]
        public ActionResult Amenity(FormCollection fc)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int aid = Convert.ToInt32(fc["ida"]);
            decimal arate = Convert.ToDecimal(fc["ratea"]);
            string aname = Convert.ToString(fc["namea"]);
            decimal everate= Convert.ToDecimal(fc["eve"]);
         

            Amenity amm = UResidence.AmenityController.GetbyId(aid);
            bool IsE = amm.IsEquipment;
            bool IsW = amm.IsWeekend;


            ViewBag.ise = IsE.ToString();
            ViewBag.isw = IsW.ToString();

            Session["IsEquipment"] = ViewBag.ise;
            Session["IsWeekend"] = ViewBag.isw;
            ViewBag.RateCalendar = arate;
            Session["ID"] = aid;
            Session["RATE"] = arate;
            Session["EVERATE"] = everate;
            Session["NAME"] = aname;
            if (arate == 0)
            {
                decimal child = Convert.ToDecimal(fc["ratec"]);
                decimal adult = Convert.ToDecimal(fc["rateaa"]);
                Session["child"] = child;
                Session["adult"] = adult;
            }
            Amenity amenity = UResidence.AmenityController.GetAmenityImage(aid);
            string amenitylink = amenity.Url.ToString();
            Session["AmenityURL"] = amenitylink;

            int amenityy = 1;
            Session["amenity"] = amenityy;

            return RedirectToAction("Calendar", "Reserve");

        }
        public ActionResult Calendar()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }       
         
            int calendar = 1;
            Session["calendar"] = calendar;
            if (Session["amenity"] == null)
            {
                return Redirect("~/Reserve/Amenity");
            }


            string type;
            if (Convert.ToInt32(Session["Level"]) == 8)
            {
                type = (Session["TOR"]).ToString();
            }
            else if (Convert.ToInt32(Session["Level"]) == 9)
            {
                type = (Session["TOR"]).ToString();
            }
            else
            {
                type = (Session["TORA"]).ToString();
            }
            if (type == "Owner")
            {
                Owner a = new Owner();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UIDA"].ToString());
                }
                Session["URLL"] = a.URL;              
            }

            else
            {
                Tenant t = new Tenant();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UIDA"].ToString());
                }
                Session["URLL"] = t.URL;             
            }
            Session["aa"] = 1;
            ViewBag.Amenity=(Session["NAME"]).ToString();
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);
        }
        public ActionResult Choose_Date()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }

          
            if (Session["calendar"] == null)
            {               
                return Redirect("~/Reserve/Calendar");
            }
            if (Session["amenity"] == null)
            {
                return Redirect("~/Reserve/Amenity");
            }
            if (Convert.ToInt32(Session["RATE"]) != 0)
            {

            }
            else
            {
                return Redirect("~/Reserve/Swimming");
            }
                string type;
            if (Convert.ToInt32(Session["Level"]) == 8)
            {
                type = (Session["TOR"]).ToString();
            }
            else if (Convert.ToInt32(Session["Level"]) == 9)
            {
                type = (Session["TOR"]).ToString();
            }
            else
            {
                type = (Session["TORA"]).ToString();
            }
            if (type == "Owner")
            {
                Owner a = new Owner();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UIDA"].ToString());
                }
                Session["URLL"] = a.URL;
            }

            else
            {
                Tenant t = new Tenant();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UIDA"].ToString());
                }
                Session["URLL"] = t.URL;
            }
            ViewBag.name = (Session["NAME"]).ToString();
            ViewBag.Message = Convert.ToDecimal(Session["RATE"]);
            ViewBag.EveRate = Convert.ToDecimal(Session["EVERATE"]);
            ViewBag.isw = (Session["IsWeekend"]).ToString();
            return View();
        }
        [HttpPost]
        public ActionResult Choose_Date(FormCollection fc)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int choose_date = 1;
            Session["choose_date"] = choose_date;
            //string nameamenity = (Session["NAME"]).ToString();
            if (Session["IsEquipment"].ToString() == "False")
            {
                string result = Convert.ToString(fc["result"]);
                if (result != "1")
                {
                    string sd = Convert.ToString(fc["stime"]);
                    string ed = Convert.ToString(fc["etime"]);
                    Session["sd"] = sd;
                    Session["ed"] = ed;
                    decimal drate = Convert.ToDecimal(fc["tratee"]);
                    Session["drate"] = drate;
                    int aid = Convert.ToInt32(Session["ID"]);

                    List<SchedReservation> schedList = UResidence.SchedReservationController.GetAllChoose(sd, ed, aid);
                    if (schedList.Count > 0)
                    {
                        Response.Write("<script>alert('Your chosen date and time is not available')</script>");
                        ViewBag.Message = Convert.ToDecimal(Session["RATE"]);
                        ViewBag.EveRate = Convert.ToDecimal(Session["EVERATE"]);
                        return View();
                    }
                    else
                    {
                        Response.Write("<script>alert('Successful')</script>");
                        return RedirectToAction("Summary", "Reserve");
                    }
                }
                else
                {
                    ViewBag.Message = Convert.ToDecimal(Session["RATE"]);
                    ViewBag.EveRate = Convert.ToDecimal(Session["EVERATE"]);
                    return View();
                }
            }
            else
            {
                string result = Convert.ToString(fc["result"]);
                if (result != "1")
                {
                    string sd = Convert.ToString(fc["stime"]);
                    string ed = Convert.ToString(fc["etime"]);
                    Session["sd"] = sd;
                    Session["ed"] = ed;
                    decimal drate = Convert.ToDecimal(fc["tratee"]);
                    Session["drate"] = drate;
                    int aid = Convert.ToInt32(Session["ID"]);

                    List<SchedReservation> schedList = UResidence.SchedReservationController.GetAllChoose(sd, ed, aid);
                    if (schedList.Count > 0)
                    {
                        Response.Write("<script>alert('Your chosen date and time is not available')</script>");
                        ViewBag.Message = Convert.ToDecimal(Session["RATE"]);
                        ViewBag.EveRate = Convert.ToDecimal(Session["EVERATE"]);
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
                    ViewBag.Message = Convert.ToDecimal(Session["RATE"]);
                    ViewBag.EveRate = Convert.ToDecimal(Session["EVERATE"]);
                    return View();
                }





            }
        }
        public ActionResult Choose_Equipment()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            if (Convert.ToInt32(Session["RATE"]) != 0)
            {
                if (Session["choose_date"] == null)
                {
                    return Redirect("~/Reserve/Choose_Date");
                }
            }
            else
            {
                if (Session["swimming"] == null)
                {
                    return Redirect("~/Reserve/Swimming");
                }
            }
            if (Session["calendar"] == null)
            {
                return Redirect("~/Reserve/Calendar");
            }
            if (Session["amenity"] == null)
            {
                return Redirect("~/Reserve/Amenity");
            }


            
            string type;
            if (Convert.ToInt32(Session["Level"]) == 8)
            {
                type = (Session["TOR"]).ToString();
            }
            else if (Convert.ToInt32(Session["Level"]) == 9)
            {
                type = (Session["TOR"]).ToString();
            }
            else
            {
                type = (Session["TORA"]).ToString();
            }
            if (type == "Owner")
            {
                Owner a = new Owner();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UIDA"].ToString());
                }
                Session["URLL"] = a.URL;
            }

            else
            {
                Tenant t = new Tenant();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UIDA"].ToString());
                }
                Session["URLL"] = t.URL;
            }
            ViewBag.Amenity = (Session["NAME"]).ToString();
           
                int[] eqpid;        
                int oldid = 0;
                int stock = 0;
                List<int> qid = new List<int>();
                string sd = (string)Session["sd"];
                string ed = (string)Session["ed"];
                List<Equipment> equipList = UResidence.EquipmentController.GetAll(sd, ed);
                List<object> data = new List<object>();
                if (equipList.Count > 0)
                {
                oldid = equipList[0].Id;

                for (int i = 0; i <= equipList.Count - 1; i++)
                {
                    if (equipList[i].Id == oldid)
                    {
                        stock += equipList[i].Stocks;
                        oldid = equipList[i].Id;                      
                    }                                 
                    else
                    {
                      
                        object[] d = { oldid, stock };
                        data.AddRange(d);
                        oldid = equipList[i].Id;
                        stock = 0;
                        i--;
                    }
                }

                object[] d1 = { oldid, stock };
                data.AddRange(d1);              
                }

                List<Equipment> equip = UResidence.EquipmentController.GetAll();
                List<object> model = new List<object>();
                model.Add(data.ToList());
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
        public void Choose_Equipment(int[] data, decimal[] datar)
        {
           
            string sd = (string)Session["sd"];
            string ed = (string)Session["ed"];

            int choose_equipment = 1;
            Session["choose_equipment"] = choose_equipment;

            if (data != null)
            {

                int[] quantity = data;
                Session["quantity"] = quantity;

                decimal[] ratee = datar;
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
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            try
            {
                if (Session["IsEquipment"].ToString() == "True")
                {
                    if (Session["choose_equipment"] == null)
                    {
                        return Redirect("~/Reserve/Choose_Equipment");
                    }
                }
            }
            catch (Exception)
            {
                return Redirect("~/Reserve/Choose_Equipment");
            }
            if (Convert.ToInt32(Session["RATE"]) != 0)
            {
                if (Session["choose_date"] == null)
                {
                    return Redirect("~/Reserve/Choose_Date");
                }
            }
            else
            {
                if (Session["swimming"] == null)
                {
                    return Redirect("~/Reserve/Swimming");
                }
            }
            if (Session["calendar"] == null)
            {
                return Redirect("~/Reserve/Calendar");
            }
            if (Session["amenity"] == null)
            {
                return Redirect("~/Reserve/Amenity");
            }



            string type;
            if (Convert.ToInt32(Session["Level"]) == 8)
            {
                type = (Session["TOR"]).ToString();
            }
            else if (Convert.ToInt32(Session["Level"]) == 9)
            {
                type = (Session["TOR"]).ToString();
            }
            else
            {
                type = (Session["TORA"]).ToString();
            }
            if (type == "Owner")
            {
                Owner a = new Owner();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UIDA"].ToString());
                }
                Session["URLL"] = a.URL;
            }

            else
            {
                Tenant t = new Tenant();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UIDA"].ToString());
                }
                Session["URLL"] = t.URL;
            }
            
            ViewBag.Amenity = (Session["NAME"]).ToString();
            List<Equipment> equip = UResidence.EquipmentController.GetAll();
            string sd = (string)Session["sd"];
            string ed = (string)Session["ed"];
            ViewBag.start = sd;
            ViewBag.end = ed;
            ViewBag.ratea =Session["drate"];
            ViewBag.amenname = Session["NAME"];
            decimal totalamenityrate =(decimal)Session["drate"];
            ViewBag.quan = Session["quantity"];
            ViewBag.rat = Session["ratee"];
            ViewBag.QA= Session["qa"] ;
            ViewBag.QC = Session["qc"];
            ViewBag.AR = "₱" + Session["ar"];
            ViewBag.CR = "₱" + Session["cr"];
            int[] equantity = (Int32[])Session["quantity"];
            decimal[] ratee = (Decimal[])Session["ratee"];

            if (Session["quantity"]!=null)
            {
                for(int i=0;i<=equip.Count-1;i++)
                {
                    totalamenityrate += (equantity[i]*ratee[i]);
                }               
            }    
                ViewBag.Overall = totalamenityrate;
            
            return View(equip);
        }

        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }


        [HttpPost]
        public ActionResult Summary(FormCollection fc)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int summary = 1;
            Session["summary"] = summary;
            string sd = (string)Session["sd"];
            sd += ":00.000";
            string ed = (string)Session["ed"];
            ed += ":00.000";
            decimal rate = Convert.ToDecimal(Session["drate"]);
            int uid = (int)Session["UID"];
            int aid = (int)Session["ID"];
            DateTime date = DateTime.Now;
            string sdate = date.ToString();
            Session["date"] = sdate;
            SchedReservation a = new SchedReservation
            {
                AmenityId = aid,
                StartTime = Convert.ToDateTime(sd),
                EndTIme = Convert.ToDateTime(ed),
                Rate = Convert.ToDecimal(rate),
                Date =date,
                Deleted=0

            };
            status = UResidence.SchedReservationController.Insert(a);
           
            if (status == true)
            {
                string amenityname = (Session["NAME"]).ToString();
                string qa = (string)Session["qa"];
                string qc = (string)Session["qc"];
                List<SchedReservation> b = new List<SchedReservation>();
               b = UResidence.SchedReservationController.GetAmenityNo(aid.ToString(),sd.ToString(), ed.ToString(),date);
                
                int sid =b[0].Id;
                string tor;
                int UserId;
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                     tor = (string)Session["TOR"];
                     UserId = (int)Session["UID"];
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                     tor = (string)Session["TOR"];
                     UserId = (int)Session["UID"];
                }
                else
                {
                     tor = (string)Session["TORA"];
                     UserId = (int)Session["UIDA"];
                }
             
                string fullname = "";
           
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

                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    fullname = (Session["Fullname"]).ToString();
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    fullname = (Session["Fullname"]).ToString();
                }
                else
                {
                    fullname = (Session["FULLN"]).ToString();
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
                Reservation rv=ReservationController.GetId(sid);
                if (Convert.ToInt32(Session["RATE"]) == 0)
                {
                    Swimming swim = new Swimming
                    {
                        Adult = Convert.ToInt32(qa),
                        Child = Convert.ToInt32(qc),
                        RefNo = rv.Id,
                        AmenityId = a.AmenityId
                    };
                    status=UResidence.SwimmingController.Insert(swim);
                }
                if (status == true)
                {
                    UResidence.Reservation reserve = new UResidence.Reservation();
                    reserve = UResidence.ReservationController.GetId(sid);
                    int refno = reserve.Id;
                    int[] equantity = (Int32[])Session["quantity"];
                    int[] eid = (Int32[])Session["eqpid"];
                    decimal[] ratee = (Decimal[])Session["ratee"];
                    if (equantity != null)
                    {
                        for (int i = 0; i <= equantity.Count() - 1; i++)
                        {

                            if (Convert.ToInt32(equantity[i]) != 0)
                            {
                                EquipReservation er = new EquipReservation
                                {

                                    EquipmentId = Convert.ToInt32(eid[i]),
                                    Quantity = Convert.ToInt32(equantity[i]),
                                    RefNo = refno,
                                    Rate = Convert.ToDecimal(ratee[i]) * Convert.ToInt32(equantity[i]),
                                };

                                status = UResidence.EquipReservationController.Insert(er);
                            }

                        }
                     
                    }
                }
            }
            if(status==true)
            {
                Session["sessmodal"] = true;
               
            }
            if (Convert.ToInt32(Session["Level"]) == 8)
            {

                return RedirectToAction("Home", "Reserve");
            }
            else if (Convert.ToInt32(Session["Level"]) == 9)
            {
                return RedirectToAction("Home", "Reserve");
            }
            else
            { 
                return RedirectToAction("SelectOT", "ReservationA");
            }
     

        }

        public ActionResult GuestAdd()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            Session["drate"] = null;
            Session["NAME"] = null;
            Session["quantity"] = null;
            Session["ratee"] = null;
            Session["qa"] = null;
            Session["qc"] = null;
            Session["ar"] = null;
            Session["cr"] = null;
            Session["sd"] = null;
            Session["ed"] = null;
            string bn = Convert.ToString(Session["BLDG"]);
            string un = Convert.ToString(Session["UNO"]);
            ViewBag.Bldg =bn;
            ViewBag.UnitNo = un;
            ViewBag.purp = "";
            ViewBag.date = DateTime.Now.ToString("yyyy-MM-dd");
            List<Logbook> list = new List<Logbook>();
            list = LogbookController.GET_ALL(DateTime.Now,bn,un);
            return View(list);
        }
        [HttpPost]
        public ActionResult GuestAdd(FormCollection fc, HttpPostedFileBase logbookpic)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string bn = Convert.ToString(Session["BLDG"]);
            string un = Convert.ToString(Session["UNO"]);
            DateTime date = Convert.ToDateTime(fc["gdate"]);
            string purpose = Convert.ToString(fc["purpose"]);
            string vname = Convert.ToString(fc["vname"]);
            bool status = false;
            string name = Session["FULLNAME"].ToString();


            var image = logbookpic;

            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/LogBookImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {

                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/LogBookImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/LogBookImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {

                        string folderpath1 = "~/Content/LogBookImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }
                    status = true;


                    Logbook log = new Logbook
                    {
                        date = date,
                        VisitorName = vname,
                        ResidentName = name,
                        Purpose = purpose,
                        BuildingNo = bn,
                        UnitNo = un,
                        Timein = Convert.ToDateTime("00:00:00"),
                        Timeout = Convert.ToDateTime("00:00:00"),
                        URL = finalpath
                    };
                    status = LogbookController.Insert(log);
                    ViewBag.purp = purpose;
                    ViewBag.date = date.ToString("yyyy-MM-dd");
                    ViewBag.Bldg = bn;
                    ViewBag.UnitNo = un;

                }

            }
            else
            {
                Logbook log = new Logbook
                {
                    date = date,
                    VisitorName = vname,
                    ResidentName = name,
                    Purpose = purpose,
                    BuildingNo = bn,
                    UnitNo = un,
                    Timein = Convert.ToDateTime("00:00:00"),
                    Timeout = Convert.ToDateTime("00:00:00"),
                    URL = "~/Content/LogBookImages/user.png"
                };
                status = LogbookController.Insert(log);
                ViewBag.purp = purpose;
                ViewBag.date = date.ToString("yyyy-MM-dd");
                ViewBag.Bldg = bn;
                ViewBag.UnitNo = un;
            }
            List<Logbook> list = new List<Logbook>();
            list = LogbookController.GET_ALL(date, bn, un);
            return View(list);
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
            List<ReservationProcess> reservationList = default(List<ReservationProcess>);
          int id = Convert.ToInt32(Session["UID"]);
            string tor = Convert.ToString(Session["TOR"]);

            if (tor == "Owner") { 
             reservationList = ReservationProcessController.GET_ALLO(id);
            }
        else
            {
               reservationList = ReservationProcessController.GET_ALLT(id);
            }
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
     
        public ActionResult Swimming()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            
            if (Session["calendar"] == null)
            {
                return Redirect("~/Reserve/Calendar");
            }
            if (Session["amenity"] == null)
            {
                return Redirect("~/Reserve/Amenity");
            }
            if (Convert.ToInt32(Session["RATE"]) != 0)
            {
                return Redirect("~/Reserve/Choose_Equipment");
            }
            else
            {
               
            }

            ViewBag.isw = (Session["IsWeekend"]).ToString();
            string type;
            if (Convert.ToInt32(Session["Level"]) == 8)
            {
                type = (Session["TOR"]).ToString();
            }
            else if (Convert.ToInt32(Session["Level"]) == 9)
            {
                type = (Session["TOR"]).ToString();
            }
            else
            {
                type = (Session["TORA"]).ToString();
            }
            if (type == "Owner")
            {
                Owner a = new Owner();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                }
                else
                {
                    a = UResidence.OwnerController.GetIdOwner(Session["UIDA"].ToString());
                }
                Session["URLL"] = a.URL;
            }

            else
            {
                Tenant t = new Tenant();
                if (Convert.ToInt32(Session["Level"]) == 8)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else if (Convert.ToInt32(Session["Level"]) == 9)
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                }
                else
                {
                    t = UResidence.TenantController.GetIdTenant(Session["UIDA"].ToString());
                }
                Session["URLL"] = t.URL;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Swimming(FormCollection fc)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int swimming = 1;
            Session["swimming"] = swimming;
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
            string drate = (fc["rate"]).Replace("₱", "");
            decimal drate1 = Convert.ToDecimal(drate);
            Session["drate"] = drate1;

            if (drate == "" || drate == "0") {
                return View();
            }

            int aid = Convert.ToInt32(Session["ID"]);

            if (Session["IsEquipment"].ToString() == "False")
            {
                CheckSwimming cs = new CheckSwimming();
                cs = CheckSwimmingController.Get(sd, aid);
                if (cs == null)
                {
                    return RedirectToAction("Summary", "Reserve");
                }
                else
                {
                    if (cs.Capacity > 0)
                    {
                        Response.Write("<script>alert('Successful')</script>");
                        return RedirectToAction("Summary", "Reserve");

                    }
                    else
                    {
                        Response.Write("<script>alert('Your chosen date and time is not available')</script>");
                        ViewBag.Message = Convert.ToInt32(Session["RATE"]);
                    }
                }
            }
            else
            {
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
            }
          
            return View();
        }


        public ActionResult CalendarViewOT()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }

            Session["amenity"] = null;
            Session["calendar"] = null;
            Session["choose_date"] = null;
            Session["swimming"] = null;
            Session["choose_equipment"] = null;
            Session["summary"] = null;

            Session["drate"] = null;
            Session["NAME"] = null;
            Session["quantity"] = null;
            Session["ratee"] = null;
            Session["qa"] = null;
            Session["qc"] = null;
            Session["ar"] = null;
            Session["cr"] = null;
            Session["sd"] = null;
            Session["ed"] = null;
            string type = (Session["TOR"]).ToString();
            if (type == "Owner")
            {
                Owner a = new Owner();
                a = UResidence.OwnerController.GetIdOwner(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }

            else
            {
                Tenant t = new Tenant();
                t = UResidence.TenantController.GetIdTenant(Session["UID"].ToString());
                Session["URLL"] = t.URL;
            }
            Session["aa"] = 1;
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);
        }
        public ActionResult DownloadReservation(int refno1)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            Session["drate"] = null;
            Session["NAME"] = null;
            Session["quantity"] = null;
            Session["ratee"] = null;
            Session["qa"] = null;
            Session["qc"] = null;
            Session["ar"] = null;
            Session["cr"] = null;
            Session["sd"] = null;
            Session["ed"] = null;
            string tor = Session["TOR"].ToString();
            List<ReportReservation> data = default(List<ReportReservation>);
            List<EquipReservation> data1 = default(List<EquipReservation>);
            List<Swimming> data2 = default(List<Swimming>);
            if (tor == "Owner")
            {

                data = UResidence.ReportReservationAmenityController.GETO(refno1);
                data1 = UResidence.EquipReservationController.Getr(refno1);
                data2 = SwimmingController.GETR(refno1); 
            }
            else
            {
                data = UResidence.ReportReservationAmenityController.GETT(refno1);
                data1 = UResidence.EquipReservationController.Getr(refno1);
                data2 = SwimmingController.GETR(refno1);
            }
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Views/Report/ReservationFormO.rdlc");
            ReportDataSource rd1 = new ReportDataSource();
            ReportDataSource rd2 = new ReportDataSource();
            ReportDataSource rd = new ReportDataSource();

            rd.Name = "ReservationO";
            rd.Value = data.ToList();
            localreport.DataSources.Add(rd);
            rd1.Name = "EquipReserve";
            rd1.Value = data1.ToList();
            localreport.DataSources.Add(rd1);
            rd2.Name = "PersonRate";
            rd2.Value = data2.ToList();
            localreport.DataSources.Add(rd2);
            string reportType = "PDF";
            string mimetype;
            string encoding;
            string filenameExtension = "pdf";
            string[] streams;
            Warning[] warnings;
            byte[] renderbyte;
            string deviceInfo = "<DeviceInfo><OutputFormat>PDF</OutputFormat><PageWidth>8.5in</PageWidth><PageHeight>11in</PageHeight><MarginTop>0.5in</MarginTop><MarginLeft>11in</MarginLeft><MarginRight>11in</MarginRight><MarginBottom>0.5in</MarginBottom></DeviceInfo>";
            renderbyte = localreport.Render(reportType, deviceInfo, out mimetype, out encoding, out filenameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename=ReservationForm." + filenameExtension);
            return File(renderbyte, filenameExtension);
          
        }
   
        public ActionResult AboutUs()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            Session["drate"] = null;
            Session["NAME"] = null;
            Session["quantity"] = null;
            Session["ratee"] = null;
            Session["qa"] = null;
            Session["qc"] = null;
            Session["ar"] = null;
            Session["cr"] = null;
            Session["sd"] = null;
            Session["ed"] = null;
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
        public JsonResult AddGuest(Logbook data)
        {
            string bn = Convert.ToString(Session["BLDG"]);
            string un = Convert.ToString(Session["UNO"]);
            bool status = false;
            string name = Session["FULLNAME"].ToString();
            Logbook log = new Logbook
            {
                date=data.date,
                VisitorName = data.VisitorName,
                ResidentName = name,
                Purpose=data.Purpose,
                BuildingNo= bn,
                UnitNo=un,
                Timein = Convert.ToDateTime("00:00:00"),
                Timeout = Convert.ToDateTime("00:00:00"),
                URL= "~/Content/LogBookImages/user.png"
            };
            status = LogbookController.Insert(log);
            List<Logbook> list = new List<Logbook>();
            list = LogbookController.GET_ALL(DateTime.Now, bn, un);
            var data1 = list.ToList();
            ViewBag.purp = data.Purpose;
            ViewBag.date = data.date;
            ViewBag.Bldg = bn;
            ViewBag.UnitNo = un;
            return new JsonResult { Data = data1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ViewGuest(DateTime date)
        {
            string bn = Convert.ToString(Session["BLDG"]);
            string un = Convert.ToString(Session["UNO"]);
            List<Logbook> list = new List<Logbook>();
            list = LogbookController.GET_ALL(date, bn, un);
            var data = list.ToList();
            return new JsonResult {Data=data,JsonRequestBehavior=JsonRequestBehavior.AllowGet };
        }
        public JsonResult UpdateGuest(Logbook data)
        {
            Logbook log = new Logbook
            {
               
                VisitorName = data.VisitorName,
                Id=data.Id
            };
            var data1 = LogbookController.UpdateName(log);
            return new JsonResult { Data = data1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult DeleteGuest(int id)
        {        
            var data1 = LogbookController.Delete(id);
            return new JsonResult { Data = data1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}    
       

      


      
      
        
      


     
    
    
      


 