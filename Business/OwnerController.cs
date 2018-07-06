﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class OwnerController
    {
        public static List<Owner> GetAll()
        {
            const string GET_ALL = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted FROM [tbOwner] where Deleted=0 order by Id";

            List<Owner> ret = default(List<Owner>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Owner>(com);
            return ret;
        }
<<<<<<< HEAD

        public static Owner GetEmailOwner(string email)
        {
            const string GET_RECORD = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted FROM [tbOwner] WHERE Email = @Email";

            Owner ret = default(Owner);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Email", email));
            ret = SqlManager.Select<Owner>(com).First();

            return ret;
        }




=======
        public static List<Owner> GetById(string idOwner)
        {
            const string GET_ALL = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted FROM[tbOwner] where Id=@Id";

            List<Owner> ret = default(List<Owner>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Id", idOwner));
            ret = SqlManager.Select<Owner>(com);
            
            return ret;
        }
>>>>>>> 23ca7d36a09bbfd9d277b5a70acda10c69910d2e

        public static Owner GetIdOwner(string idOwner)
        {
            const string GET_RECORD = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted FROM [tbOwner] WHERE Id = @Id";
            Owner ret = default(Owner);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", idOwner));
            ret = SqlManager.Select<Owner>(com).First();

            return ret;
        }


        public static bool Update(Owner own)
        {

            const string GET_UPDATE = @"update [tbOwner] set Fname= @Fname, Mname= @Mname, Lname= @Lname, CelNo=@CelNo,Email=@Email,BldgNo=@BldgNo,UnitNo=@UnitNo  WHERE Id = @Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Fname", own.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", own.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", own.Lname));
            com.Parameters.Add(new SqlParameter("@CelNo", own.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", own.Email));
            com.Parameters.Add(new SqlParameter("@Bdate", own.Bdate));
            com.Parameters.Add(new SqlParameter("@Id", own.Id));
            com.Parameters.Add(new SqlParameter("@BldgNo", own.BldgNo));
            com.Parameters.Add(new SqlParameter("@UnitNo", own.UnitNo));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool OUpdate(Owner own)
        {
            const string GET_UPDATE = @"update [tbOwner] set Fname= @Fname, Mname= @Mname,  Lname= @Lname WHERE Id = @Id";

<<<<<<< HEAD

        public static bool UpdateDelete(Owner own)
        {

            const string GET_UPDATE = @"update [tbOwner] set Deleted=@Deleted  WHERE Id = @Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", own.Id));
            com.Parameters.Add(new SqlParameter("@Deleted", own.Deleted));
            return SqlManager.ExecuteNonQuery(com);
        }




=======
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", own.Id));
            com.Parameters.Add(new SqlParameter("@Fname", own.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", own.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", own.Lname));
            return SqlManager.ExecuteNonQuery(com);
        }
>>>>>>> 23ca7d36a09bbfd9d277b5a70acda10c69910d2e
        public static bool Delete(Owner own)
        {

            const string GET_DELETE = @"delete [tbOwner] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", own.Id));
            return SqlManager.ExecuteNonQuery(com);
        }
        
        public static bool Insert(Owner own)
        {
         
            const string GET_INSERT = @"insert [tbOwner] (BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted) output inserted.Id values (@BldgNo,@UnitNo,@Fname,@Mname,@Lname,@Bdate,@CelNo,@Email,@Deleted) ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@BldgNo", own.BldgNo));
            com.Parameters.Add(new SqlParameter("@UnitNo", own.UnitNo));
            com.Parameters.Add(new SqlParameter("@Fname", own.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", own.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", own.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", own.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", own.CelNo));
            com.Parameters.Add(new SqlParameter("@Email", own.Email));
            com.Parameters.Add(new SqlParameter("@Deleted", own.Deleted));
           
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}

