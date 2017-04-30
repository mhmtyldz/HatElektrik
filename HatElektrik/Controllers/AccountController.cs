using HatElektrik.Helper.CustomFilter;
using HatElektrik.Models.DataContext;
using HatElektrik.Models.Tablolar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HatElektrik.Controllers
{
    public class AccountController : Controller
    {
        HatElektrikContext db = new HatElektrikContext();

        [LoginFilter]
        public ActionResult Admin()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var login = db.Kullanici.Where(u => u.Email == kullanici.Email && u.Sifre==kullanici.Sifre).SingleOrDefault();
            if (login!=null)
            {
                if (login.Email == kullanici.Email && login.Sifre == kullanici.Sifre)
                {
                    Session["uyeid"] = login.ID;
                    Session["email"] = login.Email;
                    return RedirectToAction("Admin", "Account");
                }
                else
                {
                    TempData["Bilgi"] = "Yanlış Kullanıcı Adı Yada Şifre Girdiniz";
                    return View();
                }
            }
            else
            {
                TempData["Bilgi"] = "Böyle Bir Kullanici Bulunamadı";
                return View();
            }
            
        }

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }


	}
}