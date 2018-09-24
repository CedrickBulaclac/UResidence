using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.IO;
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
           
            var events = reservationList.ToList();
            return new JsonResult
            {
                Data = events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }
        public ActionResult Download(int Level, string Search)
        {
           
            ReportDocument rd = new ReportDocument();
            List<ReservationList> data = default(List<ReservationList>);
            if (Level == 3)
            {
                rd.Load(Path.Combine(Server.MapPath("~/Views/Report"), "ReservationList.rpt"));
                data = ReservationListController.GetAllO();
               
            }
            else
            {

                    rd.Load(Path.Combine(Server.MapPath("~/Views/Report"), "ReservationList.rpt"));
                    if (Level == 0)
                    {
                        data = ReservationListController.GetAllA(Search);
                    }
                    else if (Level == 1)
                    {
                        data = ReservationListController.GetAllByDate(Search);
                    }
                    else if (Level == 2)
                    {
                        data = ReservationListController.GetAllO(Search);
                    }
             
            }
            rd.SetDataSource(data.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();

                return File(stream, "application/pdf", "ReservationList.pdf");



            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}