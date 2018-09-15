﻿using System;
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
            //const string GET_ALL = @"select sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName, BldgNo, UnitNo, ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )), Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed') from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo";
            const string GET_ALL = @"select sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName, BldgNo, UnitNo, ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id ), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )and er.RefNo=rf.Id), Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),ChairQuantity = (select ISNULL(SUM(er.Quantity),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id),TableQuantity=(select ISNULL(SUM(er.Quantity),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' ) and er.RefNo=rf.Id) from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }
        public static List<ReservationProcess> GETALL(string status)
        {
            const string GET_ALL = @"select sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName, BldgNo, UnitNo, ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' ) and er.RefNo=rf.Id), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' ) and er.RefNo=rf.Id) , Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),ChairQuantity = (select ISNULL(SUM(er.Quantity),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id),TableQuantity=(select ISNULL(SUM(er.Quantity),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' ) and er.RefNo=rf.Id) from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo where Status=@status";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@status", status));
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }
        public static List<ReservationProcess> GETById(string id)
        {
            //const string GET_ALL = @"select sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName, BldgNo, UnitNo, ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )), Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed') from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo";
            const string GET_ALL = @"select sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName, BldgNo, UnitNo, ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' ) and er.RefNo=rf.Id), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )and er.RefNo=rf.Id) , Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),ChairQuantity = (select ISNULL(SUM(er.Quantity),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id),TableQuantity=(select ISNULL(SUM(er.Quantity),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' ) and er.RefNo=rf.Id) from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo where o.Id=@id and Status='Reserved'";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@id", id));
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }
        public static List<ReservationProcess> GET_ALL(int refno)
        {
            const string GET_ALL = @"select sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName, BldgNo, UnitNo, ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id ), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )and er.RefNo=rf.Id) , Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),ChairQuantity = (select ISNULL(SUM(er.Quantity),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id),TableQuantity=(select ISNULL(SUM(er.Quantity),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' ) and er.RefNo=rf.Id) from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo where rf.Id=@refno";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refno", refno));
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }
      
        public static ReservationProcess GETALL(int id)
        {
            const string GET_ALL = @"select sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName, BldgNo, UnitNo, ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' ) and er.RefNo=rf.Id), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' ) and er.RefNo=rf.Id) , Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),ChairQuantity = (select ISNULL(SUM(er.Quantity),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id),TableQuantity=(select ISNULL(SUM(er.Quantity),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' ) and er.RefNo=rf.Id) from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo where sr.Id=@Id";

            ReservationProcess ret = default(ReservationProcess);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<ReservationProcess>(com).First();
            return ret;
        }

    }
}
