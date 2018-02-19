using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    class TenantController
    {
        public static List<Tenant> GetAll()
        {
            const string GET_ALL = @"SELECT Id,UnitNo,BldgNo,TenantNo,Fname,Mname,Lname,Gender,Age,TelNo,CelNo,Email,Citizenship,Status,LeaseStart,LeaseEnd FROM [tbTenant] order by Id";

            List<Tenant> ret = default(List<Tenant>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Tenant>(com);
            return ret;
        }

    


        public static Tenant GetIdTenant(int idTenant)
        {
            const string GET_RECORD = @" Id,UnitNo,BldgNo,TenantNo,Fname,Mname,Lname,Gender,Age,TelNo,CelNo,Email,Citizenship,Status,LeaseStart,LeaseEnd FROM [tbTenant] WHERE Id = @Id";

            Tenant ret = default(Tenant);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", idTenant));
            ret = SqlManager.Select<Tenant>(com).First();

            return ret;
        }



        public static bool Update(Tenant usr)
        {
            const string GET_UPDATE = @"update [tbTenant] set Fname = @Fname, Mname = @Mname, Lname = @Lname, Age = @Age, TelNo = @TelNo, CelNo = @CelNo, Email = @Email , Citizenship = @Citizenship  WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Fname", usr.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", usr.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", usr.Lname));
            com.Parameters.Add(new SqlParameter("@Age", usr.Age));
            com.Parameters.Add(new SqlParameter("@TelNo", usr.TelNo));
            com.Parameters.Add(new SqlParameter("@CelNo", usr.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", usr.Email));
            com.Parameters.Add(new SqlParameter("@Citizenship", usr.Citizenship));
            com.Parameters.Add(new SqlParameter("@Id", usr.Id));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Tenant usr)
        {
            const string GET_DELETE = @"delete [tbTenant] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", usr.Id));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Modify(Tenant usr)
        {
            if (usr.Id == 0)
                return Insert(usr);
            else
                return Update(usr);
        }

        public static bool Insert(Tenant usr)
        {

            const string GET_INSERT = @"insert [tbTenant] (UnitNo,BldgNo,TenantNo,Fname,Mname,Lname,Gender,Age,TelNo,CelNo,Email,Citizenship,Status,LeaseStart,LeaseEnd) values (@UnitNo,@BldgNo,@TenantNo,@Fname,@Mname,@Lname,@Gender,@Age,@TelNo,@CelNo,@Email,@Citizenship,@Status, getdate() , getdate())";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@UnitNo", usr.UnitNo));
            com.Parameters.Add(new SqlParameter("@BldgNo", usr.BldgNo));
            com.Parameters.Add(new SqlParameter("@TenantNo", usr.TenantNo));
            com.Parameters.Add(new SqlParameter("@Fname", usr.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", usr.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", usr.Lname));
            com.Parameters.Add(new SqlParameter("@Gender", usr.Gender));
            com.Parameters.Add(new SqlParameter("@Age", usr.Age));
            com.Parameters.Add(new SqlParameter("@TelNo", usr.TelNo));
            com.Parameters.Add(new SqlParameter("@CelNo", usr.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", usr.Email));
            com.Parameters.Add(new SqlParameter("@Citizenship", usr.Citizenship));
            com.Parameters.Add(new SqlParameter("@Status", usr.CelNo));
            com.Parameters.Add(new SqlParameter("@LeaseStart", usr.Email));
            com.Parameters.Add(new SqlParameter("@LeaseEnd", usr.Citizenship));

            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
