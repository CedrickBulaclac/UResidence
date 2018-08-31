using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class ReversalRecordController : Controller
    {
        // GET: ReversalRecord
        public ActionResult Record()
        {
            return View();
        }
        public ActionResult Records()
        {
            return View();
        }
        public JsonResult GetModal(int refno)
        {
            List<ReservationProcess> reservationList = ReservationProcessController.GET_ALL(refno);
            var events = reservationList.ToList();

            return new JsonResult
            {
                Data = events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }


        public JsonResult GetP()
        {
            List<ReversalList> revlist = default(List<ReversalList>);
            revlist = ReversalListController.GET_ALLP();
            return new JsonResult
            {
                Data = revlist,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetA()
        {
            List<ReversalList> revlist = default(List<ReversalList>);
            revlist = ReversalListController.GET_ALLA();
            return new JsonResult
            {
                Data = revlist,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetD()
        {
            List<ReversalList> revlist = default(List<ReversalList>);
            revlist = ReversalListController.GET_ALLD();
            return new JsonResult
            {
                Data = revlist,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetC()
        {
            List<ReversalList> revlist = default(List<ReversalList>);
            revlist = ReversalListController.GET_ALLC();
            return new JsonResult
            {
                Data = revlist,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult Update(Reversal data)
        {
            bool status = false;
            Reversal rev = new Reversal
            {
                Description=data.Description,
                Amount=data.Amount,
                Id=data.Id,
                Status="Pending"
             };
            status = ReversalController.Update(rev);
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult UpdateStatus(Reversal data)
        {
            bool status = false;
            string fullname = (Session["Fullname"]).ToString();
            Reversal rev = new Reversal
            {
                
                Id = data.Id,
                Status = data.Status,
                ApprovedBy= fullname
            };
            status = ReversalController.UpdateA(rev);
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
    }
}