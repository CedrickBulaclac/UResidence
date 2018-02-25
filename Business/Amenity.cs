using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class Amenity : BaseProperty<Amenity>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string AmenityName { get; set; }

        public Amenity CreateObject(SqlDataReader reader)
        {
            Amenity ret = new Amenity();
            ret.Id = reader.GetInt32(0);
            ret.Description = reader.GetString(1);
            ret.Capacity = reader.GetInt32(2);
            ret.AmenityName = reader.GetString(3);
            return ret;
        }
        public bool Validate(out string[] errors)
        {
            bool ret = true;

            List<string> err = new List<string>();

            if (this.AmenityName == string.Empty || this.AmenityName is null)
            {
                err.Add("Amenity name is required!");
                ret = false;
            }
            if (this.Capacity <= 0) 
                {
                    err.Add("Amenity Capacity must be at least 1");
                    ret = false;
                }
            
            if (this.Description == string.Empty || this.Description is null)
            {
                err.Add("Desription must not be empty.");
                ret = false;
            }

            errors = err.ToArray();
            return ret;
        }
        public static Amenity CreateObject(NameValueCollection fc)
        {
            int capacity = 0;
            if (!int.TryParse(fc["Capacity"], out capacity))
                capacity = 0;
            Amenity ret = new Amenity()
            {
                Description=fc["Description"],
                Capacity=capacity,
                AmenityName = fc["AmenityName"]

            };
            return ret;
        }
        public void Reset()
        {
            this.Description = string.Empty;
            this.Capacity = 0;
            this.AmenityName = string.Empty;
        }
     
    }
}
