using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
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
        public ActionResult AmenityAdd(Amenity amen,HttpPostedFileBase image)
        {
            string[] err = new string[] { };
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), imagefileName);
                    string folderpath = "~/Content/AmenityImages/" + imagefileName;
                    image.SaveAs(folderPath);
                    Amenity a = new Amenity()
                    {
                        AmenityName = amen.AmenityName,
                        Url = folderpath,
                        Capacity = amen.Capacity,
                        Description = amen.Description,
                        Rate = amen.Rate,
                        Color = amen.Color
                    };

                    if (amen.Validate(out err))
                    {

                        ViewBag.Message = UResidence.AmenityController.Insert(a);
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = false;
                        ViewBag.ErrorMessages = FixMessages(err);
                        return View(amen);
                    }
                }

            }

            else
            {
                string script = "<script type = 'text/javascript'>alert('No picture attached');</script>";
                Response.Write(script);
                return View();
            }
            return View();
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
        public ActionResult AmenityEdit(Amenity amen,HttpPostedFileBase image)
        {
            string[] err = new string[]{ };
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), imagefileName);
                    string folderpath = "~/Content/AmenityImages/" + imagefileName;
                    image.SaveAs(folderPath);
                    Amenity a = new Amenity()
                    {
                        Id=amen.Id,
                        AmenityName = amen.AmenityName,
                        Url = folderpath,
                        Capacity = amen.Capacity,
                        Description = amen.Description,
                        Rate = amen.Rate,
                        Color=amen.Color
                    };
                    if (a.Validate(out err))
                    {
                        status = UResidence.AmenityController.Update(a);
                        if (status == true)
                        {
                            ViewBag.UpdateMessage = status;
                            AmenityView();
                            return View("AmenityView");
                        }
                    }
                    return View();
                }
                else
                {

                    ViewBag.ErrorMessages = FixMessages(err);
                }
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
