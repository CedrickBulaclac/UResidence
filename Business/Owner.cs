﻿using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;


namespace UResidence
{
    public class Owner : BaseProperty<Owner>
    {
        public int Id { get; set; }

        [StringLength(10, ErrorMessage = "Building Number cannot be longer than 10 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string BldgNo { get; set; }

        [StringLength(10, ErrorMessage = "Unit Number cannot be longer than 10 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Number only is allowed")]
        public string UnitNo { get; set; }

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Letters only")]
        [StringLength(30, ErrorMessage = "First Name cannot be longer than 30 characters.")]
        public string Fname { get; set; }

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Letters only")]
        [StringLength(30, ErrorMessage = "Middle Name cannot be longer than 30 characters.")]
        public string Mname { get; set; }

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Letters only")]
        [StringLength(30, ErrorMessage = "Last Name cannot be longer than 30 characters.")]
        public string Lname { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Bdate { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Invalid Mobile Number")]
        public string CelNo { get; set; }

        [RegularExpression(".+@.+\\..+",ErrorMessage ="Email format seems wrong")]
        [Required(ErrorMessage ="Enter Email")]
        public string Email { get; set; }
        public string FormattedDate => Bdate.ToShortDateString();

        public string Deleted { get; set; }
        public string URL { get; set; }
        public string Form { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public HttpPostedFileBase Image1 { get; set; }
        public string Fullname { get; set; }
        public string Birthday { get; set; }
        public int LoginId { get; set; }
        public class MinimumAgeAttribute : ValidationAttribute
        {
            int _minimumAge;

            public MinimumAgeAttribute(int minimumAge)
            {
                _minimumAge = minimumAge;
            }

            public override bool IsValid(object value)
            {
                DateTime date;
                if (DateTime.TryParse(value.ToString(), out date))
                {
                    return date.AddYears(_minimumAge) < DateTime.Now;
                }

                return false;
            }
        }


        public Owner CreateObject(SqlDataReader reader)
        {
            Owner ret = new Owner();
            ret.Id = reader.GetInt32(0);
            ret.BldgNo = RemoveWhitespace(reader.GetString(1));
            ret.UnitNo = RemoveWhitespace(reader.GetString(2));
            ret.Fname = RemoveWhitespace(reader.GetString(3));
            ret.Mname = RemoveWhitespace(reader.GetString(4));
            ret.Lname = RemoveWhitespace(reader.GetString(5));
            ret.Bdate = reader.GetDateTime(6);
            ret.CelNo = RemoveWhitespace(reader.GetString(7));
            ret.Email = RemoveWhitespace(reader.GetString(8));
            ret.Deleted = RemoveWhitespace(reader.GetString(9));
            ret.URL = RemoveWhitespace(reader.GetString(10));
            ret.Fullname = reader.GetString(11);
            ret.Birthday = reader.GetString(12);
            ret.Form = reader.GetString(13);
            ret.LoginId = reader.GetInt32(14);
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
            if (this.UnitNo.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Owner Unit No is required");
            }
            if (this.BldgNo.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Owner Building No is required");
            }
            if (this.Lname.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Owner Last name is required");
            }
            if (this.Email.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Owner Email is required");
            }
            if (this.Bdate == DateTime.Today)
            {
                ret = false;
                err.Add("Owner Birthday is required");
            }
            errors = err.ToArray();
            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
            this.BldgNo = string.Empty;
            this.UnitNo = string.Empty;
            this.Fname = string.Empty;
            this.Mname = string.Empty;
            this.Lname = string.Empty;
            this.Bdate = DateTime.Now;
            this.CelNo = string.Empty;
            this.Email = string.Empty;
            this.Deleted = string.Empty;
            this.URL = string.Empty;
            this.Form = string.Empty;

        }

        public static implicit operator Owner(string v)
        {
            throw new NotImplementedException();
        }
    }
}
