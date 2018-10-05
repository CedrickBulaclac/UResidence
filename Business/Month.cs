using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Month : BaseProperty<Month>
    {
        public string AmenityName { get; set; }
        public int Number { get; set; }

        public Month CreateObject(SqlDataReader reader)
        {
            Month ret = new Month();
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
