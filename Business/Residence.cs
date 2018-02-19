using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    class Residence : BaseProperty<Residence>
    {
        public int Id { get; set; }
        public string OwnerNo { get; set; }
        public string TenantNo { get; set; }


        public void Reset()
        {
            this.Id = 0;
            this.OwnerNo = string.Empty;
            this.TenantNo = string.Empty;        
        }

        public Residence CreateObject(SqlDataReader reader)
        {
            Residence ret = new Residence();

            ret.Id = reader.GetInt32(0);
            ret.OwnerNo = reader.GetString(1);
            ret.TenantNo = reader.GetString(2);     
            return ret;
        }
    }
}
