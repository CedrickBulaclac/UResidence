using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class EquipmentController : Controller
    {
        private bool status;

        // GET: Equipment
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Equipment eqp)
        {
            string[] err = new string[] { };
            if (eqp.Validate(out err))
            {
                UResidence.EquipmentController.Insert(eqp);
                ViewBag.Message = true;
            }
            else
            {
                ViewBag.ErrorMessage = FixMessages(err);
                ViewBag.Message = false;
            }
            return View();
        }

        public ActionResult EquipmentView()
        {
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
        public ActionResult EquipmentEdit(int id)
        {
            if (ModelState.IsValid)
            {
               
                Equipment equipmentList = UResidence.EquipmentController.GetbyId(id);
                return View(equipmentList);
            }
            return View("EquipmentView");
        }
        [HttpPost]
        public ActionResult EquipmentEdit(Equipment eqp)
        {
            string[] err = new string[] { };
            if (eqp.Validate(out err))
            {
                status = UResidence.EquipmentController.Update(eqp);
                if (status == true)
                {
                    return RedirectToAction("AdminView");
                }
            }
            else
            {
                ViewBag.ErrorMessages = FixMessages(err);
            }
            ViewBag.UpdateMessage = true;
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