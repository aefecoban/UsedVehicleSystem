using Microsoft.AspNetCore.Mvc;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Services;

namespace UsedVehicleSystem.Controllers
{
    public class SistemYoneticisiController : Controller
    {
        ISistemYonetimi _sistemYonetimi;

        public SistemYoneticisiController(ISistemYonetimi sistemYonetimi)
        {
            _sistemYonetimi = sistemYonetimi;
        }

        public SistemYoneticisi? SistemYoneticisiGetir()
        {
            if (HttpContext.Session.GetString("systemID") == null)
                return null;

            int ID = int.Parse(HttpContext.Session.GetString("systemID") ?? "-1");
            return _sistemYonetimi.YoneticiGetir(ID);
        }

        public IActionResult Index()
        {
            return RedirectToAction("Home");
        }

        public IActionResult Home()
        {
            SistemYoneticisi sy = SistemYoneticisiGetir();
            if (sy != null)
                return View(sy);
            else
                return RedirectToAction("Giris");
        }

        public IActionResult Giris()
        {
            if (SistemYoneticisiGetir() != null)
                return RedirectToAction("Home");
            else
                return View();
        }

        [HttpPost]
        public IActionResult GirisYap(SistemYoneticisi sy)
        {
            if (SistemYoneticisiGetir() != null)
                return RedirectToAction("Home");
            else
            {
                SistemYoneticisi yonetici = _sistemYonetimi.GirisYap(sy.takmaAd, sy.sifre);
                if(yonetici == null)
                {
                    ViewBag.Hata = "Takma ad veya şifre yanlış.";
                    return View("Giris");
                }
                else
                {
                    HttpContext.Session.SetString("systemID", yonetici.ID.ToString());
                    return RedirectToAction("Home");
                }
            }
        }

        public IActionResult Ilanlar()
        {
            List<Ilan> ilanlar = _sistemYonetimi.TumIlanlariGetir();
            return View(ilanlar);
        }

        public IActionResult IlanOnayla(int ID)
        {
            _sistemYonetimi.IlanOnayla(ID);
            return RedirectToAction("Ilanlar");
        }
        /*
        public IActionResult Araclar()
        {
            List<Arac> araclar = _sistemYonetimi.TumAraclariGetir();
            return View();
        }*/

        public IActionResult Cikis()
        {
            if (SistemYoneticisiGetir() != null)
                HttpContext.Session.Remove("systemID");

            return RedirectToAction("Home");
        }

        public IActionResult AracMarkaEkle()
        {
            ViewBag.Markalar = _sistemYonetimi.TumAracMarkalariniGetir();
            return View();
        }

        [HttpPost]
        public IActionResult AracMarkaEkle(AracMarkasi marka)
        {
            SistemYoneticisi yonetici = SistemYoneticisiGetir();
            if (yonetici == null)
                return RedirectToAction("Home");

            ViewBag.Markalar = _sistemYonetimi.TumAracMarkalariniGetir();

            if (_sistemYonetimi.AracMarkaEkle(marka) == false)
                ViewBag.Hata = "Arac Markasi Ekleme Basarisiz. Daha önce var olan markayı ekleyemezsiniz.";
            else
                ViewBag.AracMarka = marka;

            return View();
        }

        public IActionResult AracModelEkle()
        {
            ViewBag.Modeller = _sistemYonetimi.TumAracModelleriniGetir();
            return View();
        }

        [HttpPost]
        public IActionResult AracModelEkle(AracModeli model)
        {
            SistemYoneticisi yonetici = SistemYoneticisiGetir();
            if (yonetici == null)
                return RedirectToAction("Home");

            ViewBag.Modeller = _sistemYonetimi.TumAracModelleriniGetir();

            if(_sistemYonetimi.AracModelEkle(model) == false)
                ViewBag.Hata = "Arac Modeli Ekleme Basarisiz. Daha önce var olan modeli ekleyemezsiniz.";
            else
                ViewBag.AracModel = model;

            return View();
        }

    }
}
