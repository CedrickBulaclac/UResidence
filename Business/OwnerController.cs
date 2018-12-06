using System;
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
            const string GET_ALL = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,URL,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Form,LoginId,ENo,EName,EAddress FROM [tbOwner] where Deleted=0 order by Id";

            List<Owner> ret = default(List<Owner>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Owner>(com);
            return ret;
        }

        public static Owner GetEmailOwner(string email)
        {
            const string GET_RECORD = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,URL,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Form,LoginId,ENo,EName,EAddress FROM [tbOwner] WHERE Email = @Email and Deleted=0";

            Owner ret = default(Owner);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Email", email));
            ret = SqlManager.Select<Owner>(com).First();

            return ret;
        }

        public static Owner GETID(int id)
        {
            const string GET_RECORD = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,URL,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Form,LoginId,ENo,EName,EAddress FROM [tbOwner] WHERE Id = @Id and Deleted=0";

            Owner ret = default(Owner);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<Owner>(com).First();

            return ret;
        }

        public static List<Owner> GetEmailOwnerList(string email)
        {
            const string GET_RECORD = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,URL,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Form,LoginId,ENo,EName,EAddress FROM [tbOwner] WHERE Email = @Email and Deleted=0";
            List<Owner> ret = default(List<Owner>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Email", email));
            ret = SqlManager.Select<Owner>(com);

            return ret;
        }
        public static bool Update(int lid, string email)
        {
            const string GET_UPDATE = @"update [tbOwner] set LoginId=@lid WHERE Email=@Email";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@lid", lid));
            com.Parameters.Add(new SqlParameter("@email", email));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static List<Owner> GetOwnerReserve(string bldgno, string unitno)
        {
            const string GET_RECORD = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,URL,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Form,LoginId,ENo,EName,EAddress FROM [tbOwner] where BldgNo=@BldgNo and UnitNo=@UnitNo and Deleted=0";
            List<Owner> ret = default(List<Owner>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@BldgNo", bldgno));
            com.Parameters.Add(new SqlParameter("@UnitNo", unitno));
            ret = SqlManager.Select<Owner>(com);

            return ret;
        }

        public static Owner GetBldgUnit(string bldgno, string unitno)
        {
            const string GET_RECORD = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,URL,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Form,LoginId,ENo,EName,EAddress FROM [tbOwner] where BldgNo=@BldgNo and UnitNo=@UnitNo and Deleted=0";
            Owner ret = default(Owner);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@BldgNo", bldgno));
            com.Parameters.Add(new SqlParameter("@UnitNo", unitno));
            ret = SqlManager.Select<Owner>(com).First();

            return ret;
        }

        public static Owner GetOwnerReservee(string bldgno, string unitno)
        {
            const string GET_RECORD = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,URL,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Form,LoginId,ENo,EName,EAddress FROM [tbOwner] where BldgNo=@BldgNo and UnitNo=@UnitNo and Deleted=0";
            Owner ret = default(Owner);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@BldgNo", bldgno));
            com.Parameters.Add(new SqlParameter("@UnitNo", unitno));
            ret = SqlManager.Select<Owner>(com).First();

            return ret;
        }




        public static List<Owner> GetById(string idOwner)
        {
            const string GET_ALL = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,URL,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Form,LoginId,ENo,EName,EAddress FROM[tbOwner] where Id=@Id and Deleted=0";

            List<Owner> ret = default(List<Owner>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Id", idOwner));
            ret = SqlManager.Select<Owner>(com);
            
            return ret;
        }

       

        public static Owner GetIdOwner(string idOwner)
        {
            const string GET_RECORD = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,URL,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Form,LoginId,ENo,EName,EAddress FROM [tbOwner] WHERE Id = @Id and Deleted=0";
            Owner ret = default(Owner);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", idOwner));
            ret = SqlManager.Select<Owner>(com).First();

            return ret;
        }
        public static Owner GetIdOwner(int idOwner)
        {
            const string GET_RECORD = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,URL,Fname+' '+Mname+' '+Lname as Fullname,FORMAT(Bdate,'MMM dd yyyy') as Birthday,Form,LoginId,ENo,EName,EAddress FROM [tbOwner] WHERE Id = @Id and Deleted=0";
            Owner ret = default(Owner);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", idOwner));
            ret = SqlManager.Select<Owner>(com).First();

            return ret;
        }

        public static bool Update(Owner own)
        {

            const string GET_UPDATE = @"update [tbOwner] set Fname= @Fname, Mname= @Mname, Lname= @Lname, CelNo=@CelNo,Email=@Email,Bdate=@Bdate,BldgNo=@BldgNo,UnitNo=@UnitNo,Eno=@Eno,EName=@EName,EAddress=@EAddress  WHERE Id = @Id";
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
            com.Parameters.Add(new SqlParameter("@ENo", own.ENo));
            com.Parameters.Add(new SqlParameter("@EName", own.EName));
            com.Parameters.Add(new SqlParameter("@EAddress", own.EAddress));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool OUpdate(Owner own)
        {
            const string GET_UPDATE = @"update [tbOwner] set Fname= @Fname, Mname= @Mname,  Lname= @Lname, Email=@Email WHERE Id = @Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", own.Id));
            com.Parameters.Add(new SqlParameter("@Fname", own.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", own.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", own.Lname));
            com.Parameters.Add(new SqlParameter("@Email", own.Email));
            return SqlManager.ExecuteNonQuery(com);
        }


        public static bool UpdateDelete(Owner own)
        {

            const string GET_UPDATE = @"update [tbOwner] set Deleted=@Deleted  WHERE Id = @Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", own.Id));
            com.Parameters.Add(new SqlParameter("@Deleted", own.Deleted));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool UpdateDP(Owner own)
        {

            const string GET_UPDATE = @"update [tbOwner] set URL=@URL  WHERE Id = @Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", own.Id));
            com.Parameters.Add(new SqlParameter("@URL", own.URL));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool UpdateForm(Owner own)
        {

            const string GET_UPDATE = @"update [tbOwner] set Form=@Form  WHERE Id = @Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", own.Id));
            com.Parameters.Add(new SqlParameter("@Form", own.Form));
            return SqlManager.ExecuteNonQuery(com);
        }





        public static bool Delete(Owner own)
        {

            const string GET_DELETE = @"delete [tbOwner] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", own.Id));
            return SqlManager.ExecuteNonQuery(com);
        }
        
        public static bool Insert(Owner own)
        {
         
            const string GET_INSERT = @"insert [tbOwner] (BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,Email,Deleted,URL,Form,LoginId,ENo,EName,EAddress) output inserted.Id values (@BldgNo,@UnitNo,@Fname,@Mname,@Lname,@Bdate,@CelNo,@Email,@Deleted,@URL,@Form,0,@ENo,@EName,@EAddress) ";

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
            com.Parameters.Add(new SqlParameter("@URL", own.URL));
            com.Parameters.Add(new SqlParameter("@Form", own.Form));
            com.Parameters.Add(new SqlParameter("@ENo", own.ENo));
            com.Parameters.Add(new SqlParameter("@EName", own.EName));
            com.Parameters.Add(new SqlParameter("@EAddress", own.EAddress));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}

