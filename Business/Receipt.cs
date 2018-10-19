using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class Receipt : BaseProperty<Receipt>
    {
        public int ORNo { get; set; }
        public int RefNo { get; set; }
        public decimal Totalpayment { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime st { get; set; }
        public DateTime et { get; set; }
        public string amen { get; set; }
        public string status { get; set; }

        public void Reset()
        {
            this.ORNo = 0;
            this.RefNo =0;
            this.Totalpayment = 0;     
            this.Date = DateTime.Today;
            this.Description = string.Empty;
            this.CreatedBy = string.Empty;
        }

        public Receipt CreateObject(SqlDataReader reader)
        {
            Receipt ret = new Receipt();

            ret.ORNo = reader.GetInt32(0);
            ret.RefNo = reader.GetInt32(1);
            ret.Totalpayment = reader.GetDecimal(2);
            ret.Date = reader.GetDateTime(3);
            ret.Description = reader.GetString(4);
            ret.CreatedBy = reader.GetString(5);
            return ret;

            
        }
    }
}
