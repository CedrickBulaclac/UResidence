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
        public static List<Swimming> GETALL()
        {
            const string GET_ALL = "Select Id,SchedID,Adult,Child from [tbSwimmingPayment] order by SchedID ";

            List<Swimming> ret = default(List<Swimming>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Swimming>(com);
            return ret;
        }
        public static List<Swimming> GETALL(int id)
        {
            const string GET_ALL = "Select Id,SchedID,Adult,Child from [tbSwimmingPayment] where SchedID=@SchedID order by SchedID";

            List<Swimming> ret = default(List<Swimming>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@SchedID", id));
            ret = SqlManager.Select<Swimming>(com);
            return ret;
        }

        public static bool Update(Swimming swm)
        {
            const string GET_UPDATE = "Update from [tbSwimmingPayment] set SchedID=@SchedID, Adult=@Adult, Child=@Child";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@SchedID", swm.SchedID));
            com.Parameters.Add(new SqlParameter("@Adult", swm.Adult));
            com.Parameters.Add(new SqlParameter("@Child", swm.Child));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Swimming swm)
        {
            const string GET_DELETE = "Delete from [tbSwimmingPayment] where SchedID=@SchedID";
            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@SchedID", swm.SchedID));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Insert(Swimming swm)
        {
            const string GET_INSERT = "Insert into [tbSwimmingPayment] (SchedID,Adult,Child) values (@SchedID,@Adult,@Child)";
            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@SchedID", swm.SchedID));
            com.Parameters.Add(new SqlParameter("@Adult", swm.Adult));
            com.Parameters.Add(new SqlParameter("@Child", swm.Child));
            return SqlManager.ExecuteNonQuery(com);
        }

    }
}
