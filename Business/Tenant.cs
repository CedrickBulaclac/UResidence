using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
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
        }

        public Tenant CreateObject(SqlDataReader reader)
        {
            Tenant ret = new Tenant();

            ret.Id = reader.GetInt32(0);
            ret.UnitNo = reader.GetString(1);
            ret.BldgNo = reader.GetString(2);
            ret.Fname = reader.GetString(3);
            ret.Mname = reader.GetString(4);
            ret.Lname = reader.GetString(5);
            ret.Bdate = reader.GetDateTime(6);
            ret.CelNo = reader.GetString(7);
            ret.Email = reader.GetString(8);
            ret.LeaseStart = reader.GetDateTime(9);
            ret.LeaseEnd = reader.GetDateTime(10);

            return ret;
        }
    }
}
