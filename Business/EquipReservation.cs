using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class EquipReservation : BaseProperty<EquipReservation>
    {
       public int EquipNo { get; set; }
       public int Quantity  { get; set; }
       public int RefNo { get; set; }
        public int Rate { get; set; }
        public EquipReservation CreateObject(SqlDataReader reader)
        {
            EquipReservation ret = new EquipReservation();
            ret.EquipNo = reader.GetInt32(0);
            ret.Quantity = reader.GetInt32(1);
            ret.RefNo = reader.GetInt32(3);
            ret.Rate = reader.GetInt32(4);
            return ret;
        }

        public void Reset()
        {
            this.EquipNo = 0;
            this.Quantity = 0;
            this.RefNo = 0;
            this.Rate = 0;
        }
    }
}
