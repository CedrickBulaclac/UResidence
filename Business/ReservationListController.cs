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
            const string GET_ALL = @"select distinct o.Id,ISNULL(t.Id,0),TypeResident,RefNo,o.Fname,o.Mname,o.Lname,ISNULL(t.Fname,''),ISNULL(t.Mname,''),ISNULL(t.Lname,''),Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,a.AmenityName,sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id),ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )and er.RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id) from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo where TypeResident='Owner'";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }
        public static List<ReservationList> GetAllO(string name)
        {
            const string GET_ALL = @"select distinct o.Id,ISNULL(t.Id,0),TypeResident,RefNo,o.Fname,o.Mname,o.Lname,ISNULL(t.Fname,''),ISNULL(t.Mname,''),ISNULL(t.Lname,''),Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,a.AmenityName,sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id),ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )and er.RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id) from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo where o.Fname+' '+o.Mname+' '+o.Lname like '%'+@Name+'%' and TypeResident='Owner'";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Name", name));
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }
        public static List<ReservationList> GetAllA(string name)
        {
            const string GET_ALL = @"select distinct o.Id,ISNULL(t.Id,0),TypeResident,RefNo,o.Fname,o.Mname,o.Lname,ISNULL(t.Fname,''),ISNULL(t.Mname,''),ISNULL(t.Lname,''),Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,a.AmenityName,sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id),ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )and er.RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id) from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo where AmenityName like '%'+@Name+'%' ";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Name", name));
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }
        public static List<ReservationList> GetAllT(string name)
        {
            const string GET_ALL = @"select distinct o.Id,ISNULL(t.Id,0),TypeResident,RefNo,o.Fname,o.Mname,o.Lname,ISNULL(t.Fname,''),ISNULL(t.Mname,''),ISNULL(t.Lname,''),Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,a.AmenityName,sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id),ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )and er.RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id) from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo where t.Fname+' '+t.Mname+' '+t.Lname like '%'+@Name+'%' and TypeResident='Tenant'";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Name", name));
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }    
        public static List<ReservationList> GetAllByDate(string Date)
        {
            const string GET_ALL = @"select distinct o.Id,ISNULL(t.Id,0),TypeResident,RefNo,o.Fname,o.Mname,o.Lname,ISNULL(t.Fname,''),ISNULL(t.Mname,''),ISNULL(t.Lname,''),Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,a.AmenityName,sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id),ChairCost = (select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id), TableCost=(select ISNULL(sum((er.Quantity * er.Rate)),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )and er.RefNo=rf.Id),Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id) from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbReservationForm rf on r.Id=rf.ResidentId inner join tbSchedReservation sr on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReceipt rc on rf.Id=rc.RefNo full join tbTenant t on t.Id=r.TenantNo where Convert(Date,StartTime,111)=Convert(Date,@Date,111) ";
            List<ReservationList> ret = default(List<ReservationList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Date",Date ));
            ret = SqlManager.Select<ReservationList>(com);
            return ret;
        }
    }
}
