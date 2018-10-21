using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class ChargeController
    {
        public static List<Charge> GetAll(int refno)
        {
            const string GET_ALL = @"select Id,RefNo,Date,CreatedBy,Charge,Description from tbCharge where RefNo=@refno";

            List<Charge> ret = default(List<Charge>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refno", refno));
            ret = SqlManager.Select<Charge>(com);
            return ret;
        }

        public static Charge GetRef(int refno,DateTime date)
        {
            const string GET_ALL = @"select Id,RefNo,Date,CreatedBy,Charge,Description from tbCharge where RefNo=@refno and Date=@date";

            Charge ret = default(Charge);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@refno", refno));
            com.Parameters.Add(new SqlParameter("@date", date));
            ret = SqlManager.Select<Charge>(com).First();
            return ret;
        }
        public static bool Insert(Charge charge)
        {
            const string GET_INSERT = @"insert [tbCharge] (Refno,Date,CreatedBy,Charge,Description) values (@Refno,@Date,@CreatedBy,@charge,@Description) ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Refno", charge.Refno));
            com.Parameters.Add(new SqlParameter("@Date", charge.Date));
            com.Parameters.Add(new SqlParameter("@CreatedBy", charge.CreatedBy));
            com.Parameters.Add(new SqlParameter("@charge", charge.charge));
            com.Parameters.Add(new SqlParameter("@Description", charge.Description));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(int id)
        {
            const string GET_DELETE = @"delete [tbCharge] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", id));

            return SqlManager.ExecuteNonQuery(com);
        }

    }
}
