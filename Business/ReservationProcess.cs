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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Rate { get; set; }
        public int Total { get; set; }
        public int TableCost { get; set; }
        public int ChairCost { get; set; }
        public int Charge { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string ReservedBy { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public int AId { get; set; }
        public string AName { get; set; }
        public string BldgNo { get; set; }
        public string UnitNo { get; set; }
        public int Amount { get; set; }

        public ReservationProcess CreateObject(SqlDataReader reader)
        {
            ReservationProcess ret = new ReservationProcess();
            ret.SRId = reader.GetInt32(0);
            ret.RFId = reader.GetInt32(1);
            ret.StartTime = reader.GetDateTime(2);
            ret.EndTime = reader.GetDateTime(3);
            ret.Rate = reader.GetInt32(4);
            ret.Charge = reader.GetInt32(5);
            ret.ReservedBy = reader.GetString(6);
            ret.Status = RemoveWhitespace(reader.GetString(7));
            ret.Color = RemoveWhitespace(reader.GetString(8));
            ret.AId = reader.GetInt32(9);
            ret.AName = RemoveWhitespace(reader.GetString(10));
            ret.BldgNo = RemoveWhitespace(reader.GetString(11));
            ret.UnitNo = RemoveWhitespace(reader.GetString(12));
            ret.ChairCost= reader.GetInt32(13);
            ret.TableCost = reader.GetInt32(14);
            ret.Total = reader.GetInt32(15);
            ret.Amount = reader.GetInt32(16);
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
            this.Total = 0;
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
            this.Rate = 0;
            this.TableCost = 0;
            this.ChairCost = 0;
            this.Total = 0;
            this.Charge = 0;
            this.Amount = 0;
            this.ReservedBy = string.Empty;
            this.Status = string.Empty;
            this.Color = string.Empty;
            this.AId = 0;
            this.AName = string.Empty;
            this.BldgNo = string.Empty;
            this.UnitNo = string.Empty;
            this.CreatedBy = string.Empty;
            this.ApprovedBy = string.Empty;
            
        }

        
    }
}
