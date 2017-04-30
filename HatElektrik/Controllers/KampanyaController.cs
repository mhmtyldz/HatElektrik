using HatElektrik.Helper.CustomFilter;
using HatElektrik.Models.DataContext;
using HatElektrik.Models.Tablolar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using System.Net;
using System.IO;
using HatElektrik.Helper.JsonMessage;

namespace HatElektrik.Controllers
{
    public class KampanyaController : Controller
    {
        HatElektrikContext db = new HatElektrikContext();

        #region Kampanya Listele
        [HttpGet]
        [LoginFilter]
        public ActionResult Index(int Sayfa = 1)
        {
            return View(db.Kampanya.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 10));
        }
        #endregion

        #region Kampanya Ekle
        [HttpGet]
        [LoginFilter]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [LoginFilter]
        public ActionResult Ekle(Kampanya kampanya, HttpPostedFileBase ResimURL)
        {
            if (kampanya.ResimURL != null)
            {
                if (ResimURL.ContentLength > 0)
                {
                    string Dosya = Guid.NewGuid().ToString().Replace("-", "");
                    string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string ResimYolu = "/External/Kampanya/" + Dosya + Uzanti;

                    ResimURL.SaveAs(Server.MapPath(ResimYolu));
                    kampanya.ResimURL = ResimYolu;
                }

                try
                {
                    db.Kampanya.Add(kampanya);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Kampanya");
                }
                catch (Exception)
                {

                    return View(kampanya);
                }

            }
            return View(kampanya);


        }

        #endregion

        #region Kampanya Düzenle
        [HttpGet]
        [LoginFilter]
        public ActionResult Duzenle(int id)
        {
            Kampanya dbKampanya = db.Kampanya.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            if (dbKampanya == null)
            {
                return HttpNotFound();

            }
            return View(dbKampanya);
        }

        [HttpPost]
        [LoginFilter]
        public ActionResult Duzenle(Kampanya kampanya, HttpPostedFileBase ResimURL)
        {
            Kampanya dbKampanya = db.Kampanya.Find(kampanya.ID);
            dbKampanya.Baslik = kampanya.Baslik;
            dbKampanya.KisaAciklama = kampanya.KisaAciklama;
            dbKampanya.Aciklama = kampanya.Aciklama;
            if (ResimURL != null)
            {
                string dosyaadi = dbKampanya.ResimURL;
                string dosyaYolu = Server.MapPath(dosyaadi);
                FileInfo dosya = new FileInfo(dosyaYolu);
                if (dosya.Exists)
                {
                    dosya.Delete();
                }
                string file_name = Guid.NewGuid().ToString().Replace("-", "");
                string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYol = "/External/Kampanya/" + file_name + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(TamYol));
                dbKampanya.ResimURL = TamYol;

            }
            db.SaveChanges();
            TempData["Bilgi"] = "Güncelle işleminiz Başarılı";
            return RedirectToAction("Index", "Kampanya");
        }

        #endregion

        #region Kampanya Sil
        [LoginFilter]
        public JsonResult Sil(Kampanya kampanya)
        {
            Kampanya dbKampanya = db.Kampanya.Find(kampanya.ID);
            if (dbKampanya == null)
            {
                return Json(new ResultJson { Success = false, Message = "Kampanya Bulunamadı ! " });

            }
            try
            {
                if (dbKampanya.ResimURL != null)
                {
                    string ResimURL = dbKampanya.ResimURL;
                    string ResimPath = Server.MapPath(ResimURL);

                    FileInfo file = new FileInfo(ResimPath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                db.Kampanya.Remove(dbKampanya);
                db.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "Kampanya Silme İşleminiz Başarılı ! " });
            }
            catch (Exception ex)
            {

                return Json(new ResultJson { Success = false, Message = "Kampanya Silme İşleminiz Başarısız ! " });
            }
        }
        #endregion
	}
}