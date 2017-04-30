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
using System.Net;
using System.IO;
using HatElektrik.Helper.JsonMessage;

namespace HatElektrik.Controllers
{
    public class UrunController : Controller
    {
        HatElektrikContext db = new HatElektrikContext();
        #region Ürün Listele
        [HttpGet]
        [LoginFilter]
        public ActionResult Index(int Sayfa = 1)
        {
            return View(db.Urun.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 10));
        }

        #endregion

        #region Ürün Ekle

        [HttpGet]
        [LoginFilter]
        public ActionResult Ekle()
        {
            SetKategoriListesi();
            return View();
        }

        [HttpPost]
        [LoginFilter]
        public ActionResult Ekle(Urun urun, int KategoriID, HttpPostedFileBase VitrinResmi)
        {
            if (ModelState.IsValid)
            {
                if (urun != null)
                {
                    urun.KategoriID = KategoriID;
                    if (VitrinResmi != null)
                    {
                        string DosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                        string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                        string TamYol = "/External/Urun/" + DosyaAdi + Uzanti;
                        Request.Files[0].SaveAs(Server.MapPath(TamYol));
                        urun.ResimURL = TamYol;
                    }
                    db.Urun.Add(urun);
                    db.SaveChanges();


                    TempData["Bilgi"] = "Ürün Ekleme İşleminiz Başarılı";
                    return RedirectToAction("Index", "Urun");
                }
                TempData["BilgiDanger"] = "Ürün Ekleme İşleminiz Başarısız!";
                return RedirectToAction("Index", "Urun");

            }
            TempData["BilgiDanger"] = "Ürün Ekleme İşleminiz Başarısız Bir şeyleri Eksik Giriyorsunuz!";
            return RedirectToAction("Index", "Urun");
        }

        #endregion

        #region Ürün Düzenle
        [HttpGet]
        [LoginFilter]
        public ActionResult Duzenle(int id)
        {
            Urun dbUrun = db.Urun.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            if (dbUrun == null)
            {
                return HttpNotFound();
            }
            SetKategoriListesi();
            return View(dbUrun);
        }

        [HttpPost]
        [LoginFilter]
        public ActionResult Duzenle(Urun urun, HttpPostedFileBase VitrinResmi)
        {
            Urun gelenUrun = db.Urun.Find(urun.ID);
            if (ModelState.IsValid)
            {
                gelenUrun.Aciklama = urun.Aciklama;
                gelenUrun.AktifMi = urun.AktifMi;
                gelenUrun.Baslik = urun.Baslik;
                gelenUrun.KisaAciklama = urun.KisaAciklama;
                gelenUrun.Fiyat = urun.Fiyat;
                gelenUrun.Adet = urun.Adet;
                gelenUrun.KategoriID = urun.KategoriID;
                if (VitrinResmi != null)
                {
                    string dosyaadi = gelenUrun.ResimURL;
                    string dosyaYolu = Server.MapPath(dosyaadi);
                    FileInfo dosya = new FileInfo(dosyaYolu);
                    if (dosya.Exists)
                    {
                        dosya.Delete();
                    }
                    string file_name = Guid.NewGuid().ToString().Replace("-", "");
                    string uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string TamYol = "/External/Urun/" + file_name + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(TamYol));
                    gelenUrun.ResimURL = TamYol;
                }
                db.SaveChanges();
                TempData["Bilgi"] = "Güncelle işleminiz Başarılı";
                return RedirectToAction("Index", "Urun");
            }
            TempData["BilgiDanger"] = "Güncelle işleminiz Başarısız Birşeyler Eksik yada Hatalı Girilmiştir!";
            return RedirectToAction("Index", "Urun");
        }

        #endregion

        #region Ürün Sil
        [LoginFilter]
        public JsonResult Sil(Urun urun)
        {
            Urun dbUrun = db.Urun.Find(urun.ID);
            if (dbUrun == null)
            {
                return Json(new ResultJson { Success = false, Message = "Urun Bulunamadı ! " });

            }
            try
            {
                if (dbUrun.ResimURL != null)
                {
                    string ResimURL = dbUrun.ResimURL;
                    string ResimPath = Server.MapPath(ResimURL);

                    FileInfo file = new FileInfo(ResimPath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                db.Urun.Remove(dbUrun);
                db.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "Ürün Silme İşleminiz Başarılı ! " });
            }
            catch (Exception ex)
            {

                return Json(new ResultJson { Success = false, Message = "Ürün Silme İşleminiz Başarısız ! " });
            }
        }
        #endregion

        #region Set Kategori Listele
        public void SetKategoriListesi(object kategori = null)
        {
            var KategoriList = db.Kategori.ToList();
            ViewBag.Kategori = KategoriList;

            db.SaveChanges();
        }

        #endregion
    }
}