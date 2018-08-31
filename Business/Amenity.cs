using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace UResidence
{
    public class Amenity : BaseProperty<Amenity>
    {
       
        public int Id { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string AmenityName { get; set; }
        public string Url { get; set; }
        public int Rate { get; set; }
        public string Color {get; set;}
        public HttpPostedFileBase Image { get; set; }
       
        public Amenity CreateObject(SqlDataReader reader)
        {
            Amenity ret = new Amenity();
            ret.Id = reader.GetInt32(0);
            ret.Description = RemoveWhitespace(reader.GetString(1));
            ret.Capacity = reader.GetInt32(2);
            ret.AmenityName = reader.GetString(3);
           ret.Url = RemoveWhitespace(reader.GetString(4));
            ret.Rate = reader.GetInt32(5);
            ret.Color = RemoveWhitespace(reader.GetString(6));
            return ret;
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
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
                err.Add("Description must not be empty.");
                ret = false;
            }
            if(this.Rate<=0)
            {
                err.Add("Amenity rate must be at least 1");
                ret = false;
            }
           
            errors = err.ToArray();
            return ret;
        }
       
        public void Reset()
        {
            this.Description = string.Empty;
            this.Capacity = 0;
            this.AmenityName = string.Empty;
            this.Url = string.Empty;
            this.Rate = 0;
            this.Color = string.Empty;
        }
       
    }
}
