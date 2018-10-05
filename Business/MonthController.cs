using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class MonthController
    {
        public static List<Month> GetAll(int month,int year)
        {
            const string GET_ALL = @"select a.AmenityName,COUNT(sr.Id) as Count from tbSchedReservation sr inner join tbReservationForm rf on rf.SchedId=sr.Id inner join tbAmenity a on sr.AmenityId=a.Id where Status='Reserved' and (MONTH(StartTime))=@month and YEAR(StartTime)=@year group by AmenityName";
            List<Month> ret = default(List<Month>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@month", month));
            com.Parameters.Add(new SqlParameter("@year", year));
            ret = SqlManager.Select<Month>(com);
            return ret;
        }
    }
}
