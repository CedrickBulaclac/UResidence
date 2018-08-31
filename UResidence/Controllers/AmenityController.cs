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
        public ActionResult AmenityAdd(Amenity amen, HttpPostedFileBase image)
        {
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
            amn = UResidence.AmenityController.GetbyId(id);
            return View(amn);
        }


        //public JsonResult UpdateI(string url)
        //{
        //    string[] err = new string[] { };
        //    Amenity amenn = new Amenity
        //    {
        //        Url = url
        //    };
        //    if (amenn.Validate(out err))
        //    {
        //        status = UResidence.AmenityController.UpdateImage(amenn);
        //        if (status == true)
        //        {
        //            ViewBag.UpdateMessage = status;
        //        }
        //    }
        //    return new JsonResult
        //    {
        //        Data = status,
        //        JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    };

        //}

        public JsonResult UpdateImage(Amenity amenity)
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
                    Amenity a = new Amenity
                    {
                        Id = id,
                        Url = folderpath1
                    };


                    status = UResidence.AmenityController.UpdateImage(a);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        




        //[HttpPost]
        //public ActionResult Upload(Amenity amen, HttpPostedFileBase image,string url)
        //{
        //    string[] err = new string[] { };
        //    Amenity a = new Amenity()
        //    {
        //      Url = url
        //    };
        //    if (a.Validate(out err))
        //    {
        //        status = UResidence.AmenityController.UpdateImage(a);
        //        if (status == true)
        //        {
        //            ViewBag.UpdateMessage = status;
        //        }

        //    }
        //    else
        //    {

        //        ViewBag.ErrorMessages = FixMessages(err);
        //    }
        //    return View(amen);
        //}
        //for (int i = 0; i < Request.Files.Count; i++)
        //{
        //    var file = Request.Files;

        //    var fileName = Path.GetFileName(file.FileName);

        //    var path = Path.Combine(Server.MapPath("~/Content/AmenityImages/"), fileName);
        //    file.SaveAs(path);




        [HttpPost]
        public ActionResult AmenityEdit(Amenity amen, HttpPostedFileBase image)
        {
            string[] err = new string[] { };
          
            Amenity a = new Amenity()
            {
                Id = amen.Id,
                AmenityName = amen.AmenityName,
                Capacity = amen.Capacity,
                Description = amen.Description,
                Rate = amen.Rate,
                Color = amen.Color
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




