using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class ReservationList : BaseProperty<ReservationList>
    {
        public int OwnerId { get; set; }
        public int TenantId { get; set; }
        public string typeResident { get; set; }
        public string oFname { get; set; }
        public string oMname { get; set; }
        public string oLname { get; set; }
        public string oDate { get; set; }
        public string tFname { get; set; }
        public string tMname { get; set; }
        public string tLname { get; set; }
        public string Date { get; set; }
        public string AmenityName { get; set; }
        public decimal Rate { get; set; }
        public decimal Charge { get; set; }
        public decimal TotalPayment { get; set; }
        public int RefNo { get; set; }
        public decimal Outstanding { get; set; }
        public string BldgNo { get; set; }
        public string UnitNo { get; set; }
        public string ReservedBy { get; set; }
        public ReservationList CreateObject(SqlDataReader reader)
        {
            ReservationList ret = new ReservationList();
            ret.OwnerId = reader.GetInt32(0);
            ret.TenantId = reader.GetInt32(1);
            ret.typeResident = reader.GetString(2);
           ret.RefNo = reader.GetInt32(3);
            ret.oFname = reader.GetString(4);
            ret.oMname =reader.GetString(5);
            ret.oLname = reader.GetString(6);
            ret.tFname = reader.GetString(7);
            ret.tMname = reader.GetString(8);
            ret.tLname = reader.GetString(9);
            ret.Date = reader.GetString(10);
            ret.AmenityName = reader.GetString(11);
            ret.Rate = reader.GetDecimal(12);
            ret.Charge = reader.GetDecimal(13);
            ret.TotalPayment = reader.GetDecimal(14);           
            ret.BldgNo = reader.GetString(15);
            ret.UnitNo = reader.GetString(16);
            ret.ReservedBy = reader.GetString(17);
            ret.Outstanding = reader.GetDecimal(18);
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
            this.tFname = string.Empty;
            this.tMname = string.Empty;
            this.tLname = string.Empty;
            this.oFname = string.Empty;
            this.oMname = string.Empty;
            this.oLname = string.Empty;
            this.Date = string.Empty;
            this.AmenityName = string.Empty;
            this.Rate = 0;
            this.Charge = 0;
            this.TotalPayment = 0;
            this.Outstanding = 0;
        }
    }
}
