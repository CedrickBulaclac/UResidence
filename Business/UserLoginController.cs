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
            const string GET_ALL = @"SELECT Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,LastModified,Level,Lockout,LastLogin,AdminId,OwnerId,TenantId FROM [tbLogin] order by Id";


            List<UserLogin> ret = default(List<UserLogin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<UserLogin>(com);
            return ret;
        }
        public static List<UserLogin> GetAll(string email)
        {
            const string GET_ALL = @"SELECT Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,LastModified,Level,Lockout,LastLogin,AdminId,OwnerId,TenantId FROM [tbLogin] where Username=@email";


            List<UserLogin> ret = default(List<UserLogin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@email", email));
            ret = SqlManager.Select<UserLogin>(com);
            return ret;
        }
        public static List<UserLogin> AGetAll(int id)
        {
            const string GET_ALL = @"SELECT Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,LastModified,Level,Lockout,LastLogin,AdminId,OwnerId,TenantId FROM [tbLogin] where AdminId=@Rid";


            List<UserLogin> ret = default(List<UserLogin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Rid", id));
            ret = SqlManager.Select<UserLogin>(com);
            return ret;
        }
        public static List<UserLogin> OGetAll(int id)
        {
            const string GET_ALL = @"SELECT Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,LastModified,Level,Lockout,LastLogin,AdminId,OwnerId,TenantId FROM [tbLogin] where OwnerId=@Rid";


            List<UserLogin> ret = default(List<UserLogin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Rid", id));
            ret = SqlManager.Select<UserLogin>(com);
            return ret;
        }
        public static List<UserLogin> TGetAll(int id)
        {
            const string GET_ALL = @"SELECT Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,LastModified,Level,Lockout,LastLogin,AdminId,OwnerId,TenantId FROM [tbLogin] where TenantId=@Rid";


            List<UserLogin> ret = default(List<UserLogin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Rid", id));
            ret = SqlManager.Select<UserLogin>(com);
            return ret;
        }
        public static UserLogin Get(string uid,string uhash)
        {
            UserLogin ret = default(UserLogin);
            try
            {
                const string GET_RECORD = @"SELECT Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,LastModified,Level,Lockout,LastLogin,AdminId,OwnerId,TenantId FROM [tbLogin] WHERE Username = @Username and Hash=@Hash";

                
                SqlCommand com = new SqlCommand(GET_RECORD);
                com.Parameters.Add(new SqlParameter("@Username", uid));
                com.Parameters.Add(new SqlParameter("@Hash", uhash));
                ret = SqlManager.Select<UserLogin>(com).First();

                return ret;
            }
            catch(InvalidOperationException )
            {
                return ret;
            }
        }

        public static UserLogin Get(int id)
        {
            const string GET_RECORD = @"SELECT Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,LastModified,Level,Lockout,LastLogin,AdminId,OwnerId,TenantId FROM [tbLogin] WHERE Id = @Id";

            UserLogin ret = default(UserLogin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<UserLogin>(com).First();

            return ret;
        }
        public static bool UpdateLog(int id)
        {
            const string GET_UPDATE = @"update [tbLogin] set LastLogin=GETDATE() where Id=@Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", id));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool Update(string email,string hash)
        {
            const string GET_UPDATE = @"update [tbLogin] set Username=@email, Hash=@hash where Username=@email";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@email", email));
            com.Parameters.Add(new SqlParameter("@hash", hash));
            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool Update(UserLogin usr)
        {
            const string GET_UPDATE = @"update [tbLogin] set Username= @Username,Hash = @Hash, Lockout= @Lockout, ModifiedBy = @ModifiedBy, LastModified = getdate() WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Username", usr.Username));
            com.Parameters.Add(new SqlParameter("@Hash", usr.Hash));
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

          

            const string GET_INSERT = @"insert [tbLogin] (Username,Hash, Lockout, CreatedBy, ModifiedBy, CreatedDate, LastModified,Level,LastLogin) values (@Username,@Hash, @Lockout, @CreatedBy, @ModifiedBy, getdate(), getdate(), @Level, getdate())";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Username", usr.Username));
            com.Parameters.Add(new SqlParameter("@Hash", usr.Hash));
            com.Parameters.Add(new SqlParameter("@Lockout", usr.Locked));
            com.Parameters.Add(new SqlParameter("@CreatedBy", usr.CreatedBy));
            com.Parameters.Add(new SqlParameter("@ModifiedBy", usr.ModifyBy));
            com.Parameters.Add(new SqlParameter("@Level", usr.Level));

            return SqlManager.ExecuteNonQuery(com);
        }



        public static bool InsertOwnerId(UserLogin usr)
        {



            const string GET_INSERT = @"insert [tbLogin] (Username,Hash, Lockout, CreatedBy, ModifiedBy, CreatedDate, LastModified,Level,LastLogin,OwnerId) values (@Username,@Hash, @Lockout, @CreatedBy, @ModifiedBy, getdate(), getdate(), @Level, getdate(), @OwnerId)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Username", usr.Username));
            com.Parameters.Add(new SqlParameter("@Hash", usr.Hash));
            com.Parameters.Add(new SqlParameter("@Lockout", usr.Locked));
            com.Parameters.Add(new SqlParameter("@CreatedBy", usr.CreatedBy));
            com.Parameters.Add(new SqlParameter("@ModifiedBy", usr.ModifyBy));
            com.Parameters.Add(new SqlParameter("@Level", usr.Level));
            com.Parameters.Add(new SqlParameter("@OwnerId", usr.OwnerId));

            return SqlManager.ExecuteNonQuery(com);
        }


        public static bool InsertTenantId(UserLogin usr)
        {
            const string GET_INSERT = @"insert [tbLogin] (Username,Hash, Lockout, CreatedBy, ModifiedBy, CreatedDate, LastModified,Level,LastLogin,TenantId) values (@Username,@Hash, @Lockout, @CreatedBy, @ModifiedBy, getdate(), getdate(), @Level, getdate(), @TenantId)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Username", usr.Username));
            com.Parameters.Add(new SqlParameter("@Hash", usr.Hash));
            com.Parameters.Add(new SqlParameter("@Lockout", usr.Locked));
            com.Parameters.Add(new SqlParameter("@CreatedBy", usr.CreatedBy));
            com.Parameters.Add(new SqlParameter("@ModifiedBy", usr.ModifyBy));
            com.Parameters.Add(new SqlParameter("@Level", usr.Level));
            com.Parameters.Add(new SqlParameter("@TenantId", usr.TenantId));

            return SqlManager.ExecuteNonQuery(com);
        }


        public static bool InsertAdminId(UserLogin usr)
        {
            const string GET_INSERT = @"insert [tbLogin] (Username,Hash, Lockout, CreatedBy, ModifiedBy, CreatedDate, LastModified,Level,LastLogin,AdminId) values (@Username,@Hash, @Lockout, @CreatedBy, @ModifiedBy, getdate(), getdate(), @Level, getdate(), @AdminId)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Username", usr.Username));
            com.Parameters.Add(new SqlParameter("@Hash", usr.Hash));
            com.Parameters.Add(new SqlParameter("@Lockout", usr.Locked));
            com.Parameters.Add(new SqlParameter("@CreatedBy", usr.CreatedBy));
            com.Parameters.Add(new SqlParameter("@ModifiedBy", usr.ModifyBy));
            com.Parameters.Add(new SqlParameter("@Level", usr.Level));
            com.Parameters.Add(new SqlParameter("@AdminId", usr.AdminId));

            return SqlManager.ExecuteNonQuery(com);
        }


    }
}
