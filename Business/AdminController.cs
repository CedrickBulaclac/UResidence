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
            const string GET_ALL = @"SELECT Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted FROM [tbAdmin] where Deleted=0 order by Id";

            List<Admin> ret = default(List<Admin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Admin>(com);
            return ret;
        }
        public static List<Admin> Get(int id)
        {
            const string GET_ALL = @"SELECT Id,Fname,Mname,Lname,Bdate,CelNo,Email FROM[tbAdmin] where Id=@Rid";

            List<Admin> ret = default(List<Admin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Rid", id));
            ret = SqlManager.Select<Admin>(com);
            return ret;
        }
        public static Admin GetbyID(int Id)
        {
            const string GET_RECORD = @"SELECT Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted FROM [tbAdmin] WHERE Id = @Id";

           Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", Id));
            ret = SqlManager.Select<Admin>(com).First();

            return ret;
        }

        public static Admin GetbyIDEdit(string Id)
        {
            const string GET_RECORD = @"SELECT Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted FROM[tbAdmin] WHERE Id = @Id";

            Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", Id));
            ret = SqlManager.Select<Admin>(com).First();

            return ret;
        }


        public static Admin GetEmailAdmin(string email)
        {
            const string GET_RECORD = @"SELECT Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted FROM[tbAdmin] WHERE Email = @Email";

           Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Email", email));
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
        public static bool AUpdate(Admin adm)
        {
            const string GET_UPDATE = @"update [tbAdmin] set Fname= @Fname, Mname= @Mname,  Lname= @Lname WHERE Id = @Id";

<<<<<<< HEAD
        public static bool UpdateDelete(Admin adm)
        {
            const string GET_UPDATE = @"update [tbAdmin] set Deleted=@Deleted  WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", adm.Id));
            com.Parameters.Add(new SqlParameter("@Deleted", adm.Deleted));

            return SqlManager.ExecuteNonQuery(com);
        }



=======
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", adm.Id));
            com.Parameters.Add(new SqlParameter("@Fname", adm.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", adm.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", adm.Lname));
            return SqlManager.ExecuteNonQuery(com);
        }
>>>>>>> 23ca7d36a09bbfd9d277b5a70acda10c69910d2e
        public static bool Delete(Admin adm)
        {
            const string GET_DELETE = @"delete [tbAdmin] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", adm.Id));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool Insert(Admin adm)
        {
            const string GET_INSERT = @"insert [tbAdmin] (Fname,Mname,Lname,Bdate,CelNo,Email,Deleted) values (@Fname,@Mname,@Lname,@Bdate,@CelNo,@Email,@Deleted) ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Fname", adm.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", adm.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", adm.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", adm.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", adm.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", adm.Email));
            com.Parameters.Add(new SqlParameter("@Deleted", adm.Deleted));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
