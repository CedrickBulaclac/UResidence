using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class StockController
    {

        public static List<Stocks> GetStocks(DateTime st, DateTime et)
        {
            const string GET_ALL = @"select rf.Id,e.Id,e.Name,(e.Stocks - er.Quantity) as Stocks from tbEquipReservation er inner join tbReservationForm rf on er.RefNo=rf.Id inner join tbEquipment e on e.Id=er.EquipmentId inner join tbSchedReservation sr on sr.Id=rf.SchedId where EndTIme between @st and @et and rf.Status='Reserved'";

            List<Stocks> ret = default(List<Stocks>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@st", st));
            com.Parameters.Add(new SqlParameter("@et", et));
            ret = SqlManager.Select<Stocks>(com);
            return ret;
        }


    }
}
