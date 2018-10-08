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
            const string GET_ALL = @" select a.Id,a.Description,a.Capacity,a.AmenityName,a.URL,a.Rate,a.Color,a.Location,ISNULL(sp.AdultRate,0) as AdultRate,ISNULL(sp.ChildRate,0) as ChildRate,a.EveningRate from tbAmenity a full join tbSwimmingPool sp on a.Id=sp.AmenityId where Color!='Black' order by a.Id ";


            List<Amenity> ret = default(List<Amenity>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Amenity>(com);
            return ret;
        }
        
        public static List<Amenity> GetAllM()
        {
            const string GET_ALL = @"  select a.Id,a.Description,a.Capacity,a.AmenityName,a.URL,a.Rate,a.Color,a.Location,ISNULL(sp.AdultRate,0) as AdultRate,ISNULL(sp.ChildRate,0) as ChildRate,a.EveningRate from tbAmenity a full join tbSwimmingPool sp on a.Id=sp.AmenityId where Color='Black' order by a.Id ";


            List<Amenity> ret = default(List<Amenity>);
            SqlCommand com = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Amenity>(com);
            return ret;
        }
        public static List<Amenity> GetPicById(int id)
        {
            const string GET_ALL = @"select a.Id,Description,Capacity,AmenityName,ia.URL,Rate,Color,Location,EveningRate from tbAmenity a inner join tbImageAmenity ia on a.Id=ia.AmenityId where a.AmenityId=@Id";


            List<Amenity> ret = default(List<Amenity>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<Amenity>(com);
            return ret;
        }
        public static Amenity GetbyAmenityName(string name)
        {
            const string GET_RECORD = @" select a.Id,a.Description,a.Capacity,a.AmenityName,a.URL,a.Rate,a.Color,a.Location,ISNULL(sp.AdultRate,0) as AdultRate,ISNULL(sp.ChildRate,0) as ChildRate,a.EveningRate from tbAmenity a full join tbSwimmingPool sp on a.Id=sp.AmenityId WHERE a.AmenityName = @AmenityName order by a.AmenityName ";

            Amenity ret = default(Amenity);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@AmenityName", name));
            ret = SqlManager.Select<Amenity>(com).First();

            return ret;
        }


        public static Amenity GetAmenityImage(int Id)
        {
            const string GET_RECORD = @" select a.Id,a.Description,a.Capacity,a.AmenityName,a.URL,a.Rate,a.Color,a.Location,ISNULL(sp.AdultRate,0) as AdultRate,ISNULL(sp.ChildRate,0) as ChildRate,a.EveningRate from tbAmenity a full join tbSwimmingPool sp on a.Id=sp.AmenityId where a.Id=@Id and Color!='Black' order by Id ";

            Amenity ret = default(Amenity);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", Id));
            ret = SqlManager.Select<Amenity>(com).First();

            return ret;
        }

        public static Amenity GetbyId(int id)
        {
            const string GET_RECORD = @" select a.Id,a.Description,a.Capacity,a.AmenityName,a.URL,a.Rate,a.Color,a.Location,ISNULL(sp.AdultRate,0) as AdultRate,ISNULL(sp.ChildRate,0) as ChildRate,a.EveningRate from tbAmenity a full join tbSwimmingPool sp on a.Id=sp.AmenityId WHERE a.Id = @Id";
            Amenity ret = default(Amenity);
            SqlCommand com = new SqlCommand(GET_RECORD);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<Amenity>(com).First();
            return ret;
        }

        public static bool Update(Amenity amty)
        {
            const string GET_UPDATE = @"update [tbAmenity] set Description= @Description, Capacity= @Capacity, AmenityName= @AmenityName, Rate=@Rate, Color=@Color , Location=@Location,EveningRate=@EveningRate WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Description", amty.Description));
            com.Parameters.Add(new SqlParameter("@Capacity", amty.Capacity));
            com.Parameters.Add(new SqlParameter("@AmenityName", amty.AmenityName));
            com.Parameters.Add(new SqlParameter("@Id", amty.Id));
            com.Parameters.Add(new SqlParameter("@Rate", amty.Rate));
            com.Parameters.Add(new SqlParameter("@Color", amty.Color));
            com.Parameters.Add(new SqlParameter("@Location", amty.Location));
            com.Parameters.Add(new SqlParameter("@EveningRate", amty.EveRate));
            return SqlManager.ExecuteNonQuery(com);
        }


        public static bool UpdateImage(Amenity amty)
        {
            const string GET_UPDATE = @"update [tbAmenity] set URL=@Url WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Url", amty.Url));
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
            const string GET_INSERT = @"insert [tbAmenity] (Description,Capacity,AmenityName,URL,Rate,Color,Location,EveningRate) values (@Description, @Capacity, @AmenityName,@Url,@Rate,@Color,@Location,@EveningRate)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Description", amty.Description));
            com.Parameters.Add(new SqlParameter("@Capacity", amty.Capacity));
            com.Parameters.Add(new SqlParameter("@AmenityName", amty.AmenityName));
            com.Parameters.Add(new SqlParameter("@Url", amty.Url));
            com.Parameters.Add(new SqlParameter("@Rate", amty.Rate));
            com.Parameters.Add(new SqlParameter("@Color", amty.Color));
            com.Parameters.Add(new SqlParameter("@Location", amty.Location));
            com.Parameters.Add(new SqlParameter("@EveningRate", amty.EveRate));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool InsertImage(Amenity amty)
        {
            const string GET_INSERT = @"insert [tbImahgeAmenity] (Id,URL) values (@Id,@Url)";

            SqlCommand com = new SqlCommand(GET_INSERT);
           
            com.Parameters.Add(new SqlParameter("@Url", amty.Url));
            com.Parameters.Add(new SqlParameter("@Id", amty.Id));

            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
