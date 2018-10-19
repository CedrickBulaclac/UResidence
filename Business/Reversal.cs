using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Reversal : BaseProperty<Reversal>
    {
        public int Id { get; set; }
        public int RefNo { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public decimal Amount { get; set; }

       public Reversal CreateObject(SqlDataReader reader)
        {
            Reversal ret = new Reversal();
            ret.Id = reader.GetInt32(0);
            ret.RefNo = reader.GetInt32(1);
            ret.Description = reader.GetString(2);
            ret.Status = reader.GetString(3);
            ret.CreatedBy = reader.GetString(4);
            ret.ApprovedBy = reader.GetString(5);
            ret.Amount = reader.GetDecimal(6);
            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
            this.RefNo = 0;
            this.Description = string.Empty;
            this.Status = string.Empty;
            this.CreatedBy = string.Empty;
            this.ApprovedBy = string.Empty;
            this.Amount = 0;
        }        
    }
}
