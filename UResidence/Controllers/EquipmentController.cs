﻿using System;
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
            string Name = fc["Name"];
            int Stocks = Convert.ToInt32(fc["Stocks"]);
            int Rate = Convert.ToInt32(fc["Rate"]);
            string eno = fc["EquipmentNo"];
            Equipment eq = new Equipment()
            {
                Name = Name,
                Stocks = Stocks,
                Rate = Rate,
                 EquipmentNo = eno
            };

            status = UResidence.EquipmentController.Insert(eq);
            if (status == true)
            {
                ViewBag.Message = true;
            }
            else
            {
                ViewBag.Message = false;
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

        [HttpPost]
        public ActionResult EquipmentView(int? eno)
        {
            Equipment eq = new Equipment
            {
                EquipmentNo = eno.ToString()
            };

            status = UResidence.EquipmentController.Delete(eq);
                if (status == true)
            {
                List<Equipment> equipmentList = default(List<Equipment>);
                equipmentList = UResidence.EquipmentController.GetAll();
                ViewBag.equipment = equipmentList;
            }
            return View();
        }


        }




        }


            

