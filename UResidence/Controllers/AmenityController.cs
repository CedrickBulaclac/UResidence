using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UResidence;
namespace UResidence.Controllers
{
    public class AmenityController : Controller
    {
        // GET: Amenity
        public ActionResult Reserve()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Reserve(FormCollection fc)
        {
            string Desc = fc["Description"];
            int Capa = Convert.ToInt32(fc["Capacity"]);
            string ano = fc["AmenityNo"];
            Amenity am = new Amenity()
            {
                Description =Desc ,
              Capacity = Capa,
            AmenityNo = ano
        };
            UResidence.AmenityController.Insert(am);         
            return View();
        }

      
    }
}