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
            const string GET_ALL = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,TelNo,Email,Citizenship,CivilStatus,Gender FROM[tbOwner] order by Id";

            List<Owner> ret = default(List<Owner>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Owner>(com);
            return ret;
        }

        
        public static Owner GetIdOwner(int idOwner)
        {
            const string GET_RECORD = @"SELECT Id,BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,TelNo,Email,Citizenship,CivilStatus,Gender FROM [tbOwner] WHERE Id = @Id";
            Owner ret = default(Owner);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", idOwner));
            ret = SqlManager.Select<Owner>(com).First();

            return ret;
        }


        public static bool Update(Owner own)
        {

            const string GET_UPDATE = @"update [tbAdmin] set Fname= @Fname, Mname= @Mname, Lname= @Lname, CelNo=@CelNo, TelNo=@TelNo, Citizenship=@Citizenship ,CivilStatus=@Status ,Email=@Email  WHERE Id = @Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Fname", own.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", own.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", own.Lname));
            com.Parameters.Add(new SqlParameter("@CelNo", own.CelNo));
            com.Parameters.Add(new SqlParameter("@TelNo", own.TelNo));
            com.Parameters.Add(new SqlParameter("@Email", own.Email));
            com.Parameters.Add(new SqlParameter("@Bdate", own.Bdate));
            com.Parameters.Add(new SqlParameter("@Citizenship", own.Citizenship));
            com.Parameters.Add(new SqlParameter("@Status", own.CivilStatus));
            com.Parameters.Add(new SqlParameter("@Id", own.Id));
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
            const string GET_INSERT = @"insert [tbOwner] (BldgNo,UnitNo,Fname,Mname,Lname,Bdate,CelNo,TelNo,Email,Citizenship,CivilStatus,Gender) values (@BldgNo,@UnitNo,@Fname,@Mname,@Lname,@Bdate,@CelNo,@TelNo,@Email,@Citizenship,@Status,@Gender) ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@BldgNo", own.BldgNo));
            com.Parameters.Add(new SqlParameter("@UnitNo", own.UnitNo));
            com.Parameters.Add(new SqlParameter("@Fname", own.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", own.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", own.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", own.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", own.CelNo));
            com.Parameters.Add(new SqlParameter("@TelNo", own.TelNo));
            com.Parameters.Add(new SqlParameter("@Email", own.Email));
            com.Parameters.Add(new SqlParameter("@Citizenship", own.Citizenship));
            com.Parameters.Add(new SqlParameter("@Status", own.CivilStatus));
            com.Parameters.Add(new SqlParameter("@Gender", own.Gender));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}

