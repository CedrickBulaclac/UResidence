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
        public ActionResult Registration(FormCollection fc)
        {
            NameValueCollection values = (NameValueCollection)fc;
            Equipment eq = Equipment.CreateObject(values);
            if (eq.Validate() == true)
            {
                status = UResidence.EquipmentController.Insert(eq);
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

        public ActionResult EquipmentView()
        {
            List<Equipment> equipmentList = default(List<Equipment>);
            equipmentList = UResidence.EquipmentController.GetAll();
            ViewBag.equipment = equipmentList;
            return View();
        }


        public ActionResult Delete(int? eno)
        {
            Equipment eq = new Equipment
            {
                EquipmentNo = eno.ToString()
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
        public ActionResult EquipmentEdit(int? eno)
        {
            if (ModelState.IsValid)
            {
                List<Equipment> equipmentList = default(List<Equipment>);
                equipmentList = UResidence.EquipmentController.GetAll();
                ViewBag.equipment = equipmentList;
                return View();
            }
            return View("EquipmentView");
        }
        [HttpPost]
        public ActionResult EquipmentEdit(FormCollection fc)
        {
            NameValueCollection values = new NameValueCollection();
            Equipment eq = Equipment.CreateObject(values);
            if (eq.Validate() == true)
            {
                status = UResidence.EquipmentController.Update(eq);
                if (status == true)
                {
                    EquipmentView();
                }
                ViewBag.UpdateMessage = status;
            }
            return View("EquipmentView");
        }




    }
}