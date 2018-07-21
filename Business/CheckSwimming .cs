using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{


    public class CheckSwimming : BaseProperty<CheckSwimming>
    {
        public int Capacity { get; set; }


        public CheckSwimming CreateObject(SqlDataReader reader)
        {
            CheckSwimming ret = new CheckSwimming();
            ret.Capacity = reader.GetInt32(0);
               
            return ret;
        }

        public void Reset()
        {
            this.Capacity = 0;

        }

     
    }
}
