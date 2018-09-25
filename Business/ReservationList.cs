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
        public int Rate { get; set; }
        public int Charge { get; set; }
        public int ChairCost { get; set; }
        public int TableCost { get; set; }
        public int TotalPayment { get; set; }
        public int RefNo { get; set; }
        public int Outstanding { get; set; }
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
            ret.oFname = RemoveWhitespace(reader.GetString(4));
            ret.oMname = RemoveWhitespace(reader.GetString(5));
            ret.oLname = RemoveWhitespace(reader.GetString(6));
            ret.tFname = RemoveWhitespace(reader.GetString(7));
            ret.tMname = RemoveWhitespace(reader.GetString(8));
            ret.tLname = RemoveWhitespace(reader.GetString(9));
            ret.Date = reader.GetString(10);
            ret.AmenityName = reader.GetString(11);
            ret.Rate = reader.GetInt32(12);
            ret.Charge = reader.GetInt32(13);
            ret.ChairCost = reader.GetInt32(14);
            ret.TableCost = reader.GetInt32(15);
            ret.TotalPayment = reader.GetInt32(16);           
            ret.BldgNo = reader.GetString(17);
            ret.UnitNo = reader.GetString(18);
            ret.ReservedBy = reader.GetString(19);
            ret.Outstanding = reader.GetInt32(20);
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
            this.ChairCost = 0;
            this.TableCost = 0;
            this.TotalPayment = 0;
            this.Outstanding = 0;
        }
    }
}
