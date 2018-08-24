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
            List<ReservationProcess> reservationList = ReservationProcessController.GET_ALL();
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }


        public JsonResult InsertPayment(Receipt receipt)
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

        public JsonResult UpdatePayment(int charge1, int refno1, string rstatus1)
        {

            bool status = false;
            Receipt receipt = new Receipt
            {
                Charge = charge1,
                RefNo = refno1
            };
            status = ReceiptController.UpdateCharge(receipt);
            if (status == true)
            {
                Reservation reservation = new Reservation
                {
                    Status = rstatus1,
                    Id = refno1,
                };
                status = ReservationController.Update(reservation);
            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public JsonResult ReceiptHistory(int refno1)
        {
            List<Receipt> receipt = ReceiptController.GetAll(refno1);
            var events = receipt.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Calendar
        public ActionResult CalendarView()
        {
            return View();
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
                        Charge = cg,
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