using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class BillingList : BaseProperty<BillingList>
    {
        public int RFID { get; set; }
        public string Date { get; set; }
        public string AmenityName { get; set; }
        public decimal TotalRate { get; set; }
        public decimal TotalPayment { get; set; }
        public decimal OutstandingTotal { get; set; }
        public BillingList CreateObject(SqlDataReader reader)
        {
            BillingList ret = new BillingList();
            ret.RFID = reader.GetInt32(0);
            ret.Date = reader.GetString(1);
            ret.AmenityName = reader.GetString(2);
            ret.TotalRate = reader.GetDecimal(3);
            ret.TotalPayment = reader.GetDecimal(4);
            ret.OutstandingTotal = reader.GetDecimal(5);
            return ret;
        }

        public void Reset()
        {
            this.RFID = 0;
            this.Date = string.Empty;
            this.AmenityName = string.Empty;
            this.TotalPayment = 0;
            this.TotalRate = 0;
            this.OutstandingTotal = 0;
        }
    }
}
