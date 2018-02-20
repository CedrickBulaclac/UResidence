using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Amenity : BaseProperty<Amenity>
    {
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string AmenityNo { get; set; }
        public Amenity CreateObject(SqlDataReader reader)
        {
            Amenity ret = new Amenity();
            ret.Description = reader.GetString(0);
            ret.Capacity = reader.GetInt32(1);
            ret.AmenityNo = reader.GetString(2);
            return ret;
        }

        public void Reset()
        {
            this.Description = string.Empty;
            this.Capacity = 0;
            this.AmenityNo = string.Empty;
        }
     
    }
}
