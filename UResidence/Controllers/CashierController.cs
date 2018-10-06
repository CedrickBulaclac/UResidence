using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class CashierController : Controller
    {
        // GET: Cashier
        public ActionResult Calendar()
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
        public JsonResult ReceiptHistory(int refno1)
        {
            List<Receipt> receipt = ReceiptController.GetAll(refno1);
            var events = receipt.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}