using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Billing : BaseProperty<Billing>
    {
        public int Rate { get; set; }
        public int Charge { get; set; }
        public int ChairCost { get; set; }
        public int TableCost { get; set; }
        public int Totale { get; set; }
        public int Amount { get; set; }
        public Billing CreateObject(SqlDataReader reader)
        {
            Billing ret = new Billing();
            ret.Rate = reader.GetInt32(0);
            ret.Charge = reader.GetInt32(1);
            ret.ChairCost = reader.GetInt32(2);
            ret.TableCost = reader.GetInt32(3);
            ret.Totale = reader.GetInt32(4);
            ret.Amount = reader.GetInt32(5);
            return ret;
        }

        public void Reset()
        {
            this.Rate = 0;
            this.Charge = 0;
            this.ChairCost = 0;
            this.TableCost = 0;
            this.Totale = 0;
            this.Amount = 0;
        }
    }
}
