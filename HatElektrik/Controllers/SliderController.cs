using HatElektrik.Helper.CustomFilter;
using HatElektrik.Helper.JsonMessage;
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

namespace HatElektrik.Controllers
{
    public class SliderController : Controller
    {
        HatElektrikContext db = new HatElektrikContext();

        #region Slider Listele
        [HttpGet]
        [LoginFilter]
        public ActionResult Index(int Sayfa = 1)
        {
            return View(db.Slider.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 10));
        }

        #endregion

        #region Slider Ekle

        [HttpGet]
        [LoginFilter]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        [LoginFilter]
        public ActionResult Ekle(Slider slider, HttpPostedFileBase ResimURL)
        {
            if (slider.ResimURL != null)
            {
                if (ResimURL.ContentLength > 0)
                {
                    string Dosya = Guid.NewGuid().ToString().Replace("-", "");
                    string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string ResimYolu = "/External/Slider/" + Dosya + Uzanti;

                    ResimURL.SaveAs(Server.MapPath(ResimYolu));
                    slider.ResimURL = ResimYolu;
                }

                try
                {
                    db.Slider.Add(slider);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Slider");
                }
                catch (Exception)
                {

                    return View(slider);
                }

            }
            return View(slider);


        }

        #endregion

        #region Slider Düzenle
        [HttpGet]
        [LoginFilter]
        public ActionResult Duzenle(int id)
        {
            Slider dbSlider = db.Slider.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            if (dbSlider == null)
            {
                return HttpNotFound();

            }
            return View(dbSlider);
        }

        [HttpPost]
        [LoginFilter]
        public ActionResult Duzenle(Slider slider, HttpPostedFileBase ResimURL)
        {
            Slider dbSlider = db.Slider.Find(slider.ID);
            dbSlider.Baslik = slider.Baslik;
            dbSlider.Aciklama = slider.Aciklama;
            dbSlider.AktifMi = slider.AktifMi;
            if (ResimURL != null)
            {
                string dosyaadi = dbSlider.ResimURL;
                string dosyaYolu = Server.MapPath(dosyaadi);
                FileInfo dosya = new FileInfo(dosyaYolu);
                if (dosya.Exists)
                {
                    dosya.Delete();
                }
                string file_name = Guid.NewGuid().ToString().Replace("-", "");
                string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                string TamYol = "/External/Slider/" + file_name + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(TamYol));
                dbSlider.ResimURL = TamYol;

            }
            db.SaveChanges();
            TempData["Bilgi"] = "Güncelle işleminiz Başarılı";
            return RedirectToAction("Index", "Slider");



        }

        #endregion

        #region Slider Sil
        [LoginFilter]
        public JsonResult Sil(Slider slider)
        {
            Slider dbSlider = db.Slider.Find(slider.ID);
            if (dbSlider == null)
            {
                return Json(new ResultJson { Success = false, Message = "Slider Bulunamadı ! " });

            }
            try
            {
                if (dbSlider.ResimURL != null)
                {
                    string ResimURL = dbSlider.ResimURL;
                    string ResimPath = Server.MapPath(ResimURL);

                    FileInfo file = new FileInfo(ResimPath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                db.Slider.Remove(dbSlider);
                db.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "Slider Silme İşleminiz Başarılı ! " });
            }
            catch (Exception ex)
            {

                return Json(new ResultJson { Success = false, Message = "Slider Silme İşleminiz Başarısız ! " });
            }
        }
        #endregion
    }
}