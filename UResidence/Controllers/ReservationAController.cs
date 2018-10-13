﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace UResidence.Controllers
{
    public class ReservationAController : Controller
    {
        // GET: ReservationAdmin
        private bool status = false;
        public ActionResult SelectAmenity()
        {
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
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
      

        public ActionResult Amenity()
        {
            //int balance = 0;
            //List<Billing> billing = new List<Billing>();
            //int uid = Convert.ToInt32(Session["UIDA"]);
            //string type = (Session["TORA"]).ToString();
            //if (type == "Owner")
            //{
            //    billing = UResidence.BillingController.GetOwner(uid);
            //}
            //else
            //{
            //    billing = UResidence.BillingController.GetTenant(uid);
            //}
            //for (int i = 0; i <= billing.Count - 1; i++)
            //{
            //    balance += ((billing[i].Rate + billing[i].Charge + billing[i].ChairCost + billing[i].TableCost) - (billing[i].Totale - billing[i].Amount));
            //}
            //if (balance > 0)
            //{
            //    Session["status"] = true;
            //    //return Content("<script language='javascript' type='text/javascript'>alert('There is still remaining balance of: " + balance + "');</script>");
            //    ViewBag.Message = string.Format("There is still remaining balance of: ₱" + balance);
            //    return View();

            //}
            //else
            //{
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
                return View(amenityList);
            //}
        }

        [HttpPost]
        public ActionResult Amenity(FormCollection fc)
        {
            int aid = Convert.ToInt32(fc["ida"]);
            int arate = Convert.ToInt32(fc["ratea"]);
            string aname = Convert.ToString(fc["namea"]);
            int everate = Convert.ToInt32(fc["eve"]);

            Session["ID"] = aid;
            Session["RATE"] = arate;
            Session["EVERATE"] = everate;
            Session["NAME"] = aname;
            if (aname.ToUpper().Contains("SWIMMING"))
            {
                int child = Convert.ToInt32(fc["ratec"]);
                int adult = Convert.ToInt32(fc["rateaa"]);
                Session["child"] = child;
                Session["adult"] = adult;
            }
            Amenity amenity = UResidence.AmenityController.GetAmenityImage(aid);
            string amenitylink = amenity.Url.ToString();
            Session["AmenityURL"] = amenitylink;


            return RedirectToAction("Calendar", "ReservationA");

        }


        public ActionResult Calendar()
        {
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            ViewBag.Amenity = (Session["NAME"]).ToString();
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);         
        }


        public ActionResult Choose_Date()
        {
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            ViewBag.name = (Session["NAME"]).ToString();
            ViewBag.Message = Convert.ToInt32(Session["RATE"]);
            ViewBag.EveRate = Convert.ToInt32(Session["EVERATE"]);
            return View();
        }


        [HttpPost]
        public ActionResult Choose_Date(FormCollection fc)
        {
            string nameamenity = (Session["NAME"]).ToString();
            if (nameamenity.ToUpper() != "BASKETBALL COURT")
            {
                string result = Convert.ToString(fc["result"]);
                if (result != "1")
                {
                    string sd = fc["stime"];
                string ed = fc["etime"];
                Session["sd"] = sd;
                Session["ed"] = ed;
                string drate = fc["tratee"];
                Session["drate"] = drate;
                int aid = Convert.ToInt32(Session["ID"]);

                List<SchedReservation> schedList = UResidence.SchedReservationController.GetAllChoose(sd, ed, aid);
                if (schedList.Count > 0)
                {
                    Response.Write("<script>alert('Your chosen date and time is not available')</script>");
                    ViewBag.Message = Convert.ToInt32(Session["RATE"]);
                    ViewBag.EveRate = Convert.ToInt32(Session["EVERATE"]);
                    return View();
                }
                else
                {
                    Response.Write("<scripxt>alert('Successful')</script>");
                    return RedirectToAction("Choose_Equipment", "ReservationA");
                }
                 }
                else
                {
                    ViewBag.Message = Convert.ToInt32(Session["RATE"]);
                    ViewBag.EveRate = Convert.ToInt32(Session["EVERATE"]);
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
                string drate = fc["tratee"];
                Session["drate"] = drate;
                int aid = Convert.ToInt32(Session["ID"]);

                List<SchedReservation> schedList = UResidence.SchedReservationController.GetAll(sd, ed, aid);
                if (schedList.Count > 0)
                {
                    Response.Write("<script>alert('Your chosen date and time is not available')</script>");
                    ViewBag.Message = Convert.ToInt32(Session["RATE"]);
                        ViewBag.EveRate = Convert.ToInt32(Session["EVERATE"]);
                        return View();
                }
                else
                {
                    Response.Write("<script>alert('Successful')</script>");
                    return RedirectToAction("Summary", "ReservationA");
                }
                }
                else
                {
                    ViewBag.Message = Convert.ToInt32(Session["RATE"]);
                    ViewBag.EveRate = Convert.ToInt32(Session["EVERATE"]);
                    return View();
                }
            }
        }
     

        public ActionResult Choose_Equipment()
        {
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
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
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
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
            ViewBag.QA = Session["qa"];
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
            string sdate = String.Format("{0:d/M/yyyy HH:mm:ss}", date);
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
                b = UResidence.SchedReservationController.GetAmenityNo(aid.ToString(), sd, ed, Convert.ToDateTime(sdate));

                int sid = b.Id;
                string tor = (string)Session["TORA"];
                int UserId = (int)Session["UIDA"];
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
                fullname = (Session["FULLN"]).ToString();

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
                Reservation rv = ReservationController.GetId(sid);
                if (amenityname.ToUpper().Contains("SWIMMING"))
                {
                    Swimming swim = new Swimming
                    {
                        Adult = Convert.ToInt32(qa),
                        Child = Convert.ToInt32(qc),
                        RefNo = rv.Id,
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
                    return RedirectToAction("Home", "Admin");

        }



        public JsonResult GetEvents()
        {
            string name = (Session["NAME"]).ToString();
            List<SchedReservation> schedList = UResidence.SchedReservationController.GetAllA(name);
            var events = schedList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }




        public ActionResult Swimming()
        {
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            ViewBag.Message = Convert.ToInt32(Session["RATE"]);
            return View();
        }



        [HttpPost]
        public ActionResult Swimming(FormCollection fc)
        {
            string qa = fc["adult"];
            string qc = fc["child"];
            string ar = fc["rateadult"];
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


            if (drate == "" || drate == "0")
            {
                return View();
            }

            int aid = Convert.ToInt32(Session["ID"]);


            CheckSwimming cs = new CheckSwimming();
            cs = CheckSwimmingController.Get(sd, aid);
            if (cs == null)
            {
                return RedirectToAction("Choose_Equipment", "ReservationA");
            }
            else
            {
                if (cs.Capacity > 0)
                {
                    Response.Write("<script>alert('Successful')</script>");
                    return RedirectToAction("Choose_Equipment", "ReservationA");

                }
                else
                {
                    Response.Write("<script>alert('Your chosen date and time is not available')</script>");
                    ViewBag.Message = Convert.ToInt32(Session["RATE"]);
                }
            }
            return View();
        }


        public ActionResult SelectOT()
        {
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);
        }

        [HttpPost]
        public ActionResult SelectOT(string bldgno, string unitno, int level)
        {
         
            if (level == 4)
            {
                try
                {
                    Owner ownerList = UResidence.OwnerController.GetOwnerReservee(bldgno, unitno);
                    string fname = ownerList.Fname;
                    string mname = ownerList.Mname;
                    string lname = ownerList.Lname;
                    string fullname = fname + ' ' + mname + ' ' + lname;
                    Session["FULLN"] = fullname;
                    Session["TORA"] = "Owner";
                    Session["UIDA"] = ownerList.Id;
                   


                    int balance = 0;
                    List<Billing> billing = new List<Billing>();
                    int uid = Convert.ToInt32(Session["UIDA"]);
                    string type = (Session["TORA"]).ToString();
                    if (type == "Owner")
                    {
                        billing = UResidence.BillingController.GetOwner(uid);
                    }
                    else
                    {
                        billing = UResidence.BillingController.GetTenant(uid);
                    }
                    for (int i = 0; i <= billing.Count - 1; i++)
                    {
                        balance += ((billing[i].Rate + billing[i].Charge + billing[i].ChairCost + billing[i].TableCost) - (billing[i].Totale - billing[i].Amount));
                    }
                    if (balance > 0)
                    {
                        Session["status"] = true;
                        //return Content("<script language='javascript' type='text/javascript'>alert('There is still remaining balance of: " + balance + "');</script>");
                        int data;
                        return Json(data = balance );

                    }
                    else
                    {
                        bool data;
                        return Json(data = true);
                    }




                           
                }
                catch (InvalidOperationException)
                {
                    string data;
                    return Json(data = "Owner");
                }



            }
            else if (level == 5)
            {


                try
                {
                    Tenant tenantList = UResidence.TenantController.GetTenantReserve(bldgno, unitno);
                    string fname = tenantList.Fname;
                    string mname = tenantList.Mname;
                    string lname = tenantList.Lname;
                    string fullname = fname + ' ' + mname + ' ' + lname;
                    Session["FULLN"] = fullname;
                    Session["TORA"] = "Tenant";
                    Session["UIDA"] = tenantList.Id;


                    Tenant a = new Tenant();
                    a = UResidence.TenantController.GetIdTenant(tenantList.Id.ToString());
                    if (a.LeaseEnd < DateTime.Now)
                    {
                        Tenant t = new Tenant
                        {
                            Deleted = "1",
                            Id = tenantList.Id
                        };
                        UResidence.TenantController.UpdateDelete(t);
                        string data;
                        return Json(data = "Tenant");
                    }

                    else
                    { 
                    int balance = 0;
                    List<Billing> billing = new List<Billing>();
                    int uid = Convert.ToInt32(Session["UIDA"]);
                    string type = (Session["TORA"]).ToString();
                    if (type == "Owner")
                    {
                        billing = UResidence.BillingController.GetOwner(uid);
                    }
                    else
                    {
                        billing = UResidence.BillingController.GetTenant(uid);
                    }
                    for (int i = 0; i <= billing.Count - 1; i++)
                    {
                        balance += ((billing[i].Rate + billing[i].Charge + billing[i].ChairCost + billing[i].TableCost) - (billing[i].Totale - billing[i].Amount));
                    }
                    if (balance > 0)
                    {
                        Session["status"] = true;
                        //return Content("<script language='javascript' type='text/javascript'>alert('There is still remaining balance of: " + balance + "');</script>");
                        int data;
                        return Json(data = balance);
                    }
                    else
                    {
                        bool data;
                        return Json(data = true);
                    }
                }
                }
                catch (InvalidOperationException)
                {
                    string data;
                    return Json(data = "Tenant");
                }


            }
            return View();
        }

      
        public ActionResult CalendarReservationAdmin() {
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);
        }


        public JsonResult GetEventsA()
        {
            List<ReservationProcess> reservationList = ReservationProcessController.GET_ALLReserved();
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult UpdateStatus(int refno1, string rstatus1)
        {
            string name = (Session["FullName"]).ToString();
            bool status = false;

                Reservation reservation = new Reservation
                {
                    Status = rstatus1,
                    Id = refno1,
                };
                status = ReservationController.Update(reservation);
            
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


        public JsonResult UpdateImage(Admin adm)
        {
            var image = adm.Image;
            bool status = false;
            int id = adm.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/ReservationAImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/ReservationAImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/ReservationAImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/ReservationAImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }

                    Admin a = new Admin
                    {
                        Id = id,
                        URL = finalpath
                    };


                    status = UResidence.AdminController.UpdateDP(a);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


    }
}