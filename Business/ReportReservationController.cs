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
        public static List<ReportReservation> GETO(int refno)
        {
            const string GET_ALL = @"select o.BldgNo,o.UnitNo,(o.Fname+' '+o.Mname+' '+o.Lname) as OwnerName,rf.Id as RefNo,StartTime,EndTIme,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,a.AmenityName,a.Rate,DATEDIFF(HOUR,StartTime,EndTIme) as AmenityHour,s.Rate as TotalAmenityRate,s.Date,TypeResident,Charge =(select ISNULL(SUM(Charge),0) as Charge from tbCharge where RefNo=rf.Id),rc.Totalpayment,rc.Description,rc.Date as DateP from tbSchedReservation s inner join tbReservationForm  rf on rf.SchedId=s.Id inner join tbResidence r on r.Id=rf.ResidentId inner join tbOwner o on o.Id=r.OwnerNo full join tbTenant t on t.Id=r.TenantNo inner join tbAmenity a on a.Id=s.AmenityId inner join tbReceipt rc on rf.Id=rc.Refno where rf.Id=@refid";

            List<ReportReservation> ret = default(List<ReportReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refid", refno));
            ret = SqlManager.Select<ReportReservation>(com);
            return ret;
        }
        public static List<ReportReservation> GETT(int refno)
        {
            const string GET_ALL = @"select t.BldgNo,t.UnitNo,ISNULL((t.Fname+' '+t.Mname+' '+t.Lname),0) as OwnerName,rf.Id as RefNo,StartTime,EndTIme,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,a.AmenityName,a.Rate,DATEDIFF(HOUR,StartTime,EndTIme) as AmenityHour,s.Rate as TotalAmenityRate,s.Date,TypeResident,Charge =(select ISNULL(SUM(Charge),0) as Charge from tbCharge where RefNo=rf.Id),rc.Totalpayment,rc.Description,rc.Date as DateP from tbSchedReservation s inner join tbReservationForm  rf on rf.SchedId=s.Id inner join tbResidence r on r.Id=rf.ResidentId full join tbTenant t on t.Id=r.TenantNo inner join tbAmenity a on a.Id=s.AmenityId inner join tbReceipt rc on rf.Id=rc.RefNo  where rf.Id=@refid";

            List<ReportReservation> ret = default(List<ReportReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refid", refno));
            ret = SqlManager.Select<ReportReservation>(com);
            return ret;
        }
    }
}
