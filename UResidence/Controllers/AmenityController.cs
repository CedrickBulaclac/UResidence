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

        public JsonResult ViewImage(ImageAmenity am)
        {
            List<ImageAmenity> amenList = default(List<ImageAmenity>);
            amenList=UResidence.ImageAmenityController.GetPicByAId(am.Id);
            var events=amenList.ToList();
            return new JsonResult {
                Data=events,
                JsonRequestBehavior=JsonRequestBehavior.AllowGet
            };
        }


        [HttpPost]
        public ActionResult AmenityAdd(Amenity amen)
        {
          
            string[] err = new string[] { };
              
                    string folderpath = "~/Content/AmenityImages/noimage.jpeg";
                
                    Amenity a = new Amenity()
                    {
                        AmenityName = amen.AmenityName,
                        Url = folderpath,
                        Capacity = amen.Capacity,
                        Description = amen.Description,
                        Rate = amen.Rate,
                        Color = amen.Color,
                        Location = amen.Location
                    };

            if (amen.Validate(out err))
            {

                ViewBag.Message = UResidence.AmenityController.Insert(a);
                status = true;
                ViewBag.AddMessage = status;
                AmenityView();
                return View("AmenityView");
            }
            else
            {
                ViewBag.Message = false;
                ViewBag.ErrorMessages = FixMessages(err);
                return View(amen);
            }           
        }

        public ActionResult AmenityView()
        {
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);
        }
        public JsonResult DeleteImage(ImageAmenity ia)
        {
            bool status = false;
            status=ImageAmenityController.Delete(ia.Id);
            string folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), ia.URL);
            string folderpath = "~/Content/AmenityImages/" + ia.URL;
            if (status==true)
            {
                if (System.IO.File.Exists(folderPath))
                {
                    System.IO.File.Delete(folderPath);
                }
            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult ViewAmenity()
        {
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            var events = amenityList.ToList();
            return new JsonResult
            {
                Data=events,
                JsonRequestBehavior=JsonRequestBehavior.AllowGet
            };
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
            amn = UResidence.AmenityController.GetbyId(id);
            return View(amn);
        }


       
        public JsonResult InsertImage(ImageAmenity amenity)
        {
            bool events=false;
            var image = amenity.Image;
            string[] err = new string[] { };
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), imagefileName);
                    string folderpath = "~/Content/AmenityImages/" + imagefileName;
                    if (System.IO.File.Exists(folderPath))
                    {
                        System.IO.File.Delete(folderPath);
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        image.SaveAs(folderPath);
                    }
                    ImageAmenity amn = new ImageAmenity
                    {
                        AmenityId = amenity.Id,
                        URL = folderpath
                    };
                    events = UResidence.ImageAmenityController.InsertImage(amn);
                }
            }

            return new JsonResult
            {
                Data = events,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult UpdateImage(ImageAmenity amenity)
        {
            var image = amenity.Image;
            bool status = false;
            int id = amenity.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), imagefileName);
                    string folderpath1 = "~/Content/AmenityImages/" + imagefileName;
                    if (System.IO.File.Exists(folderPath))
                    {
                        System.IO.File.Delete(folderPath);
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        image.SaveAs(folderPath);
                    }
                    ImageAmenity a = new ImageAmenity
                    {
                        Id = id,
                        URL = folderpath1
                    };


                    status = UResidence.ImageAmenityController.UpdateImage(a);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        

        [HttpPost]
        public ActionResult AmenityEdit(Amenity amen, HttpPostedFileBase image)
        {
            string[] err = new string[] { };
            if (amen.AmenityName.ToUpper() == "SWIMMING POOL")
            {

                SwimmingRate sr = new SwimmingRate()
                {
                    AmenityId = amen.Id,
                    Adult = amen.Adult,
                    Child = amen.Child
                };

                Amenity a = new Amenity()
                {
                    Id = amen.Id,
                    AmenityName = amen.AmenityName,
                    Capacity = amen.Capacity,
                    Description = amen.Description,
                    Rate = 0,
                    Color = amen.Color,
                    Location= amen.Location
                };
                if (a.Validate(out err))
                {
                    status = UResidence.SwimmingRateController.Update(sr);
                    status = UResidence.AmenityController.Update(a);
                    if (status == true)
                    {
                        ViewBag.UpdateMessage = status;
                        AmenityView();
                        return View("AmenityView");
                    }

                }
                else
                {

                    ViewBag.ErrorMessages = FixMessages(err);
                }
            }
            else {
                Amenity a = new Amenity()
                {
                    Id = amen.Id,
                    AmenityName = amen.AmenityName,
                    Capacity = amen.Capacity,
                    Description = amen.Description,
                    Rate = amen.Rate,
                    Color = amen.Color,
                    Location = amen.Location
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
                else
                {

                    ViewBag.ErrorMessages = FixMessages(err);
                }
            }

            return View(amen);
        }


        public ActionResult AmenityAddPool() {
            return View();
        }

        [HttpPost]
        public ActionResult AmenityAddPool(Amenity amen)
        {
            string[] err = new string[] { };

            string folderpath = "~/Content/AmenityImages/noimage.jpeg";


            Amenity a = new Amenity()
            {
                AmenityName = amen.AmenityName,
                Url = folderpath,
                Capacity = amen.Capacity,
                Description = amen.Description,
                Rate = 0,
                Color = amen.Color,
                Location = amen.Location
            };

            if (amen.Validate(out err))
            {

                ViewBag.Message = UResidence.AmenityController.Insert(a);
                string amenityname = amen.AmenityName;
                Amenity amm = UResidence.AmenityController.GetbyAmenityName(amenityname);
                SwimmingRate sr = new SwimmingRate()
                {
                    AmenityId = amm.Id,
                    Adult=amen.Adult,
                    Child =amen.Child
                };
                ViewBag.Message = UResidence.SwimmingRateController.Insert(sr);
                status = true;
                ViewBag.AddMessage = status;
                AmenityView();
                return View("AmenityView");
            }
            else
            {
                ViewBag.Message = false;
                ViewBag.ErrorMessages = FixMessages(err);
                return View("AmenityAddPool");
            }
           
        }

        public string FixMessages(string[] err)
        {
            string errors = "Please check the following: <br />";
            foreach (string er in err) errors += (er + "<br />");
            return errors;
        }

    }
}




