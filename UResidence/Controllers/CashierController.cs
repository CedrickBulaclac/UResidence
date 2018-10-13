using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UResidence.Controllers
{
    public class CashierController : Controller
    {
        // GET: Cashier
        public ActionResult Calendar()
        {
            int level = Convert.ToInt32(Session["Level"]);
            if (level <= 7)
            {
                Admin a = new Admin();
                a = UResidence.AdminController.GetIdAdmin(Session["UID"].ToString());
                Session["URLL"] = a.URL;
            }
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);
        }
        public JsonResult GetEvents()
        {
            List<ReservationProcess> reservationList = ReservationProcessController.GET_ALL();
            var events = reservationList.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
          
        }
        private bool Try(Receipt receipt)
        {
            bool status = false;
            status = ReceiptController.Insert(receipt);
            if (status == true)
            {

                Reservation reservation = new Reservation
                {
                    Status = "Reserved",
                    Id = receipt.RefNo,
                };
                status = ReservationController.Update(reservation);
            }
            return status;
        }
        public JsonResult InsertPayment(Receipt receipt)
        {
            if (receipt.Description != null && receipt.Description != "")
            {
                List<EquipReservation> er = new List<EquipReservation>();
                er = EquipReservationController.Getr(receipt.RefNo);
                if (er.Count != 0)
                {
                    List<Stocks> stok = new List<Stocks>();
                    stok = StockController.GetStocks(receipt.st, receipt.et);
                    string[] label = new string[stok.Count];
                    if (stok.Count != 0)
                    {
                        bool status = false;
                        for (int i = 0; i <= er.Count - 1; i++)
                        {
                            int stock = stok[i].EStocks - er[i].Quantity;
                            if (stock < 0)
                            {
                                label[i] = "false";
                            }
                            else
                            {
                                label[i] = "true";
                            }

                        }
                        if (label.Contains("false"))
                        {
                            Reservation reservation = new Reservation
                            {
                                Status = "Cancelled",
                                Id = receipt.RefNo,
                            };
                            status = ReservationController.Update(reservation);
                            string stat = "a";
                            return new JsonResult { Data = stat, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                        else
                        {

                            Amenity amen = new Amenity();
                            amen = UResidence.AmenityController.GetbyAmenityName(receipt.amen);
                            List<SchedReservation> schedList = UResidence.SchedReservationController.GetAllC(receipt.st, receipt.et, amen.Id);
                            if (receipt.status == "Reserved")
                            {
                                status = Try(receipt);
                                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                            }
                            else
                            {
                                if (amen.AmenityName.ToUpper().Contains("SWIMMING"))
                                {
                                    status = Try(receipt);
                                    return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                }
                                if (schedList.Count == 0)
                                {
                                    status = Try(receipt);
                                    return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                                }
                                else
                                {
                                    Reservation reservation = new Reservation
                                    {
                                        Status = "Cancelled",
                                        Id = receipt.RefNo,
                                    };
                                    status = ReservationController.Update(reservation);
                                    string b = "failed";
                                    return new JsonResult { Data = b };
                                }
                            }
                        }
                    }
                    else
                    {
                        if (receipt.status == "Reserved")
                        {
                            bool status = Try(receipt);
                            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                        else
                        {
                            Amenity amen = new Amenity();
                            amen = UResidence.AmenityController.GetbyAmenityName(receipt.amen);
                            List<SchedReservation> schedList = UResidence.SchedReservationController.GetAllC(receipt.st, receipt.et, amen.Id);
                            if (amen.AmenityName.ToUpper().Contains("SWIMMING"))
                            {
                                bool status = Try(receipt);
                                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                            }
                            if (schedList.Count == 0)
                            {
                                bool status = false;
                                status = Try(receipt);
                                return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                            }
                            else
                            {
                                Reservation reservation = new Reservation
                                {
                                    Status = "Cancelled",
                                    Id = receipt.RefNo,
                                };
                                bool status = ReservationController.Update(reservation);
                                string b = "failed";
                                return new JsonResult { Data = b };
                            }
                        }
                    }
                }
                else
                {
                    if (receipt.status == "Reserved")
                    {
                        bool status = false;
                        status = Try(receipt);
                        return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                    }
                    else
                    {
                        Amenity amen = new Amenity();
                        amen = UResidence.AmenityController.GetbyAmenityName(receipt.amen);
                        List<SchedReservation> schedList = UResidence.SchedReservationController.GetAllC(receipt.st, receipt.et, amen.Id);
                        if (schedList.Count == 0)
                        {
                            bool status = false;
                            status = Try(receipt);
                            return new JsonResult { Data = status, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                        }
                        else
                        {

                            Reservation reservation = new Reservation
                            {
                                Status = "Cancelled",
                                Id = receipt.RefNo,
                            };
                            bool status = ReservationController.Update(reservation);
                            string b = "failed";
                            return new JsonResult { Data = b };
                        }

                    }
                }
            }
            else
            {
                bool status = false;
                return new JsonResult { Data = status };
            }
        }
        public JsonResult ReceiptHistory(int refno1)
        {
            List<Receipt> receipt = ReceiptController.GetAll(refno1);
            var events = receipt.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}