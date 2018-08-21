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
        public int Totalpayment { get; set; }
        public int Charge { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }

        public void Reset()
        {
            this.ORNo = 0;
            this.RefNo =0;
            this.Totalpayment = 0;
            this.Charge = 0;
            this.Date = DateTime.Today;
            this.Description = string.Empty;
            this.CreatedBy = string.Empty;
            this.ApprovedBy = string.Empty;
        }

        public Receipt CreateObject(SqlDataReader reader)
        {
            Receipt ret = new Receipt();

            ret.ORNo = reader.GetInt32(0);
            ret.RefNo = reader.GetInt32(1);
            ret.Totalpayment = reader.GetInt32(2);
            ret.Charge = reader.GetInt32(3);
            ret.Date = reader.GetDateTime(4);
            ret.Description = reader.GetString(5);
            ret.CreatedBy = reader.GetString(6);
            ret.ApprovedBy = reader.GetString(7);
            return ret;

            
        }
    }
}
