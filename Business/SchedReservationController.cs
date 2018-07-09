using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class SchedReservationController
    {
        public static List<SchedReservation> GetAll()
        {
            const string GET_ALL = @"SELECT Id,AmenityId,StartTime,EndTIme,Rate FROM [tbSchedReservation] order by AmenityId";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }

        public static List<SchedReservation> GetAll(string sd, string ed,int aid)
        {
            const string GET_ALL = @"SELECT Id,AmenityId,StartTime,EndTIme,Rate FROM [tbSchedReservation] where EndTime between @sd and @ed and AmenityId=@id";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@sd", sd));
            com.Parameters.Add(new SqlParameter("@ed", ed));
            com.Parameters.Add(new SqlParameter("@id", aid));
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }


        public static List<SchedReservation> GetAll(int id)
        {
            const string GET_ALL = @"SELECT a.Id,AmenityId,b.AmenityName,a.Rate,StartTime,EndTIme,b.Color from [tbSchedReservation] a inner join [tbAmenity] b on a.AmenityId=b.Id where AmenityId=@id";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@id", id));
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }
        public static List<SchedReservation> GetAllA(int id)
        {
            const string GET_ALL = @"SELECT a.Id,AmenityId,b.AmenityName,a.Rate,StartTime,EndTIme,b.Color from [tbSchedReservation] a inner join [tbAmenity] b on a.AmenityId=b.Id where a.AmenityId=@id";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@id", id));
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }
        public static List<SchedReservation> GetAllA()
        {
            const string GET_ALL = @"SELECT a.Id,AmenityId,b.AmenityName,a.Rate,StartTime,EndTIme,b.Color from [tbSchedReservation] a inner join [tbAmenity] b on a.AmenityId=b.Id";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }
        public static List<SchedReservation> GetAllM()
        {
            const string GET_ALL = @"SELECT a.Id,AmenityId,b.AmenityName,a.Rate,StartTime,EndTIme,b.Color from [tbSchedReservation] a inner join [tbAmenity] b on a.AmenityId=b.Id";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }
        public static SchedReservation GetId(int id)
        {
            const string GET_RECORD = @"SELECT Id,AmenityId,StartTime,EndTIme,Rate,Theme FROM [tbSchedReservation] WHERE Id = @Id";

            SchedReservation ret = default(SchedReservation);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<SchedReservation>(com).First();

            return ret;
        }
        public static SchedReservation GetAmenityNo(string ano)
        {
            const string GET_RECORD = @"SELECT Id,AmenityId,StartTime,EndTIme,Rate FROM [tbSchedReservation] WHERE AmenityId = @AmenityId";

            SchedReservation ret = default(SchedReservation);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@AmenityId", ano));
            ret = SqlManager.Select<SchedReservation>(com).First();

            return ret;
        }


        public static SchedReservation GetAmenityNo(string ano, string sd, string ed)
        {
            const string GET_RECORD = @"SELECT Id,AmenityId,StartTime,EndTIme,Rate FROM [tbSchedReservation] WHERE AmenityId = @AmenityId and StartTime = @StartTime and EndTime = @EndTime";

            SchedReservation ret = default(SchedReservation);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@AmenityId", ano));
            com.Parameters.Add(new SqlParameter("@StartTime", sd));
            com.Parameters.Add(new SqlParameter("@EndTime", ed));
            ret = SqlManager.Select<SchedReservation>(com).First();

            return ret;
        }





        public static bool Update(SchedReservation usr)
        {
            const string GET_UPDATE = @"update [tbSchedReservation] set AmenityId= @AmenityId, Rate = @Rate WHERE AmenityId = @AmenityId";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@AmenityId", usr.AmenityId));
            com.Parameters.Add(new SqlParameter("@Rate", usr.Rate));
         
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(SchedReservation usr)
        {
            const string GET_DELETE = @"delete [tbSChedReservation] WHERE AmenityId = @AmenityId";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@AmenityId", usr.AmenityId));

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
            const string GET_INSERT = @"insert [tbSchedReservation] (AmenityId,StartTime, EndTIme, Rate) values (@AmenityId ,@StartTime,@EndTIme , @Rate)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@AmenityId", usr.AmenityId));
            com.Parameters.Add(new SqlParameter("@StartTime", usr.StartTime));
            com.Parameters.Add(new SqlParameter("@EndTIme", usr.EndTIme));
            com.Parameters.Add(new SqlParameter("@Rate", usr.Rate));
  

            return SqlManager.ExecuteNonQuery(com);
        }




        }
    }

