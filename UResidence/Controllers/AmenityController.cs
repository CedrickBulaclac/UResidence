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
        public ActionResult GetAmenity()
        {
            List<Amenity> ret = new List<Amenity>();
            ret = UResidence.AmenityController.GetAll();
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        // GET: Amenity
        public ActionResult AmenityAdd()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
           
            return View();
        }      

        public JsonResult UpdateImage(Amenity amenity)
        {
           
            var image = amenity.Image;
            bool status = false;
            int id = amenity.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/AmenityImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/AmenityImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }

                    Amenity e = new Amenity
                    {
                        Id = id,
                        Url = finalpath
                    };


                    status = UResidence.AmenityController.UpdateImage(e);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


        [HttpPost]
        public ActionResult AmenityAdd(Amenity amen, HttpPostedFileBase Image)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string[] err = new string[] { };

            string finalpath = "";
            var image = Image;
            bool status = false;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), imagefileName);

                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/AmenityImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/AmenityImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);


                    }
                }
            }

      
                Amenity a = new Amenity()
                {
                    AmenityName = amen.AmenityName,
                    Url = finalpath,
                    Capacity = amen.Capacity,
                    Description = amen.Description,
                    Rate = amen.Rate,
                    Color = amen.Color,
                    Location = amen.Location,
                    EveRate = amen.EveRate,
                    IsEquipment = amen.IsEquipment,
                    IsWeekend = amen.IsWeekend,
                    Deleted = 0
                };
       
            if (amen.Validate(out err))
            {

                if (image != null)
                {
                    Amenity aa = new Amenity()
                    {
                        AmenityName = amen.AmenityName,
                        Url = finalpath,
                        Capacity = amen.Capacity,
                        Description = amen.Description,
                        Rate = amen.Rate,
                        Color = amen.Color,
                        Location = amen.Location,
                        EveRate = amen.EveRate,
                        IsEquipment = amen.IsEquipment,
                        IsWeekend = amen.IsWeekend,
                        Deleted = 0
                    };
                    status = UResidence.AmenityController.Insert(aa);
                }
                else
                {
                    Amenity aa = new Amenity()
                    {
                        AmenityName = amen.AmenityName,
                        Url = "~/Content/AmenityImages/noimage.jpeg",
                        Capacity = amen.Capacity,
                        Description = amen.Description,
                        Rate = amen.Rate,
                        Color = amen.Color,
                        Location = amen.Location,
                        EveRate = amen.EveRate,
                        IsEquipment = amen.IsEquipment,
                        IsWeekend = amen.IsWeekend,
                        Deleted = 0
                    };
                   status = UResidence.AmenityController.Insert(aa);
                }

                Session["AddMessage"] = status;
                return RedirectToAction("AmenityView", "Amenity");
            }
            else
            {
                status = false;
                ViewBag.ErrorMessages = FixMessages(err);
                Session["AddMessage"] = status;
                ViewBag.Alert = true;
                ViewBag.Kind = 2;
                return View("AmenityView");
            }           
        }

        public ActionResult AmenityView()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            if (Session["UpdateMess"] != null)
            {
                ViewBag.UpdateMessage = Session["UpdateMess"];
                Session["UpdateMess"] = null;
            }
            if (Session["AddMessage"] != null)
            {
                ViewBag.AddMessage = Session["AddMessage"];
                Session["AddMessage"] = null;
            }
            if (Session["DeleteStatus"] != null)
            {
                ViewBag.DeleteStatus = Session["DeleteStatus"];
                Session["DeleteStatus"] = null;
            }
            return View();
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
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            Amenity am = new Amenity()
            {
                Id = id
            };
            status = UResidence.AmenityController.UpdateDelete(id);                  
            Session["DeleteStatus"] = status;
            return RedirectToAction("AmenityView", "Amenity");
        }
        public ActionResult AmenityEdit()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            return View();
        }
        [HttpGet]
        public ActionResult AmenityView(int? id)
        {
            ViewBag.ModalView = 0;
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            if (Session["UpdateMess"] != null)
            {
                ViewBag.UpdateMessage = Session["UpdateMess"];
                Session["UpdateMess"] = null;
            }
            if (Session["AddMessage"] != null)
            {
                ViewBag.AddMessage = Session["AddMessage"];
                Session["AddMessage"] = null;
            }
            if (Session["DeleteStatus"] != null)
            {
                ViewBag.DeleteStatus = Session["DeleteStatus"];
                Session["DeleteStatus"] = null;
            }
            if (id == null)
            {
                return View();
            }
            else
            {
                Amenity amn = default(Amenity);
                amn = UResidence.AmenityController.GetbyId((int)id);
                ViewBag.ModalView = 1;
                return View(amn);
            }
        }

        public JsonResult InsertImage(ImageAmenity amenity)
        {
            var image = amenity.Image;
            string[] err = new string[] { };
            bool events = false;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/AmenityImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/AmenityImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }

                    ImageAmenity amn = new ImageAmenity
                    {
                        AmenityId = amenity.Id,
                        URL = finalpath
                    };
                    events = UResidence.ImageAmenityController.InsertImage(amn);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


        [HttpPost]
        public ActionResult AmenityView(Amenity amen, HttpPostedFileBase image)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string[] err = new string[] { };
            Amenity ret = new Amenity();
            ret = UResidence.AmenityController.GetbyId(amen.Id);
            if (amen.Rate==0)
            {
                if (amen.Adult != ret.Adult || amen.Child != ret.Child)
                {
                    status = UResidence.AmenityController.UpdateDelete(amen.Id);
                    status = UResidence.SwimmingRateController.UpdateDelete(amen.Id);

                    Amenity a = new Amenity()
                    {
                        AmenityName = amen.AmenityName,
                        Url = ret.Url,
                        Capacity = amen.Capacity,
                        Description = amen.Description,
                        Rate = amen.Rate,
                        Color = amen.Color,
                        Location = amen.Location,
                        EveRate = amen.EveRate,
                        IsEquipment = amen.IsEquipment,
                        IsWeekend = amen.IsWeekend,
                        Deleted = 0
                    };
                    status = UResidence.AmenityController.Insert(a);
                    Amenity aa = new Amenity();
                    aa = UResidence.AmenityController.GetId(amen.AmenityName);
                    SwimmingRate sr = new SwimmingRate()
                    {
                        AmenityId = aa.Id,
                        Adult = amen.Adult,
                        Child = amen.Child,
                        Deleted = 0
                    };
                    status = UResidence.SwimmingRateController.Insert(sr);
        
                    if (status == true)
                    {                     
                        Session["UpdateMess"] = status;
                        Session["ModalView"]= 3;
                        return RedirectToAction("AmenityView", "Amenity", new { id = "" });
                    }
                }
                else
                {
                    SwimmingRate sr = new SwimmingRate()
                    {
                        AmenityId = amen.Id,
                        Adult = amen.Adult,
                        Child = amen.Child,
                        Deleted = 0
                    };

                    Amenity a = new Amenity()
                    {
                        Id = amen.Id,
                        AmenityName = amen.AmenityName,
                        Capacity = amen.Capacity,
                        Description = amen.Description,
                        Rate = 0,
                        Color = amen.Color,
                        Location = amen.Location,
                        EveRate = amen.EveRate,
                        IsEquipment = amen.IsEquipment,
                        IsWeekend = amen.IsWeekend,
                        Deleted = 0
                    };
                    status = UResidence.SwimmingRateController.Update(sr);
                    status = UResidence.AmenityController.Update(a);
                    if (status == true)
                    {                     
                        Session["UpdateMess"] = status;
                        Session["ModalView"] = 3;
                        return RedirectToAction("AmenityView", "Amenity", new { id = "" });
                    }
                }
                                   
            }
            else
            {
                if (amen.Rate != ret.Rate || amen.EveRate != ret.EveRate)
                {
                    status = UResidence.AmenityController.UpdateDelete(amen.Id);
                    Amenity a = new Amenity()
                    {
                        AmenityName = amen.AmenityName,
                        Url = ret.Url,
                        Capacity = amen.Capacity,
                        Description = amen.Description,
                        Rate = amen.Rate,
                        Color = amen.Color,
                        Location = amen.Location,
                        EveRate = amen.EveRate,
                        IsEquipment = amen.IsEquipment,
                        IsWeekend = amen.IsWeekend,
                        Deleted = 0
                    };
                    status = UResidence.AmenityController.Insert(a);
                    if (status == true)
                    {                    
                        Session["UpdateMess"] = status;
                        Session["ModalView"] = 3;
                        return RedirectToAction("AmenityView", "Amenity",new {id=""});
                    }
                }
                else
                {
                    Amenity a = new Amenity()
                    {
                        Id = amen.Id,
                        AmenityName = amen.AmenityName,
                        Capacity = amen.Capacity,
                        Description = amen.Description,
                        Rate = amen.Rate,
                        Color = amen.Color,
                        Location = amen.Location,
                        EveRate = amen.EveRate,
                        IsEquipment = amen.IsEquipment,
                        IsWeekend = amen.IsWeekend,
                        Deleted = 0
                    };


                    status = UResidence.AmenityController.Update(a);
                    if (status == true)
                    {                       
                        Session["UpdateMess"] = status;
                        Session["ModalView"] = 3;
                        return RedirectToAction("AmenityView", "Amenity", new { id = "" });
                    }
                }

            }
            return View(amen);
        }


        public ActionResult AmenityAddPool() {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            return View();
        }

        [HttpPost]
        public ActionResult AmenityAddPool(Amenity amen, HttpPostedFileBase Image)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string[] err = new string[] { };
            int amenID;
            string finalpath = "";
            var image = Image;
            bool status = false;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), imagefileName);

                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/AmenityImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/AmenityImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/AmenityImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);


                    }
                }
            }


            Amenity a = new Amenity()
            {
                AmenityName = amen.AmenityName,
                Url = finalpath,
                Capacity = amen.Capacity,
                Description = amen.Description,
                Rate = 0,
                Color = amen.Color,
                Location = amen.Location,
                EveRate = 0,
                IsEquipment = amen.IsEquipment,
                IsWeekend = amen.IsWeekend,
                Deleted = 0
            };
          
            if (amen.Validate(out err))
            {
                if (image != null)
                {
                    Amenity aa = new Amenity()
                    {
                        AmenityName = amen.AmenityName,
                        Url = finalpath,
                        Capacity = amen.Capacity,
                        Description = amen.Description,
                        Rate = amen.Rate,
                        Color = amen.Color,
                        Location = amen.Location,
                        EveRate = amen.EveRate,
                        IsEquipment = amen.IsEquipment,
                        IsWeekend = amen.IsWeekend,
                        Deleted = 0
                    };
                   status = UResidence.AmenityController.Insert(aa);
                     amenID = amen.Id;
                    string amenityname = amen.AmenityName;
                    Amenity amm = UResidence.AmenityController.GetbyAmenityName(amenityname);
                    SwimmingRate sr = new SwimmingRate()
                    {
                        AmenityId = amm.Id,
                        Adult = amen.Adult,
                        Child = amen.Child,
                        Deleted = 0
                    };
                   status = UResidence.SwimmingRateController.Insert(sr);                                   
                    Session["AddMessage"] = status;
                    return RedirectToAction("AmenityView", "Amenity");
                }

                else
                {
                    Amenity aa = new Amenity()
                    {
                        AmenityName = amen.AmenityName,
                        Url = "~/Content/AmenityImages/noimage.jpeg",
                        Capacity = amen.Capacity,
                        Description = amen.Description,
                        Rate = amen.Rate,
                        Color = amen.Color,
                        Location = amen.Location,
                        EveRate = amen.EveRate,
                        IsEquipment = amen.IsEquipment,
                        IsWeekend = amen.IsWeekend,
                        Deleted = 0
                    };
                    status = UResidence.AmenityController.Insert(aa);
                    amenID = amen.Id;
                    string amenityname = amen.AmenityName;
                    Amenity amm = UResidence.AmenityController.GetbyAmenityName(amenityname);
                    SwimmingRate sr = new SwimmingRate()
                    {
                        AmenityId = amm.Id,
                        Adult = amen.Adult,
                        Child = amen.Child,
                        Deleted=0
                    };
                    status = UResidence.SwimmingRateController.Insert(sr);                                                     
                }                                       
            }
            else
            {
                status = false;
                ViewBag.Alert = true;
                ViewBag.Kind = 2;
                return View("AmenityView");
            }
            Session["AddMessage"] = status;
            return RedirectToAction("AmenityView", "Amenity");
        }
        public string FixMessages(string[] err)
        {
            string errors = "Please check the following: <br />";
            foreach (string er in err) errors += (er + "<br />");
            return errors;
        }

    }
}




