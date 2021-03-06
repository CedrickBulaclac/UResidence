﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace UResidence.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            List<UserLogin> userList = default(List<UserLogin>);
            string chash;
            string hash= fc["Hash"];
            string username;
            username = fc["Username"];
            chash = Hash(hash);
            userList = UResidence.UserController.Get(username,chash);
            if(userList.Count==0)
            {
                return RedirectToAction("Index","Home");            
            }
            else
            {
                ViewBag.Message = "Welcome";
            }
            return View();
        }
        public string Hash(string p)
        {
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            string hash = BitConverter.ToString(sh.ComputeHash(utf8.GetBytes(p.ToString())));
            return hash;
        }

    }
}