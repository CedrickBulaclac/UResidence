using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UResidence
{
    public class ImageAmenity : BaseProperty<ImageAmenity>
    {
        public int Id { get; set; }
        public int AmenityId { get; set; }
        public string URL { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public ImageAmenity CreateObject(SqlDataReader reader)
        {
            ImageAmenity ret = new ImageAmenity();
            ret.Id = reader.GetInt32(0);
            ret.AmenityId = reader.GetInt32(1);
            ret.URL = reader.GetString(2);
            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
            this.AmenityId = 0;
            this.URL = string.Empty;
        }
    }
}
