using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Dash : BaseProperty<Dash>
    {
        public string AmenityName { get; set; }
        public int Value { get; set; }

        public Dash CreateObject(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
