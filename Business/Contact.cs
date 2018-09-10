using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Contact : BaseProperty<Contact>
    {
        public int ContactID { get; set; }
        public string ContactName { get; set; }
        public string ContactNo { get; set; }
        public DateTime AddedOn { get; set; }

        public Contact CreateObject(SqlDataReader reader)
        {
            Contact ret = new Contact();
            ret.ContactID = reader.GetInt32(0);
            ret.ContactName = reader.GetString(1);
            ret.ContactNo = reader.GetString(2);
            ret.AddedOn = reader.GetDateTime(3);
            return ret;
        }

        public void Reset()
        {

        }
    }
}

