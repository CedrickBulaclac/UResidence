using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class Admin : BaseProperty<Admin>
    {
        public int Id { get; set; }
        public string AdminNo { get; set; }
        public string Fname{ get; set; }
        public string Mname{ get; set; }
        public string Lname { get; set; }
        public DateTime Bdate { get; set; }
        public string CelNo { get; set; }
        public string TelNo { get; set; }
        public char Gender { get; set; }
        public int Age { get; set; }
        public string Email{ get; set; }

        public Admin CreateObject(SqlDataReader reader)
        {
            Admin ret = new Admin();
            ret.Id = reader.GetInt32(0);
            ret.AdminNo=reader.GetString(1);
            ret.Fname = reader.GetString(2);
            ret.Mname = reader.GetString(3);
            ret.Lname = reader.GetString(4);
            ret.Bdate = reader.GetDateTime(5);
            ret.CelNo = reader.GetString(6);
            ret.TelNo = reader.GetString(7);
            ret.Gender = reader.GetChar(8);
            ret.Age = reader.GetInt32(9);
            ret.Email = reader.GetString(10);        
            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
            this.AdminNo = string.Empty;
            this.Fname = string.Empty;
            this.Mname = string.Empty;
            this.Lname = string.Empty;
            this.Bdate = DateTime.Now;
            this.CelNo = string.Empty;
            this.TelNo = string.Empty;
            this.Gender = 'm';
            this.Age = 0;
            this.Email = string.Empty;
        }
    }

    }

