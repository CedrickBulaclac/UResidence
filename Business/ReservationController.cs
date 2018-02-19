using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    class ReservationController
    {
        public static List<Reservation> GetAll()
        {
            const string GET_ALL = @"SELECT RefNo,OwnerNo,AcknowledgeBy,ReservedBy,Status FROM [tbReservationForm] order by RefNo";


            List<Reservation> ret = default(List<Reservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Reservation>(com);
            return ret;
        }



        public static Reservation GetStatus(string status)
        {
            const string GET_RECORD = @"SELECT RefNo,OwnerNo,AcknowledgeBy,ReservedBy,Status FROM [tbReservationForm] WHERE status = @status";

            Reservation ret = default(Reservation);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@RefNo", status));
            ret = SqlManager.Select<Reservation>(com).First();

            return ret;
        }





        public static Reservation GetRefNo(string rno)
        {
            const string GET_RECORD = @"SELECT RefNo,OwnerNo,AcknowledgeBy,ReservedBy,Status FROM [tbReservationForm] WHERE  RefNo = @RefNo";

            Reservation ret = default(Reservation);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@RefNo", rno));
            ret = SqlManager.Select<Reservation>(com).First();

            return ret;
        }




        public static bool Update(Reservation usr)
        {
            const string GET_UPDATE = @"update [tbReservationForm] set  Status = @Status WHERE RefNo = @RefNo";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Status", usr.Status));
            com.Parameters.Add(new SqlParameter("@RefNo", usr.RefNo));

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
            const string GET_INSERT = @"insert [tbReservationForm] (RefNo, OwnerNo, AcknowledgeBy, ReservedBy, Status) values (@RefNo, @OwnerNo, @AcknowledgeBy, @ReservedBy, @Status)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@RefNo", usr.RefNo));
            com.Parameters.Add(new SqlParameter("@OwnerNo", usr.OwnerNo));
            com.Parameters.Add(new SqlParameter("@AcknowledgeBy", usr.AcknowledgeBy));
            com.Parameters.Add(new SqlParameter("@ReservedBy", usr.ReservedBy));
            com.Parameters.Add(new SqlParameter("@Status", usr.Status));

            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
