using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Notification : BaseProperty<Notification>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }
        public int refno { get; set; }
        public DateTime Date { get; set; }
        public int Visit { get; set; }
        public int OwnerId { get; set; }
        public int TenantId { get; set; }
        public string typer { get; set; }
        public string Type { get; set; }
      
        public Notification CreateObject(SqlDataReader reader)
        {
            Notification ret = new Notification();
            ret.Id = reader.GetInt32(0);
            ret.Rate = reader.GetDecimal(1);
            ret.refno = reader.GetInt32(2);
            ret.Date = reader.GetDateTime(3);
            ret.Visit = reader.GetInt32(4);
            ret.OwnerId = reader.GetInt32(5);
            ret.TenantId = reader.GetInt32(6);
            ret.Type = reader.GetString(7);
            return ret;

        }

        public void Reset()
        {
            this.Id = 0;
            this.refno = 0;
            this.Rate = 0;
            this.Visit = 0;
            this.OwnerId = 0; ;
            this.TenantId = 0;
            this.Type = string.Empty;
            this.Date = DateTime.Now;
        }
    }
}
