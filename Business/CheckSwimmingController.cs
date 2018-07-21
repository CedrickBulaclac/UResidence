using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class CheckSwimmingController
    {


        public static CheckSwimming Get(string sd, int aid)
        {
            CheckSwimming ret = default(CheckSwimming);
            try
            {
                const string GET_RECORD = @"select (Capacity-SUM(child+adult)) as Total from tbSwimmingPayment sp inner join tbSchedReservation sr on sr.Id=sp.SchedId inner join tbAmenity a on a.Id=sr.AmenityId where StartTime=@sd and AmenityId=@id group by Capacity,Child,Adult";


                SqlCommand com = new SqlCommand(GET_RECORD);
                com.Parameters.Add(new SqlParameter("@sd", sd));
                com.Parameters.Add(new SqlParameter("@id", aid));
                ret = SqlManager.Select<CheckSwimming>(com).First();
            }
            catch (Exception)
            {
                ret = null;
            }
            return ret;
        }

    }

}

