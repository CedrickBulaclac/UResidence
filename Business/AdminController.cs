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
            const string GET_ALL = @"SELECT Id,AdminNo,Fname,Mname,Lname,Bdate,CelNo,TelNo,Gender,Age,Email FROM[tbAdmin] order by Id";

            List<Admin> ret = default(List<Admin>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Admin>(com);
            return ret;
        }

        public static List<Admin> Get(string AdminNo)
        {
            const string GET_RECORD = @"SELECT Id,AdminNo, Fname, Mname, Lname, Bdate, CelNo, TelNo, Gender, Age, Email FROM[tbAdmin] WHERE AdminNo = @AdminNo";

            List<Admin> ret = default(List<Admin>);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@AdminNo", AdminNo));
            ret = SqlManager.Select<Admin>(com);

            return ret;
        }

        public static Admin Get(int Id)
        {
            const string GET_RECORD = @"SELECT Id,AdminNo, Fname, Mname, Lname, Bdate, CelNo, TelNo, Gender, Age, Email FROM[tbAdmin] order by Id WHERE Id = @Id";

            Admin ret = default(Admin);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", Id));
            ret = SqlManager.Select<Admin>(com).First();

            return ret;
        }


        public static bool Update(Admin adm)
        {
            const string GET_UPDATE = @"update [tbAdmin] set AdminNo= @AdminNo, Fname= @Fname, Mname= @Mname,  Lname= @Lname, Bdate=@Bdate, CelNo=@CelNo, TelNo=@TelNo, Gender=@Gender, Age=@Age, Email=@Email  WHERE AdminNo = @AdminNo";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@AdminNo", adm.AdminNo));
            com.Parameters.Add(new SqlParameter("@Fname", adm.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", adm.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", adm.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", adm.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", adm.CelNo));
            com.Parameters.Add(new SqlParameter("@TelNo", adm.TelNo));
            com.Parameters.Add(new SqlParameter("@Gender", adm.Gender));
            com.Parameters.Add(new SqlParameter("@Age", adm.Age));
            com.Parameters.Add(new SqlParameter("@Email", adm.Email));
            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Admin adm)
        {
            const string GET_DELETE = @"delete [tbAdmin] WHERE AdminNo = @AdminNo";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@AdminNo", adm.AdminNo));

            return SqlManager.ExecuteNonQuery(com);
        }
        /*
        public static bool Modify(Equipment eqp)
        {
            if (eqp.EquipmentNo == 0)
                return Insert(eqp);
            else
                return Update(eqp);
        }
        */
        public static bool Insert(Admin adm)
        {
            const string GET_INSERT = @"insert [tbAdmin] (AdminNo,Fname,Mname,Lname,Bdate,CelNo,TelNo,Gender,Age,Email) values (@AdminNo,@Fname,@Mname,@Lname,@Bdate,@CelNo,@TelNo,@Gender,@Age,@Email) ";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@AdminNo", adm.AdminNo));
            com.Parameters.Add(new SqlParameter("@Fname", adm.Fname));
            com.Parameters.Add(new SqlParameter("@Mname", adm.Mname));
            com.Parameters.Add(new SqlParameter("@Lname", adm.Lname));
            com.Parameters.Add(new SqlParameter("@Bdate", adm.Bdate));
            com.Parameters.Add(new SqlParameter("@CelNo", adm.CelNo));
            com.Parameters.Add(new SqlParameter("@TelNo", adm.TelNo));
            com.Parameters.Add(new SqlParameter("@Gender", adm.Gender));
            com.Parameters.Add(new SqlParameter("@Age", adm.Age));
            com.Parameters.Add(new SqlParameter("@Email", adm.Email));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
