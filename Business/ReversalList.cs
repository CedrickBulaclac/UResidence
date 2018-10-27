using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class ReversalList : BaseProperty<ReversalList>
    {
        public string CreatedBy { get; set; }
        public string Fullname { get; set; }
        public string DateofReservation { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
       public string Date { get; set; }
        public int Id { get; set; }
        public int RefNo { get; set; }
        public string AmenityName { get; set; }
        public ReversalList CreateObject(SqlDataReader reader)
        {
            ReversalList ret = new ReversalList();
            ret.CreatedBy = reader.GetString(0);
            ret.Fullname = reader.GetString(1);
            ret.DateofReservation = reader.GetString(2);
            ret.Amount = reader.GetDecimal(3);
            ret.Description = reader.GetString(4);
            ret.Status = reader.GetString(5);
            ret.Date = reader.GetString(6);
            ret.Id = reader.GetInt32(7);
            ret.RefNo = reader.GetInt32(8);
            ret.AmenityName = reader.GetString(9);
            return ret;
        }

        public void Reset()
        {
            this.CreatedBy = string.Empty;
            this.Fullname = string.Empty;
            this.DateofReservation = string.Empty;
            this.Amount = 0;
            this.Description = string.Empty;
            this.Status = string.Empty;
            this.Date = string.Empty;
            this.Id = 0;
            this.RefNo = 0;
        }
    }
}
