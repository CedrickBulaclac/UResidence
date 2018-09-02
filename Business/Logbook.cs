using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Logbook : BaseProperty<Logbook>
    {
        public int Id { get; set; }
        public DateTime date { get; set; }
        public string VisitorName { get; set; }
        public string ResidentName { get; set; }
        public string Purpose { get; set; }
        public DateTime Timein { get; set; }
        public DateTime Timeout { get; set; }
        public Logbook CreateObject(SqlDataReader reader)
        {
            Logbook ret = new Logbook();

            ret.Id = reader.GetInt32(0);
            ret.date = reader.GetDateTime(1);
            ret.VisitorName = reader.GetString(2);
            ret.ResidentName = reader.GetString(3);
            ret.Timein = reader.GetDateTime(4);
            ret.Timeout = reader.GetDateTime(5);
            ret.Purpose = reader.GetString(6);

            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
            this.date = DateTime.Now;
            this.VisitorName = string.Empty;
            this.ResidentName = string.Empty;
            this.Timein = DateTime.Now;
            this.Timeout = DateTime.Now;
            this.Purpose = string.Empty;
        }
    }
}
