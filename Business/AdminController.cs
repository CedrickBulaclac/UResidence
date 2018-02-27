using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class AdminController
    {
        public static List<Admin> GetAll()
        {
            const string GET_ALL = @"SELECT Id,Fname,Mname,Lname,Bdate,CelNo,Email FROM[tbAdmin] order by Id";

            List<Admin> ret = default(List<Admin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Admin>(com);
            return ret;
        }

        public static Admin GetbyID(int Id)
        {
            const string GET_RECORD = @"SELECT Id,Fname, Mname, Lname, Bdate, CelNo,Email FROM[tbAdmin] WHERE Id = @Id";

           Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", Id));
            ret = SqlManager.Select<Admin>(com).First();

            return ret;
        }


        public static bool Update(Admin adm)
        {
            const string GET_UPDATE = @"update [tbAdmin] set Fname= @Fname, Mname= @Mname,  Lname= @Lname, Bdate=@Bdate, CelNo=@CelNo,Email=@Email  WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", adm.Id));
            com.Parameters.Add(new SqlParameter("@Fname", adm.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", adm.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", adm.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", adm.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", adm.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", adm.Email));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Admin adm)
        {
            const string GET_DELETE = @"delete [tbAdmin] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", adm.Id));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool Insert(Admin adm)
        {
            const string GET_INSERT = @"insert [tbAdmin] (Fname,Mname,Lname,Bdate,CelNo,Email) values (@Fname,@Mname,@Lname,@Bdate,@CelNo,@Email) ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Fname", adm.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", adm.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", adm.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", adm.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", adm.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", adm.Email));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
