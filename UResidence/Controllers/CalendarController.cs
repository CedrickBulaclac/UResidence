using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class CalendarController : Controller
    {

        public JsonResult GetEvents()
        {
            string status = "Pending";
            List<ReservationProcess> reservationList = ReservationProcessController.GET_ALL();
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult GET_EVENTS(ReservationProcess rese)
        {
            List<ReservationProcess> reservationList = ReservationProcessController.GETALL(rese.Status);
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult Search(ReservationProcess rese)
        {
            List<ReservationProcess> reservationList = ReservationProcessController.GET_ALL(rese.RFId);
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult InsertPayment(Receipt receipt)
        {
            if (receipt.Description != null && receipt.Description != "")
            {

                bool status = false;
                status = ReceiptController.Insert(receipt);
                if (status == true)
                {
                    Reservation reservation = new Reservation
                    {
                        Status = "Reserved",
                        Id = receipt.RefNo,
                    };
                    status = ReservationController.Update(reservation);
                }
                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            else
            {
                bool status = false;
                return new JsonResult { Data = status};
            }
        }
        public JsonResult UpdatePayment(int charge1, int refno1, string rstatus1,string desc,Notification data)
        {
            string name = (Session["FullName"]).ToString();
            bool status = false;
            Charge charge = new Charge
            {
                charge = charge1,
                Refno = refno1,
                Date=DateTime.Now,
                CreatedBy=name,
                Description=desc
            };
            status = ChargeController.Insert(charge);
            if (status == true)
            {
                Reservation reservation = new Reservation
                {
                    Status = rstatus1,
                    Id = refno1,
                };
                status = ReservationController.Update(reservation);
                if(status==true)
                {
                    if (data.type == "Owner")
                    {
                        Notification not = new Notification
                        {
                            Description = data.Description,
                            Visit = 0,
                            OwnerId = data.OwnerId,
                            Date = DateTime.Now
                        };
                        status = NotificationController.InsertO(not);
                    }
                    else
                    {
                        Notification not = new Notification
                        {
                            Description = data.Description,
                            Visit = 0,
                            TenantId = data.OwnerId,
                            Date = DateTime.Now
                        };
                        status = NotificationController.InsertT(not);
                    }
                }
                    
            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult AddReversal(Reversal data)
        {
            bool status = false;
            string name = (Session["Fullname"]).ToString();
            if (data.Description != null && data.Description != "")
            {
                List<Reversal> re = default(List<Reversal>);
                re = UResidence.ReversalController.GET_ALL(data.RefNo);
                if (re.Count == 0)
                {
                    Reversal reversal = new Reversal
                    {
                        RefNo = data.RefNo,
                        Amount = data.Amount,
                        Description = data.Description,
                        Status = data.Status,
                        CreatedBy = name,
                        ApprovedBy = "None"
                    };
                    status = ReversalController.Insert(reversal);
                    Reservation rese = new Reservation
                    {
                        Status = "Cancelled",
                        Id=data.RefNo
                    };
                    status = ReservationController.Update(rese);
                    return new JsonResult
                    {
                        Data = status,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    string b = "failed";
                    return new JsonResult
                    {
                        Data = b,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            else
            {
                 status = false;
                return new JsonResult { Data = status };
            }

        }
        public JsonResult SwimmingInfo(int refno1)
        {
            List<Swimming> swimming = SwimmingController.GETALL(refno1);
            var events = swimming.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ReceiptHistory(int refno1)
        {
            List<Receipt> receipt = ReceiptController.GetAll(refno1);
            var events = receipt.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ReversalHistory(int refno1)
        {
            List<Reversal> reversal = ReversalController.GET_ALL(refno1);
            var events = reversal.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult ChargeHistory(int refno1)
        {
            List<Charge> charge = ChargeController.GetAll(refno1);
            var events = charge.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        // GET: Calendar
        public ActionResult CalendarView()
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
        public ActionResult CalendarView(FormCollection fc, string mySelect, int dp)
        {
            int rid = Convert.ToInt32(fc["rid"]);
            int rfid = Convert.ToInt32(fc["rfid"]);
            int down = dp;
            int fp = Convert.ToInt32(fc["fp"]);
            int cg = Convert.ToInt32(fc["cg"]);
            string comment = Convert.ToString(fc["description"]);
            string status = mySelect;

            bool stats = false;
            List<ReservationProcess> reservationList = ReservationProcessController.GET_ALL();
            {

                if (rid > 0)
                {
                    //UPDATE EVENT
                    //var v = reservationList.Where(a => a.RId ==rid).FirstOrDefault();


                    Receipt r = new Receipt
                    {
                        ORNo = rid,
                        Totalpayment = dp,
                        Date = DateTime.Today,
                        Description = comment
                    };
                    stats = UResidence.ReceiptController.Update(r);
                    if (stats == true)
                    {
                        Reservation rv = new Reservation
                        {
                            Status = status,
                            Id = rfid,
                        };
                        stats = UResidence.ReservationController.Update(rv);
                    }
                }
                return CalendarView();
            }
        }

    }
}