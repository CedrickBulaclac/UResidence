using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    class SchedReservationController
    {
        public static List<SchedReservation> GetAll()
        {
            const string GET_ALL = @"SELECT AmenityNo,StartTime,EndTIme,Rate FROM [tbSchedReservation] order by AmenityNo";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }

        public static SchedReservation GetAmenityNo(string ano)
        {
            const string GET_RECORD = @"SELECT AmenityNo,StartTime,EndTIme,Rate FROM [tbSchedReservation] WHERE AmenityNo = @AmenityNo";

            SchedReservation ret = default(SchedReservation);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@AmenityNo", ano));
            ret = SqlManager.Select<SchedReservation>(com).First();

            return ret;
        }

       


        public static bool Update(SchedReservation usr)
        {
            const string GET_UPDATE = @"update [tbSchedReservation] set AmenityNo= @AmenityNo, Rate = @Rate WHERE AmenityNo = @AmenityNo";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@AmenityNo", usr.AmenityNo));
            com.Parameters.Add(new SqlParameter("@Rate", usr.Rate));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(SchedReservation usr)
        {
            const string GET_DELETE = @"delete [tbSChedReservation] WHERE AmenityNo = @AmenityNo";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@AmenityNo", usr.AmenityNo));

            return SqlManager.ExecuteNonQuery(com);
        }

        //public static bool Modify(SchedReservation usr)
        //{
        //    if (usr.Id == 0)
        //        return Insert(usr);
        //    else
        //        return Update(usr);
        //}

        public static bool Insert(SchedReservation usr)
        {



            const string GET_INSERT = @"insert [tbSchedReservation] (AmenityNo,StartTime, EndTIme, Rate) values (@AmenityNo ,getdate(), getdate(), @Rate  )";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@AmenityNo", usr.AmenityNo));
            com.Parameters.Add(new SqlParameter("@StartTime", usr.StartTime));
            com.Parameters.Add(new SqlParameter("@EndTIme", usr.EndTIme));
            com.Parameters.Add(new SqlParameter("@Rate", usr.Rate));

            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
