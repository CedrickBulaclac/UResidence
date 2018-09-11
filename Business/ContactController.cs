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
            const string GET_ALL = @"SELECT ContactID,ContactName,ContactNo,AddedOn FROM [Contacts] order by ContactID Desc";
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Contact>(com);
            return ret;
        }
        public static List<Contact> GetCount()
        {
            List<Contact> ret = default(List<Contact>);
            const string GET_ALL = @"SELECT ContactID,ContactName,ContactNo,AddedOn FROM [Contacts] where Visit=0";
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Contact>(com);
            return ret;
        }
        public static bool UpdateVisit(Contact cot)
        {
            const string GET_UPDATE = @"update [Contacts] set Visit=@Visit  WHERE ContactID = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", cot.ContactID));
            com.Parameters.Add(new SqlParameter("@Visit", cot.Visit));

            return SqlManager.ExecuteNonQuery(com);
        }


    }
}
