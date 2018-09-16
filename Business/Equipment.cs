using System;
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
    public class Equipment : BaseProperty<Equipment>
    {

        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Letters only")]
        [StringLength(30, ErrorMessage = "First Name cannot be longer than 30 characters.")]
        public string Name { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Numbers only")]
        public int Stocks { get; set; }

        [RegularExpression("^[0-9]*$", ErrorMessage = "Numbers only")]
        public int Rate { get; set; }
        public int Id { get; set; }
        public string Url { get; set; }

        [StringLength(250, ErrorMessage = "Description cannot be longer than 250 characters.")]
        public string Description { get; set; }

        public HttpPostedFileBase Image { get; set; }


        public Equipment CreateObject(SqlDataReader reader)
        {
            Equipment ret = new Equipment();

            ret.Id = reader.GetInt32(0);
            ret.Name = RemoveWhitespace(reader.GetString(1));
            ret.Stocks = reader.GetInt32(2);
            ret.Rate = reader.GetInt32(3);
            ret.Url = reader.GetString(4);
            ret.Description = reader.GetString(5);

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

            if (this.Name == string.Empty || this.Name is null)
            {
                err.Add("Equipmemt name is required!");
                ret = false;
            }
            if (this.Stocks <= 0)
            {
                err.Add("Equipmemt Stocks must be at least 1");
                ret = false;
            }

            if (this.Rate <= 0)
            {
                err.Add("Equipmemt Rate must be at least 1");
                ret = false;
            }
         
            errors = err.ToArray();
            return ret;
        }
        public void Reset()
        {
            this.Name = string.Empty;
            this.Stocks = 0;
            this.Rate = 0;
            this.Id =0;
            this.Url = string.Empty;
            this.Description = string.Empty;
        }
    }
}
