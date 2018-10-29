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
        public decimal Rate { get; set; }
        public decimal EveningRate { get; set; }
        public int AmenityHour { get; set; }
        public decimal TotalAmenityRate { get; set; }
        public int RefNo { get; set; }
        public string EquipmentName { get; set; }
        public decimal EquipmentRate { get; set; }
        public int EquipmentQuantity { get; set; }
        public decimal EquipmentCost { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public decimal Charge { get; set; }
        public decimal Totalpayment { get; set; }
        public string Description { get; set; }
        public DateTime DateP { get; set; }
        public decimal AdultRate { get; set; }
        public decimal ChildRate { get; set; }
        public DateTime Starttime{get;set;}
        public DateTime EndTime { get; set; }
        public decimal TotalEquipmentRate { get; set; }
        public ReportReservation CreateObject(SqlDataReader reader)
        {
            ReportReservation ret = new ReportReservation();
            ret.BldgNo = reader.GetString(0);
            ret.UnitNo = reader.GetString(1);
            ret.OwnerName = reader.GetString(2);           
            ret.RefNo = reader.GetInt32(3);
            ret.Starttime = reader.GetDateTime(4);
            ret.EndTime = reader.GetDateTime(5);
            ret.DateofReservation = reader.GetString(6);   
            ret.AmenityName = reader.GetString(7);
            ret.Rate = reader.GetDecimal(8);
            ret.EveningRate = reader.GetDecimal(9);
            ret.AmenityHour = reader.GetInt32(10);
            ret.TotalAmenityRate = reader.GetDecimal(11);
            ret.Date = reader.GetDateTime(12);           
            ret.TypeResident = reader.GetString(13);        
            ret.Charge = reader.GetDecimal(14);
            ret.Totalpayment = reader.GetDecimal(15);
            ret.Description = reader.GetString(16);
            ret.DateP = reader.GetDateTime(17);
            ret.TotalEquipmentRate = reader.GetDecimal(18);
            return ret;
        }

        public void Reset()
        {
          
        }
    }
}
