using System;
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

        public void Reset()
        {
            this.Name = string.Empty;
            this.Stocks = 0;
            this.Rate = 0;
            this.EquipmentNo = string.Empty;
        }
    }
}
