using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class BillingController : Controller
    {
        // GET: Billing
        public ActionResult BillingView()
        {
            return View();
        }
        public JsonResult GetEvents()
        {
            string uid = (Session["UID"]).ToString();
            List<ReservationProcess> reservationList = ReservationProcessController.GETById(uid);
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }
        public JsonResult ReceiptHistory(int refno1)
        {
            List<Receipt> receipt = ReceiptController.GetAll(refno1);
            var events = receipt.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}