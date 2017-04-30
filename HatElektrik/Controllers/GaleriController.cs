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
    public class GaleriController : Controller
    {
        HatElektrikContext db = new HatElektrikContext();

        #region Galeri Listele
        [HttpGet]
        [LoginFilter]
        public ActionResult Index(int Sayfa = 1)
        {
            return View(db.Galeri.OrderByDescending(x => x.EklenmeTarihi).ToPagedList(Sayfa, 10));
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
        public ActionResult Ekle(Galeri galeri, HttpPostedFileBase ResimURL)
        {
            if (galeri.ResimURL != null)
            {
                if (ResimURL.ContentLength > 0)
                {
                    string Dosya = Guid.NewGuid().ToString().Replace("-", "");
                    string Uzanti = System.IO.Path.GetExtension(Request.Files[0].FileName);
                    string ResimYolu = "/External/Galeri/" + Dosya + Uzanti;

                    ResimURL.SaveAs(Server.MapPath(ResimYolu));
                    galeri.ResimURL = ResimYolu;
                }

                try
                {
                    db.Galeri.Add(galeri);
                    db.SaveChanges();
                    TempData["Bilgi"] = "Galeri Resim Ekleme  işleminiz Başarılı";
                    return RedirectToAction("Index", "Galeri");
                }
                catch (Exception)
                {
                   
                    return View(galeri);
                }

            }
            
            return View(galeri);


        }

        #endregion



        #region Slider Sil
        [LoginFilter]
        public JsonResult Sil(Galeri galeri)
        {
            Galeri dbGaleri = db.Galeri.Find(galeri.ID);
            if (dbGaleri == null)
            {
                return Json(new ResultJson { Success = false, Message = "Galeri Bulunamadı ! " });

            }
            try
            {
                if (dbGaleri.ResimURL != null)
                {
                    string ResimURL = dbGaleri.ResimURL;
                    string ResimPath = Server.MapPath(ResimURL);

                    FileInfo file = new FileInfo(ResimPath);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                db.Galeri.Remove(dbGaleri);
                db.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "Galeri Resmi Silme İşleminiz Başarılı ! " });
            }
            catch (Exception ex)
            {

                return Json(new ResultJson { Success = false, Message = "Galeri Resmi Silme İşleminiz Başarısız ! " });
            }
        }
        #endregion

        
	}
}