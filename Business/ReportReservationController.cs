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
            const string GET_ALL = @"select o.BldgNo,o.UnitNo,(o.Fname+' '+o.Mname+' '+o.Lname) as OwnerName,ISNULL((t.Fname+' '+t.Mname+' '+t.Lname),'') as TenantName,rf.Id as RefNo,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,a.AmenityName,a.Rate,ISNULL(swimp.AdultRate,0) as AdultRate,ISNULL(swimp.ChildRate,0) as ChildRate,DATEDIFF(HOUR,StartTime,EndTIme) as AmenityHour,s.Rate as TotalAmenityRate,s.Date,ISNULL(e.Name,'None') as EquipmentName,ISNULL(er.Quantity,0) as EquipmentQuantity,ISNULL(er.Rate,0) as EquipmentRate,ISNULL((er.Rate*er.Quantity),0) as EquipmentCost,TypeResident,ISNULL(sp.Adult,0) as Adult,ISNULL(sp.Child,0) as Child,Charge =(select ISNULL(SUM(Charge),0) as Charge from tbCharge where RefNo=rf.Id),rc.Totalpayment,rc.Description,rc.Date as DateP from tbSchedReservation s inner join tbReservationForm  rf on rf.SchedId=s.Id full join tbEquipReservation er on rf.Id=er.RefNo full join tbEquipment e on er.EquipmentId=e.Id inner join tbResidence r on r.Id=rf.ResidentId inner join tbOwner o on o.Id=r.OwnerNo full join tbTenant t on t.Id=r.TenantNo full join tbSwimmingPayment sp on sp.SchedID=s.id inner join tbAmenity a on a.Id=s.AmenityId inner join tbReceipt rc on rf.Id=rc.RefNo full join tbSwimmingPool swimp on swimp.AmenityId=a.Id   where Status='Reserved' and rf.Id=@refid";

            List<ReportReservation> ret = default(List<ReportReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refid", refno));
            ret = SqlManager.Select<ReportReservation>(com);
            return ret;
        }
        public static List<ReportReservation> GETT(int refno)
        {
            const string GET_ALL = @"select t.BldgNo,t.UnitNo,ISNULL((t.Fname+' '+t.Mname+' '+t.Lname),0) as OwnerName,rf.Id as RefNo,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,a.AmenityName,a.Rate,ISNULL(swimp.AdultRate,0) as AdultRate,ISNULL(swimp.ChildRate,0) as ChildRate,DATEDIFF(HOUR,StartTime,EndTIme) as AmenityHour,s.Rate as TotalAmenityRate,s.Date,ISNULL(e.Name,'None') as EquipmentName,ISNULL(er.Quantity,0) as EquipmentQuantity,ISNULL(er.Rate,0) as EquipmentRate,ISNULL((er.Rate*er.Quantity),0) as EquipmentCost,TypeResident,ISNULL(sp.Adult,0) as Adult,ISNULL(sp.Child,0) as Child,Charge =(select ISNULL(SUM(Charge),0) as Charge from tbCharge where RefNo=rf.Id),rc.Totalpayment,rc.Description,rc.Date as DateP from tbSchedReservation s inner join tbReservationForm  rf on rf.SchedId=s.Id full join tbEquipReservation er on rf.Id=er.RefNo full join tbEquipment e on er.EquipmentId=e.Id inner join tbResidence r on r.Id=rf.ResidentId full join tbTenant t on t.Id=r.TenantNo full join tbSwimmingPayment sp on sp.SchedID=s.id inner join tbAmenity a on a.Id=s.AmenityId inner join tbReceipt rc on rf.Id=rc.RefNo full join tbSwimmingPool swimp on swimp.AmenityId=a.Id   where Status='Reserved' and rf.Id=@refid";

            List<ReportReservation> ret = default(List<ReportReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refid", refno));
            ret = SqlManager.Select<ReportReservation>(com);
            return ret;
        }
    }
}
