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
        public static List<ReversalList> GET_ALLP()
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as ReservedBy,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100),rev.Id,rf.Id from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId where rev.Status='Pending'";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }

        public static List<ReversalList> GET_ALLA()
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as ReservedBy,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100),rev.Id,rf.Id from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId where rev.Status='Approved'";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }
        public static List<ReversalList> GET_ALLD()
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as ReservedBy,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100),rev.Id,rf.Id from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId where rev.Status='Denied'";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }
        public static List<ReversalList> GET_ALLC()
        {
            const string GET_ALL = @"select rev.CreatedBy,rf.ReservedBy as ReservedBy,Convert(varchar(25),StartTime,100)+' - '+Convert(varchar(25),EndTIme,100) as DateofReservation,rev.Amount,rev.Description,rev.Status,CONVERT(varchar(25),rev.Date,100),rev.Id,rf.Id from tbReversal rev inner join tbReservationForm rf on rev.RefNo=rf.Id inner join tbSchedReservation sr on sr.Id=rf.SchedId where rev.Status='Completed'";

            List<ReversalList> ret = default(List<ReversalList>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<ReversalList>(com);
            return ret;
        }
    }
}
