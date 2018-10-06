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
            List<ReservationProcess> reservationList = ReservationProcessController.GET_ALL(refno);
            var events = reservationList.ToList();

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
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Views/Report"), "ReversalList.rpt"));
            List<ReversalList> revlist = default(List<ReversalList>);
            if (type1.ToUpper() == "MONTHLY")
            {
                revlist = ReversalListController.GET_ALLC(month1, year1);
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
                revlist = ReversalListController.GET_ALLCY(year1);
                my = year1.ToString();
            }
           
            TextObject text = (TextObject)rd.ReportDefinition.Sections["Section1"].ReportObjects["Text11"];
            text.Text = "("+type1+")";
            TextObject text1 = (TextObject)rd.ReportDefinition.Sections["Section1"].ReportObjects["Text12"];
            text1.Text = my;
            rd.SetDataSource(revlist.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            try
            {
                Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                rd.Close();
                rd.Dispose();

                return File(stream, "application/pdf", "ReversalList.pdf");

            }
            catch (Exception)
            {
                throw;
            }
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