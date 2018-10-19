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
        public int RefNo { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public decimal AdultRate { get; set; }
        public decimal ChildRate { get; set; }
        public int AmenityId { get; set; }

        public Swimming CreateObject(SqlDataReader reader)
        {
            Swimming ret = new Swimming();
            ret.Id = reader.GetInt32(0);
            ret.RefNo = reader.GetInt32(1);
            ret.Adult = reader.GetInt32(2);
            ret.Child = reader.GetInt32(3);
            ret.AdultRate = reader.GetDecimal(4);
            ret.ChildRate = reader.GetDecimal(5);
            ret.AmenityId = reader.GetInt32(6);

            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
            this.RefNo = 0;
            this.Adult = 0;
            this.Child = 0;
            this.AmenityId = 0;

        }


    }
}
