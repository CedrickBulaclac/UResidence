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
            const string GET_ALL = @"SELECT Id,AmenityId,StartTime,EndTIme,Rate,Deleted FROM [tbSchedReservation] where Deleted=0";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }

        public static List<SchedReservation> GetAll(string sd, string ed,int aid)
        {
            const string GET_ALL = @"SELECT Id,AmenityId,StartTime,EndTIme,Rate,Deleted FROM [tbSchedReservation] where EndTime between @sd and @ed and AmenityId=@id and Deleted=0";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@sd", sd));
            com.Parameters.Add(new SqlParameter("@ed", ed));
            com.Parameters.Add(new SqlParameter("@id", aid));
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }

        public static List<SchedReservation> GetAllChoose(string sd, string ed, int aid)
        {
            const string GET_ALL = @"SELECT sr.Id,AmenityId,StartTime,EndTIme,Rate,Deleted FROM [tbSchedReservation] sr inner join tbReservationForm rf on rf.SchedId=sr.Id  where Status='Reserved' and EndTime between @sd and @ed and AmenityId=@id and Deleted=0";
            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@sd", sd));
            com.Parameters.Add(new SqlParameter("@ed", ed));
            com.Parameters.Add(new SqlParameter("@id", aid));
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }
        public static List<SchedReservation> GetAllC(DateTime sd, DateTime ed, int aid)
        {
            const string GET_ALL = @"SELECT sr.Id,AmenityId,StartTime,EndTIme,Rate,Deleted FROM [tbSchedReservation] sr inner join tbReservationForm rf on rf.SchedId=sr.Id  where Status='Reserved' and EndTime between @sd and @ed and AmenityId=@id and Deleted=0";
             

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
            const string GET_ALL = @"SELECT a.Id,AmenityId,b.AmenityName,a.Rate,StartTime,EndTIme,b.Color,a.Deleted from [tbSchedReservation] a inner join [tbAmenity] b on a.AmenityId=b.Id where AmenityId=@id and a.Deleted=0";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@id", id));
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }
        public static List<SchedReservation> GetAllA(string name)
        {
            const string GET_ALL = @"SELECT a.Id,AmenityId,b.AmenityName,a.Rate,StartTime,EndTIme,b.Color,a.Deleted from [tbSchedReservation] a full join [tbAmenity] b on a.AmenityId=b.Id inner join tbReservationForm c on a.Id=c.SchedId where AmenityName=@name and a.Id is not null and Status='Reserved' and a.Deleted=0";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@name",name));
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }
        public static List<SchedReservation> GetAllA()
        {
            const string GET_ALL = @"SELECT a.Id,AmenityId,b.AmenityName,a.Rate,StartTime,EndTIme,b.Color,a.Deleted from [tbSchedReservation] a inner join [tbAmenity] b on a.AmenityId=b.Id where a.Deleted=0";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }
        public static List<SchedReservation> GetAllM()
        {
            const string GET_ALL = @"SELECT a.Id,AmenityId,b.AmenityName,a.Rate,StartTime,EndTIme,b.Color,a.Deleted from [tbSchedReservation] a inner join [tbAmenity] b on a.AmenityId=b.Id where a.Deleted=0";


            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<SchedReservation>(com);
            return ret;
        }
        public static SchedReservation GetId(int id)
        {
            const string GET_RECORD = @"SELECT Id,AmenityId,StartTime,EndTIme,Rate,Theme,Deleted FROM [tbSchedReservation] WHERE Id = @Id and Deleted=0";

            SchedReservation ret = default(SchedReservation);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<SchedReservation>(com).First();

            return ret;
        }

      

        public static List<SchedReservation> GetAmenityNo(string ano)
        {
            const string GET_RECORD = @"SELECT Id,AmenityId,StartTime,EndTIme,Rate,Deleted FROM [tbSchedReservation] WHERE AmenityId = @AmenityId and Deleted=0";

            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@AmenityId", ano));
            ret = SqlManager.Select<SchedReservation>(com);

            return ret;
        }


        public static List<SchedReservation> GetAmenityNo(string ano, string sd, string ed,DateTime date)
        {
            const string GET_RECORD = @"SELECT Id,AmenityId,StartTime,EndTIme,Rate,Deleted  FROM [tbSchedReservation] WHERE AmenityId = @AmenityId and StartTime = @StartTime and EndTime = @EndTime and Date = @Date and Deleted=0";

            List<SchedReservation> ret = default(List<SchedReservation>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@AmenityId", ano));
            com.Parameters.Add(new SqlParameter("@StartTime", sd));
            com.Parameters.Add(new SqlParameter("@EndTime", ed));
            com.Parameters.Add(new SqlParameter("@Date", date));
            ret = SqlManager.Select<SchedReservation>(com);

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

        public static bool UpdateDelete(SchedReservation usr)
        {
            const string GET_UPDATE = @"update [tbSchedReservation] set Deleted= @Deleted WHERE Id= @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", usr.Id));
            com.Parameters.Add(new SqlParameter("@Deleted", usr.Deleted));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Deleted(SchedReservation usr)
        {
            const string GET_DELETE = @"delete [tbSChedReservation] WHERE AmenityId = @AmenityId";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@AmenityId", usr.AmenityId));

            return SqlManager.ExecuteNonQuery(com);
        }

    

        public static bool Insert(SchedReservation usr)
        {
            const string GET_INSERT = @"insert [tbSchedReservation] (AmenityId,StartTime, EndTIme, Rate,Date,Deleted ) values (@AmenityId ,@StartTime,@EndTIme , @Rate,@Date,@Deleted )";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@AmenityId", usr.AmenityId));
            com.Parameters.Add(new SqlParameter("@StartTime", usr.StartTime));
            com.Parameters.Add(new SqlParameter("@EndTIme", usr.EndTIme));
            com.Parameters.Add(new SqlParameter("@Rate", usr.Rate));
            com.Parameters.Add(new SqlParameter("@Date",usr.Date));
            com.Parameters.Add(new SqlParameter("@Deleted", usr.Deleted));

            return SqlManager.ExecuteNonQuery(com);
        }




        }
    }

