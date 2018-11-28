using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
namespace UResidence.Controllers
{
    public class EquipmentController : Controller
    {
        private bool status;
        public ActionResult GetEquipment()
        {
            List<Equipment> ret = new List<Equipment>();
            ret = UResidence.EquipmentController.GetAll();
            return Json(new { data = ret }, JsonRequestBehavior.AllowGet);
        }
        // GET: Equipment
        public ActionResult Registration()
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

        [HttpPost]
        public ActionResult Registration(Equipment eqp, HttpPostedFileBase Image)
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
                    string folderPath = Path.Combine(Server.MapPath("~/Content/EquipmentImages"), imagefileName);

                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/EquipmentImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/EquipmentImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/EquipmentImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);


                    }
                }
            }



            if (image != null)
            {
                Equipment eqp1 = new Equipment()
                {
                    Name = eqp.Name,
                    Stocks = eqp.Stocks,
                    Rate = eqp.Rate,
                    Url = finalpath,
                    Description = eqp.Description,
                    Deleted=0
                };

                if (eqp1.Validate(out err))
                {
                    UResidence.EquipmentController.Insert(eqp1);
                    status = true;
                    ViewBag.AddMessage = status;
                    Session["AddMessage"] = status;
                    return RedirectToAction("EquipmentView", "Equipment");
                }
                else
                {
                    ViewBag.ErrorMessage = FixMessages(err);
                    ViewBag.Message = false;
                    ViewBag.Alert = true;
                    return View("EquipmentView");
                }
            }
            else
            {
                Equipment eqp1 = new Equipment()
                {
                    Name = eqp.Name,
                    Stocks = eqp.Stocks,
                    Rate = eqp.Rate,
                    Url = "~/Content/EquipmentImages/Noimageavailable.jpeg",
                    Description = eqp.Description,
                    Deleted=0
                };

                if (eqp1.Validate(out err))
                {
                    UResidence.EquipmentController.Insert(eqp1);
                    status = true;
                    ViewBag.AddMessage = status;
                    Session["AddMessage"] = status;
                    return RedirectToAction("EquipmentView", "Equipment");
                }
                else
                {
                    ViewBag.ErrorMessage = FixMessages(err);
                    ViewBag.Message = false;
                    ViewBag.Alert = true;
                    return View("EquipmentView");
                }
            }
                
        }


        public JsonResult UpdateImage(Equipment eqp)
        {
            var image = eqp.Image;
            bool status = false;
            int id = eqp.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/EquipmentImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/EquipmentImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/EquipmentImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/EquipmentImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }

                    Equipment e = new Equipment
                    {
                        Id = id,
                        Url = finalpath
                    };


                    status = UResidence.EquipmentController.UpdateImage(e);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


        [HttpGet]
        public ActionResult EquipmentView(int? id)
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
            if (id == null)
            {
                return View();
            }
            else
            {
                Equipment equipmentList = default(Equipment);
                equipmentList = UResidence.EquipmentController.GetbyId((int)id);
                ViewBag.ModalView = 1;
                return View(equipmentList);
            }           
        }


        public ActionResult Delete(int id)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            Equipment eq = new Equipment
            {
                Id = id
            };
            status = UResidence.EquipmentController.UpdateDelete(id);
            ViewBag.DeleteStatus = status;
            Session["DeleteStatus"] = status;
            return RedirectToAction("EquipmentView", "Equipment");
        }
        
        public ActionResult EquipmentEdit()
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            return View();
        }
 
        [HttpPost]
        public ActionResult EquipmentView(Equipment eqp)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            string[] err = new string[] { };

           
           Equipment ret = new Equipment();
            ret = UResidence.EquipmentController.GetbyId(eqp.Id);

            if (eqp.Rate != ret.Rate)
            {
                status = UResidence.EquipmentController.UpdateDelete(eqp.Id);

                Equipment eqpp = new Equipment()
                {
                    Name = eqp.Name,
                    Stocks = eqp.Stocks,
                    Rate = eqp.Rate,
                    Url = ret.Url,
                    Description = eqp.Description,
                    Deleted = 0
                };
                UResidence.EquipmentController.Insert(eqpp);

                if (status == true)
                {
                    ViewBag.UpdateMessage = status;
                    Session["UpdateMess"] = status;
                    return RedirectToAction("EquipmentView", "Equipment",new { id=""});
                }
                else
                {
                    ViewBag.UpdateMessage = status;
                }
                ViewBag.ErrorMessages = FixMessages(err);
            }
            else
            {
                Equipment eqp1 = new Equipment()
                {
                    Id = eqp.Id,
                    Name = eqp.Name,
                    Stocks = eqp.Stocks,
                    Rate = eqp.Rate,
                    Description = eqp.Description,
                    Deleted = 0
                };
                if (eqp1.Validate(out err))
                {
                    status = UResidence.EquipmentController.Update(eqp1);
                    if (status == true)
                    {
                        ViewBag.UpdateMessage = status;
                        Session["UpdateMess"] = status;
                        return RedirectToAction("EquipmentView", "Equipment", new { id = "" });
                    }
                    else
                    {
                        ViewBag.UpdateMessage = status;
                    }
                    ViewBag.ErrorMessages = FixMessages(err);
                }
            }
            return View(eqp);
        }
            
                        
        public string FixMessages(string[] err)
        {
            string errors = "Please check the following: <br/>";
            foreach (string er in err) errors += (er + "<br/>");
            return errors;
        }


    }
}