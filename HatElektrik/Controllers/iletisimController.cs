using HatElektrik.Helper.CustomFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using HatElektrik.Models.DataContext;
using HatElektrik.Models.Tablolar;
using System.Net;
using HatElektrik.Helper.JsonMessage;
namespace HatElektrik.Controllers
{
    public class iletisimController : Controller
    {
        HatElektrikContext db = new HatElektrikContext();
        [LoginFilter]
        public ActionResult Index(int Sayfa=1)
        {
            return View(db.iletisim.OrderByDescending(x=>x.EklenmeTarihi).ToPagedList(Sayfa,15));
        }

        public ActionResult Ayrinti(int id)
        {
            iletisim dbiletisim = db.iletisim.Find(id);
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (dbiletisim == null)
            {
                return HttpNotFound();
            }
            return View(dbiletisim);
        }

        #region Slider Sil
        [LoginFilter]
        public JsonResult Sil(iletisim mesaj)
        {
            iletisim dbiletisim = db.iletisim.Find(mesaj.ID);
            if (dbiletisim == null)
            {
                return Json(new ResultJson { Success = false, Message = "Mesaj Bulunamadı ! " });

            }
            try
            {

                db.iletisim.Remove(dbiletisim);
                db.SaveChanges();
                return Json(new ResultJson { Success = true, Message = "Mesaj Silme İşleminiz Başarılı ! " });
            }
            catch (Exception ex)
            {

                return Json(new ResultJson { Success = false, Message = "Mesaj Silme İşleminiz Başarısız ! " });
            }
        }
        #endregion
	}
}