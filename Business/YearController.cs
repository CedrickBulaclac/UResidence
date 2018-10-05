using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class YearController
    {
        public static List<Year> GetAll(int year)
        {
            const string GET_ALL = @"select a.AmenityName,COUNT(sr.Id) as Count from tbSchedReservation sr inner join tbReservationForm rf on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id where Status='Reserved' and  YEAR(StartTime)=@year group by AmenityName";
            List<Year> ret = default(List<Year>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@year", year));
            ret = SqlManager.Select<Year>(com);
            return ret;
        }

    }
}
