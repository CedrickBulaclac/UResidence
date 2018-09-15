using System;
using System.Collections.Generic;
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
        public DateTime Date { get; set; }
        public int Visit { get; set; }
        public int OwnerId { get; set; }
        public int TenantId { get; set; }

        public Notification CreateObject(SqlDataReader reader)
        {
            Notification ret = new Notification();
            ret.Id = reader.GetInt32(0);
            ret.Description = reader.GetString(1);
            ret.Date = reader.GetDateTime(2);
            ret.Visit = reader.GetInt32(3);
            ret.OwnerId = reader.GetInt32(4);
            ret.TenantId = reader.GetInt32(5);
            return ret;

        }

        public void Reset()
        {
            this.Id = 0;
            this.Description = string.Empty;
            this.Visit = 0;
            this.OwnerId = 0; ;
            this.TenantId = 0;
            this.Date = DateTime.Now;
        }
    }
}
