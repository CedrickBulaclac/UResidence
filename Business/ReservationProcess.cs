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
        public int OwnerId { get; set; }
        public int TenantId { get; set; }
        public string typeResident { get; set; }
        public int SRId { get; set; }
        public int RFId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Rate { get; set; }
        public decimal Total { get; set; }
        public decimal Charge { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string ReservedBy { get; set; }
        public string Status { get; set; }
        public string Color { get; set; }
        public int AId { get; set; }
        public string AName { get; set; }
        public string BldgNo { get; set; }
        public string UnitNo { get; set; }
        public decimal Amount { get; set; }
        public int Deleted { get; set; }


        public ReservationProcess CreateObject(SqlDataReader reader)
        {
            ReservationProcess ret = new ReservationProcess();
            ret.OwnerId = reader.GetInt32(0);
            ret.TenantId = reader.GetInt32(1);
            ret.typeResident = reader.GetString(2);
            ret.SRId = reader.GetInt32(3);
            ret.RFId = reader.GetInt32(4);
            ret.StartTime = reader.GetDateTime(5);
            ret.EndTime = reader.GetDateTime(6);
            ret.Rate = reader.GetDecimal(7);
            ret.Charge = reader.GetDecimal(8);
            ret.ReservedBy = reader.GetString(9);
            ret.Status = RemoveWhitespace(reader.GetString(10));
            ret.Color = RemoveWhitespace(reader.GetString(11));
            ret.AId = reader.GetInt32(12);
            ret.AName =reader.GetString(13);
            ret.BldgNo = RemoveWhitespace(reader.GetString(14));
            ret.UnitNo = RemoveWhitespace(reader.GetString(15));
            ret.Total = reader.GetDecimal(16);
            ret.Amount = reader.GetDecimal(17);
            return ret;
        }

        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }


        public void Reset()
        {
            this.OwnerId = 0;
            this.TenantId = 0;
            this.typeResident = string.Empty;
            this.SRId = 0;
            this.RFId = 0;
            this.Total = 0;
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
            this.Rate = 0;
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
