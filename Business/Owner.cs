using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Owner : BaseProperty<Owner>
    {
        public int Id { get; set; }
        public string OwnerNo { get; set; }
        public string BldgNo { get; set; }
        public string UnitNo { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public DateTime Bdate { get; set; }
        public string CelNo { get; set; }
        public string TelNo { get; set; }
        public string Email { get; set; }
        public string Citizenship { get; set; }
        public string Status { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
       
        public Owner CreateObject(SqlDataReader reader)
        {
            Owner ret = new Owner();
            ret.Id = reader.GetInt32(0);
            ret.OwnerNo = reader.GetString(1);
            ret.BldgNo = reader.GetString(2);
            ret.UnitNo = reader.GetString(3);
            ret.Fname = reader.GetString(4);
            ret.Mname = reader.GetString(5);
            ret.Lname = reader.GetString(6);
            ret.Bdate = reader.GetDateTime(7);
            ret.CelNo = reader.GetString(8);
            ret.TelNo = reader.GetString(9);
            ret.Email = reader.GetString(10);
            ret.Citizenship = reader.GetString(11);
            ret.Status = reader.GetString(12);
            ret.Gender = reader.GetInt32(13);
            ret.Age = reader.GetInt32(14);
            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
            this.OwnerNo = string.Empty;
            this.BldgNo = string.Empty;
            this.UnitNo = string.Empty;
            this.Fname = string.Empty;
            this.Mname = string.Empty;
            this.Lname = string.Empty;
            this.Bdate = DateTime.Now;
            this.CelNo = string.Empty;
            this.TelNo = string.Empty;
            this.Email = string.Empty;
            this.Citizenship = string.Empty;
            this.Status = string.Empty;
            this.Gender = 0;
            this.Age = 0;

        }
    }
}
