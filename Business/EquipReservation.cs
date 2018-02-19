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
       public string EquipNo { get; set; }
       public int Quantity  { get; set; }
       public string RefNo { get; set; }
        public EquipReservation CreateObject(SqlDataReader reader)
        {
            EquipReservation ret = new EquipReservation();
            ret.EquipNo = reader.GetString(0);
            ret.Quantity = reader.GetInt32(1);
            ret.RefNo = reader.GetString(3);
            return ret;
        }

        public void Reset()
        {
            this.EquipNo = string.Empty;
            this.Quantity = 0;
            this.RefNo = string.Empty;
        }
    }
}
