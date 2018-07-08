using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class Receipt : BaseProperty<Receipt>
    {
        public int ORNo { get; set; }
        public int RefNo { get; set; }
        public int Downpayment { get; set; }
        public int Charge { get; set; }
        public int Fullpayment { get; set; }


        public void Reset()
        {
            this.ORNo = 0;
            this.RefNo =0;
            this.Downpayment = 0;
            this.Charge = 0;
            this.Fullpayment = 0;
        }

        public Receipt CreateObject(SqlDataReader reader)
        {
            Receipt ret = new Receipt();

            ret.ORNo = reader.GetInt32(0);
            ret.RefNo = reader.GetInt32(1);
            ret.Downpayment = reader.GetInt32(2);
            ret.Charge = reader.GetInt32(3);
            ret.Fullpayment = reader.GetInt32(4);
            return ret;

            
        }
    }
}
