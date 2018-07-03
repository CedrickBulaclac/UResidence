using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class Residence : BaseProperty<Residence>
    {
        public int Id { get; set; }
        public int OwnerNo { get; set; }
        public int TenantNo { get; set; }


        public void Reset()
        {
            this.Id = 0;
            this.OwnerNo = 0;
            this.TenantNo = 0;        
        }

        public Residence CreateObject(SqlDataReader reader)
        {
            Residence ret = new Residence();

            ret.Id = reader.GetInt32(0);
            ret.OwnerNo = reader.GetInt32(1);
            ret.TenantNo = reader.GetInt32(2);     
            return ret;
        }
    }
}
