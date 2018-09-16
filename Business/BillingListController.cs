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
            const string GET_ALL = @"select a.Id,a.DateofReservation,a.AmenityName,(a.TotalRate+a.EquipmentRate) as TotalRate,(a.TotalPayment-a.ReversalAmount) as TotalPayment,OutstandingTotal =(a.TotalRate+a.EquipmentRate)-(a.TotalPayment-a.ReversalAmount) from (select rf.Id,convert(varchar(10),StartTime,103) as DateofReservation,a.AmenityName,(sr.Rate +ISNULL(Charge,0)) as TotalRate,EquipmentRate=(select ISNULL(SUM(Rate),0) from tbEquipReservation er where er.RefNo=rf.Id ),TotalPayment=(select ISNULL(SUM(Totalpayment),0) from tbReceipt where RefNo=rf.Id ),ISNULL(r.Amount,0) as ReversalAmount from tbSchedReservation sr inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReservationForm rf on rf.SchedId=sr.Id left join tbCharge c on c.RefNo=rf.Id left join tbReversal r on r.RefNo=rf.Id inner join tbResidence d on d.Id=rf.ResidentId where rf.Status='Reserved' and d.OwnerNo=@OwnerId ) a where (a.TotalRate+a.EquipmentRate)-(a.TotalPayment-a.ReversalAmount)!=0";
            List<BillingList> ret = default(List<BillingList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("OwnerId", id));
            ret = SqlManager.Select<BillingList>(com);
            return ret;
        }
        public static List<BillingList> GetAllT(int id)
        {
            const string GET_ALL = @"select a.Id,a.DateofReservation,a.AmenityName,(a.TotalRate+a.EquipmentRate) as TotalRate,(a.TotalPayment-a.ReversalAmount) as TotalPayment,OutstandingTotal =(a.TotalRate+a.EquipmentRate)-(a.TotalPayment-a.ReversalAmount) from (select rf.Id,convert(varchar(10),StartTime,103) as DateofReservation,a.AmenityName,(sr.Rate +ISNULL(Charge,0)) as TotalRate,EquipmentRate=(select ISNULL(SUM(Rate),0) from tbEquipReservation er where er.RefNo=rf.Id ),TotalPayment=(select ISNULL(SUM(Totalpayment),0) from tbReceipt where RefNo=rf.Id ),ISNULL(r.Amount,0) as ReversalAmount from tbSchedReservation sr inner join tbAmenity a on sr.AmenityId=a.Id inner join tbReservationForm rf on rf.SchedId=sr.Id left join tbCharge c on c.RefNo=rf.Id left join tbReversal r on r.RefNo=rf.Id inner join tbResidence d on d.Id=rf.ResidentId where rf.Status='Reserved' and d.TenantNo=@TenantId ) a where (a.TotalRate+a.EquipmentRate)-(a.TotalPayment-a.ReversalAmount)!=0";
            List<BillingList> ret = default(List<BillingList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("TenantId", id));
            ret = SqlManager.Select<BillingList>(com);
            return ret;
        }
    }
}
