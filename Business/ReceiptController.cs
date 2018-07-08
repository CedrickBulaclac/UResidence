using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class ReceiptController
    {
        public static List<Receipt> GetAll()
        {
            const string GET_ALL = @"SELECT Id,RefNo,Downpayment,Charge,Fullpayment FROM [tbReceipt] order by Id";

            List<Receipt> ret = default(List<Receipt>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Receipt>(com);
            return ret;
        }

        public static Receipt GetORno(string orn)
        {
            const string GET_RECORD = @"SELECT Id,RefNo,Downpayment,Charge,Fullpayment FROM [tbReceipt] WHERE Id = @ORNo";

            Receipt ret = default(Receipt);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@ORNo", orn));
            ret = SqlManager.Select<Receipt>(com).First();

            return ret;
        }


        public static Receipt GetRefNo(string rno)
        {
            const string GET_RECORD = @"SELECT Id,RefNo,OwnerNo,Downpayment,Charge,Fullpayment FROM [tbReceipt] WHERE RefNo = @RefNo";

            Receipt ret = default(Receipt);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@RefNo", rno));
            ret = SqlManager.Select<Receipt>(com).First();

            return ret;
        }



        public static bool Update(Receipt usr)
        {
            const string GET_UPDATE = @"update [tbReceipt] set  Downpayment = @Downpayment ,Charge = @Charge , Fullpayment = @Fullpayment WHERE Id= @ORNo";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Downpayment", usr.Downpayment));
            com.Parameters.Add(new SqlParameter("@Charge", usr.Charge));
            com.Parameters.Add(new SqlParameter("@Fullpayment", usr.Fullpayment));
            com.Parameters.Add(new SqlParameter("@ORNo", usr.ORNo));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Receipt usr)
        {
            const string GET_DELETE = @"delete [tbReceipt] WHERE ORNo = @ORNo";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@ORNo", usr.ORNo));

            return SqlManager.ExecuteNonQuery(com);
        }

        //public static bool Modify(UserLogin usr)
        //{
        //    if (usr.Id == 0)
        //        return Insert(usr);
        //    else
        //        return Update(usr);
        //}

        public static bool Insert(Receipt usr)
        {

            const string GET_INSERT = @"insert [tbReceipt] (RefNo,Downpayment,Charge,Fullpayment) values (@RefNo, @Downpayment, @Charge, @Fullpayment)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@RefNo", usr.RefNo));
            com.Parameters.Add(new SqlParameter("@Downpayment", usr.Downpayment));
            com.Parameters.Add(new SqlParameter("@Charge", usr.Charge));
            com.Parameters.Add(new SqlParameter("@Fullpayment", usr.Fullpayment));

            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
