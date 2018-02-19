using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    class RecieptController
    {
        public static List<Reciept> GetAll()
        {
            const string GET_ALL = @"SELECT ORNo,RefNo,OwnerNo,Downpayment,Charge,Fullpayment FROM [tbReceipt] order by ORNo";

            List<Reciept> ret = default(List<Reciept>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Reciept>(com);
            return ret;
        }

        public static Reciept GetORno(string orn)
        {
            const string GET_RECORD = @"SELECT ORNo,RefNo,OwnerNo,Downpayment,Charge,Fullpayment FROM [tbReceipt] WHERE ORNo = @ORNo";

            Reciept ret = default(Reciept);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@ORNo", orn));
            ret = SqlManager.Select<Reciept>(com).First();

            return ret;
        }


        public static Reciept GetRefNo(string rno)
        {
            const string GET_RECORD = @"SELECT ORNo,RefNo,OwnerNo,Downpayment,Charge,Fullpayment FROM [tbReceipt] WHERE RefNo = @RefNo";

            Reciept ret = default(Reciept);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@RefNo", rno));
            ret = SqlManager.Select<Reciept>(com).First();

            return ret;
        }



        public static bool Update(Reciept usr)
        {
            const string GET_UPDATE = @"update [tbReceipt] set  Downpayment = @Downpayment ,Charge = @Charge , Fullpayment = @Fullpayment WHERE ORNo= @ORNo";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Downpayment", usr.Downpayment));
            com.Parameters.Add(new SqlParameter("@Charge", usr.Charge));
            com.Parameters.Add(new SqlParameter("@Fullpayment", usr.Fullpayment));
            com.Parameters.Add(new SqlParameter("@ORNo", usr.ORNo));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Reciept usr)
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

        public static bool Insert(Reciept usr)
        {

            const string GET_INSERT = @"insert [tbReceipt] (ORNo,RefNo,OwnerNo,Downpayment,Charge,Fullpayment) values (@ORNo, @RefNo, @OwnerNo, @Downpayment, @Charge, @Fullpayment)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@ORNo", usr.ORNo));
            com.Parameters.Add(new SqlParameter("@RefNo", usr.RefNo));
            com.Parameters.Add(new SqlParameter("@OwnerNo", usr.OwnerNo));
            com.Parameters.Add(new SqlParameter("@Downpayment", usr.Downpayment));
            com.Parameters.Add(new SqlParameter("@Charge", usr.Charge));
            com.Parameters.Add(new SqlParameter("@Fullpayment", usr.Fullpayment));

            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
