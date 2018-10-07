using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class Stocks : BaseProperty<Stocks>
    {
        public int RFId { get; set; }
        public int EId { get; set; }
        public string EName { get; set; }
        public int EStocks { get; set; }

        public Stocks CreateObject(SqlDataReader reader)
        {
            Stocks ret = new Stocks();
            ret.RFId = reader.GetInt32(0);
            ret.EId = reader.GetInt32(1);
            ret.EName = reader.GetString(2);
            ret.EStocks = reader.GetInt32(3);
            return ret;
        }

        public void Reset()
        {
            this.RFId = 0;
            this.EId = 0;
            this.EName = string.Empty;
            this.EStocks = 0;
        }
    }
}
