using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Dashboard : BaseProperty<Dashboard>
    {
        public string AmenityName { get; set; }
        public int Number { get; set; }

        public Dashboard CreateObject(SqlDataReader reader)
        {
            Dashboard ret = new Dashboard();
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
