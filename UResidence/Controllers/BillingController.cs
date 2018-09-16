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
            int balance = 0;
            List<Billing> billing = default(List<Billing>);
            List<BillingList> ret = default(List<BillingList>);
            int uid = Convert.ToInt32(Session["UID"]);
            string type = (Session["TOR"]).ToString();
            if (type == "Owner")
            {
                billing = UResidence.BillingController.GetOwner(uid);
                ret = UResidence.BillingListController.GetAllO(uid);
            }
            else
            {
                billing = UResidence.BillingController.GetTenant(uid);
                ret = UResidence.BillingListController.GetAllT(uid);
            }
            for (int i = 0; i <= billing.Count - 1; i++)
            {
                balance += ((billing[i].Rate + billing[i].Charge + billing[i].ChairCost + billing[i].TableCost) - (billing[i].Totale - billing[i].Amount));
            }
            ViewBag.Balance = balance;
           
            
            
            return View(ret);
        }
        public JsonResult GetEvents(ReservationProcess data)
        {
            string uid = (Session["UID"]).ToString();
            List<ReservationProcess> reservationList = ReservationProcessController.GETById(uid,data.RFId);
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