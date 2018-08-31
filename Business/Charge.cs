using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Charge : BaseProperty<Charge>
    {
        public int Id { get; set; }
        public int Refno { get; set; }
        public DateTime Date { get; set; }
        public string CreatedBy { get; set; }
        public int charge { get; set; }
        public string Description { get; set; }

        public Charge CreateObject(SqlDataReader reader)
        {
            Charge ret = new Charge();
           ret.Id = reader.GetInt32(0);
            ret.Refno = reader.GetInt32(1);
            ret.Date = reader.GetDateTime(2);
            ret.CreatedBy = reader.GetString(3);
            ret.charge = reader.GetInt32(4);
            ret.Description = reader.GetString(5);
            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
            this.Refno = 0;
            this.Date = DateTime.Now;
            this.CreatedBy = string.Empty;
            this.charge = 0;
            this.Description = string.Empty;
        }
    }
}
