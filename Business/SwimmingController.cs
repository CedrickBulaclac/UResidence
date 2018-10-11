using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class SwimmingController
    {
        public static List<Swimming> GETR(int refno)
        {
            const string GET_RECORD = @"select sp.Id,RefNo, Adult, Child,spp.AdultRate,spp.ChildRate,sp.AmenityId from tbSwimmingPayment sp inner join tbSwimmingPool spp on sp.AmenityId = spp.AmenityId where RefNo=@refno order by RefNo";
            List<Swimming> ret = default(List<Swimming>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@refno", refno));
            ret = SqlManager.Select<Swimming>(com);
            return ret;
        }
        public static List<Swimming> GETALL()
        {
            const string GET_ALL = "select sp.Id,RefNo, Adult, Child,spp.AdultRate,spp.ChildRate,sp.AmenityId from tbSwimmingPayment sp inner join tbSwimmingPool spp on sp.AmenityId = spp.AmenityId order by RefNo ";

            List<Swimming> ret = default(List<Swimming>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Swimming>(com);
            return ret;
        }

        public static bool Update(Swimming swm)
        {
            const string GET_UPDATE = "Update from [tbSwimmingPayment] set RefNo=@RefNo, Adult=@Adult, Child=@Child";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@RefNo", swm.RefNo));
            com.Parameters.Add(new SqlParameter("@Adult", swm.Adult));
            com.Parameters.Add(new SqlParameter("@Child", swm.Child));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Swimming swm)
        {
            const string GET_DELETE = "Delete from [tbSwimmingPayment] where RefNo=@RefNo";
            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@RefNo", swm.RefNo));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Insert(Swimming swm)
        {
            const string GET_INSERT = "Insert into [tbSwimmingPayment] (RefNo,Adult,Child,AmenityId) values (@RefNo,@Adult,@Child,@AmenityId)";
            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@RefNo", swm.RefNo));
            com.Parameters.Add(new SqlParameter("@Adult", swm.Adult));
            com.Parameters.Add(new SqlParameter("@Child", swm.Child));
            com.Parameters.Add(new SqlParameter("@AmenityId", swm.AmenityId));
            return SqlManager.ExecuteNonQuery(com);
        }

    }
}
