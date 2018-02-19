using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class UserController
    {

        public static List<UserLogin> GetAll()
        {
            const string GET_ALL = @"SELECT Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,LastModified,Level,Lockout,LastLogin FROM [tbLogin] order by Id";


            List<UserLogin> ret = default(List<UserLogin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<UserLogin>(com);
            return ret;
        }

        public static UserLogin Get(string uid)
        {
            const string GET_RECORD = @"SELECT Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,LastModified,Level,Lockout,LastLogin FROM [tbLogin] WHERE Username = @uid";

            UserLogin ret = default(UserLogin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Username", uid));
            ret = SqlManager.Select<UserLogin>(com).First();

            return ret;
        }

        public static UserLogin Get(int id)
        {
            const string GET_RECORD = @"SELECT Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,LastModified,Level,Lockout,LastLogin FROM [tbLogin] WHERE Id = @Id";

            UserLogin ret = default(UserLogin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<UserLogin>(com).First();

            return ret;
        }


        public static bool Update(UserLogin usr)
        {
            const string GET_UPDATE = @"update [tbLogin] set Username= @Username, Lockout= @Lockout, ModifiedBy = @ModifiedBy, LastModified = getdate() WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Username", usr.Username));
            com.Parameters.Add(new SqlParameter("@Lockout", usr.Locked));
            com.Parameters.Add(new SqlParameter("@ModifiedBy", usr.ModifyBy));
            com.Parameters.Add(new SqlParameter("@Id", usr.Id));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(UserLogin usr)
        {
            const string GET_DELETE = @"delete [tbLogin] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", usr.Id));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Modify(UserLogin usr)
        {
            if (usr.Id == 0)
                return Insert(usr);
            else
                return Update(usr);
        }

        public static bool Insert(UserLogin usr)
        {

          

            const string GET_INSERT = @"insert [tbLogin] (Username,Hash, Lockout, CreatedBy, ModifyBy, CreatedDate, LastModified) values (@Username, @Lockout, @CreatedBy, @ModifyBy, getdate(), getdate())";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Username", usr.Username));
            com.Parameters.Add(new SqlParameter("@Hash", usr.Hash));
            com.Parameters.Add(new SqlParameter("@Lockout", usr.Locked));
            com.Parameters.Add(new SqlParameter("@CreatedBy", usr.CreatedBy));
            com.Parameters.Add(new SqlParameter("@ModifyBy", usr.ModifyBy));

            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
