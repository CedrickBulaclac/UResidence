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
    public class Admin : BaseProperty<Admin>
    {
        public int Id { get; set; }
        public string Fname{ get; set; }
        public string Mname{ get; set; }
        public string Lname { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Bdate { get; set; }
        public string CelNo { get; set; }
        public string Email{ get; set; }

        public Admin CreateObject(SqlDataReader reader)
        {
            Admin ret = new Admin();
            ret.Id = reader.GetInt32(0);
            ret.Fname = reader.GetString(1);
            ret.Mname = reader.GetString(2);
            ret.Lname = reader.GetString(3);
            ret.Bdate = reader.GetDateTime(4);
            ret.CelNo = reader.GetString(5);
            ret.Email = reader.GetString(6);        
            return ret;
        }
       
        public bool Validate(out string[] errors)
        {
            bool ret = true;
            List<string> err = new List<string>();

            if (this.Bdate.Date == DateTime.Now )
            {
                err.Add("Birthday is required");
                ret = false;
            }
            errors = err.ToArray();
            return ret;
        }
        public void Reset()
        {
            this.Id = 0;
            this.Fname = string.Empty;
            this.Mname = string.Empty;
            this.Lname = string.Empty;
            this.Bdate = DateTime.Now;
            this.CelNo = string.Empty;
            this.Email = string.Empty;
        }
    }

    }

