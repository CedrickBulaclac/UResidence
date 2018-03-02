using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UResidence
{
    class SchedAmenity : BaseProperty<SchedAmenity>
    {

        public int AmenityId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTIme { get; set; }
        public int Rate { get; set; }
        public int Id { get; set; }
        public string AmenityName { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public string Url { get; set; }
        public List<Amenity> amenityList { get; set; }


        public void Reset()
        {
            this.AmenityId = 0;
            this.StartTime = DateTime.Today;
            this.EndTIme = DateTime.Today;
            this.Rate = 0;
            this.Id = 0;
            this.Capacity = 0;
            this.AmenityName = string.Empty;
            this.Description = string.Empty;
            this.Url = string.Empty;
        }


        public SchedAmenity CreateObject(SqlDataReader reader)
        {
            if (reader.FieldCount == 5)
            {
                SchedAmenity ret = new SchedAmenity();
                ret.Id = reader.GetInt32(0);
                ret.AmenityId = reader.GetInt32(1);
                ret.StartTime = reader.GetDateTime(2);
                ret.EndTIme = reader.GetDateTime(3);
                ret.Rate = reader.GetInt32(4);

                return ret;
            }
            else
            {
                SchedAmenity ret = new SchedAmenity();
                ret.Id = reader.GetInt32(0);
                ret.AmenityId = reader.GetInt32(1);
                ret.AmenityName = reader.GetString(2);
                ret.Rate = reader.GetInt32(3);
                ret.StartTime = reader.GetDateTime(4);
                ret.EndTIme = reader.GetDateTime(5);
                return ret;
            }
        }






    }



}
