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
            const string GET_ALL = @"select Id,Convert(varchar(15),Date,101) as Date,VisitorName,ResidentName,Convert(varchar(15),TimeIn,108),Convert(varchar(15),TimeOut,108),Purpose from [tbLogbook] where Convert(varchar(15),Date,101)=Convert(varchar(15),GETDATE(),101)";
            List<Logbook> ret = default(List<Logbook>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Logbook>(com);
            return ret;

        }
        public static List<Logbook> GET_ALL(DateTime date)
        {
            const string GET_ALL = @"select Id,Convert(varchar(15),Date,101) as Date,VisitorName,ResidentName,Convert(varchar(15),TimeIn,108),Convert(varchar(15),TimeOut,108),Purpose from [tbLogbook] where Convert(varchar(15),Date,101)=Convert(varchar(15),@date,101)";
            List<Logbook> ret = default(List<Logbook>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@date", date));
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


        public static bool Insert(Logbook log)
        {
            const string GET_INSERT = @"insert [tbLogbook] (Date,VisitorName,ResidentName,TimeIn,TimeOut,Purpose) values (@Date,@VisitorName,@ResidentName,@TimeIn,@TimeOut,@Purpose) ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Date", log.date));
            com.Parameters.Add(new SqlParameter("@VisitorName", log.VisitorName));
            com.Parameters.Add(new SqlParameter("@ResidentName", log.ResidentName));
            com.Parameters.Add(new SqlParameter("@TimeIn", log.Timein));
            com.Parameters.Add(new SqlParameter("@TimeOut", log.Timeout));
            com.Parameters.Add(new SqlParameter("@Purpose", log.Purpose));

            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
