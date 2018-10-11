using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class SwimmingRateController
    {
       

        public static bool Insert(SwimmingRate swm)
            {
                const string GET_INSERT = "Insert into [tbSwimmingPool] (AmenityId,AdultRate,ChildRate) values (@AmenityId,@AdultRate,@ChildRate)";
                SqlCommand com = new SqlCommand(GET_INSERT);
                com.Parameters.Add(new SqlParameter("@AmenityId", swm.AmenityId));
                com.Parameters.Add(new SqlParameter("@AdultRate", swm.Adult));
                com.Parameters.Add(new SqlParameter("@ChildRate", swm.Child));
                return SqlManager.ExecuteNonQuery(com);
            }
            public static bool Update(SwimmingRate swm)
            {
                const string GET_UPDATE = "Update [tbSwimmingPool] set AdultRate=@AdultRate, ChildRate=@ChildRate where AmenityId=@AmenityId";
                SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@AmenityId", swm.AmenityId));
            com.Parameters.Add(new SqlParameter("@AdultRate", swm.Adult));
                com.Parameters.Add(new SqlParameter("@ChildRate", swm.Child));
                return SqlManager.ExecuteNonQuery(com);
            }

    }
}
