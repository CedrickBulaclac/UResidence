using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class BillingListController
    {
        public static List<BillingList> GetAllO(int id)
        {
            const string GET_ALL = @"select b.Id,b.DateofReservation,b.AmenityName,b.TotalRate,b.TotalPayment,b.Outstanding from (select a.Id,a.DateofReservation,a.AmenityName,(a.Rate+a.EquipmentRate+a.Charge) as TotalRate,a.TotalPayment,(a.Rate+a.EquipmentRate+a.Charge)-(TotalPayment) as Outstanding from (select rf.Id,convert(varchar(10),StartTime,103) as DateofReservation,AmenityName=(select AmenityName from tbAmenity a where a.Id=sr.AmenityId),sr.Rate,EquipmentRate=(select ISNULL(SUM(Rate),0) from tbEquipReservation er where er.RefNo=rf.Id),Charge=(select ISNULL(SUM(Charge),0) from tbCharge c where c.RefNo=rf.Id ),TotalPayment=(select ISNULL(SUM(r.Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),OwnerNo,TypeResident  from tbReservationForm rf inner join tbSchedReservation sr on rf.SchedId=sr.Id  inner join tbResidence d on d.Id=rf.ResidentId where rf.Status='Reserved')a where a.OwnerNo=@OwnerId and a.TypeResident='Owner')b where b.Outstanding!=0";
            List<BillingList> ret = default(List<BillingList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("OwnerId", id));
            ret = SqlManager.Select<BillingList>(com);
            return ret;
        }
        public static List<BillingList> GetAllT(int id)
        {
            const string GET_ALL = @"select b.Id,b.DateofReservation,b.AmenityName,b.TotalRate,b.TotalPayment,b.Outstanding from (select a.Id,a.DateofReservation,a.AmenityName,(a.Rate+a.EquipmentRate+a.Charge) as TotalRate,a.TotalPayment,(a.Rate+a.EquipmentRate+a.Charge)-(TotalPayment) as Outstanding from (select rf.Id,convert(varchar(10),StartTime,103) as DateofReservation,AmenityName=(select AmenityName from tbAmenity a where a.Id=sr.AmenityId),sr.Rate,EquipmentRate=(select ISNULL(SUM(Rate),0) from tbEquipReservation er where er.RefNo=rf.Id),Charge=(select ISNULL(SUM(Charge),0) from tbCharge c where c.RefNo=rf.Id ),TotalPayment=(select ISNULL(SUM(r.Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),TenantNo,TypeResident  from tbReservationForm rf inner join tbSchedReservation sr on rf.SchedId=sr.Id  inner join tbResidence d on d.Id=rf.ResidentId where rf.Status='Reserved')a where a.TenantNo=@TenantId and a.TypeResident='Tenant')b where b.Outstanding!=0";
            List<BillingList> ret = default(List<BillingList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("TenantId", id));
            ret = SqlManager.Select<BillingList>(com);
            return ret;
        }
    }
}
