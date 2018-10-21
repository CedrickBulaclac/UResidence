using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class ReversalListController
    {
        public static List<ReversalList> GET_ALLP(int month,int year)
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as Fullname,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100) as Date,rev.Id ,rf.Id as RefNo,AmenityName=(select AmenityName from tbAmenity a where a.Id=sr.AmenityId) from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId  where rev.Status='Pending' and MONTH(rev.Date)=@month and YEAR(rev.Date)=@year";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("month", month));
            com.Parameters.Add(new SqlParameter("year", year));
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }
        public static List<ReversalList> GET_ALLPY(int year)
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as Fullname,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100) as Date,rev.Id ,rf.Id as RefNo,AmenityName=(select AmenityName from tbAmenity a where a.Id=sr.AmenityId) from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId  where rev.Status='Pending' and YEAR(rev.Date)=@year";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("year", year));
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }
        public static List<ReversalList> GET_ALLA(int month, int year)
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as Fullname,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100) as Date,rev.Id ,rf.Id as RefNo,AmenityName=(select AmenityName from tbAmenity a where a.Id=sr.AmenityId) from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId  where rev.Status='Approved' and MONTH(rev.Date)=@month and YEAR(rev.Date)=@year";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("month", month));
            com.Parameters.Add(new SqlParameter("year", year));
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }
        public static List<ReversalList> GET_ALLAY(int year)
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as Fullname,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100) as Date,rev.Id ,rf.Id as RefNo,AmenityName=(select AmenityName from tbAmenity a where a.Id=sr.AmenityId) from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId  where rev.Status='Approved' and YEAR(rev.Date)=@year";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("year", year));
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }
        public static List<ReversalList> GET_ALLD(int month, int year)
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as Fullname,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100) as Date,rev.Id ,rf.Id as RefNo,AmenityName=(select AmenityName from tbAmenity a where a.Id=sr.AmenityId) from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId  where rev.Status='Denied' and MONTH(rev.Date)=@month and YEAR(rev.Date)=@year";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("month", month));
            com.Parameters.Add(new SqlParameter("year", year));
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }
        public static List<ReversalList> GET_ALLDY(int year)
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as Fullname,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100) as Date,rev.Id ,rf.Id as RefNo,AmenityName=(select AmenityName from tbAmenity a where a.Id=sr.AmenityId) from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId  where rev.Status='Denied' and YEAR(rev.Date)=@year";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("year", year));
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }
        public static List<ReversalList> GET_ALLC(int month, int year)
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as Fullname,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100) as Date,rev.Id ,rf.Id as RefNo,AmenityName=(select AmenityName from tbAmenity a where a.Id=sr.AmenityId) from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId  where rev.Status='Completed' and MONTH(rev.Date)=@month and YEAR(rev.Date)=@year";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("month", month));
            com.Parameters.Add(new SqlParameter("year", year));
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }
        public static List<ReversalList> GET_ALLCY(int year)
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as Fullname,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100) as Date,rev.Id ,rf.Id as RefNo,AmenityName=(select AmenityName from tbAmenity a where a.Id=sr.AmenityId) from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId  where rev.Status='Completed' and YEAR(rev.Date)=@year";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("year", year));
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }
    }
}
