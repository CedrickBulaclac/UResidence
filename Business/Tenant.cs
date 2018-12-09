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
     public class Tenant : BaseProperty<Tenant>
    {
        public int Id { get; set; }

        [StringLength(10, ErrorMessage = "Unit Number cannot be longer than 10 characters.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Number only is allowed")]
        public string UnitNo { get; set; }

        [StringLength(10, ErrorMessage = "Building Number cannot be longer than 10 characters.")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        public string BldgNo { get; set; }

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
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime Bdate { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Invalid Mobile Number")]
        public string CelNo { get; set; }

        [RegularExpression(".+@.+\\..+", ErrorMessage = "Email format seems wrong")]
        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }
        public string FormattedDate => Bdate.ToShortDateString();

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LeaseStart { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LeaseEnd { get; set; }

        public string Lease { get; set; }
        public string Deleted { get; set; }
        public string URL { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public string MovingIn { get; set; }
        public string MovingOut { get; set; }
        public HttpPostedFileBase Image1 { get; set; }
        public HttpPostedFileBase Image2 { get; set; }
        public string Fullname { get; set; }
        public string Birthday { get; set; }
        public int LoginId { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Invalid Mobile Number")]
        public string ENo { get; set; }

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Letters only")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters.")]
        public string EName { get; set; }

        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        public string EAddress { get; set; }


        [StringLength(100, ErrorMessage = "Address cannot be longer than 100 characters.")]
        public string Address { get; set; }

        public bool Validate(out string[] errors)
        {
            bool ret = true;
            List<string> err = new List<string>();
            if (this.UnitNo.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Tenant Unit No is required");
            }
            if (this.BldgNo.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Tenant Building No is required");
            }
            if (this.Lname.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Tenant Last name is required");
            }
            if (this.Email.Trim() == string.Empty)
            {
                ret = false;
                err.Add("Tenant Email is required");
            }
            if (this.Bdate == DateTime.Today)
            {
                ret = false;
                err.Add("Tenant Birthday is required");
            }
            errors = err.ToArray();
            return ret;
        }
        public string RemoveWhitespace(string str)
        {
            return string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
        }

        public void Reset()
        {
            this.Id = 0;
            this.UnitNo = string.Empty;
            this.BldgNo = string.Empty;
            this.Fname = string.Empty;
            this.Mname = string.Empty;
            this.Lname = string.Empty;
            this.Bdate = DateTime.Today;
            this.CelNo = string.Empty;
            this.Email = string.Empty;
            this.LeaseStart = DateTime.Today;
            this.LeaseEnd = DateTime.Today;
            this.Deleted = string.Empty;
            this.URL = string.Empty;
            this.MovingIn = string.Empty;
            this.MovingOut = string.Empty;

        }

        public Tenant CreateObject(SqlDataReader reader)
        {
            Tenant ret = new Tenant();

            ret.Id = reader.GetInt32(0);
            ret.UnitNo = RemoveWhitespace(reader.GetString(1));
            ret.BldgNo = RemoveWhitespace(reader.GetString(2));
            ret.Fname = reader.GetString(3);
            ret.Mname = reader.GetString(4);
            ret.Lname =reader.GetString(5);
            ret.Bdate = reader.GetDateTime(6);
            ret.CelNo = RemoveWhitespace(reader.GetString(7));
            ret.Email = reader.GetString(8);
            ret.LeaseStart = reader.GetDateTime(9);
            ret.LeaseEnd = reader.GetDateTime(10);
            ret.Deleted = RemoveWhitespace(reader.GetString(11));
            ret.URL = RemoveWhitespace(reader.GetString(12));
            ret.MovingIn = RemoveWhitespace(reader.GetString(13));
            ret.MovingOut = RemoveWhitespace(reader.GetString(14));
            ret.Fullname = reader.GetString(15);
            ret.Birthday = reader.GetString(16);
            ret.Lease = reader.GetString(17);
            ret.LoginId = reader.GetInt32(18);
            ret.ENo = reader.GetString(19);
            ret.EName = reader.GetString(20);
            ret.EAddress = reader.GetString(21);
            ret.Address = reader.GetString(22);
            return ret;
        }
    }
}
