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
            const string GET_ALL = @"select Id,Date,VisitorName,ResidentName,TimeIn,TimeOut,Purpose from [tbLogbook]";
            List<Logbook> ret = default(List<Logbook>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Logbook>(com);
            return ret;

        }
    }
}
