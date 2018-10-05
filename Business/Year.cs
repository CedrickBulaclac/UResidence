using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Year : BaseProperty<Year>
    {
        public string AmenityName { get; set; }
        public int Number { get; set; }       
        public Year CreateObject(SqlDataReader reader)
        {
            Year ret = new Year();
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
