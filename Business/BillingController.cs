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
        public static Billing GetOwner(int owner)
        {
            const string GET_ALL = @"SELECT SUM(Rate-(Downpayment+Fullpayment+Charge)) as Balance from tbReceipt a inner join tbReservationForm b on a.RefNo=b.Id inner join tbSchedReservation c on c.Id=b.SchedId inner join tbResidence d on ResidentId=d.Id where d.OwnerNo=@ownerid";
            Billing ret = default(Billing);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("ownerid" ,owner));
            ret = SqlManager.Select<Billing>(com).First();
            return ret;
        }
        public static Billing GetTenant(int ten)
        {
            const string GET_ALL = @"SELECT SUM(Rate-(Downpayment+Fullpayment+Charge)) as Balance from tbReceipt a inner join tbReservationForm b on a.RefNo=b.Id inner join tbSchedReservation c on c.Id=b.SchedId inner join tbResidence d on ResidentId=d.Id where d.TenantNo=@tenantid";
            Billing ret = default(Billing);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("tenantid", ten));
            ret = SqlManager.Select<Billing>(com).First();
            return ret;
        }
    }
}
