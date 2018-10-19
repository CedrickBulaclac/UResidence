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
        public decimal Rate { get; set; }
        public decimal Charge { get; set; }
        public decimal ChairCost { get; set; }
        public decimal TableCost { get; set; }
        public decimal Totale { get; set; }
        public decimal Amount { get; set; }
        public Billing CreateObject(SqlDataReader reader)
        {
            Billing ret = new Billing();
            ret.Rate = reader.GetDecimal(0);
            ret.Charge = reader.GetInt32(1);
            ret.ChairCost = reader.GetDecimal(2);
            ret.TableCost = reader.GetDecimal(3);
            ret.Totale = reader.GetDecimal(4);
            ret.Amount = reader.GetDecimal(5);
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
