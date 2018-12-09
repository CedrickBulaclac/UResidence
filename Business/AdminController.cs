﻿using System;
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
            const string GET_ALL = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case Level when 0 then 'Super Admin'  when 1 then 'Manager' when 2 then 'Finance' when 3 then 'Cashier' when 4 then 'Reservation Admin' when 5 then 'Registration' when 6 then 'Security Guard' when 7 then 'Security Guard' end as Role, Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address FROM [tbAdmin] a inner join tbLogin l on l.Id=a.LoginId where Deleted=0 and Level!=0 order by Role asc";
            List<Admin> ret = default(List<Admin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Admin>(com);
            return ret;
        }
        public static List<Admin> GetAllCashier()
        {
            const string GET_ALL = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case Level when 0 then 'Super Admin'  when 1 then 'Manager' when 2 then 'Finance' when 3 then 'Cashier' when 4 then 'Reservation Admin' when 5 then 'Registration' when 6 then 'Security Guard' when 7 then 'Security Guard' end as Role,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address  FROM [tbAdmin] a inner join tbLogin l on l.Id=a.LoginId where Deleted=0 and Level=3 order by Role asc";
            List<Admin> ret = default(List<Admin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Admin>(com);
            return ret;
        }
        public static List<Admin> GetAllSecurity()
        {
            const string GET_ALL = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case Level when 0 then 'Super Admin'  when 1 then 'Manager' when 2 then 'Finance' when 3 then 'Cashier' when 4 then 'Reservation Admin' when 5 then 'Registration' when 6 then 'Security Guard' when 7 then 'Security Guard' end as Role, Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address  FROM [tbAdmin] a inner join tbLogin l on l.Id=a.LoginId where Deleted=0 and Level=7 order by Role asc";
            List<Admin> ret = default(List<Admin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Admin>(com);
            return ret;
        }
        public static List<Admin> Get(int id)
        {
            const string GET_ALL = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case Level when 0 then 'Super Admin'  when 1 then 'Manager' when 2 then 'Finance' when 3 then 'Cashier' when 4 then 'Reservation Admin' when 5 then 'Registration' when 6 then 'Security Guard' when 7 then 'Security Guard' end as Role, Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address  FROM [tbAdmin] a inner join tbLogin l on l.Id=a.LoginId where Deleted=0 and a.Id=@Rid and Level!=0 order by Role asc";
            List<Admin> ret = default(List<Admin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Rid", id));
            ret = SqlManager.Select<Admin>(com);
            return ret;
        }
        public static Admin GetbyID(int Id)
        {
           const string GET_RECORD = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case Level when 0 then 'Super Admin'  when 1 then 'Manager' when 2 then 'Finance' when 3 then 'Cashier' when 4 then 'Reservation Admin' when 5 then 'Registration' when 6 then 'Security Guard' when 7 then 'Security Guard' end as Role, Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address  FROM [tbAdmin] a inner join tbLogin l on l.Id=a.LoginId where a.LoginId = @Id and a.Deleted=0 order by Role asc";
           Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", Id));
            ret = SqlManager.Select<Admin>(com).First();

            return ret;
        }
        public static Admin GETID(int Id)
        {
            const string GET_RECORD = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case Level when 0 then 'Super Admin'  when 1 then 'Manager' when 2 then 'Finance' when 3 then 'Cashier' when 4 then 'Reservation Admin' when 5 then 'Registration' when 6 then 'Security Guard' when 7 then 'Security Guard' end as Role, Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address  FROM [tbAdmin] a inner join tbLogin l on l.Id=a.LoginId where a.Id = @Id and a.Deleted=0 order by Role asc";
            Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", Id));
            ret = SqlManager.Select<Admin>(com).First();

            return ret;
        }

        public static Admin GetIdAdmin(string idAdmin)
        {
            const string GET_RECORD = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case Level when 0 then 'Super Admin'  when 1 then 'Manager' when 2 then 'Finance' when 3 then 'Cashier' when 4 then 'Reservation Admin' when 5 then 'Registration' when 6 then 'Security Guard' when 7 then 'Security Guard' end as Role, Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address  FROM [tbAdmin] a inner join tbLogin l on l.Id=a.LoginId where a.Id =@Id and Deleted=0  order by Role asc";
            Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", idAdmin));
            ret = SqlManager.Select<Admin>(com).First();

            return ret;
        }
        public static Admin GetbyIDEdit(string Id)
        {
            const string GET_RECORD = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case Level when 0 then 'Super Admin'  when 1 then 'Manager' when 2 then 'Finance' when 3 then 'Cashier' when 4 then 'Reservation Admin' when 5 then 'Registration' when 6 then 'Security Guard' when 7 then 'Security Guard' end as Role , Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address   FROM [tbAdmin] a inner join tbLogin l on l.Id=a.LoginId where a.Id=@Id and Level!=0 order by Role asc";
            Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", Id));
            ret = SqlManager.Select<Admin>(com).First();

            return ret;
        }

        public static Admin GetbyIDEditt(string Id)
        {
            const string GET_RECORD = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case Level when 0 then '0'  when 1 then '1' when 2 then '2' when 3 then '3' when 4 then '4' when 5 then '5' when 6 then '6' when 7 then '7' end as Role , Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address FROM [tbAdmin] a inner join tbLogin l on l.Id=a.LoginId where a.Id=@Id and Level!=0 order by Role asc";
            Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", Id));
            ret = SqlManager.Select<Admin>(com).First();

            return ret;
        }

        public static Admin GetbyLevel(int Id)
        {
            const string GET_RECORD = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case Level when 0 then '0'  when 1 then '1' when 2 then '2' when 3 then '3' when 4 then '4' when 5 then '5' when 6 then '6' when 7 then '7' end as Role , Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address FROM [tbAdmin] a inner join tbLogin l on l.Id=a.LoginId where a.Id=@Id and Level!=0 order by Role asc";
            Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", Id));
            ret = SqlManager.Select<Admin>(com).First();

            return ret;
        }

        public static Admin GetEmailAdmin(string email)
        {
            const string GET_RECORD = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case ISNULL(Level,0) when 0 then 'Super Admin'  when 1 then 'Manager' when 2 then 'Finance' when 3 then 'Cashier' when 4 then 'Reservation Admin' when 5 then 'Registration' when 6 then 'Security Guard' when 7 then 'Security Guard' end as Role, Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address   FROM [tbAdmin] a full join tbLogin l on l.Id=a.LoginId WHERE Email = @Email and Deleted=0";
            Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Email", email));
            ret = SqlManager.Select<Admin>(com).First();

            return ret;
        }
        public static List<Admin> GetEmailAdminList(string email)
        {
            const string GET_RECORD = @"SELECT a.Id,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,case ISNULL(Level,0) when 0 then 'Super Admin'  when 1 then 'Manager' when 2 then 'Finance' when 3 then 'Cashier' when 4 then 'Reservation Admin' when 5 then 'Registration' when 6 then 'Security Guard' when 7 then 'Security Guard' end as Role, Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address   FROM [tbAdmin] a full join tbLogin l on l.Id=a.LoginId WHERE Email = @Email and Deleted=0";
            List<Admin> ret = default(List<Admin>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Email", email));
            ret = SqlManager.Select<Admin>(com);
            return ret;
        }
        public static bool Update(Admin adm)
        {
            const string GET_UPDATE = @"update [tbAdmin] set Fname= @Fname, Mname= @Mname,  Lname= @Lname, Bdate=@Bdate, CelNo=@CelNo,Email=@Email,ENo=@ENo,EName=@EName,EAddress=@EAddress,Address=@Address   WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", adm.Id));
            com.Parameters.Add(new SqlParameter("@Fname", adm.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", adm.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", adm.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", adm.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", adm.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", adm.Email));
            com.Parameters.Add(new SqlParameter("@ENo", adm.ENo));
            com.Parameters.Add(new SqlParameter("@EName", adm.EName));
            com.Parameters.Add(new SqlParameter("@EAddress", adm.EAddress));
            com.Parameters.Add(new SqlParameter("@Address", adm.Address));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Update(int lid,string email)
        {
            const string GET_UPDATE = @"update [tbAdmin] set LoginId=@lid WHERE Email=@Email";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@lid", lid));
            com.Parameters.Add(new SqlParameter("@email", email));
            return SqlManager.ExecuteNonQuery(com);
        }


        public static bool UpdateBoss(Admin adm)
        {
            const string GET_UPDATE = @"update [tbAdmin] set Fname= @Fname, Mname= @Mname,  Lname= @Lname, Bdate=@Bdate, CelNo=@CelNo,Email=@Email ,ReservationModule=@ReservationModule,RegistrationModule=@RegistrationModule,PaymentModule=@PaymentModule,ReversalModule=@ReversalModule,LogBookModule=@LogBookModule,ENo=@ENo,EName=@EName,EAddress=@EAddress,Address=@Address  WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", adm.Id));
            com.Parameters.Add(new SqlParameter("@Fname", adm.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", adm.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", adm.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", adm.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", adm.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", adm.Email));
            com.Parameters.Add(new SqlParameter("@ReservationModule", adm.ReservationModule));
            com.Parameters.Add(new SqlParameter("@RegistrationModule", adm.RegistrationModule));
            com.Parameters.Add(new SqlParameter("@PaymentModule", adm.PaymentModule));
            com.Parameters.Add(new SqlParameter("@ReversalModule", adm.ReversalModule));
            com.Parameters.Add(new SqlParameter("@LogBookModule", adm.LogBookModule));
            com.Parameters.Add(new SqlParameter("@ENo", adm.ENo));
            com.Parameters.Add(new SqlParameter("@EName", adm.EName));
            com.Parameters.Add(new SqlParameter("@EAddress", adm.EAddress));
            com.Parameters.Add(new SqlParameter("@Address", adm.Address));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool AUpdate(Admin adm)
        {
            const string GET_UPDATE = @"update [tbAdmin] set Fname= @Fname, Mname= @Mname,  Lname= @Lname, Email=@Email WHERE Id = @Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", adm.Id));
            com.Parameters.Add(new SqlParameter("@Fname", adm.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", adm.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", adm.Lname));
            com.Parameters.Add(new SqlParameter("@Email", adm.Email));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool UpdateDelete(Admin adm)
        {
            const string GET_UPDATE = @"update [tbAdmin] set Deleted=@Deleted  WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", adm.Id));
            com.Parameters.Add(new SqlParameter("@Deleted", adm.Deleted));

            return SqlManager.ExecuteNonQuery(com);
        }


        public static bool UpdateDP(Admin adm)
        {

            const string GET_UPDATE = @"update [tbAdmin] set URL=@URL  WHERE Id = @Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", adm.Id));
            com.Parameters.Add(new SqlParameter("@URL", adm.URL));
            return SqlManager.ExecuteNonQuery(com);
        }



        public static bool Delete(Admin adm)
        {
            const string GET_DELETE = @"delete [tbAdmin] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", adm.Id));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool InsertBoss(Admin adm)
        {
            const string GET_INSERT = @"insert [tbAdmin] (Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,ReservationModule,RegistrationModule,PaymentModule,ReversalModule,LogBookModule,LoginId,ENo,EName,EAddress,Address) values (@Fname,@Mname,@Lname,@Bdate,@CelNo,@Email,@Deleted,@Url,@ReservationModule,@RegistrationModule,@PaymentModule,@ReversalModule,@LogBookModule,0,@ENo,@EName,@EAddress,@Address)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Fname", adm.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", adm.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", adm.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", adm.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", adm.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", adm.Email));
            com.Parameters.Add(new SqlParameter("@Deleted", adm.Deleted));
            com.Parameters.Add(new SqlParameter("@Url", adm.URL));
            com.Parameters.Add(new SqlParameter("@ReservationModule", adm.ReservationModule));
            com.Parameters.Add(new SqlParameter("@RegistrationModule", adm.RegistrationModule));
            com.Parameters.Add(new SqlParameter("@PaymentModule", adm.PaymentModule));
            com.Parameters.Add(new SqlParameter("@ReversalModule", adm.ReversalModule));
            com.Parameters.Add(new SqlParameter("@LogBookModule", adm.LogBookModule));
            com.Parameters.Add(new SqlParameter("@ENo", adm.ENo));
            com.Parameters.Add(new SqlParameter("@EName", adm.EName));
            com.Parameters.Add(new SqlParameter("@EAddress", adm.EAddress));
            com.Parameters.Add(new SqlParameter("@Address", adm.Address));
            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool Insert(Admin adm)
        {
            const string GET_INSERT = @"insert [tbAdmin] (Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,Url,ENo,EName,EAddress,Address) values (@Fname,@Mname,@Lname,@Bdate,@CelNo,@Email,@Deleted,@Url,@ENo,@EName,@EAddress,@Address)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Fname", adm.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", adm.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", adm.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", adm.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", adm.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", adm.Email));
            com.Parameters.Add(new SqlParameter("@Deleted", adm.Deleted));
            com.Parameters.Add(new SqlParameter("@Url", adm.URL));
            com.Parameters.Add(new SqlParameter("@ENo", adm.ENo));
            com.Parameters.Add(new SqlParameter("@EName", adm.EName));
            com.Parameters.Add(new SqlParameter("@EAddress", adm.EAddress));
            com.Parameters.Add(new SqlParameter("@ENo", adm.ENo));
            com.Parameters.Add(new SqlParameter("@EName", adm.EName));
            com.Parameters.Add(new SqlParameter("@EAddress", adm.EAddress));
            com.Parameters.Add(new SqlParameter("@Address", adm.Address));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
