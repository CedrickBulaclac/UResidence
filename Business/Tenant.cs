using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
     public class Tenant : BaseProperty<Tenant>
    {
        public int Id { get; set; }
        public string UnitNo { get; set; }
        public string BldgNo { get; set; }
        public string TenantNo { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public string Gender { get; set; } 
        public int Age { get; set; }
        public string TelNo { get; set; }
        public string CelNo { get; set; }
        public string Email { get; set; }
        public string Citizenship { get; set; }
        public string Status { get; set; }
        public DateTime LeaseStart { get; set; }
        public DateTime LeaseEnd { get; set; }
        

        public void Reset()
        {
            this.Id = 0;
            this.UnitNo = string.Empty;
            this.BldgNo = string.Empty;
            this.TenantNo = string.Empty;
            this.Fname = string.Empty;
            this.Mname = string.Empty;
            this.Lname = string.Empty;
            this.Gender = string.Empty;
            this.Age = 0;
            this.TelNo = string.Empty;
            this.CelNo = string.Empty;
            this.Email = string.Empty;
            this.Citizenship = string.Empty;

            this.Status = string.Empty;
            this.LeaseStart = DateTime.Today;
            this.LeaseEnd = DateTime.Today;    
        }

        public Tenant CreateObject(SqlDataReader reader)
        {
            Tenant ret = new Tenant();

            ret.Id = reader.GetInt32(0);
            ret.UnitNo = reader.GetString(1);
            ret.BldgNo = reader.GetString(2);
            ret.TenantNo = reader.GetString(3);
            ret.Fname = reader.GetString(4);
            ret.Mname = reader.GetString(5);
            ret.Lname = reader.GetString(6);
            ret.Gender = reader.GetString(7);
            ret.Age = reader.GetInt32(8);
            ret.TelNo = reader.GetString(9);
            ret.CelNo = reader.GetString(10);
            ret.Email = reader.GetString(11);
            ret.Citizenship = reader.GetString(12);
            ret.Status = reader.GetString(13);
            ret.LeaseStart = reader.GetDateTime(14);
            ret.LeaseEnd = reader.GetDateTime(15);

            return ret;
        }
    }
}
