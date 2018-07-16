using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{


    public class Swimming : BaseProperty<Swimming>
    {
        public int Id { get; set; }
        public int SchedID { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }


        public Swimming CreateObject(SqlDataReader reader)
        {
            Swimming ret = new Swimming();
            ret.Id = reader.GetInt32(0);
            ret.SchedID = reader.GetInt32(1);
            ret.Adult = reader.GetInt32(2);
            ret.Child = reader.GetInt32(3);
            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
            this.SchedID = 0;
            this.Adult = 0;
            this.Child = 0;
        }


    }
}
