using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence

{
    public class Equipment : BaseProperty<Equipment>
    {
        public string Name { get; set; }
        public int Stocks { get; set; }
        public int Rate { get; set; }
        public int Id { get; set; }

        public Equipment CreateObject(SqlDataReader reader)
        {
            Equipment ret = new Equipment();

            ret.Id = reader.GetInt32(0);
            ret.Name = reader.GetString(1);
            ret.Stocks = reader.GetInt32(2);
            ret.Rate = reader.GetInt32(3);
            
            return ret;
        }
        public static Equipment CreateObject(NameValueCollection fc)
        {
            string Name = fc["Name"];
            int Stocks = Convert.ToInt32(fc["Stocks"]);
            int Rate = Convert.ToInt32(fc["Rate"]);
            int eno = Convert.ToInt32(fc["Id"]);
            Equipment eq = new Equipment()
            {
                Name = Name,
                Stocks = Stocks,
                Rate = Rate,
                Id = eno
            };
            return eq;
        }
        public bool Validate(out string[] errors)
        {
            bool ret = true;

            List<string> err = new List<string>();
            if(this.Name==string.Empty)
            {
                err.Add("Equipment Name is required");
                ret = false;
            }
            if (this.Stocks <= 0)
            {
                err.Add("Equipment Stocks must be at least 1");
                ret = false;
            }
            if (this.Rate <= 0)
            {
                err.Add("Equipment Rate must be at least 1");
                ret = false;
            }

           
            errors = err.ToArray();
            return ret;
        }
        public void Reset()
        {
            this.Name = string.Empty;
            this.Stocks = 0;
            this.Rate = 0;
            this.Id =0;
        }
    }
}
