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
using HatElektrik.Helper.JsonMessage;
namespace HatElektrik.Controllers
{
    public class KategoriController : Controller
    {
        HatElektrikContext db = new HatElektrikContext();
        #region Kategori Listele
        [LoginFilter]
        public ActionResult Index(int Sayfa = 1)
        {
            return View(db.Kategori.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 12));
        }
        #endregion

        #region Kategori Ekle
        [HttpGet]
        [LoginFilter]
        public ActionResult Ekle()
        {
            return View();

        }

        [HttpPost]
        [LoginFilter]
        public ActionResult Ekle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                db.Kategori.Add(kategori);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(kategori);
        }

        #endregion

        #region Kategori Güncelle
        [HttpGet]
        [LoginFilter]
        public ActionResult Duzenle(int id)
        {
            Kategori dbKategori = db.Kategori.Find(id);
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                
            }
            if (dbKategori == null)
            {
                return HttpNotFound();

            }
            return View(dbKategori);
        }
        [HttpPost]
        [LoginFilter]
        public ActionResult Duzenle(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                Kategori dbKategori = db.Kategori.Find(kategori.ID);
                dbKategori.KategoriAdi = kategori.KategoriAdi;
                dbKategori.URL = kategori.URL;
                dbKategori.AktifMi = kategori.AktifMi;
                db.SaveChanges();
                return RedirectToAction("Index","Kategori");
            }

            else
            {
                return View(kategori);
            }
        }
        #endregion

        #region Kategori Sil

        public JsonResult Sil(int ID)
        {
            Kategori dbKategori = db.Kategori.Find(ID);
            if (dbKategori == null)
            {
                return Json(new ResultJson { Success = false, Message = "Böyle Bir Kategori Bulunamadı" });
            }
            db.Kategori.Remove(dbKategori);
            db.SaveChanges();
            return Json(new ResultJson { Success = true, Message = "Kategori silme İşleminiz Başarılı" });
        }
        #endregion
    }
}