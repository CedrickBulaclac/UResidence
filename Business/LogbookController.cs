using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class LogbookController
    {
        public static List<Logbook> GET_ALL()
        {
            const string GET_ALL = @"select Id,Convert(varchar(15),Date,101) as Date,VisitorName,ResidentName,RIGHT(Convert(varchar,TimeIn,100),7),RIGHT(Convert(varchar,TimeOut,100),7),Purpose,ISNULL(URL,'none'),BldgNo,UnitNo from [tbLogbook] where Convert(varchar(15),Date,101)=Convert(varchar(15),GETDATE(),101)";
            List<Logbook> ret = default(List<Logbook>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Logbook>(com);
            return ret;

        }
        public static List<Logbook> GET_ALL(DateTime date)
        {
            const string GET_ALL = @"select Id,Convert(varchar(15),Date,101) as Date,VisitorName,ResidentName,RIGHT(Convert(varchar,TimeIn,100),7),RIGHT(Convert(varchar,TimeOut,100),7),Purpose,ISNULL(URL,'none'),BldgNo,UnitNo from [tbLogbook] where Convert(varchar(15),Date,101)=Convert(varchar(15),@date,101)";
            List<Logbook> ret = default(List<Logbook>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@date", date));
            ret = SqlManager.Select<Logbook>(com);
            return ret;

        }
        public static List<Logbook> GET_ALL(DateTime date,string bldg, string unitno)
        {
            const string GET_ALL = @"select Id,Convert(varchar(15),Date,101) as Date,VisitorName,ResidentName,RIGHT(Convert(varchar,TimeIn,100),7),RIGHT(Convert(varchar,TimeOut,100),7),Purpose,ISNULL(URL,'none'),BldgNo,UnitNo from [tbLogbook] where Convert(varchar(15),Date,101)=Convert(varchar(15),@date,101) and BldgNo=@bldg and UnitNo=@unitno";
            List<Logbook> ret = default(List<Logbook>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@date", date));
            com.Parameters.Add(new SqlParameter("@bldg", bldg));
            com.Parameters.Add(new SqlParameter("@unitno", unitno));
            ret = SqlManager.Select<Logbook>(com);
            return ret;

        }
        public static bool Update(Logbook log)
        {
            const string GET_INSERT = @"update [tbLogbook] set TimeOut=@TimeOut where Id=@Id  ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Id", log.Id));
            com.Parameters.Add(new SqlParameter("@TimeOut", log.Timeout));
            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool UpdateTimein(Logbook log)
        {
            const string GET_INSERT = @"update [tbLogbook] set TimeIn=@TimeIn where Id=@Id  ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Id", log.Id));
            com.Parameters.Add(new SqlParameter("@TimeIn", log.Timein));
            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool UpdateName(Logbook log)
        {
            const string GET_INSERT = @"update [tbLogbook] set VisitorName=@VisitorName where Id=@Id  ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Id", log.Id));
            com.Parameters.Add(new SqlParameter("@VisitorName", log.VisitorName));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool UpdateImage(Logbook log)
        {
            const string GET_INSERT = @"update [tbLogbook] set URL=@URL where Id=@Id  ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Id", log.Id));
            com.Parameters.Add(new SqlParameter("@URL", log.URL));
            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool Delete(int id)
        {
            const string GET_DELETE = @"delete [tbLogbook] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", id));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool Insert(Logbook log)
        {
            const string GET_INSERT = @"insert [tbLogbook] (Date,VisitorName,ResidentName,TimeIn,TimeOut,Purpose,URL,BldgNo,UnitNo) values (@Date,@VisitorName,@ResidentName,@TimeIn,@TimeOut,@Purpose,@URL,@BldgNo,@UnitNo) ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Date", log.date));
            com.Parameters.Add(new SqlParameter("@VisitorName", log.VisitorName));
            com.Parameters.Add(new SqlParameter("@ResidentName", log.ResidentName));
            com.Parameters.Add(new SqlParameter("@TimeIn", log.Timein));
            com.Parameters.Add(new SqlParameter("@TimeOut", log.Timeout));
            com.Parameters.Add(new SqlParameter("@Purpose", log.Purpose));
            com.Parameters.Add(new SqlParameter("@URL", log.URL));
            com.Parameters.Add(new SqlParameter("@BldgNo", log.BuildingNo));
            com.Parameters.Add(new SqlParameter("@UnitNo", log.UnitNo));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
