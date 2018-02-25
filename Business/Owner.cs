using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace UResidence
{
    public class Owner : BaseProperty<Owner>
    {
        public int Id { get; set; }
        public string BldgNo { get; set; }
        public string UnitNo { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        [DataType(DataType.Date)]
        public DateTime Bdate { get; set; }
        public string CelNo { get; set; }
        public string TelNo { get; set; }
        public string Email { get; set; }
        public string Citizenship { get; set; }
        public string CivilStatus { get; set; }
        public string Gender { get; set; }


        public Owner CreateObject(SqlDataReader reader)
        {
            Owner ret = new Owner();
            ret.Id = reader.GetInt32(0);
            ret.BldgNo = reader.GetString(1);
            ret.UnitNo = reader.GetString(2);
            ret.Fname = reader.GetString(3);
            ret.Mname = reader.GetString(4);
            ret.Lname = reader.GetString(5);
            ret.Bdate = reader.GetDateTime(6);
            ret.CelNo = reader.GetString(7);
            ret.TelNo = reader.GetString(8);
            ret.Email = reader.GetString(9);
            ret.Citizenship = reader.GetString(10);
            ret.CivilStatus = reader.GetString(11);
            ret.Gender = reader.GetString(12);
            return ret;
        }

        public bool Validate(out string[] errors)
        {
            bool ret = true;
            List<string> err = new List<string>();
            if (this.UnitNo.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Owner Unit No is required");
            }
            if (this.BldgNo.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Owner Building No is required");
            }
            if (this.Lname.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Owner Last name is required");
            }
            if (this.Gender.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Owner Gender is required");
            }
            if (this.Email.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Owner Email is required");
            }
            if (this.Citizenship.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Owner Citizenship is required");
            }
            if (this.Bdate == DateTime.Today)
            {
                ret = false;
                err.Add("Owner Birthday is required");
            }
            errors = err.ToArray();
            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
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
            this.CivilStatus = string.Empty;
            this.Gender = string.Empty;

        }
    }
}
