using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class EquipmentController
    {
        public static List<Equipment> GetAll()
        {
            const string GET_ALL = @"SELECT Id,Name,Stocks,Rate FROM [tbEquipment] order by Id";


            List<Equipment> ret = default(List<Equipment>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Equipment>(com);
            return ret;
        }

        public static Equipment Get(string name)
        {
            const string GET_RECORD = @"SELECT Id,Name,Stocks,Rate FROM [tbEquipment] order by EquipmentNo WHERE Name = @Name";

            Equipment ret = default(Equipment);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Name", name));
            ret = SqlManager.Select<Equipment>(com).First();

            return ret;
        }

        public static Equipment GetbyId(int eno)
        {
            const string GET_RECORD = @"SELECT Id,Name,Stocks,Rate FROM [tbEquipment] WHERE Id = @Id";

            Equipment ret = default(Equipment);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", eno));
            ret = SqlManager.Select<Equipment>(com).First();

            return ret;
        }


        public static bool Update(Equipment eqp)
        {
            const string GET_UPDATE = @"update [tbEquipment] set Name=@Name, Stocks=@Stocks, Rate=@Rate  WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Name", eqp.Name));
            com.Parameters.Add(new SqlParameter("@Stocks", eqp.Stocks));
            com.Parameters.Add(new SqlParameter("@Rate", eqp.Rate));
            com.Parameters.Add(new SqlParameter("@Id", eqp.Id));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Equipment eqp)
        {
            const string GET_DELETE = @"delete [tbEquipment] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", eqp.Id));

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
        public static bool Insert(Equipment eqp)
        {
            const string GET_INSERT = @"insert [tbEquipment] (Name,Stocks,Rate) values (@Name, @Stocks, @Rate)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Name", eqp.Name));
            com.Parameters.Add(new SqlParameter("@Stocks", eqp.Stocks));
            com.Parameters.Add(new SqlParameter("@Rate", eqp.Rate));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}

