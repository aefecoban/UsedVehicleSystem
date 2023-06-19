using Microsoft.AspNetCore.Mvc;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Services;
using UsedVehicleSystem.Mediator;
using UsedVehicleSystem.Facade;

namespace UsedVehicleSystem.Controllers
{
	public class IlanController : Controller
	{
		IIlanYonetimi _ilanYonetimi;
        IFacadeService _facadeService;
        

        public IlanController(IFacadeService facadeService, IIlanYonetimi ilanYonetimi)
		{
			_ilanYonetimi = ilanYonetimi;
            _facadeService = facadeService;
        }

        private AracSaticisi SessionKontrol()
        {
            if (HttpContext.Session.GetString("SessionID") == null) return null;

            int ID = int.Parse(HttpContext.Session.GetString("SessionID"));
            return _facadeService.AracSaticisiGetir(ID);
        }

        public IActionResult IlanEkle()
		{
            AracSaticisi aracSaticisi = SessionKontrol();
            if (aracSaticisi == null)
				return RedirectToAction("Index", "Home");

            var markaModel = _facadeService.AracMarkaModelGetir();
            ViewBag.Markalar = markaModel.Item1;
            ViewBag.Modeller = markaModel.Item2;
            ViewBag.AracSaticisi = aracSaticisi;
            return View();
		}

		[HttpPost]
		public IActionResult IlanEkle(Ilan ilan, int kilometre, string aracMarkasiID, string aracModelID, string? AracDonanimlari = "")
        {
            AracSaticisi aracSaticisi = SessionKontrol();
            if (aracSaticisi == null)
                return RedirectToAction("Index", "Home");

            ilan.aracSaticisiID = aracSaticisi.ID;
            ilan.aracSaticisi = aracSaticisi;

            Arac ilandakiArac = new Arac();
            ilandakiArac.kilometre = kilometre;
            ilandakiArac.aracMarkasiID = int.Parse(aracMarkasiID);
            ilandakiArac.aracMarkasi = _facadeService.AracMarkaGetir(int.Parse(aracMarkasiID));
            ilandakiArac.aracModeliID = int.Parse(aracModelID);
            ilandakiArac.aracModeli = _facadeService.AracModelGetir(int.Parse(aracModelID));

            //ilandaki aracı kaydet
            _facadeService.AracEkle(ilandakiArac);
            ViewBag.AracSaticisi = aracSaticisi;
            ViewBag.Ilan = _facadeService.IlanEkle(ilan, ilandakiArac, aracSaticisi, AracDonanimlari); ;
            return RedirectToAction("Ilanlarim");
		}

        [HttpPost]
        public IActionResult IlanGuncelle(Ilan ilan, int kilometre, string aracMarkasiID, string aracModelID)
        {
            AracSaticisi aracSaticisi = SessionKontrol();
            if (aracSaticisi == null)
                return RedirectToAction("Index", "Home");
            _ilanYonetimi.IlanGuncelle(ilan.ID, ilan);
            return RedirectToAction("Ilanlarim");
        }

		public IActionResult Ilanlar() 
        {
            List<Ilan> ilanlar = _facadeService.TumIlanlariGetir();
            ViewBag.Markalar = _facadeService.AracMarkaModelGetir().Item1 ?? new List<AracMarkasi>();
            ViewBag.Modeller = _facadeService.AracMarkaModelGetir().Item2 ?? new List<AracModeli>();
            return View(ilanlar);
        }

        public IActionResult Ilanlarim()
        {
            AracSaticisi aracSaticisi = SessionKontrol();
            if (aracSaticisi == null)
                return RedirectToAction("Index", "Home");

            List<Ilan> benimIlanlarim = _facadeService.IlanlariListele(aracSaticisi);

            return View(benimIlanlarim);
        }

        public IActionResult Ilanlari()
        {
            AracSaticisi aracSaticisi = SessionKontrol();
            if (aracSaticisi == null)
                return RedirectToAction("Index", "Home");
            return View();
		}
		
		public IActionResult IlanSil(int ID)
        {
            AracSaticisi aracSaticisi = SessionKontrol();
            if (aracSaticisi == null)
                return RedirectToAction("Index", "Home");

            _facadeService.AracSil(_ilanYonetimi.IlanGetir(ID).ilandakiAracID);
            _ilanYonetimi.IlanSil(ID);

            return RedirectToAction("Ilanlarim");
		}

        public IActionResult IlanArama(Ilan ilan, string? Sanziman, string? AracTuru, string? UretimYili, string? YakitTuru, string? MotorTuru, string? Marka, string? Model)
        {
            ilan.ilandakiArac = new Arac();
            ilan.ilandakiArac.aracMarkasi = new AracMarkasi();
            ilan.ilandakiArac.aracMarkasi.ID = int.Parse(Marka ?? "0");
            ilan.ilandakiArac.aracModeli = new AracModeli();
            ilan.ilandakiArac.aracModeli.ID = int.Parse(Model ?? "0");
            ilan.ilandakiArac.aracModeli.sanziman = Sanziman ?? "";
            ilan.ilandakiArac.aracModeli.aracTuru = AracTuru ?? "";
            ilan.ilandakiArac.aracModeli.uretimYili = int.Parse(UretimYili ?? "0");
            ilan.ilandakiArac.aracModeli.yakitTuru = YakitTuru ?? "";
            ilan.ilandakiArac.aracModeli.motorTuru = MotorTuru ?? "";

            List<Ilan> ilanlar = _ilanYonetimi.IlanAra(ilan);

            ViewBag.Ilanlar = ilanlar;
            ViewBag.Filtre = ilan;

            return View();
        }

		public IActionResult IlanGoruntule(int ID)
        {
            ViewBag.CanIEdit = false;
            ViewBag.Uye = _facadeService.UyeGetir(int.Parse(HttpContext.Session.GetString("SessionID")));

            AracSaticisi aracSaticisi = SessionKontrol();

            Ilan ilan = _ilanYonetimi.IlanGetir(ID);

            List<Yorum> aracSaticisinaYorumlar = _facadeService.TumYorumlariGetir(ilan.aracSaticisiID);
            ViewBag.SaticiyaYapilanYorumar = aracSaticisinaYorumlar;
            if (ilan == null)
                return RedirectToAction("Index", "Home");
            if (aracSaticisi != null)
            {
                ViewBag.CanIEdit = true;
            }

            ViewBag.Markalar = _facadeService.AracMarkaModelGetir().Item1 ?? new List<AracMarkasi>();
            ViewBag.Modeller = _facadeService.AracMarkaModelGetir().Item2 ?? new List<AracModeli>();
            return View(ilan);
		}

        [HttpPost]
        public IActionResult YorumYap(Yorum yorum)
        {
            _facadeService.YorumEkle(yorum.AracSaticisiID, yorum.yorumIcerigi, yorum.musteriAdi);

            return RedirectToAction("Ilanlar");
        }

        public IActionResult IlanKarsilastir(int ilanID1, int ilanID2)
        {
            Ilan ilan1 = _facadeService.IlanGetir(ilanID1);
            Ilan ilan2 = _facadeService.IlanGetir(ilanID2);

            if (ilan1==null||ilan2==null) //SADECE TEST ETMEK ICIN
            {
                ilan1 = _facadeService.IlanGetir(1);
                ilan2 = _facadeService.IlanGetir(2);
            }

            ViewBag.Ilan1 = ilan1;
            ViewBag.Ilan2 = ilan2;

            return View();
		}

	}
}
