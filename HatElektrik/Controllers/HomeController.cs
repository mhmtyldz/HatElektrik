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

namespace HatElektrik.Controllers
{
    public class HomeController : Controller
    {
        HatElektrikContext db = new HatElektrikContext();

        public ActionResult Index()
        {
            return View();
        }

        #region Kampanya Çek

        public PartialViewResult _KampanyaPartial()
        {
            return PartialView(db.Kampanya.OrderByDescending(x => x.EklenmeTarihi).Take(3).ToList());
        }

        #endregion


        #region Popüler Ürünler
        public PartialViewResult _PopulerUrunler()
        {
            return PartialView(db.Urun.OrderByDescending(x => x.Okunma).Take(3).ToList());
        }

        #endregion


        #region Kategori Listesi
        public PartialViewResult _KategoriPartial()
        {
            return PartialView(db.Kategori.OrderByDescending(x => x.EklenmeTarihi).ToList());
        }
        #endregion


        #region Referanslar
        public PartialViewResult _ReferansPartial()
        {
            return PartialView(db.Referans.OrderByDescending(x=>x.EklenmeTarihi).Take(5));
        }

        #endregion


        #region Son Eklenen Ürünler

        public PartialViewResult _SonEklenenUrunlerAnasayfa()
        {

            return PartialView(db.Urun.OrderByDescending(x=>x.EklenmeTarihi).Take(3).ToList());
        }



        #endregion


        #region Slider Anasayfa

        public PartialViewResult _SliderPartial()
        {
            return PartialView(db.Slider.OrderByDescending(x=>x.EklenmeTarihi).Take(5).ToList());
        }

        #endregion


        #region Hakkimizda
        public ActionResult Hakkimizda()
        {
            return View();
        }
        #endregion


        #region Ürünler
        public ActionResult Urunler(int Sayfa=1)
        {

            return View(db.Urun.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 9));
        }



        #endregion


        #region 
        public ActionResult KategoriUrunler(int id, int Sayfa = 1)
        {
            return View(db.Urun.Where(x=>x.KategoriID==id).OrderByDescending(x=>x.EklenmeTarihi).ToPagedList(Sayfa,10));
        }


        #endregion


        #region Ürün Ayrıntı
        public ActionResult UrunAyrinti(int id)
        {
            Urun dbUrun = db.Urun.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (dbUrun == null)
            {
                return RedirectToAction("HttpNotFound","Home");
            }
            dbUrun.Okunma++;
            db.SaveChanges();
            return View(dbUrun);
        }


        #endregion


        #region Kampanyalar

        public ActionResult Kampanyalar(int Sayfa=1)
        {
            return View(db.Kampanya.OrderByDescending(x=>x.EklenmeTarihi).ToPagedList(Sayfa,5));
        }


        #endregion


        #region Galeri 
        public ActionResult Galeri(int Sayfa=1)
        {
            return View(db.Galeri.OrderByDescending(x=>x.EklenmeTarihi).ToPagedList(Sayfa,30));
        }



        #endregion


        #region İletişim
        public ActionResult iletisim()
        {
            return View();
        }

        [HttpPost]
        public ActionResult iletisim(iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                db.iletisim.Add(iletisim);
                db.SaveChanges();
                TempData["Bilgi"] = "Mesajınız Başarıyla Gönderilmiştir.";
                return View();

            }
            return View(iletisim);
          
        }


        #endregion


        #region HttpNot Found
        public ActionResult HttpNotFound()
        {
            return View();
        }


        #endregion


    }
}