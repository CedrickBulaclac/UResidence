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
            const string GET_ALL = @"select o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where sr.Deleted=0";


            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }
        public static List<ReservationProcess> GET_ALLD()
        {           
            const string GET_ALL = @"select o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where sr.Deleted=0 and Status='Pending' and StartTime<=GETDATE()";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }

        public static ReservationProcess GetMaxId()
        {
            const string GET_RECORD = @"SELECT TOP 1 o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo ORDER BY sr.Id DESC";

            ReservationProcess ret = default(ReservationProcess);
            SqlCommand com = new SqlCommand(GET_RECORD);
            ret = SqlManager.Select<ReservationProcess>(com).First();

            return ret;
        }


        public static ReservationProcess GetSpecificId(int id)
        {
            const string GET_RECORD = @"SELECT o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where a.Id=@Id and Status='Pending'";

            ReservationProcess ret = default(ReservationProcess);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<ReservationProcess>(com).First();

            return ret;
        }



        public static List<ReservationProcess> GET_ALLReserved()
        {
 
            const string GET_ALL = @"select o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where rf.Status='Reserved' and sr.Deleted=0";


            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }

        public static List<ReservationProcess> GET_ALLO(int ownerid)
        {
         
            const string GET_ALL = @"select o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where OwnerNo=@Id and TypeResident ='Owner' and sr.Deleted=0";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Id", ownerid));
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }

        public static List<ReservationProcess> GET_ALLT(int tenantid)
        {
        

            const string GET_ALL = @"select o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where TenantNo=@Id and TypeResident ='Tenant' and sr.Deleted=0";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Id", tenantid));
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }
        public static List<ReservationProcess> GETALL(string status)
        {
          
            const string GET_ALL = @"select o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where Status=@status and sr.Deleted=0";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@status", status));
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }
        public static List<ReservationProcess> GETById(string id)
        {
        
            const string GET_ALL = @"select o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where o.Id=@id and Status='Reserved' and sr.Deleted=0";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@id", id));
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }
        public static List<ReservationProcess> GETById(string id,int refno)
        {
       
            const string GET_ALL = @"select o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where t.Id=@id and Status='Reserved' and rf.Id=@refno and sr.Deleted=0";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@id", id));
            com.Parameters.Add(new SqlParameter("@refno", refno));
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }
        public static List<ReservationProcess> GET_ALL(int refno)
        {
         
            const string GET_ALL = @"select o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where rf.Id=@refno and sr.Deleted=0";
            List<ReservationProcess> ret = default(List<ReservationProcess>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refno", refno));
            ret = SqlManager.Select<ReservationProcess>(com);
            return ret;
        }
      
        public static ReservationProcess GETALL(int id)
        {
       
            const string GET_ALL = @"select o.Id,ISNULL(t.Id,0),TypeResident,sr.Id as SchedReservationID, rf.Id as ReservationFormID, StartTime,EndTIme, sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id), ReservedBy,rf.Status, Color, AmenityId, AmenityName,o.BldgNo,o.UnitNo, Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed'),sr.Deleted from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo left join tbTenant t on t.Id=rd.TenantNo where sr.Id=@Id and sr.Deleted=0";
            ReservationProcess ret = default(ReservationProcess);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<ReservationProcess>(com).First();
            return ret;
        }

    }
}
