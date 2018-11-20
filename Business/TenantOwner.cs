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
    public class TenantOwner
    {
        public List<object>  BuildingList{ get; set; }
        public List<object> UnitNoList { get; set; }
        public Tenant tenant { get; set; }
    }
}
