using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    class Reciept : BaseProperty<Reciept>
    {
        public string ORNo { get; set; }
        public string RefNo { get; set; }
        public string OwnerNo { get; set; }
        public int Downpayment { get; set; }
        public int Charge { get; set; }
        public int Fullpayment { get; set; }


        public void Reset()
        {
            this.ORNo = string.Empty;
            this.RefNo = string.Empty;
            this.OwnerNo = string.Empty;
            this.Downpayment = 0;
            this.Charge = 0;
            this.Fullpayment = 0;
        }

        public Reciept CreateObject(SqlDataReader reader)
        {
            Reciept ret = new Reciept();

            ret.ORNo = reader.GetString(0);
            ret.RefNo = reader.GetString(1);
            ret.OwnerNo = reader.GetString(2);
            ret.Downpayment = reader.GetInt32(3);
            ret.Charge = reader.GetInt32(4);
            ret.Fullpayment = reader.GetInt32(5);
            return ret;

            
        }
    }
}
