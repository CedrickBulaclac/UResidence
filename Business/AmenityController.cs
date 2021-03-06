﻿using System;
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
            const string GET_ALL = @"SELECT Id,Description, Capacity, AmenityName FROM[tbAmenity] order by Id";


            List<Amenity> ret = default(List<Amenity>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Amenity>(com);
            return ret;
        }

        public static Amenity GetbyAmenityName(string name)
        {
            const string GET_RECORD = @"SELECT Id,Description, Capacity, AmenityName FROM[tbAmenity] order by AmenityName WHERE AmenityName = @AmenityName";

            Amenity ret = default(Amenity);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@AmenityName", name));
            ret = SqlManager.Select<Amenity>(com).First();

            return ret;
        }

        public static Amenity GetbyId(int id)
        {
            const string GET_RECORD = @"SELECT Id,Description,Capacity,AmenityName FROM [tbAmenity] WHERE Id = @Id";
            Amenity ret = default(Amenity);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<Amenity>(com).First();
            return ret;
        }

        public static bool Update(Amenity amty)
        {
            const string GET_UPDATE = @"update [tbAmenity] set Description= @Description, Capacity= @Capacity, AmenityName= @AmenityName WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Description", amty.Description));
            com.Parameters.Add(new SqlParameter("@Capacity", amty.Capacity));
            com.Parameters.Add(new SqlParameter("@AmenityName", amty.AmenityName));
            com.Parameters.Add(new SqlParameter("@Id", amty.Id));

            return SqlManager.ExecuteNonQuery(com);
        }

        public static bool Delete(Amenity amty)
        {
            const string GET_DELETE = @"delete [tbAmenity] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", amty.Id));

            return SqlManager.ExecuteNonQuery(com);
        }
      
        public static bool Insert(Amenity amty)
        {
            const string GET_INSERT = @"insert [tbAmenity] (Description,Capacity,AmenityName) values (@Description, @Capacity, @AmenityName)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Description", amty.Description));
            com.Parameters.Add(new SqlParameter("@Capacity", amty.Capacity));
            com.Parameters.Add(new SqlParameter("@AmenityName", amty.AmenityName));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
