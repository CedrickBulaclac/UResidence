﻿using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace UResidence
{
    public class Amenity : BaseProperty<Amenity>
    {
       
        public int Id { get; set; }
        [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters.")]
        public string Description { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Numbers only")]
        public int Capacity { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [StringLength(30, ErrorMessage = "Amenity Name cannot be longer than 30 characters.")]
        public string AmenityName { get; set; }
        public string Url { get; set; }
        [RegularExpression("^[0-9 .]*$", ErrorMessage = "Numbers only")]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }

        //[RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Letters only")]
        //[StringLength(20, ErrorMessage = "The color cannot be longer than 20 characters.")]
        public string Color {get; set;}
        public HttpPostedFileBase Image { get; set; }
        [StringLength(50, ErrorMessage = "Unit Number cannot be longer than 50 characters.")]
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string Location { get; set; }
        [DataType(DataType.Currency)]
        public decimal Adult { get; set; }
        [DataType(DataType.Currency)]
        public decimal Child { get; set; }
        [RegularExpression("^[0-9 .]*$", ErrorMessage = "Numbers only")]
        [DataType(DataType.Currency)]
        public decimal EveRate { get; set; }

        public bool IsEquipment { get; set; }
        public bool IsWeekend { get; set; }
        public int Deleted { get; set; }
        public Amenity CreateObject(SqlDataReader reader)
        {
            Amenity ret = new Amenity();
            ret.Id = reader.GetInt32(0);
            ret.Description = reader.GetString(1);
            ret.Capacity = reader.GetInt32(2);
            ret.AmenityName = reader.GetString(3);
           ret.Url = RemoveWhitespace(reader.GetString(4));
            ret.Rate = reader.GetDecimal(5);
            ret.Color = RemoveWhitespace(reader.GetString(6));
            ret.Location = reader.GetString(7);
            ret.Adult = reader.GetDecimal(8);
            ret.Child = reader.GetDecimal(9);
            ret.EveRate = reader.GetDecimal(10);
            ret.IsEquipment = reader.GetBoolean(11);
            ret.IsWeekend = reader.GetBoolean(12);
            ret.Deleted = reader.GetInt32(13);
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
            if(this.Rate<0)
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
            this.Location = string.Empty;
            this.EveRate = 0;
            this.IsEquipment = false;
            this.IsWeekend = false;
            this.Deleted = 0;
        }
       
    }
}
