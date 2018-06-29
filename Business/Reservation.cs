using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class Reservation : BaseProperty<Reservation>
    {    
        public int Id { get; set; }
        public int Rid { get; set; }
        public string AcknowledgeBy { get; set; }
        public string ReservedBy { get; set; }
        public string Status { get; set; }
        public string Tor { get; set; }
        public int Sid { get; set; }

        public void Reset()
        {

            this.Id = 0;
            this.Rid = 0;
            this.AcknowledgeBy = string.Empty;
            this.ReservedBy = string.Empty;
            this.Status = string.Empty;
            this.Tor = string.Empty;
            this.Sid = 0;
        }

        public Reservation CreateObject(SqlDataReader reader)
        {
            Reservation ret = new Reservation();
            ret.Id = reader.GetInt32(0);
            ret.Rid = reader.GetInt32(1);
            ret.AcknowledgeBy = reader.GetString(2);
            ret.ReservedBy = reader.GetString(3);
            ret.Status = reader.GetString(4);
            ret.Tor = reader.GetString(5);
            ret.Sid = reader.GetInt32(6);

            return ret;
        }

    }
}
