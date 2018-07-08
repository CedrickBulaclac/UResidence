using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class ReservationProcess : BaseProperty<ReservationProcess> 
    {
        public int SRId { get; set; }
        public int RFId { get; set; }
        public int RId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Rate { get; set; }
        public int Downpayment { get; set; }
        public int Charge { get; set; }
        public int Fullpayment { get; set; }
        public string ReservedBy { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public int AId { get; set; }
        public string AName { get; set; }
        public string BldgNo { get; set; }
        public string UnitNo { get; set; }

        public ReservationProcess CreateObject(SqlDataReader reader)
        {
            ReservationProcess ret = new ReservationProcess();
            ret.SRId = reader.GetInt32(0);
            ret.RFId = reader.GetInt32(1);
            ret.RId = reader.GetInt32(2);
            ret.StartTime = reader.GetDateTime(3);
            ret.EndTime = reader.GetDateTime(4);
            ret.Rate = reader.GetInt32(5);
            ret.Downpayment = reader.GetInt32(6);
            ret.Charge = reader.GetInt32(7);
            ret.Fullpayment = reader.GetInt32(8);
            ret.ReservedBy = reader.GetString(9);
            ret.Status = RemoveWhitespace(reader.GetString(10));
            ret.Color = RemoveWhitespace(reader.GetString(11));
            ret.AId = reader.GetInt32(12);
            ret.AName = RemoveWhitespace(reader.GetString(13));
            ret.BldgNo = RemoveWhitespace(reader.GetString(14));
            ret.UnitNo = RemoveWhitespace(reader.GetString(15));
            return ret;
        }

        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }


        public void Reset()
        {
            this.SRId = 0;
            this.RFId = 0;
            this.RId = 0;
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
            this.Rate = 0;
            this.Downpayment = 0;
            this.Charge = 0;
            this.Fullpayment = 0;
            this.ReservedBy = string.Empty;
            this.Status = string.Empty;
            this.Color = string.Empty;
            this.AId = 0;
            this.AName = string.Empty;
            this.BldgNo = string.Empty;
            this.UnitNo = string.Empty;
        }

        
    }
}
