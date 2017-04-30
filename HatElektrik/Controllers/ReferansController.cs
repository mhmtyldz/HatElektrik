using HatElektrik.Helper.CustomFilter;
using HatElektrik.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using HatElektrik.Models.Tablolar;
using HatElektrik.Helper.JsonMessage;
using System.IO;

namespace HatElektrik.Controllers
{
    public class ReferansController : Controller
    {
        HatElektrikContext db = new HatElektrikContext();

        #region Referans Listele
        [HttpGet]
        [LoginFilter]
        public ActionResult Index(int Sayfa = 1)
        {
            return View(db.Referans.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 10));
        }
        #endregion

        #region Referans Ekle
        [HttpGet]
        [LoginFilter]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [LoginFilter]
        public ActionResult Ekle(Referans referans, HttpPostedFileBase ResimURL)
        {
            if (referans.ResimURL != null)
            {
                if (ResimURL.ContentLength > 0)
                {
                    string Dosya = Guid.NewGuid().ToString().Replace("-", "");
                    string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string ResimYolu = "/External/Referans/" + Dosya + Uzanti;

                    ResimURL.SaveAs(Server.MapPath(ResimYolu));
                    referans.ResimURL = ResimYolu;
                }

                try
                {
                    db.Referans.Add(referans);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Referans");
                }
                catch (Exception)
                {

                    return View(referans);
                }

            }
            return View(referans);


        }

        #endregion



        #region Referans Sil
        [LoginFilter]
        public JsonResult Sil(Referans referans)
        {
            Referans dbReferans = db.Referans.Find(referans.ID);
            if (referans == null)
            {
                return Json(new ResultJson { Success = false, Message = "Referans Bulunamadı ! " });

            }
            try
            {
                if (dbReferans.ResimURL != null)
                {
                    string ResimURL = dbReferans.ResimURL;
                    string ResimPath = Server.MapPath(ResimURL);

                    FileInfo file = new FileInfo(ResimPath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                db.Referans.Remove(dbReferans);
                db.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "Referans Silme İşleminiz Başarılı ! " });
            }
            catch (Exception ex)
            {

                return Json(new ResultJson { Success = false, Message = "Referans Silme İşleminiz Başarısız ! " });
            }
        }
        #endregion


	}
}