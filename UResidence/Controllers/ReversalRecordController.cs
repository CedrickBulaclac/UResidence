using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.IO;
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
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            return View();
        }
        public ActionResult Records()
        {
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            return View();
        }
        public JsonResult GetModal(int refno)
        {
            List<ReservationProcess> reservationList = new List<ReservationProcess>();
            List<EquipReservation> er = default(List<EquipReservation>);
            reservationList = ReservationProcessController.GET_ALL(refno);
            er = UResidence.EquipReservationController.Getr(refno);
            List<Swimming> sr = UResidence.SwimmingController.GETR(refno);
            var events = Json(new { Reservation = reservationList.ToList(), Equipment = er.ToList(), Swimming = sr.ToList() });
            return new JsonResult
            {
                Data = events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            };
        }
       
        public ActionResult Download()
        {
            string my;
            string monthly="";
            string type1= Session["type"].ToString();
            int month1= (int) Session["month"];
            int year1=(int)Session["year"];          
            List<ReversalList> data = default(List<ReversalList>);
            if (type1.ToUpper() == "MONTHLY")
            {
                data = ReversalListController.GET_ALLC(month1, year1);
                switch (month1)
                {
                    case 1:
                        monthly = "January";
                    break;
                    case 2:
                        monthly = "February";
                        break;
                    case 3:
                        monthly = "March";
                        break;
                    case 4:
                        monthly = "April";
                        break;
                    case 5:
                        monthly = "May";
                        break;
                    case 6:
                        monthly = "June";
                        break;
                    case 7:
                        monthly = "July";
                        break;
                    case 8:
                        monthly = "August";
                        break;
                    case 9:
                        monthly = "September";
                        break;
                    case 10:
                        monthly = "October";
                        break;
                    case 11:
                        monthly = "November";
                        break;
                    case 12:
                        monthly = "December";
                        break;
                }
                my = monthly + " ," + year1;
            }
            else
            {
                data = ReversalListController.GET_ALLCY(year1);
                my = year1.ToString();
            }
            
            LocalReport localreport = new LocalReport();
            localreport.ReportPath = Server.MapPath("~/Views/Report/ReversalListt.rdlc");
            ReportDataSource rd = new ReportDataSource();
            rd.Name = "ReversalListt";
            rd.Value = data.ToList();
            //ReportParameter[] param = new ReportParameter[]
            //{
            //    new ReportParameter("txtType", "CED")
            //};         
            //localreport.SetParameters(param);


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
            Response.AddHeader("content-disposition", "attachment;filename=ReversalList." + filenameExtension);
            return File(renderbyte, filenameExtension);


            //TextObject text = (TextObject)rd.ReportDefinition.Sections["Section1"].ReportObjects["Text11"];
            //text.Text = "("+type1+")";
            //TextObject text1 = (TextObject)rd.ReportDefinition.Sections["Section1"].ReportObjects["Text12"];
            //text1.Text = my;

     
        }
        public JsonResult GetP(string type,int month,int year)
        {
            List<ReversalList> revlist = default(List<ReversalList>);
            if (type.ToUpper() == "MONTHLY")
            {
                revlist = ReversalListController.GET_ALLP(month, year);
               
            }
            else
            {
                revlist = ReversalListController.GET_ALLPY(year);
            
            }
            return new JsonResult
            {
                Data = revlist,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetA(string type, int month, int year)
        {
           
            List<ReversalList> revlist = default(List<ReversalList>);
            if (type.ToUpper() == "MONTHLY")
            {
                revlist = ReversalListController.GET_ALLA(month, year);
               
            }
            else
            {
                revlist = ReversalListController.GET_ALLAY(year);
         
            }
            return new JsonResult
            {
                Data = revlist,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetD(string type, int month, int year)
        {
            List<ReversalList> revlist = default(List<ReversalList>);
            if (type.ToUpper() == "MONTHLY")
            {
                revlist = ReversalListController.GET_ALLD(month, year);
  
            }
            else
            {
                revlist = ReversalListController.GET_ALLDY(year);
               
            }
            return new JsonResult
            {
                Data = revlist,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetC(string type, int month, int year)
        {
            Session["type"] = type;
            Session["month"] = month;
            Session["year"] = year;
            List<ReversalList> revlist = default(List<ReversalList>);
            if (type.ToUpper() == "MONTHLY")
            {
                revlist = ReversalListController.GET_ALLC(month, year);              
            }
            else
            {
                revlist = ReversalListController.GET_ALLCY(year);
                            
            }
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