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
                const string GET_INSERT = "Insert into [tbSwimmingPool] (AmenityId,AdultRate,ChildRate,Deleted) values (@AmenityId,@AdultRate,@ChildRate,@Deleted)";
                SqlCommand com = new SqlCommand(GET_INSERT);
                com.Parameters.Add(new SqlParameter("@AmenityId", swm.AmenityId));
                com.Parameters.Add(new SqlParameter("@AdultRate", swm.Adult));
                com.Parameters.Add(new SqlParameter("@ChildRate", swm.Child));
                com.Parameters.Add(new SqlParameter("@Deleted", swm.Deleted));
                return SqlManager.ExecuteNonQuery(com);
            }
            public static bool Update(SwimmingRate swm)
            {
                const string GET_UPDATE = "Update [tbSwimmingPool] set AdultRate=@AdultRate, ChildRate=@ChildRate, Deleted=@Deleted where AmenityId=@AmenityId";
                SqlCommand com = new SqlCommand(GET_UPDATE);
                 com.Parameters.Add(new SqlParameter("@AmenityId", swm.AmenityId));
                 com.Parameters.Add(new SqlParameter("@AdultRate", swm.Adult));
                com.Parameters.Add(new SqlParameter("@ChildRate", swm.Child));
            com.Parameters.Add(new SqlParameter("@Deleted", swm.Deleted));
            return SqlManager.ExecuteNonQuery(com);
            }
        public static bool UpdateDelete(int id)
        {
            const string GET_UPDATE = "Update [tbSwimmingPool] set Deleted=1 where AmenityId=@AmenityId";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@AmenityId", id));
            return SqlManager.ExecuteNonQuery(com);
        }

    }
}
