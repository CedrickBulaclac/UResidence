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
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public string Date { get; set; }
        public string AmenityName { get; set; }
        public int Rate { get; set; }
        public int Charge { get; set; }
        public int ChairCost { get; set; }
        public int TableCost { get; set; }
        public int TotalPayment { get; set; }
        public int RefNo { get; set; }

        public ReservationList CreateObject(SqlDataReader reader)
        {
            ReservationList ret = new ReservationList();
            ret.RefNo = reader.GetInt32(0);
            ret.Fname = RemoveWhitespace(reader.GetString(1));
            ret.Mname = RemoveWhitespace(reader.GetString(2));
            ret.Lname = RemoveWhitespace(reader.GetString(3));
            ret.Date = reader.GetString(4);
            ret.AmenityName = reader.GetString(5);
            ret.Rate = reader.GetInt32(6);
            ret.Charge = reader.GetInt32(7);
            ret.ChairCost = reader.GetInt32(8);
            ret.TableCost = reader.GetInt32(9);
            ret.TotalPayment = reader.GetInt32(10);
            return ret;
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }
        public void Reset()
        {
            this.Fname = string.Empty;
            this.Mname = string.Empty;
            this.Lname = string.Empty;
            this.Date = string.Empty;
            this.AmenityName = string.Empty;
            this.Rate = 0;
            this.Charge = 0;
            this.ChairCost = 0;
            this.TableCost = 0;
            this.TotalPayment = 0;
        }
    }
}
