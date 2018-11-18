using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Web;
namespace UResidence
{
    public class Admin : BaseProperty<Admin>
    {
        public int Id { get; set; }

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Letters only")]
        [StringLength(30, ErrorMessage = "First Name cannot be longer than 30 characters.")]
        public string Fname{ get; set; }

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Letters only")]
        [StringLength(30, ErrorMessage = "Middle Name cannot be longer than 30 characters.")]
        public string Mname{ get; set; }

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Letters only")]
        [StringLength(30, ErrorMessage = "Last Name cannot be longer than 30 characters.")]
        public string Lname { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Bdate { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Invalid Mobile Number")]
        public string CelNo { get; set; }
        [EmailAddress]

        [RegularExpression(".+@.+\\..+", ErrorMessage = "Email format seems wrong")]
        [Required(ErrorMessage = "Enter Email")]
        public string Email{ get; set; }
        public string Deleted { get; set; }
        public string URL { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }
        public string Birthday { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public bool ReservationModule { get; set; }
        public bool RegistrationModule { get; set; }
        public bool PaymentModule { get; set; }
        public bool ReversalModule { get; set; }
        public bool LogBookModule { get; set; }
        public string FormattedDate => Bdate.ToShortDateString();
        public int LoginId { get; set; }
        public Admin CreateObject(SqlDataReader reader)
        {
            Admin ret = new Admin();
            ret.Id = reader.GetInt32(0);
            ret.Fname = RemoveWhitespace(reader.GetString(1));
            ret.Mname = RemoveWhitespace(reader.GetString(2));
            ret.Lname = RemoveWhitespace(reader.GetString(3));
            ret.Bdate = reader.GetDateTime(4);
            ret.CelNo = RemoveWhitespace(reader.GetString(5));
            ret.Email = RemoveWhitespace(reader.GetString(6));
            ret.Deleted = RemoveWhitespace(reader.GetString(7));
            ret.URL = RemoveWhitespace(reader.GetString(8));
            ret.Role = reader.GetString(9);
            ret.Fullname = reader.GetString(10);
            ret.Birthday = reader.GetString(11);
            ret.ReservationModule = reader.GetBoolean(12);
            ret.RegistrationModule = reader.GetBoolean(13);
            ret.PaymentModule = reader.GetBoolean(14);
            ret.ReversalModule = reader.GetBoolean(15);
            ret.LogBookModule = reader.GetBoolean(16);
            ret.LoginId = reader.GetInt32(17);
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

            if (this.Bdate.Date == DateTime.Now )
            {
                err.Add("Birthday is required");
                ret = false;
            }
            errors = err.ToArray();
            return ret;
        }
        public void Reset()
        {
            this.Id = 0;
            this.Fname = string.Empty;
            this.Mname = string.Empty;
            this.Lname = string.Empty;
            this.Bdate = DateTime.Now;
            this.CelNo = string.Empty;
            this.Email = string.Empty;
            this.Deleted = string.Empty;
            this.URL = string.Empty;
            this.ReservationModule = false;
            this.RegistrationModule = false;
            this.PaymentModule = false;
            this.ReversalModule = false;
            this.LogBookModule = false;
        }
    }

    }

