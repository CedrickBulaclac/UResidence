using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace UResidence
{
     public class Tenant : BaseProperty<Tenant>
    {
        public int Id { get; set; }
        public string UnitNo { get; set; }
        public string BldgNo { get; set; }  
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime Bdate { get; set; }
        public string CelNo { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LeaseStart { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LeaseEnd { get; set; }
        public string Deleted { get; set; }
        public string URL { get; set; }
        public HttpPostedFileBase Image { get; set; }

        public bool Validate(out string[] errors)
        {
            bool ret = true;
            List<string> err = new List<string>();
            if (this.UnitNo.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Tenant Unit No is required");
            }
            if (this.BldgNo.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Tenant Building No is required");
            }
            if (this.Lname.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Tenant Last name is required");
            }
            if (this.Email.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Tenant Email is required");
            }
            if (this.Bdate == DateTime.Today)
            {
                ret = false;
                err.Add("Tenant Birthday is required");
            }
            errors = err.ToArray();
            return ret;
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        public void Reset()
        {
            this.Id = 0;
            this.UnitNo = string.Empty;
            this.BldgNo = string.Empty;
            this.Fname = string.Empty;
            this.Mname = string.Empty;
            this.Lname = string.Empty;
            this.Bdate = DateTime.Today;
            this.CelNo = string.Empty;
            this.Email = string.Empty;
            this.LeaseStart = DateTime.Today;
            this.LeaseEnd = DateTime.Today;
            this.Deleted = string.Empty;
            this.URL = string.Empty;
         
        }

        public Tenant CreateObject(SqlDataReader reader)
        {
            Tenant ret = new Tenant();

            ret.Id = reader.GetInt32(0);
            ret.UnitNo = RemoveWhitespace(reader.GetString(1));
            ret.BldgNo = RemoveWhitespace(reader.GetString(2));
            ret.Fname = RemoveWhitespace(reader.GetString(3));
            ret.Mname = RemoveWhitespace(reader.GetString(4));
            ret.Lname = RemoveWhitespace(reader.GetString(5));
            ret.Bdate = reader.GetDateTime(6);
            ret.CelNo = RemoveWhitespace(reader.GetString(7));
            ret.Email = reader.GetString(8);
            ret.LeaseStart = reader.GetDateTime(9);
            ret.LeaseEnd = reader.GetDateTime(10);
            ret.Deleted = RemoveWhitespace(reader.GetString(11));
            ret.URL = RemoveWhitespace(reader.GetString(12));
            return ret;
        }
    }
}
