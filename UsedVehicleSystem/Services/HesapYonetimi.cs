using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;

namespace UsedVehicleSystem.Services
{
	public interface IHesapYonetimi
	{
		public Uye GirisYap(string Eposta, string Sifre);
		public Uye UyeOl(Uye uye, string? telefonNuamrasi = null);
		public bool UyeVarMi(string Eposta);
		public bool UyeVarMi(int ID);
        public Uye UyeGetir(int ID);
		public bool MusteriMi(int ID);
		public AracSaticisi AracSaticisiGetir(int ID);
    }

	public class HesapYonetimi : IHesapYonetimi
	{
		IUyeRepository _uyeRepository;
		ILogger _logger;

		public HesapYonetimi(IUyeRepository uyeRepository, ILogger<HesapYonetimi> logger)
		{
			_uyeRepository = uyeRepository;
			_logger = logger;
		}

		public bool UyeVarMi(string Eposta) => _uyeRepository.UyeVarMi(Eposta);

		public bool UyeVarMi(int ID) => _uyeRepository.UyeVarMi(ID);

		public Uye GirisYap(string Eposta, string Sifre) => _uyeRepository.UyeGiris(Eposta, Sifre);

		public Uye UyeOl(Uye uye, string? telefonNuamrasi = null) => _uyeRepository.UyeEkle(uye, telefonNuamrasi);

		public bool MusteriMi(int ID)
		{
			if (!UyeVarMi(ID))
				return false;

			return _uyeRepository.MusteriMi(ID);
		}

		public Uye UyeGetir(int ID) => _uyeRepository.UyeGetir(ID);

		public AracSaticisi AracSaticisiGetir(int ID) => _uyeRepository.AracSaticisiGetir(ID);

    }
}
