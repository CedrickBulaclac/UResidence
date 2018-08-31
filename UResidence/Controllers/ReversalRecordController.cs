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
    }
}