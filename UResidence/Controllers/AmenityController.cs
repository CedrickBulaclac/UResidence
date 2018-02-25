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
        public ActionResult AmenityAdd(Amenity amen)
        {
            string[] err = new string[] { };
            if (amen.Validate(out err))
            {
                ViewBag.Message = UResidence.AmenityController.Insert(amen);
            }
            else
            {
                ViewBag.Message = false;
                ViewBag.ErrorMessages=FixMessages(err);
            }
            return View(amen);
        }

        public ActionResult AmenityView()
        {
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);
        }

       
        public ActionResult Delete(int id)
        {

            Amenity am = new Amenity()
            {
                Id = id
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
        public ActionResult AmenityEdit(int id)
        {

            Amenity amn = default(Amenity);
               amn= UResidence.AmenityController.GetbyId(id);
      
            return View(amn);
        }

        [HttpPost]
        public ActionResult AmenityEdit(Amenity amen)
        {
            string[] err = new string[]{ };
            if (amen.Validate(out err))
            {
                status = UResidence.AmenityController.Update(amen);
                if (status == true)
                {
                    ViewBag.UpdateMessage = status;
                    return RedirectToAction("AmenityView");
                }
            }
            else
            {
               
                ViewBag.ErrorMessages = FixMessages(err);
            }

            return View(amen);
        }

        public string FixMessages(string[] err)
        {
            string errors = "Please check the following: <br />";
            foreach (string er in err) errors += (er + "<br />");
            return errors;
        }
    }
}
