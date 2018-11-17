using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace UResidence.Controllers
{
    public class ReservationAController : Controller
    {
       

        public ActionResult SelectOT()
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
                ViewBag.ReservationModule = a.ReservationModule;
                ViewBag.RegistrationModule = a.RegistrationModule;
                ViewBag.LogBookModule = a.LogBookModule;
                ViewBag.PaymentModule = a.PaymentModule;
                ViewBag.ReversalModule = a.ReversalModule;
                Session["ReservationModule"] = ViewBag.ReservationModule;
                Session["RegistrationModule"] = ViewBag.RegistrationModule;
                Session["LogBookModule"] = ViewBag.LogBookModule;
                Session["PaymentModule"] = ViewBag.PaymentModule;
                Session["ReversalModule"] = ViewBag.ReversalModule;
            }
           
            List<Amenity> amenityList = UResidence.AmenityController.GetAll();
            return View(amenityList);
        }

        [HttpPost]
        public ActionResult SelectOT(string bldgno, string unitno, int level)
        {
            if (Session["Level"] == null)
            {
                return Redirect("~/Login");
            }
            if (level == 4)
            {
                try
                {
                    Owner ownerList = UResidence.OwnerController.GetOwnerReservee(bldgno, unitno);
                    string fname = ownerList.Fname;
                    string mname = ownerList.Mname;
                    string lname = ownerList.Lname;
                    string fullname = fname + ' ' + mname + ' ' + lname;
                    Session["FULLN"] = fullname;
                    Session["TORA"] = "Owner";
                    Session["UIDA"] = ownerList.Id;
                   


                    decimal balance = 0;
                    List<ReservationList> revlist = new List<ReservationList>();
                    int uid = Convert.ToInt32(Session["UIDA"]);
                    string type = (Session["TORA"]).ToString();
                    if (type == "Owner")
                    {
                        revlist = UResidence.ReservationListController.GetAllO(uid);
                    }
                    else
                    {
                        revlist = UResidence.ReservationListController.GetAllT(uid);
                    }
                    if (revlist.Count > 0)
                    {
                        for (int i = 0; i <= revlist.Count - 1; i++)
                        {
                            balance += revlist[i].Outstanding;
                        }
                    }
                        if (balance > 0)
                    {
                        Session["status"] = true;                 
                        decimal data;
                        return Json(data = balance );

                    }
                    else
                    {
                        bool data;
                        return Json(data = true);
                    }




                           
                }
                catch (InvalidOperationException)
                {
                    string data;
                    return Json(data = "Owner");
                }



            }
            else if (level == 5)
            {


                try
                {
                    Tenant tenantList = UResidence.TenantController.GetTenantReserve(bldgno, unitno);
                    string fname = tenantList.Fname;
                    string mname = tenantList.Mname;
                    string lname = tenantList.Lname;
                    string fullname = fname + ' ' + mname + ' ' + lname;
                    Session["FULLN"] = fullname;
                    Session["TORA"] = "Tenant";
                    Session["UIDA"] = tenantList.Id;


                    Tenant a = new Tenant();
                    a = UResidence.TenantController.GetIdTenant(tenantList.Id.ToString());
                    if (a.LeaseEnd < DateTime.Now)
                    {
                        Tenant t = new Tenant
                        {
                            Deleted = "1",
                            Id = tenantList.Id
                        };
                        UResidence.TenantController.UpdateDelete(t);
                        string data;
                        return Json(data = "Tenant");
                    }

                    else
                    { 
                    decimal balance = 0;
                        List<ReservationList> revlist = new List<ReservationList>();
                        int uid = Convert.ToInt32(Session["UIDA"]);
                    string type = (Session["TORA"]).ToString();
                        if (type == "Owner")
                        {
                            revlist = UResidence.ReservationListController.GetAllO(uid);
                        }
                        else
                        {
                            revlist = UResidence.ReservationListController.GetAllT(uid);
                        }
                        if (revlist.Count > 0)
                        {
                            for (int i = 0; i <= revlist.Count - 1; i++)
                            {
                                balance += revlist[i].Outstanding;
                            }
                        }
                        if (balance > 0)
                    {
                        Session["status"] = true;
                        //return Content("<script language='javascript' type='text/javascript'>alert('There is still remaining balance of: " + balance + "');</script>");
                        decimal data;
                        return Json(data = balance);
                    }
                    else
                    {
                        bool data;
                        return Json(data = true);
                    }
                }
                }
                catch (InvalidOperationException)
                {
                    string data;
                    return Json(data = "Tenant");
                }


            }
            return View();
        }

     

        public JsonResult UpdateImage(Admin adm)
        {
            var image = adm.Image;
            bool status = false;
            int id = adm.Id;
            if (image != null)
            {
                if (image.ContentLength > 0)
                {
                    var fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    var extension = Path.GetExtension(image.FileName);
                    string imagefileName = Path.GetFileName(image.FileName);
                    string folderPath = Path.Combine(Server.MapPath("~/Content/ReservationAImages"), imagefileName);
                    string finalpath = "";
                    if (System.IO.File.Exists(folderPath))
                    {
                        //System.IO.File.Delete(folderPath);
                        for (int i = 1; System.IO.File.Exists(folderPath); i++)
                        {
                            folderPath = Path.Combine(Server.MapPath("~/Content/ReservationAImages"), fileName + "_" + i.ToString() + extension);
                            string folderpath1 = "~/Content/ReservationAImages/" + fileName + "_" + i.ToString() + extension;
                            finalpath = folderpath1;
                        }
                        image.SaveAs(folderPath);
                    }
                    else
                    {
                        string folderpath1 = "~/Content/ReservationAImages/" + fileName + extension;
                        finalpath = folderpath1;
                        image.SaveAs(folderPath);
                    }

                    Admin a = new Admin
                    {
                        Id = id,
                        URL = finalpath
                    };


                    status = UResidence.AdminController.UpdateDP(a);
                }

            }
            return new JsonResult
            {
                Data = status,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }


    }
}