using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    public class TenantController
    {
        public static List<Tenant> GetAll()
        {
            const string GET_ALL = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease,LoginId,ENo,EName,EAddress FROM [tbTenant] where Deleted=0  order by Id";

            List<Tenant> ret = default(List<Tenant>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Tenant>(com);
            return ret;
        }

        public static List<Tenant> GetTList(int Id)
        {
            const string GET_ALL = @"select t.Id,t.UnitNo,t.BldgNo,t.Fname,t.Mname,t.Lname,t.Bdate,t.CelNo,t.Email,t.LeaseStart,t.LeaseEnd,t.Deleted,t.URL,t.MovingIn,t.MovingOut,t.Fname+' '+t.Mname+' '+t.Lname as Fullname,FORMAT(t.Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),t.LeaseStart,100)+' - '+Convert(varchar(25),t.LeaseEnd,100) as Lease,t.LoginId,t.ENo,t.EName,t.EAddress from tbOwner o inner join tbResidence r on o.Id=r.OwnerNo inner join tbTenant t on t.Id=r.TenantNo where t.Deleted=0 and o.Id=@Id";

            List<Tenant> ret = default(List<Tenant>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Id", Id));
            ret = SqlManager.Select<Tenant>(com);
            return ret;
        }

        public static Tenant GetIdTenant(string idTenant)
        {
            const string GET_RECORD = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease,LoginId,ENo,EName,EAddress  FROM [tbTenant] WHERE Id = @Id and Deleted=0";

            Tenant ret = default(Tenant);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", idTenant));
            ret = SqlManager.Select<Tenant>(com).First();

            return ret;
        }

        public static Tenant GETID(int idTenant)
        {
            const string GET_RECORD = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease,LoginId,ENo,EName,EAddress  FROM [tbTenant] WHERE Id = @Id and Deleted=0";

            Tenant ret = default(Tenant);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", idTenant));
            ret = SqlManager.Select<Tenant>(com).First();

            return ret;
        }

        public static Tenant GetEmailTenant(string Email)
        {
            const string GET_RECORD = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease,LoginId,ENo,EName,EAddress  FROM [tbTenant] WHERE Email = @Email and Deleted=0 ";

            Tenant ret = default(Tenant);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Email", Email));
            ret = SqlManager.Select<Tenant>(com).First();

            return ret;
        }


        public static Tenant GetTenantReserve(string bldgno, string unitno)
        {
            const string GET_RECORD = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease,LoginId,ENo,EName,EAddress  FROM [tbTenant] where BldgNo=@BldgNo and UnitNo=@UnitNo and Deleted=0";

            Tenant ret = default(Tenant);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@BldgNo", bldgno));
            com.Parameters.Add(new SqlParameter("@UnitNo", unitno));
            ret = SqlManager.Select<Tenant>(com).First();


            return ret;
        }

        public static bool UpdateImage1(Tenant ten)
        {
            const string GET_UPDATE = @"update [tbTenant] set MovingIn=@MovingIn WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@MovingIn", ten.MovingIn));
            com.Parameters.Add(new SqlParameter("@Id", ten.Id));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool UpdateImage2(Tenant ten)
        {
            const string GET_UPDATE = @"update [tbTenant] set MovingOut=@MovingOut WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@MovingOut", ten.MovingOut));
            com.Parameters.Add(new SqlParameter("@Id", ten.Id));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static Tenant GetId(string id)
        {
            const string GET_RECORD = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease,LoginId,ENo,EName,EAddress  FROM [tbTenant] WHERE Id = @Id and Deleted=0";

            Tenant ret = default(Tenant);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<Tenant>(com).First();

            return ret;
        }
        public static List<Tenant> Check(Tenant ten)
        {
            const string GET_RECORD = @" SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease,LoginId,ENo,EName,EAddress FROM [tbTenant] where Deleted=0 and LeaseEnd Between @start and @end ";

            List<Tenant> ret = default(List<Tenant>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@start", ten.LeaseStart));
            com.Parameters.Add(new SqlParameter("@end", ten.LeaseEnd));
            ret = SqlManager.Select<Tenant>(com);

            return ret;
        }

        public static List<Tenant> GetId(int id)
        {
            const string GET_RECORD = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease,LoginId,ENo,EName,EAddress  FROM [tbTenant] WHERE Id = @Id and Deleted=0";

            List<Tenant> ret = default(List<Tenant>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<Tenant>(com);

            return ret;
        }


        public static bool Update(Tenant usr)
        {
            const string GET_UPDATE = @"update [tbTenant] set Fname = @Fname, Mname = @Mname, Lname = @Lname, Bdate = @Bdate, CelNo = @CelNo, Email = @Email,BldgNo=@BldgNo,UnitNo=@UnitNo,ENo=@ENo,EName=@EName,EAddress=@EAddress WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@UnitNo", usr.UnitNo));
            com.Parameters.Add(new SqlParameter("@BldgNo", usr.BldgNo));
            com.Parameters.Add(new SqlParameter("@Fname", usr.Fname));         
            com.Parameters.Add(new SqlParameter("@Mname", usr.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", usr.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", usr.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", usr.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", usr.Email));
            com.Parameters.Add(new SqlParameter("@Id", usr.Id));
            com.Parameters.Add(new SqlParameter("@ENo", usr.ENo));
            com.Parameters.Add(new SqlParameter("@EName", usr.EName));
            com.Parameters.Add(new SqlParameter("@EAddress", usr.EAddress));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool UpdateBU(Tenant usr)
        {
            const string GET_UPDATE = @"update [tbTenant] set BldgNo=@BldgNo,UnitNo=@UnitNo WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@UnitNo", usr.UnitNo));
            com.Parameters.Add(new SqlParameter("@BldgNo", usr.BldgNo));
            com.Parameters.Add(new SqlParameter("@Id", usr.Id));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool TUpdate(Tenant ten)
        {
            const string GET_UPDATE = @"update [tbTenant] set Fname= @Fname, Mname= @Mname,  Lname= @Lname, Email=@Email WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", ten.Id));
            com.Parameters.Add(new SqlParameter("@Fname", ten.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", ten.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", ten.Lname));
            com.Parameters.Add(new SqlParameter("@Email", ten.Email));
            return SqlManager.ExecuteNonQuery(com);
        }


        public static bool Update(int lid, string email)
        {
            const string GET_UPDATE = @"update [tbTenant] set LoginId=@lid WHERE Email=@Email";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@lid", lid));
            com.Parameters.Add(new SqlParameter("@email", email));
            return SqlManager.ExecuteNonQuery(com);
        }



        public static bool UpdateDP(Tenant ten)
        {

            const string GET_UPDATE = @"update [tbTenant] set URL=@URL  WHERE Id = @Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", ten.Id));
            com.Parameters.Add(new SqlParameter("@URL", ten.URL));
            return SqlManager.ExecuteNonQuery(com);
        }





        public static bool UpdateDelete(Tenant usr)
        {
            const string GET_UPDATE = @"update [tbTenant] set Deleted=@Deleted WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Deleted", usr.Deleted));
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

            const string GET_INSERT = @"insert [tbTenant] (UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,LoginId,ENo,EName,EAddress) values (@UnitNo,@BldgNo,@Fname,@Mname,@Lname,@Bdate,@CelNo,@Email, @LeaseStart, @LeaseEnd,@Deleted,@URL,@MovingIn,@MovingOut,0,@ENo,@EName,@EAddress)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@BldgNo", usr.BldgNo));
            com.Parameters.Add(new SqlParameter("@UnitNo", usr.UnitNo));
            com.Parameters.Add(new SqlParameter("@Fname", usr.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", usr.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", usr.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", usr.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", usr.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", usr.Email));
            com.Parameters.Add(new SqlParameter("@LeaseStart", usr.LeaseStart));
            com.Parameters.Add(new SqlParameter("@LeaseEnd", usr.LeaseEnd));
            com.Parameters.Add(new SqlParameter("@Deleted", usr.Deleted));
            com.Parameters.Add(new SqlParameter("@URL", usr.URL));
            com.Parameters.Add(new SqlParameter("@MovingIn", usr.MovingIn));
            com.Parameters.Add(new SqlParameter("@MovingOut", usr.MovingOut));
            com.Parameters.Add(new SqlParameter("@ENo", usr.ENo));
            com.Parameters.Add(new SqlParameter("@EName", usr.EName));
            com.Parameters.Add(new SqlParameter("@EAddress", usr.EAddress));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
