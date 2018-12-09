﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
   public class ReservationListController
    {
        public static List<ReservationList> GetAllO()
        {

            const string GET_ALL = @"select b.OwnerId,b.TenantId,b.TypeResident,b.Refno,b.OwnerFname,b.OwnerMname,b.OwnerLname,b.TFname,b.TMname,b.TLname,b.Date,b.AmenityName,b.Rate,b.Charge,b.Totale,b.BldgNo,b.UnitNo,b.ReservedBy,b.Outstanding from(select *,Outstanding =((a.Rate+a.Charge+ISNULL(a.EquipmentCost,0))-(Totale-ISNULL(ReversalAmount,0))) from(select distinct ISNULL(o.Id,0) as OwnerId,ISNULL(t.Id,0) as TenantId,ISNULL(TypeResident,'Tenant') as TypeResident,ISNULL(rf.Id,0) as Refno,ISNULL(o.Fname,'') as OwnerFname,ISNULL(o.Mname,'') as OwnerMname ,ISNULL(o.Lname,'') as OwnerLname,ISNULL(t.Fname,'') as TFname,ISNULL(t.Mname,'')as TMname,ISNULL(t.Lname,'') as TLname,ISNULL(Convert(varchar(25),StartTime,100)+' - '+RIGHT(Convert(varchar,EndTIme,100),7),'')  as Date,ISNULL(a.AmenityName,'') as AmenityName,ISNULL(sr.Rate,0) as Rate,Charge=(select ISNULL(SUM(Charge),0)from tbCharge where RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),o.BldgNo,o.UnitNo,rf.ReservedBy,EquipmentCost=(select SUM(er.Rate) as EquipmentCost from tbEquipReservation er inner join tbEquipment e on  er.EquipmentId=e.Id where RefNo=rf.Id group by RefNo ),ReversalAmount=(select SUM(rev.Amount) from tbReversal rev where rev.RefNo=rf.Id ) from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo)a) b where b.Outstanding!=0";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }
        public static List<ReservationList> GetAllO(int id)
        {

            const string GET_ALL = @"select b.OwnerId,b.TenantId,b.TypeResident,b.Refno,b.OwnerFname,b.OwnerMname,b.OwnerLname,b.TFname,b.TMname,b.TLname,b.Date,b.AmenityName,b.Rate,b.Charge,b.Totale,b.BldgNo,b.UnitNo,b.ReservedBy,b.Outstanding from(select *,Outstanding =((a.Rate+a.Charge+ISNULL(a.EquipmentCost,0))-(Totale-ISNULL(ReversalAmount,0))) from(select distinct ISNULL(o.Id,0) as OwnerId,ISNULL(t.Id,0) as TenantId,ISNULL(TypeResident,'Tenant') as TypeResident,ISNULL(RefNo,0) as Refno,ISNULL(o.Fname,'') as OwnerFname,ISNULL(o.Mname,'') as OwnerMname ,ISNULL(o.Lname,'') as OwnerLname,ISNULL(t.Fname,'') as TFname,ISNULL(t.Mname,'')as TMname,ISNULL(t.Lname,'') as TLname,ISNULL(Convert(varchar(25),StartTime,100)+' - '+RIGHT(Convert(varchar,EndTIme,100),7),'')  as Date,ISNULL(a.AmenityName,'') as AmenityName,ISNULL(sr.Rate,0) as Rate,Charge=(select ISNULL(SUM(Charge),0)from tbCharge where RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),o.BldgNo,o.UnitNo,rf.ReservedBy,EquipmentCost=(select SUM(er.Rate) as EquipmentCost from tbEquipReservation er inner join tbEquipment e on  er.EquipmentId=e.Id where RefNo=rf.Id group by RefNo ),ReversalAmount=(select SUM(rev.Amount) from tbReversal rev where rev.RefNo=rf.Id)  from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo where rf.Status='Reserved' )a) b where b.Outstanding!=0 and b.OwnerId=@ownerid";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@ownerid", id));
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }
        public static List<ReservationList> GetAllT(int id)
        {

            const string GET_ALL = @"select b.OwnerId,b.TenantId,b.TypeResident,b.Refno,b.OwnerFname,b.OwnerMname,b.OwnerLname,b.TFname,b.TMname,b.TLname,b.Date,b.AmenityName,b.Rate,b.Charge,b.Totale,b.BldgNo,b.UnitNo,b.ReservedBy,b.Outstanding from(select *,Outstanding =((a.Rate+a.Charge+ISNULL(a.EquipmentCost,0))-(Totale-ISNULL(ReversalAmount,0))) from(select distinct ISNULL(o.Id,0) as OwnerId,ISNULL(t.Id,0) as TenantId,ISNULL(TypeResident,'Tenant') as TypeResident,ISNULL(RefNo,0) as Refno,ISNULL(o.Fname,'') as OwnerFname,ISNULL(o.Mname,'') as OwnerMname ,ISNULL(o.Lname,'') as OwnerLname,ISNULL(t.Fname,'') as TFname,ISNULL(t.Mname,'')as TMname,ISNULL(t.Lname,'') as TLname,ISNULL(Convert(varchar(25),StartTime,100)+' - '+RIGHT(Convert(varchar,EndTIme,100),7),'')  as Date,ISNULL(a.AmenityName,'') as AmenityName,ISNULL(sr.Rate,0) as Rate,Charge=(select ISNULL(SUM(Charge),0)from tbCharge where RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),o.BldgNo,o.UnitNo,rf.ReservedBy,EquipmentCost=(select SUM(er.Rate) as EquipmentCost from tbEquipReservation er inner join tbEquipment e on  er.EquipmentId=e.Id where RefNo=rf.Id group by RefNo ),ReversalAmount=(select SUM(rev.Amount) from tbReversal rev where rev.RefNo=rf.Id )  from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo )a) b where b.Outstanding!=0 and b.TenantId=@tenantid";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@tenantid", id));
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }
        public static List<ReservationList> GetAllO(string name)
        {
            const string GET_ALL = @"select b.OwnerId,b.TenantId,b.TypeResident,b.Refno,b.OwnerFname,b.OwnerMname,b.OwnerLname,b.TFname,b.TMname,b.TLname,b.Date,b.AmenityName,b.Rate,b.Charge,b.Totale,b.BldgNo,b.UnitNo,b.ReservedBy,b.Outstanding from(select *,Outstanding =((a.Rate+a.Charge+ISNULL(a.EquipmentCost,0))-(Totale-ISNULL(ReversalAmount,0))) from(select distinct ISNULL(o.Id,0) as OwnerId,ISNULL(t.Id,0) as TenantId,ISNULL(TypeResident,'Tenant') as TypeResident,ISNULL(RefNo,0) as Refno,ISNULL(o.Fname,'') as OwnerFname,ISNULL(o.Mname,'') as OwnerMname ,ISNULL(o.Lname,'') as OwnerLname,ISNULL(t.Fname,'') as TFname,ISNULL(t.Mname,'')as TMname,ISNULL(t.Lname,'') as TLname,ISNULL(Convert(varchar(25),StartTime,100)+' - '+RIGHT(Convert(varchar,EndTIme,100),7),'')  as Date,ISNULL(a.AmenityName,'') as AmenityName,ISNULL(sr.Rate,0) as Rate,Charge=(select ISNULL(SUM(Charge),0)from tbCharge where RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),o.BldgNo,o.UnitNo,rf.ReservedBy,EquipmentCost=(select SUM(er.Rate) as EquipmentCost from tbEquipReservation er inner join tbEquipment e on  er.EquipmentId=e.Id where RefNo=rf.Id group by RefNo ),ReversalAmount=(select SUM(rev.Amount) from tbReversal rev where rev.RefNo=rf.Id )  from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo where o.Fname+' '+o.Mname+' '+o.Lname like '%'+@Name+'%' or t.Fname+' '+t.Mname+' '+t.Lname like '%'+@Name+'%')a) b where b.Outstanding!=0";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Name", name));
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }
        public static List<ReservationList> GetAllA(string name)
        {
            const string GET_ALL = @"select b.OwnerId,b.TenantId,b.TypeResident,b.Refno,b.OwnerFname,b.OwnerMname,b.OwnerLname,b.TFname,b.TMname,b.TLname,b.Date,b.AmenityName,b.Rate,b.Charge,b.Totale,b.BldgNo,b.UnitNo,b.ReservedBy,b.Outstanding from(select *,Outstanding =((a.Rate+a.Charge+ISNULL(a.EquipmentCost,0))-(Totale-ISNULL(ReversalAmount,0))) from(select distinct ISNULL(o.Id,0) as OwnerId,ISNULL(t.Id,0) as TenantId,ISNULL(TypeResident,'Tenant') as TypeResident,ISNULL(RefNo,0) as Refno,ISNULL(o.Fname,'') as OwnerFname,ISNULL(o.Mname,'') as OwnerMname ,ISNULL(o.Lname,'') as OwnerLname,ISNULL(t.Fname,'') as TFname,ISNULL(t.Mname,'')as TMname,ISNULL(t.Lname,'') as TLname,ISNULL(Convert(varchar(25),StartTime,100)+' - '+RIGHT(Convert(varchar,EndTIme,100),7),'')  as Date,ISNULL(a.AmenityName,'') as AmenityName,ISNULL(sr.Rate,0) as Rate,Charge=(select ISNULL(SUM(Charge),0)from tbCharge where RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),o.BldgNo,o.UnitNo,rf.ReservedBy,EquipmentCost=(select SUM(er.Rate) as EquipmentCost from tbEquipReservation er inner join tbEquipment e on  er.EquipmentId=e.Id where RefNo=rf.Id group by RefNo ),ReversalAmount=(select SUM(rev.Amount) from tbReversal rev where rev.RefNo=rf.Id )  from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo where AmenityName like '%'+@Name+'%' )a) b where b.Outstanding!=0 ";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Name", name));
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }    
        public static List<ReservationList> GetAllByDate(string Date)
        {
            const string GET_ALL = @"select b.OwnerId,b.TenantId,b.TypeResident,b.Refno,b.OwnerFname,b.OwnerMname,b.OwnerLname,b.TFname,b.TMname,b.TLname,b.Date,b.AmenityName,b.Rate,b.Charge,b.Totale,b.BldgNo,b.UnitNo,b.ReservedBy,b.Outstanding from(select *,Outstanding =((a.Rate+a.Charge+ISNULL(a.EquipmentCost,0))-(Totale-ISNULL(ReversalAmount,0))) from(select distinct ISNULL(o.Id,0) as OwnerId,ISNULL(t.Id,0) as TenantId,ISNULL(TypeResident,'Tenant') as TypeResident,ISNULL(RefNo,0) as Refno,ISNULL(o.Fname,'') as OwnerFname,ISNULL(o.Mname,'') as OwnerMname ,ISNULL(o.Lname,'') as OwnerLname,ISNULL(t.Fname,'') as TFname,ISNULL(t.Mname,'')as TMname,ISNULL(t.Lname,'') as TLname,ISNULL(Convert(varchar(25),StartTime,100)+' - '+RIGHT(Convert(varchar,EndTIme,100),7),'')  as Date,ISNULL(a.AmenityName,'') as AmenityName,ISNULL(sr.Rate,0) as Rate,Charge=(select ISNULL(SUM(Charge),0)from tbCharge where RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),o.BldgNo,o.UnitNo,rf.ReservedBy,EquipmentCost=(select SUM(er.Rate) as EquipmentCost from tbEquipReservation er inner join tbEquipment e on  er.EquipmentId=e.Id where RefNo=rf.Id group by RefNo ),ReversalAmount=(select SUM(rev.Amount) from tbReversal rev where rev.RefNo=rf.Id )  from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo where Convert(Date,StartTime,111)=Convert(Date,@Date,111)  )a) b where b.Outstanding!=0";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Date",Date ));
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }
        public static List<ReservationList> GetByRef(int refno)
        {
            const string GET_ALL = @"select b.OwnerId,b.TenantId,b.TypeResident,b.Refno,b.OwnerFname,b.OwnerMname,b.OwnerLname,b.TFname,b.TMname,b.TLname,b.Date,b.AmenityName,b.Rate,b.Charge,b.Totale,b.BldgNo,b.UnitNo,b.ReservedBy,b.Outstanding from(select *,Outstanding =((a.Rate+a.Charge+ISNULL(a.EquipmentCost,0))-(Totale-ISNULL(ReversalAmount,0))) from(select distinct ISNULL(o.Id,0) as OwnerId,ISNULL(t.Id,0) as TenantId,ISNULL(TypeResident,'Tenant') as TypeResident,ISNULL(RefNo,0) as Refno,ISNULL(o.Fname,'') as OwnerFname,ISNULL(o.Mname,'') as OwnerMname ,ISNULL(o.Lname,'') as OwnerLname,ISNULL(t.Fname,'') as TFname,ISNULL(t.Mname,'')as TMname,ISNULL(t.Lname,'') as TLname,ISNULL(Convert(varchar(25),StartTime,100)+' - '+RIGHT(Convert(varchar,EndTIme,100),7),'')  as Date,ISNULL(a.AmenityName,'') as AmenityName,ISNULL(sr.Rate,0) as Rate,Charge=(select ISNULL(SUM(Charge),0)from tbCharge where RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),o.BldgNo,o.UnitNo,rf.ReservedBy,EquipmentCost=(select SUM(er.Rate) as EquipmentCost from tbEquipReservation er inner join tbEquipment e on  er.EquipmentId=e.Id where RefNo=rf.Id group by RefNo ),ReversalAmount=(select SUM(rev.Amount) from tbReversal rev where rev.RefNo=rf.Id )  from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo)a) b where b.Refno=@refno";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refno", refno));
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }
    }
}
