using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class AccountSettingController : Controller
    {
        // GET: AccountSetting
        public ActionResult ViewAccountSetting()
        {
            
            string RType =(string) Session["TOR"];
            if(RType.Equals("Admin"))
            {
                Admin a = new Admin();
                int Aid =(int) Session["UID"];
                UResidence.AdminController.GetbyID(Aid);
                
            }
            else if(RType.Equals("Owner"))
            {

            }
            else
            {

            }
            return View();
        }
    }
}