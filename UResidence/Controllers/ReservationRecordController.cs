using Microsoft.Reporting.WebForms;
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
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            return View();
        }
        public JsonResult GET_ERESERVE(int refno)
        {
            List<EquipReservation> er = default(List<EquipReservation>);
            er = UResidence.EquipReservationController.Getr(refno);
            var data = er.ToList();
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
        public JsonResult InsertNotif(Notification not)
        {
            bool status = false;
            if (RemoveWhitespace(not.typer) == "Owner")
            {
                Notification notilist = new Notification
                {
                    Rate=not.Rate,
                    refno=not.refno,
                    Visit = 0,
                    Date = DateTime.Now,
                    OwnerId=not.OwnerId,
                    Type = not.Type
                };
                status = NotificationController.InsertO(notilist);
            }
            else
            {
                Notification notilist = new Notification
                {
                    Rate = not.Rate,
                    refno = not.refno,                    
                    Visit = 0,
                    Date = DateTime.Now,
                    TenantId = not.OwnerId,
                    Type =not.Type
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
            List<EquipReservation> er = UResidence.EquipReservationController.Getr(refno);
            List<Swimming> sr = UResidence.SwimmingController.GETR(refno);
            var events = Json(new { Reservation = reservationList.ToList(), Equipment = er.ToList(),Swimming=sr.ToList()});

            return new JsonResult
            {
                Data = events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }
        public JsonResult GetEvents()
        {
            List<object> mo = new List<object>();
            List<ReservationList> reservationList = ReservationListController.GetAllO();                      
            List<EquipReservation> er = default(List<EquipReservation>);
            er = UResidence.EquipReservationController.GetAll();
            
            var events = Json(new { Reservation = reservationList.ToList(),Equipment=er.ToList() });
            return new JsonResult
            {
                Data=events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }
        public JsonResult Get(int Level,string Search)
        {          
            List<ReservationList> reservationList = default(List<ReservationList>);

            List<EquipReservation> er = default(List<EquipReservation>);
            if (Level == 0)
            {

                reservationList = ReservationListController.GetAllA(Search);      
                er = UResidence.EquipReservationController.GetAll();
               
            }
            else if(Level==1)
            {
                reservationList = ReservationListController.GetAllByDate(Search);
                er = UResidence.EquipReservationController.GetAll();

            }
            else if (Level==2)
            {
                reservationList = ReservationListController.GetAllO(Search);
                er = UResidence.EquipReservationController.GetAll();
            }
            var events = Json(new { Reservation = reservationList.ToList(), Equipment = er.ToList() });
            return new JsonResult
            {
                Data = events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }
        public ActionResult Download(int Level, string Search)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            List<ReservationList> data = default(List<ReservationList>);
            if (Level == 3)
            {            
                data = ReservationListController.GetAllO();
            }
            else
            {
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
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Views/Report/ReservationList.rdlc");
            ReportDataSource rd = new ReportDataSource();
            rd.Name = "ReservationListt";
            rd.Value = data.ToList();
            localreport.DataSources.Add(rd);
            string reportType = "PDF";
            string mimetype;
            string encoding;
            string filenameExtension = "pdf";
            string[] streams;
            Warning[] warnings;
            byte[] renderbyte;
            string deviceInfo = "<DeviceInfo><OutputFormat>PDF</OutputFormat><PageWidth>8.5in</PageWidth><PageHeight>11in</PageHeight><MarginTop>0.5in</MarginTop><MarginLeft>11in</MarginLeft><MarginRight>11in</MarginRight><MarginBottom>0.5in</MarginBottom></DeviceInfo>";
            renderbyte = localreport.Render(reportType, deviceInfo, out mimetype, out encoding, out filenameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment;filename=ReservationList." + filenameExtension);
            return File(renderbyte, filenameExtension);           
        }
    }
}