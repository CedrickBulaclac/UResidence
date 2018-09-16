using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class ReservationRecordController : Controller
    {
        // GET: ReservationRecord
        public ActionResult Record()
        {
            return View();
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
        public JsonResult InsertNotif(Notification not)
        {
            bool status = false;
            if (RemoveWhitespace(not.type) == "Owner")
            {
                Notification notilist = new Notification
                {
                    Description = not.Description,
                    Visit = 0,
                    Date = DateTime.Now,
                    OwnerId=not.OwnerId
                };
                status = NotificationController.InsertO(notilist);
            }
            else
            {
                Notification notilist = new Notification
                {
                    Description = not.Description,
                    Visit = 0,
                    Date = DateTime.Now,
                    TenantId = not.OwnerId
                };
                status = NotificationController.InsertT(notilist);
            }
            return new JsonResult
            {
                Data=status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
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
        public JsonResult GetEvents()
        {
            List<ReservationList> reservationList = ReservationListController.GetAllO();            
            var events = reservationList.ToList();
            return new JsonResult
            {
                Data=events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }
        public JsonResult Get(int Level,string Search)
        {
            List<ReservationList> reservationList = default(List<ReservationList>);
            if (Level == 0)
            {
                reservationList = ReservationListController.GetAllA(Search);
            }
            else if(Level==1)
            {
                reservationList = ReservationListController.GetAllByDate(Search);
            }
            else if (Level==2)
            {
                reservationList = ReservationListController.GetAllO(Search);
            }
            else if (Level == 3)
            {
                reservationList = ReservationListController.GetAllT(Search);
            }
            var events = reservationList.ToList();
            return new JsonResult
            {
                Data = events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }
     
    }
}