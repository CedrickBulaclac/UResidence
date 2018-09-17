using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class ReportReservationAmenity : BaseProperty<ReportReservationAmenity>
    {
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string AmenityName { get; set; }
        public string ReservedDate { get; set; }
        public string TypeResident { get; set; }
        public int TenantId { get; set;}
        public int OwnerId { get; set; }
        public string UnitNo { get; set; }
        public string BldgNo { get; set; }
        public int Rate { get; set; }
        public int AmenityHour { get; set; }
        public int TotalRate { get; set; }
        public int ReservationFormId { get; set; }

        public ReportReservationAmenity CreateObject(SqlDataReader reader)
        {
            ReportReservationAmenity ret = new ReportReservationAmenity();
            ret.Date = reader.GetDateTime(0);
            ret.AmenityHour = reader.GetInt32(1);
            ret.Name = reader.GetString(2);
            ret.OwnerId = reader.GetInt32(3);
            ret.TenantId = reader.GetInt32(4);
            ret.TypeResident = reader.GetString(5);
            ret.ReservationFormId = reader.GetInt32(6);
            ret.ReservedDate = reader.GetString(7);
            ret.AmenityName = reader.GetString(8);
            ret.Rate = reader.GetInt32(9);
            ret.TotalRate = reader.GetInt32(10);
            ret.UnitNo = reader.GetString(11);
            ret.BldgNo = reader.GetString(12);
            return ret;
        }

        public void Reset()
        {
          
        }
    }
}
