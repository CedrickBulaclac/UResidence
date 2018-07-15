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
        public int Balance { get; set; }
        public Billing CreateObject(SqlDataReader reader)
        {
            Billing ret = new Billing();
            ret.Balance = reader.GetInt32(0);
            return ret;
        }

        public void Reset()
        {
           this.Balance = 0;
        }
    }
}
