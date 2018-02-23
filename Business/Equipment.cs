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
        public string EquipmentNo { get; set; }

        public Equipment CreateObject(SqlDataReader reader)
        {
            Equipment ret = new Equipment();
            ret.Name = reader.GetString(0);
            ret.Stocks = reader.GetInt32(1);
            ret.Rate = reader.GetInt32(2);
            ret.EquipmentNo = reader.GetString(3);
            return ret;
        }
        public static Equipment CreateObject(NameValueCollection fc)
        {
            string Name = fc["Name"];
            int Stocks = Convert.ToInt32(fc["Stocks"]);
            int Rate = Convert.ToInt32(fc["Rate"]);
            string eno = fc["EquipmentNo"];
            Equipment eq = new Equipment()
            {
                Name = Name,
                Stocks = Stocks,
                Rate = Rate,
                EquipmentNo = eno
            };
            return eq;
        }
        public bool Validate()
        {
            bool ret = false;
            if (this.EquipmentNo == string.Empty) ret = false;
            if (this.Name == string.Empty) ret = false;
            if (this.Rate == 0) ret = false;
            if (this.Stocks == 0) ret = false;
            return ret;
              
        }
            public void Reset()
        {
            this.Name = string.Empty;
            this.Stocks = 0;
            this.Rate = 0;
            this.EquipmentNo = string.Empty;
        }
    }
}
