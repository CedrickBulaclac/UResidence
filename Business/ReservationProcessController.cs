using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class ReservationProcessController
    {
        public static List<ReservationProcess> GET_ALL()
        {
            const string GET_ALL = @"select sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime, EndTIme, sr.Rate,Charge = (select (ISNULL(MAX(Charge),0))  from tbReceipt r where r.RefNo=rf.Id ), ReservedBy, Status, Color, AmenityId, AmenityName, BldgNo, UnitNo, ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )), Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id) from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }

        //public static List<ReservationProcess> GET_ALL()
        //    {
        //    const string GET_ALL = @"  select sr.Id as SchedReservationID,rf.Id as ReservationFormID,r.Id as ReceiptID,StartTime,EndTIme,sr.Rate,Totalpayment,Charge,Date,r.Description,ReservedBy,Status,Color,AmenityId,AmenityName,BldgNo,UnitNo from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbReceipt r on rf.Id=r.RefNo inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo";
        //    List<ReservationProcess> ret = default(List<ReservationProcess>);
        //    SqlCommand com = new SqlCommand(GET_ALL);
        //    ret = SqlManager.Select<ReservationProcess>(com);
        //    return ret;
        //}


        public static ReservationProcess GETALL(int id)
        {
            const string GET_ALL = @"select sr.Id as SchedReservationID,rf.Id as ReservationFormID,r.Id as ReceiptID,StartTime,EndTIme,Rate,Totalpayment,Charge,Date,Description,ReservedBy,Status from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbReceipt r on rf.Id=r.RefNo where sr.Id=@Id";

            ReservationProcess ret = default(ReservationProcess);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<ReservationProcess>(com).First();
            return ret;
        }

    }
}
