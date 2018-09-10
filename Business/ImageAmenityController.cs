using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace UResidence
{
    public class ImageAmenityController
    {
        public static List<ImageAmenity> GetPicByAId(int id)
        {
            const string GET_ALL = @"select Id,AmenityId,Url from [tbImageAmenity] where AmenityId=@Id";


            List<ImageAmenity> ret = default(List<ImageAmenity>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@Id", id));
            ret = SqlManager.Select<ImageAmenity>(com);
            return ret;
        }

        public static bool InsertImage(ImageAmenity amty)
        {
            const string GET_INSERT = @"insert [tbImageAmenity] (AmenityId,URL) values (@AmenityId,@Url)";

            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Url", amty.URL));
            com.Parameters.Add(new SqlParameter("@AmenityId", amty.AmenityId));
            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool UpdateImage(ImageAmenity amty)
        {
            const string GET_UPDATE = @"update [tbImageAmenity] set URL=@Url WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Url", amty.URL));
            com.Parameters.Add(new SqlParameter("@Id", amty.Id));
            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool Delete(int id)
        {
            const string GET_UPDATE = @"delete [tbImageAmenity]  WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", id));
            return SqlManager.ExecuteNonQuery(com);
        }
    }
}
