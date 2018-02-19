using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    class SchedReservation : BaseProperty<SchedReservation>
    {
        public string AmenityNo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTIme { get; set; }
        public int Rate { get; set; }


        public void Reset()
        {
            this.AmenityNo = string.Empty;
            this.StartTime = DateTime.Today;
            this.EndTIme = DateTime.Today;
            this.Rate = 0;
        }

        public SchedReservation CreateObject(SqlDataReader reader)
        {
            SchedReservation ret = new SchedReservation();

            ret.AmenityNo = reader.GetString(0);
            ret.StartTime = reader.GetDateTime(1);
            ret.EndTIme = reader.GetDateTime(2);
            ret.Rate = reader.GetInt32(3);    
            return ret;


        }
    }
}
