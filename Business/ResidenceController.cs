using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    class ResidenceController
    {
        public static List<Residence> GetAll()
        {
            const string GET_ALL = @"SELECT Id,OwnerNo,TenantNo FROM [tbResidence] order by Id";


            List<Residence> ret = default(List<Residence>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Residence>(com);
            return ret;
        }



        public static Residence GetAlll()
        {
            const string GET_ALL = @"select b.Id from tbResidence b inner join tbTenant a on TenantNo=a.Id where TenantNo =4  and Deleted=0 ";
            Residence ret = default(Residence);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Residence>(com).First();
            return ret;
        }



        public static Residence GetOwnerNo(string ono)
        {
            const string GET_RECORD = @"SELECT Id,OwnerNo,TenantNo FROM [tbResidence] WHERE OwnerNo = @OwnerNo";

            Residence ret = default(Residence);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@OwnerNo", ono));
            ret = SqlManager.Select<Residence>(com).First();

            return ret;
        }
       

        public static Residence Get(int id)
        {
            const string GET_RECORD = @"SELECT Id,OwnerNo,TenantNo FROM [tbResidence] WHERE Id = @Id";

            Residence ret = default(Residence);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<Residence>(com).First();

            return ret;
        }
   


        public static bool Update(Residence usr)
        {
            const string GET_UPDATE = @"update [tbResidence] set OwnerNo= @OwnerNo, TenantNo= @TenantNo WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@OwnerNo", usr.OwnerNo));
            com.Parameters.Add(new SqlParameter("@TenantNo", usr.TenantNo));
            com.Parameters.Add(new SqlParameter("@Id", usr.Id));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Residence usr)
        {
            const string GET_DELETE = @"delete [tbResidence] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", usr.Id));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Modify(Residence usr)
        {
            if (usr.Id == 0)
                return Insert(usr);
            else
                return Update(usr);
        }

        public static bool Insert(Residence usr)
        {
            const string GET_INSERT = @"insert [tbResidence] (OwnerNo,TenantNo) values (@OwnerNo, @TenantNo)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@OwnerNo", usr.OwnerNo));
            com.Parameters.Add(new SqlParameter("@TenantNo", usr.TenantNo));

            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
