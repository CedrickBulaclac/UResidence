using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class ReservationController
    {
        public static List<Reservation> GetAll()
        {
            const string GET_ALL = @"SELECT Id,ResidentId,AcknowledgeBy,ReservedBy,Status,TypeResident,SchedId FROM [tbReservationForm] order by Id";


            List<Reservation> ret = default(List<Reservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Reservation>(com);
            return ret;
        }
        public static Reservation GetId(int sid)
        {
            const string GET_RECORD = @"SELECT Id,ResidentId,AcknowledgeBy,ReservedBy,Status,TypeResident,SchedId FROM [tbReservationForm] WHERE SchedId = @SchedId";

            Reservation ret = default(Reservation);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@SchedId", sid));
            ret = SqlManager.Select<Reservation>(com).First();

            return ret;
        }


        public static Reservation GetStatus(string status)
        {
            const string GET_RECORD = @"SELECT Id,ResidentId,AcknowledgeBy,ReservedBy,Status,TypeResident,SchedId FROM [tbReservationForm] WHERE status = @status order by Id";

            Reservation ret = default(Reservation);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@status", status));
            ret = SqlManager.Select<Reservation>(com).First();

            return ret;
        }


        public static bool Update(Reservation usr)
        {
            const string GET_UPDATE = @"update [tbReservationForm] set  Status = @Status WHERE Id = @RefNo";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Status", usr.Status));
            com.Parameters.Add(new SqlParameter("@RefNo", usr.Id));
            return SqlManager.ExecuteNonQuery(com);
        }

       
        // public static bool Modify(UserLogin usr)
        //{
        //    if (usr.Id == 0)
        //        return Insert(usr);
        //    else
        //        return Update(usr);
        //}
      

        public static bool Insert(Reservation usr)
        {
            const string GET_INSERT = @"insert [tbReservationForm] (ResidentId,AcknowledgeBy,ReservedBy,Status,TypeResident,SchedId) values (@ResidentId,@AcknowledgeBy,@ReservedBy,@Status,@TypeResident,@SchedId)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@ResidentId", usr.Rid));
            com.Parameters.Add(new SqlParameter("@AcknowledgeBy", usr.AcknowledgeBy));
            com.Parameters.Add(new SqlParameter("@ReservedBy", usr.ReservedBy));
            com.Parameters.Add(new SqlParameter("@Status", usr.Status));
            com.Parameters.Add(new SqlParameter("@TypeResident", usr.Tor));
            com.Parameters.Add(new SqlParameter("@SchedId", usr.Sid));

            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
