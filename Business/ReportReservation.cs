using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UResidence
{
    public class ReportReservation : BaseProperty<ReportReservation>
    {
        public DateTime Date { get; set; }
        public string OwnerName { get; set; }
        public string TenantName { get; set; }
        public string AmenityName { get; set; }
        public string DateofReservation { get; set; }
        public string TypeResident { get; set; }
        public int TenantId { get; set; }
        public int OwnerId { get; set; }
        public string UnitNo { get; set; }
        public string BldgNo { get; set; }
        public int Rate { get; set; }
        public int AmenityHour { get; set; }
        public int TotalAmenityRate { get; set; }
        public int RefNo { get; set; }
        public string EquipmentName { get; set; }
        public int EquipmentRate { get; set; }
        public int EquipmentQuantity { get; set; }
        public int EquipmentCost { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Charge { get; set; }
        public int TotalPayment { get; set; }
        public string Description { get; set; }
        public DateTime DateP { get; set; }
        public int AdultRate { get; set; }
        public int ChildRate { get; set; }
        public DateTime Starttime{get;set;}
        public DateTime EndTime { get; set; }
        public ReportReservation CreateObject(SqlDataReader reader)
        {
            ReportReservation ret = new ReportReservation();
            ret.BldgNo = reader.GetString(0);
            ret.UnitNo = reader.GetString(1);
            ret.OwnerName = reader.GetString(2);
            ret.TenantName = reader.GetString(3);
            ret.RefNo = reader.GetInt32(4);
            ret.Starttime = reader.GetDateTime(5);
            ret.EndTime = reader.GetDateTime(6);
            ret.DateofReservation = reader.GetString(7);   
            ret.AmenityName = reader.GetString(8);
            ret.Rate = reader.GetInt32(9);
            ret.AdultRate = reader.GetInt32(10);
            ret.ChildRate = reader.GetInt32(11);
            ret.AmenityHour = reader.GetInt32(12);
            ret.TotalAmenityRate = reader.GetInt32(13);
            ret.Date = reader.GetDateTime(14);
            ret.EquipmentName = reader.GetString(15);
            ret.EquipmentQuantity = reader.GetInt32(16);
            ret.EquipmentRate = reader.GetInt32(17);
            ret.EquipmentCost = reader.GetInt32(18);
            ret.TypeResident = reader.GetString(19);
            ret.Adult = reader.GetInt32(20);
            ret.Child = reader.GetInt32(21);
            ret.Charge = reader.GetInt32(22);
            ret.TotalPayment = reader.GetInt32(23);
            ret.Description = reader.GetString(24);
            ret.DateP = reader.GetDateTime(25);

            return ret;
        }

        public void Reset()
        {
          
        }
    }
}
