using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class BillingController
    {
        public static List<Billing> GetOwner(int owner)
        {
            try
            {
                const string GET_ALL = @"select sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id),ChairCost = (select ISNULL(SUM(er.Rate),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id ), TableCost=(select ISNULL(SUM(er.Rate),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )and er.RefNo=rf.Id), Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed') from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo where OwnerNo=@ownerid and Status='Reserved' and TypeResident='Owner'";
                List<Billing> ret = default(List<Billing>);
                SqlCommand com = new SqlCommand(GET_ALL);
                com.Parameters.Add(new SqlParameter("ownerid", owner));
                ret = SqlManager.Select<Billing>(com);
                return ret;
            }
            catch(Exception)
            {
                
                return null;
            }
        }
        public static List<Billing> GetTenant(int ten)
        {
            try
            {
                const string GET_ALL = @"select sr.Rate,Charge=(select ISNULL(SUM(Charge),0) from tbCharge where RefNo=rf.Id),ChairCost = (select ISNULL(SUM(er.Rate),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Chair%' )and er.RefNo=rf.Id ), TableCost=(select ISNULL(SUM(er.Rate),0) from tbEquipReservation er where er.EquipmentId in (select Id from tbEquipment e where e.Name like '%Table%' )and er.RefNo=rf.Id), Totale=(select ISNULL(SUM(Totalpayment),0) from tbReceipt r where r.RefNo=rf.Id),Amount =(select ISNULL(SUM(Amount),0) from tbReversal rev where rev.RefNo=rf.Id and Status='Completed') from tbSchedReservation sr inner join tbReservationForm rf on sr.Id=SchedId inner join tbAmenity a on a.Id=AmenityId inner join tbResidence rd on rd.Id=rf.ResidentId inner join tbOwner o on o.Id=rd.OwnerNo where TenantNo=@tenantid and Status='Reserved' and TypeResident='Tenant' ";
                List<Billing> ret = default(List<Billing>);
                SqlCommand com = new SqlCommand(GET_ALL);
                com.Parameters.Add(new SqlParameter("tenantid", ten));
                ret = SqlManager.Select<Billing>(com);
                return ret;
            }

            catch (Exception)
            {

                return null;
            }
        }
    }
}
