using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;
using UsedVehicleSystem.Services;

namespace UsedVehicleSystem.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IHesapYonetimi _hesapYonetimi;

        public HomeController(ILogger<HomeController> logger, IHesapYonetimi hesapYonetimi)
		{
			_logger = logger;
            _hesapYonetimi = hesapYonetimi;
        }

        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("SessionID") != null)
            {
                Uye uye = _hesapYonetimi.UyeGetir(int.Parse(HttpContext.Session.GetString("SessionID")));
                bool MusteriMi = _hesapYonetimi.MusteriMi(uye.ID);
                ViewBag.Uye = uye;
                ViewBag.MusteriMi = MusteriMi;
            }


            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Arama()
        {
            return View();
        }

        public IActionResult Arama(Ilan filtrelenmisIlan)
        {
            ViewBag.Filtre = filtrelenmisIlan;
            return View();
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}