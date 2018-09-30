using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class DashResult : BaseProperty<DashResult>
    {
        public string AmenityName { get; set; }
        public int Number { get; set; }

        public DashResult CreateObject(SqlDataReader reader)
        {
            DashResult ret = new DashResult();
            ret.AmenityName = reader.GetString(0);
            ret.Number = reader.GetInt32(1);
            return ret;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
