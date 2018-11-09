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
            const string GET_ALL = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease FROM [tbTenant] where Deleted=0  order by Id";

            List<Tenant> ret = default(List<Tenant>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Tenant>(com);
            return ret;
        }
        public static Tenant GetIdTenant(string idTenant)
        {
            const string GET_RECORD = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease  FROM [tbTenant] WHERE Id = @Id and Deleted=0";

            Tenant ret = default(Tenant);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", idTenant));
            ret = SqlManager.Select<Tenant>(com).First();

            return ret;
        }
        public static Tenant GetEmailTenant(string Email)
        {
            const string GET_RECORD = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease  FROM [tbTenant] WHERE Email = @Email and Deleted=0 ";

            Tenant ret = default(Tenant);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Email", Email));
            ret = SqlManager.Select<Tenant>(com).First();

            return ret;
        }


        public static Tenant GetTenantReserve(string bldgno, string unitno)
        {
            const string GET_RECORD = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease  FROM [tbTenant] where BldgNo=@BldgNo and UnitNo=@UnitNo and Deleted=0";

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
            const string GET_RECORD = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease  FROM [tbTenant] WHERE Id = @Id and Deleted=0";

            Tenant ret = default(Tenant);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<Tenant>(com).First();

            return ret;
        }
        public static List<Tenant> Check(Tenant ten)
        {
            const string GET_RECORD = @" SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease FROM [tbTenant] where Deleted=0 and LeaseEnd Between @start and @end ";

            List<Tenant> ret = default(List<Tenant>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@start", ten.LeaseStart));
            com.Parameters.Add(new SqlParameter("@end", ten.LeaseEnd));
            ret = SqlManager.Select<Tenant>(com);

            return ret;
        }

        public static List<Tenant> GetId(int id)
        {
            const string GET_RECORD = @"SELECT Id,UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Convert(varchar(25),LeaseStart,100)+' - '+Convert(varchar(25),LeaseEnd,100) as Lease  FROM [tbTenant] WHERE Id = @Id and Deleted=0";

            List<Tenant> ret = default(List<Tenant>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<Tenant>(com);

            return ret;
        }


        public static bool Update(Tenant usr)
        {
            const string GET_UPDATE = @"update [tbTenant] set Fname = @Fname, Mname = @Mname, Lname = @Lname, Bdate = @Bdate, CelNo = @CelNo, Email = @Email,BldgNo=@BldgNo,UnitNo=@UnitNo WHERE Id = @Id";

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

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool TUpdate(Tenant ten)
        {
            const string GET_UPDATE = @"update [tbTenant] set Fname= @Fname, Mname= @Mname,  Lname= @Lname WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", ten.Id));
            com.Parameters.Add(new SqlParameter("@Fname", ten.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", ten.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", ten.Lname));
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

            const string GET_INSERT = @"insert [tbTenant] (UnitNo,BldgNo,Fname,Mname,Lname,Bdate,CelNo,Email,LeaseStart,LeaseEnd,Deleted,URL,MovingIn,MovingOut) values (@UnitNo,@BldgNo,@Fname,@Mname,@Lname,@Bdate,@CelNo,@Email, @LeaseStart, @LeaseEnd,@Deleted,@URL,@MovingIn,@MovingOut)";

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
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
