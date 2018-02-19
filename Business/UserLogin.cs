using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class UserLogin : BaseProperty<UserLogin>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public int Locked { get; set; }
        public string CreatedBy { get; set; }
        public string ModifyBy { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Level { get; set; }
        public DateTime LastLogin { get; set; }

        public void Reset()
        {
            this.Id = 0;
            this.Username = string.Empty;
            this.CreatedBy = string.Empty;
            this.Locked = 0;
            this.ModifyBy = string.Empty;
            this.DateCreated = DateTime.Today;
            this.ModifiedDate = DateTime.Today;
            this.LastLogin = DateTime.Today;
            this.Level = 0;
            this.Hash = string.Empty;
        }

        public UserLogin CreateObject(SqlDataReader reader)
        {
            UserLogin ret = new UserLogin();

            ret.Id = reader.GetInt32(0);
            ret.Username = reader.GetString(1);
            ret.Hash = reader.GetString(2);
            ret.CreatedBy = reader.GetString(4);
            ret.ModifyBy = reader.GetString(5);
            ret.DateCreated = reader.GetDateTime(6);
            ret.ModifiedDate = reader.GetDateTime(7);
            ret.Level = Convert.ToInt32(reader.GetValue(3));
            ret.Locked = Convert.ToInt32(reader.GetValue(2));
            return ret;
        }

    }
}
