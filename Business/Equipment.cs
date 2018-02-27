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
        public string Url { get; set; }

        public Equipment CreateObject(SqlDataReader reader)
        {
            Equipment ret = new Equipment();

            ret.Id = reader.GetInt32(0);
            ret.Name = reader.GetString(1);
            ret.Stocks = reader.GetInt32(2);
            ret.Rate = reader.GetInt32(3);
            ret.Url = reader.GetString(4);
            return ret;
        }
        public bool Validate(out string[] errors)
        {
            bool ret = true;

            List<string> err = new List<string>();

            if (this.Name == string.Empty || this.Name is null)
            {
                err.Add("Equipmemt name is required!");
                ret = false;
            }
            if (this.Stocks <= 0)
            {
                err.Add("Equipmemt Stocks must be at least 1");
                ret = false;
            }

            if (this.Rate <= 0)
            {
                err.Add("Equipmemt Rate must be at least 1");
                ret = false;
            }
            if(this.Url ==string.Empty|| this.Url is null)
            {
                err.Add("Images");
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
            this.Url = string.Empty;
        }
    }
}
