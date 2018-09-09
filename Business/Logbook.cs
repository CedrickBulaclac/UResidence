using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
        public string sTimein { get; set; }
        public string sTimeout { get; set; }
        public string sDate { get; set; }
        public string URL { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public Logbook CreateObject(SqlDataReader reader)
        {
            Logbook ret = new Logbook();

            ret.Id = reader.GetInt32(0);
            ret.sDate = reader.GetString(1);
            ret.VisitorName = reader.GetString(2);
            ret.ResidentName = reader.GetString(3);
            ret.sTimein = reader.GetString(4);
            ret.sTimeout = reader.GetString(5);         
            ret.Purpose = reader.GetString(6);
            ret.URL = reader.GetString(7);

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
            this.sTimein = string.Empty;
            this.sTimeout = string.Empty;
            this.sDate = string.Empty;
            this.URL = string.Empty;
        }
    }
}
