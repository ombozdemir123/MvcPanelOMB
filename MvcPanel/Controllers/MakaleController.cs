using MvcPanel.Helpers;
using MvcPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcPanel.Controllers
{
    public class MakaleController : YetkiliController
    {

        MvcPanelDB db = new MvcPanelDB();
        public new ActionResult Index()
        {
            var makaleler = db.Makales.ToList();
            return View(makaleler);
        }

        // GET: Makale/Details/5
        public ActionResult Details(int id)
        {
            var makale = db.Makales.Where(i => i.ID == id).SingleOrDefault();
            return View(makale);
        }

        public ActionResult KisiMakaleListele()
        {
            var kullaniciadi = Session["username"].ToString();
            var makaleler = db.Kullanicis.Where(a => a.KullaniciAdi == kullaniciadi).SingleOrDefault().Makales.ToList();


            return View("KisiMakaleListele", makaleler);
        }

        // GET: Makale/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi");
            return View();
        }

        // POST: Makale/Create
        [HttpPost]
        public ActionResult Create(Makale model)
        {
            try
            {
                string kullaniciadi = Session["username"].ToString();
                var kullanici = db.Kullanicis.Where(i => i.KullaniciAdi == kullaniciadi).SingleOrDefault();
                model.KullaniciID = kullanici.ID;

                db.Makales.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index","Kullanici");
            }
            catch
            {
                return View();
            }
        }

        // GET: Makale/Edit/5
        public ActionResult Edit(int id)
        {
            string kullaniciadi = Session["username"].ToString();
            var makale = db.Makales.Where(i => i.ID == id).SingleOrDefault();
            if (makale == null)
            {
                return HttpNotFound();
            }
            if (makale.Kullanici.KullaniciAdi == kullaniciadi)
            {
                ViewBag.KategoriID = new SelectList(db.Kategoris, "KategoriID", "KategoriAdi");
                return View();
            }
            return HttpNotFound();


        }

        // POST: Makale/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Makale model)
        {
            try
            {

                var makale = db.Makales.Where(i => i.ID == id).SingleOrDefault();
                makale.Baslik = model.Baslik;
                makale.Icerik = model.Icerik;
                makale.KategoriID = model.KategoriID;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Makale/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Makale/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var kullaniciadi = Session["username"].ToString();
                var kullanici = db.Kullanicis.Where(i => i.KullaniciAdi == kullaniciadi).SingleOrDefault();
                var makale = db.Makales.Where(i => i.ID == id).SingleOrDefault();
                if (OrtakSinif.DeleteIzinYetkiVarmi(id, kullanici))
                {
                    //makale.Kullanici = null;
                    foreach (var i in makale.Yorums)
                    {
                        db.Yorums.Remove();
                    }
                    makale.Yorums.Clear();
                    makale.Etikets.Clear();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public JsonResult YorumYap(string yorum, int MakaleID)
        {
            var kullaniciadi = Session["username"].ToString();
            var kullanici = db.Kullanicis.Where(i => i.KullaniciAdi == kullaniciadi).SingleOrDefault();
            if (yorum == "")
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            db.Yorums.Add(new Yorum { KullaniciID = kullanici.ID, MakaleID = MakaleID, YorumIcerik = yorum });
            db.SaveChanges();
            return Json(false,JsonRequestBehavior.AllowGet);
        }

        public ActionResult YorumDelete (int id)
        {
            var kullaniciadi = Session["username"].ToString();
            var kullanici = db.Kullanicis.Where(i => i.KullaniciAdi == kullaniciadi).SingleOrDefault();
            var yorum = db.Yorums.Where(i => i.ID == id).SingleOrDefault();
            var makale = db.Makales.Where(i => i.ID == yorum.MakaleID).SingleOrDefault();
            if (yorum == null)
            {
                return RedirectToAction("Hata", "Yetkili", new { yazilacak = "Yorum Bulunamadı" });
            }
            if(OrtakSinif.DeleteIzinYetkiVarmi(id, kullanici) ||makale.KullaniciID==kullanici.ID)
                {
                db.Yorums.Remove(db.Yorums.Find(id));
                db.SaveChanges();
                return RedirectToAction("Details", "Makale", new { id = yorum.MakaleID });
                }
            return RedirectToAction("Hata", "Yetkili", new { yazilacak = "Yorum Silinemedi" });


        }

    }
}
