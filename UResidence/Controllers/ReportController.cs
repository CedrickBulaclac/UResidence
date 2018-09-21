using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
namespace UResidence.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ReservedAmenity(int refno)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine("~/Report"),"ReservationForm.rpt");
            List<ReportReservation> data =default(List<ReportReservation>);
            data = UResidence.ReportReservationAmenityController.GETO(refno);
            rd.SetDataSource(data.ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Reservation.pdf");
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}