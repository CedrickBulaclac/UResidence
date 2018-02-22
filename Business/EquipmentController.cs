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
            const string GET_ALL = @"SELECT Name,Stocks,Rate,EquipmentNo FROM [tbEquipment] order by EquipmentNo";


            List<Equipment> ret = default(List<Equipment>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Equipment>(com);
            return ret;
        }

        public static Equipment Get(string name)
        {
            const string GET_RECORD = @"SELECT Name,Stocks,Rate,EquipmentNo FROM [tbEquipment] order by EquipmentNo WHERE Name = @Name";

            Equipment ret = default(Equipment);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Name", name));
            ret = SqlManager.Select<Equipment>(com).First();

            return ret;
        }

        public static Equipment GetbyEquipmentNo(string eno)
        {
            const string GET_RECORD = @"SELECT Name,Stocks,Rate,EquipmentNo FROM [tbEquipment] order by EquipmentNo WHERE EquipmentNo = @EquipmentNo";

            Equipment ret = default(Equipment);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@EquipmentNo", eno));
            ret = SqlManager.Select<Equipment>(com).First();

            return ret;
        }


        public static bool Update(Equipment eqp)
        {
            const string GET_UPDATE = @"update [tbEquipment] set Name= @name, Stocks= @Stocks, Rate = @Rate, EquipmentNo = @EquipmentNo WHERE EquipmentNo = @EquipmentNo";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Name", eqp.Name));
            com.Parameters.Add(new SqlParameter("@Stocks", eqp.Stocks));
            com.Parameters.Add(new SqlParameter("@Rate", eqp.Rate));
            com.Parameters.Add(new SqlParameter("@EquipmentNo", eqp.EquipmentNo));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Equipment eqp)
        {
            const string GET_DELETE = @"delete [tbEquipment] WHERE EquipmentNo = @EquipmentNo";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@EquipmentNo", eqp.EquipmentNo));

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
            const string GET_INSERT = @"insert [tbEquipment] (Name,Stocks,Rate,EquipmentNo) values (@Name, @Stocks, @Rate, @EquipmentNo)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Name", eqp.Name));
            com.Parameters.Add(new SqlParameter("@Stocks", eqp.Stocks));
            com.Parameters.Add(new SqlParameter("@Rate", eqp.Rate));
            com.Parameters.Add(new SqlParameter("@EquipmentNo", eqp.EquipmentNo));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}

