using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class ContactController
    {
        public static List<Contact> GetAll()
        {
            List<Contact> ret = default(List<Contact>);
            const string GET_ALL = @"SELECT ContactID,ContactName,ContactNo,AddedOn FROM [Contacts] order by AddedOn";
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Contact>(com);
            return ret;
        }



    }
}
