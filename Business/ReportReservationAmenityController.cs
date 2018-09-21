using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class ReportReservationAmenityController
    {
        public static List<ReportReservationAmenity> GET(int refno)
        {
            const string GET_ALL = @"select sr.Date,DATEDIFF(HOUR,StartTime,EndTIme) as AmenityHour,o.Fname+' '+CONVERT(VARCHAR(1),o.Mname) +'. '+o.Lname as Name,o.Id as OwnerID,ISNULL(t.Id,0) as TenantId,TypeResident,rf.Id as ReservationFormID,CONVERT(VARCHAR(15),StartTime,101)+' '+ CONVERT(varchar(8),StartTime,108)+'-'+CONVERT(varchar(8),EndTIme,108) as ReservedDate,AmenityName,a.Rate,sr.Rate as TotalRate,o.BldgNo,o.UnitNo from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where rf.Id=@refid";

            List<ReportReservationAmenity> ret = default(List<ReportReservationAmenity>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refid", refno));
            ret = SqlManager.Select<ReportReservationAmenity>(com);
            return ret;
        }
    }
}
