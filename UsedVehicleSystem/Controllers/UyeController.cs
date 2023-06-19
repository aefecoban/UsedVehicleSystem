using Microsoft.AspNetCore.Mvc;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Services;

namespace UsedVehicleSystem.Controllers
{
	public class UyeController : Controller
	{
        IHesapYonetimi _hesapYonetimi;

        public UyeController(IHesapYonetimi hesapYonetimi)
        {
            _hesapYonetimi = hesapYonetimi;
        }

        public IActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GirisYap(string Eposta, string Sifre)
        {
            if(Eposta == null || Sifre == null) 
            {
                ViewBag.Hata = "E-Posta ve(ya) Sifre bos birakilamaz.";
                return View("Giris");
            }

            Uye girisYapilan = _hesapYonetimi.GirisYap(Eposta, Sifre);
            if(girisYapilan == null)
            {
                ViewBag.Hata = "E-Posta ve(ya) Sifre yanlış.";
                return View("Giris");
            }
            else
            {
                HttpContext.Session.SetString("SessionID", girisYapilan.ID.ToString());
                return RedirectToAction("Profil");
            }
        }

        public IActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KayitOl(Uye kayitlanacakUye, string uyelikTuru, string? telefonNumarasi = null)
        {
            if(kayitlanacakUye.ad == null || kayitlanacakUye.soyad == null
                || kayitlanacakUye.eposta == null || kayitlanacakUye.sifre == null 
                || uyelikTuru == null)
            {
                ViewBag.Hata = "Butun alanlari doldurmaniz zorunludur. " + uyelikTuru + " - " + kayitlanacakUye.ad + " - " + kayitlanacakUye.soyad + " - " + kayitlanacakUye.eposta + " - " + kayitlanacakUye.sifre;
                return View("Kayit");
            }
            if (_hesapYonetimi.UyeVarMi(kayitlanacakUye.eposta))
            {
                ViewBag.Hata = "Bu Eposta hesabı zaten mevcut.";
                return View("Kayit");
            }

            bool musteriMi = uyelikTuru.ToUpperInvariant().ToString() == ("1").ToUpperInvariant() ? true : false;
            Uye kayit = null;
            if (musteriMi)
                kayit = _hesapYonetimi.UyeOl(kayitlanacakUye);
            else
                kayit = _hesapYonetimi.UyeOl(kayitlanacakUye, telefonNumarasi ?? "0000000000");

            HttpContext.Session.SetString("SessionID", kayit.ID.ToString());

            return RedirectToAction("Profil");
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Home()
        {
            if(HttpContext.Session.GetString("SessionID") == null)
            {
                return View("Giris");
            }

            Uye gonderilecekUye = _hesapYonetimi.UyeGetir(int.Parse(HttpContext.Session.GetString("SessionID")));

            if(gonderilecekUye == null)
            {
                HttpContext.Session.Remove("SessionID");
                ViewBag.Hata = "Giriş bilgileri hatalı. Lütfen tekrar deneyin.";
                return View("Giris");
            }

            ViewBag.Uye = gonderilecekUye;
            ViewBag.AracSaticisiMi = !_hesapYonetimi.MusteriMi(int.Parse(HttpContext.Session.GetString("SessionID")));

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Cikis()
        {
            HttpContext.Session.Remove("SessionID");
            return RedirectToAction("Home");
        }

        public IActionResult Profil()
        {
            Uye uye = _hesapYonetimi.UyeGetir(
                int.Parse(HttpContext.Session.GetString("SessionID"))
                );
            return View(uye);
        }
    }
}
