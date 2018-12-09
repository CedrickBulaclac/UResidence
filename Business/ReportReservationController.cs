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
            const string GET_ALL = @"select o.BldgNo,o.UnitNo,(o.Fname+' '+o.Mname+' '+o.Lname) as OwnerName,rf.Id as RefNo,StartTime,EndTIme,Convert(varchar(25),StartTime,100)+' - '+RIGHT(Convert(varchar,EndTIme,100),7) as DateofReservation,a.AmenityName,a.Rate,a.EveningRate,case when cast(EndTIme as time(0)) <= CONVERT( TIME, '18:00:00') Then DATEDIFF(HOUR,StartTime,EndTIme)else 0 END AS 'MorningHour',case when cast(StartTime as time(0)) >= CONVERT( TIME, '18:00:00') Then DATEDIFF(HOUR,StartTime,EndTIme) else 0 END AS 'EveningHour',case when cast(StartTime as time(0)) < CONVERT( TIME, '18:00:00') AND cast(EndTIme as time(0)) > CONVERT( TIME, '18:00:00') Then DATEDIFF(HOUR,cast(StartTime as time(0)),CONVERT( TIME, '18:00:00')) else 0 END AS 'MorningToWOEvening',case when cast(StartTime as time(0)) < CONVERT( TIME, '18:00:00') and cast(EndTIme as time(0)) > CONVERT( TIME, '18:00:00') Then DATEDIFF(HOUR,CONVERT( TIME, '18:00:00'),cast(EndTIme as time(0))) else 0 END AS 'WOMorningToEvening',s.Rate as TotalAmenityRate,s.Date,TypeResident,Charge =(select ISNULL(SUM(Charge),0) as Charge from tbCharge where RefNo=rf.Id),rc.Totalpayment,rc.Description,rc.Date as DateP,TotalEquipmentRate=(select ISNULL(SUM(er.Rate),0) from tbEquipReservation er where er.RefNo=rf.Id) from tbSchedReservation s inner join tbReservationForm  rf on rf.SchedId=s.Id inner join tbResidence r on r.Id=rf.ResidentId inner join tbOwner o on o.Id=r.OwnerNo full join tbTenant t on t.Id=r.TenantNo inner join tbAmenity a on a.Id=s.AmenityId inner join tbReceipt rc on rf.Id=rc.Refno where rf.Id=@refid";
            List<ReportReservation> ret = default(List<ReportReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refid", refno));
            ret = SqlManager.Select<ReportReservation>(com);
            return ret;
        }
        public static List<ReportReservation> GETT(int refno)
        {
            const string GET_ALL = @"select t.BldgNo,t.UnitNo,ISNULL((t.Fname+' '+t.Mname+' '+t.Lname),0) as OwnerName,rf.Id as RefNo,StartTime,EndTIme,Convert(varchar(25),StartTime,100)+' - '+RIGHT(Convert(varchar,EndTIme,100),7) as DateofReservation,a.AmenityName,a.Rate,a.EveningRate,case when cast(EndTIme as time(0)) <= CONVERT( TIME, '18:00:00') Then DATEDIFF(HOUR,StartTime,EndTIme) else 0 END AS 'MorningHour',case when cast(StartTime as time(0)) >= CONVERT( TIME, '18:00:00') Then DATEDIFF(HOUR,StartTime,EndTIme) else 0 END AS 'EveningHour',case when cast(StartTime as time(0)) < CONVERT( TIME, '18:00:00') AND cast(EndTIme as time(0)) > CONVERT( TIME, '18:00:00')  Then DATEDIFF(HOUR,cast(StartTime as time(0)),CONVERT( TIME, '18:00:00')) else 0 END AS 'MorningToWOEvening',case when cast(StartTime as time(0)) < CONVERT( TIME, '18:00:00') and cast(EndTIme as time(0)) > CONVERT( TIME, '18:00:00') Then DATEDIFF(HOUR,CONVERT( TIME, '18:00:00'),cast(EndTIme as time(0))) else 0 END AS 'WOMorningToEvening',s.Rate as TotalAmenityRate,s.Date,TypeResident,Charge =(select ISNULL(SUM(Charge),0) as Charge from tbCharge where RefNo=rf.Id),rc.Totalpayment,rc.Description,rc.Date as DateP,TotalEquipmentRate=(select ISNULL(SUM(er.Rate),0) from tbEquipReservation er where er.RefNo=rf.Id) from tbSchedReservation s inner join tbReservationForm  rf on rf.SchedId=s.Id inner join tbResidence r on r.Id=rf.ResidentId inner join tbTenant t on t.Id=r.TenantNo inner join tbAmenity a on a.Id=s.AmenityId inner join tbReceipt rc on rf.Id=rc.RefNo where rf.Id=@refid";
            List<ReportReservation> ret = default(List<ReportReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refid", refno));
            ret = SqlManager.Select<ReportReservation>(com);
            return ret;
        }
    }
}
