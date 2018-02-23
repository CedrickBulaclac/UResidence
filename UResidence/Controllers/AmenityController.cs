using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UResidence;
namespace UResidence.Controllers
{
    public class AmenityController : Controller
    {
        bool status;
        // GET: Amenity
        public ActionResult AmenityAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AmenityAdd(FormCollection fc)
        {
            NameValueCollection values = fc;
            Amenity amen = Amenity.CreateObject(values);
            if (amen.Validate() == true)
            {
                status = UResidence.AmenityController.Insert(amen);
                if (status == true)
                {
                    ViewBag.Message = true;
                }
                else
                {
                    ViewBag.Message = false;
                }
               
            }
            return View();
        }

        public ActionResult AmenityView()
        {

            List<Amenity> amenityList = default(List<Amenity>);
            amenityList = UResidence.AmenityController.GetAll();
            ViewBag.amenity = amenityList;
            
            return View();
        }

       
        public ActionResult Delete(int? id)
        {

            Amenity am = new Amenity()
            {
                AmenityNo = id.ToString()
            };
            status = UResidence.AmenityController.Delete(am);
            if (status == true)
            {
                AmenityView();               
            }
            ViewBag.DeleteStatus = status;
            return View("AmenityView");

        }
        public ActionResult AmenityEdit()
        {
            return View();
        }
        [HttpGet]
        public ActionResult AmenityEdit(int? id)
        {
            if (ModelState.IsValid)
            {
                List<Amenity> amenityList = default( List<Amenity>);
                amenityList = UResidence.AmenityController.GetbyAmenityNo(id.ToString());            
                ViewBag.updateList = amenityList;
               
            }
            return View();
        }

        [HttpPost]
        public ActionResult AmenityEdit(FormCollection fc)
        {
            NameValueCollection values = fc;
            Amenity amen = Amenity.CreateObject(values);
            if (amen.Validate())
            {
                status = UResidence.AmenityController.Update(amen);
                if (status == true)
                {
                    AmenityView();
                }
                ViewBag.UpdateMessage = status;
                return View("AmenityView");
            }
            return View();
        }
    }
}
