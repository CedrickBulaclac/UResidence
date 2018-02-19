using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    class Reservation : BaseProperty<Reservation>
    {    
        public string RefNo { get; set; }
        public string OwnerNo { get; set; }
        public string AcknowledgeBy { get; set; }
        public string ReservedBy { get; set; }
        public string Status { get; set; }

        
        public void Reset()
        {       
            this.RefNo = string.Empty;
            this.OwnerNo = string.Empty;       
            this.AcknowledgeBy = string.Empty;
            this.ReservedBy = string.Empty;
            this.Status = string.Empty;
        }

        public Reservation CreateObject(SqlDataReader reader)
        {
            Reservation ret = new Reservation();

            ret.RefNo = reader.GetString(0);
            ret.OwnerNo = reader.GetString(1);
            ret.AcknowledgeBy = reader.GetString(2);
            ret.ReservedBy = reader.GetString(3);
            ret.Status = reader.GetString(4);
            return ret;
        }

    }
}
