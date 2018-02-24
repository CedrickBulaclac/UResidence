using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class AmenityController
    {
        public static List<Amenity> GetAll()
        {
            const string GET_ALL = @"SELECT Description, Capacity, AmenityNo FROM[tbAmenity] order by AmenityNo";


            List<Amenity> ret = default(List<Amenity>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Amenity>(com);
            return ret;
        }

        public static Amenity GetbyDesc(string name)
        {
            const string GET_RECORD = @"SELECT Description, Capacity, AmenityNo FROM[tbAmenity] order by AmenityNo WHERE Description = @Description";

            Amenity ret = default(Amenity);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Name", name));
            ret = SqlManager.Select<Amenity>(com).First();

            return ret;
        }

        public static Amenity GetbyId(int id)
        {
            const string GET_RECORD = @"SELECT Description,Capacity,AmenityNo FROM [tbAmenity] WHERE Id = @Id";
            Amenity ret = default(Amenity);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<Amenity>(com).First();
            return ret;
        }

        public static List<Amenity> GetbyAmenityNo(string amp)
        {
            const string GET_RECORD = @"SELECT Description,Capacity,AmenityNo FROM [tbAmenity] WHERE AmenityNo = @AmenityNo";
            List<Amenity> ret = default(List<Amenity>);
           // Amenity ret = default(Amenity);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@AmenityNo", amp));
            // ret = SqlManager.Select<Amenity>(com).First();
            ret = SqlManager.Select<Amenity>(com);
            return ret;
        }


        public static bool Update(Amenity amty)
        {
            const string GET_UPDATE = @"update [tbAmenity] set Description= @Description, Capacity= @Capacity, AmenityNo= @AmenityNo WHERE AmenityNo = @AmenityNo";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Description", amty.Description));
            com.Parameters.Add(new SqlParameter("@Capacity", amty.Capacity));
            com.Parameters.Add(new SqlParameter("@AmenityNo", amty.AmenityNo));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Amenity amty)
        {
            const string GET_DELETE = @"delete [tbAmenity] WHERE AmenityNo = @AmenityNo";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@AmenityNo", amty.AmenityNo));

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
        public static bool Insert(Amenity amty)
        {
            const string GET_INSERT = @"insert [tbAmenity] (Description,Capacity,AmenityNo) values (@Description, @Capacity, @AmenityNo)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Description", amty.Description));
            com.Parameters.Add(new SqlParameter("@Capacity", amty.Capacity));
            com.Parameters.Add(new SqlParameter("@AmenityNo", amty.AmenityNo));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
