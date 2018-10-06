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

        // GET: Equipment
        public ActionResult Registration()
        {
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
                    Description = eqp.Description
                };

                if (eqp1.Validate(out err))
                {
                    UResidence.EquipmentController.Insert(eqp1);
                    status = true;
                    ViewBag.AddMessage = status;
                    EquipmentView();
                    return View("EquipmentView");
                }
                else
                {
                    ViewBag.ErrorMessage = FixMessages(err);
                    ViewBag.Message = false;
                    return View(eqp);
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
                    Description = eqp.Description
                };

                if (eqp1.Validate(out err))
                {
                    UResidence.EquipmentController.Insert(eqp1);
                    status = true;
                    ViewBag.AddMessage = status;
                    EquipmentView();
                    return View("EquipmentView");
                }
                else
                {
                    ViewBag.ErrorMessage = FixMessages(err);
                    ViewBag.Message = false;
                    return View(eqp);
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



        public ActionResult EquipmentView()
        {
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Equipment> equipmentList = UResidence.EquipmentController.GetAll();
            return View(equipmentList);
        }


        public ActionResult Delete(int eno)
        {
            Equipment eq = new Equipment
            {
                Id = eno
            };
            status = UResidence.EquipmentController.Delete(eq);
            if(status == true)
            {
                EquipmentView();
            }
            ViewBag.DeleteStatus = true;
            return View("EquipmentView");
        }
        
        public ActionResult EquipmentEdit()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EquipmentEdit(int eno)
        {
            int level = Convert.ToInt32(Session["Level"]);

            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            //if (ModelState.IsValid)
            //{
            Equipment equipmentList = default(Equipment);
                equipmentList = UResidence.EquipmentController.GetbyId(eno);
                return View(equipmentList);
            //}
            //return View("EquipmentView");
        }
        [HttpPost]
        public ActionResult EquipmentEdit(Equipment eqp)
        {
            string[] err = new string[] { };
          
                    Equipment eqp1 = new Equipment()
                    {
                       Id=eqp.Id,
                        Name = eqp.Name,              
                        Stocks = eqp.Stocks,
                        Rate = eqp.Rate,
                        Description = eqp.Description
                    };
                    if (eqp1.Validate(out err))
                    {
                        status = UResidence.EquipmentController.Update(eqp1);
                        if (status == true)
                        {
                            ViewBag.UpdateMessage = status;
                            EquipmentView();
                            return View("EquipmentView");
                        }
                        else
                        {
                            ViewBag.UpdateMessage = status;
                        }
                       ViewBag.ErrorMessages = FixMessages(err);
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