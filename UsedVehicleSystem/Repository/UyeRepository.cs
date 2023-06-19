using UsedVehicleSystem.Database;
using UsedVehicleSystem.Models;

namespace UsedVehicleSystem.Repository
{

	public interface IUyeRepository
	{
		public Uye UyeEkle(Uye uye, string? telefonNumarasi = null);
        public Uye UyeGiris(string Eposta, string Sifre);
		public bool UyeVarMi(int ID);
        public bool UyeVarMi(string Eposta);
        public bool MusteriMi(int ID);
		public Musteri MusteriGetir(int ID);
		public AracSaticisi AracSaticisiGetir(int ID);
		public Uye UyeGetir(int ID);
	}

	public class UyeRepository : IUyeRepository
	{
		SystemDBContext _context;

		public UyeRepository(SystemDBContext context)
		{
			_context = context;
			_context.StartUp();
		}

		public Uye UyeGiris(string Eposta, string Sifre)
		{
			return _context.Uyeler.FirstOrDefault(u => u.eposta == Eposta && u.sifre == Sifre);
		}

        public bool UyeVarMi(int ID)
        {
			return _context.Uyeler.Any(u => u.ID == ID);
        }
        public bool UyeVarMi(string Eposta)
        {
			return _context.Uyeler.Any(u => u.eposta == Eposta);
        }

        public bool MusteriMi(int ID)
		{
			if(!this.UyeVarMi(ID)) return false;
			return _context.Musteriler.Any(u => u.uyeID == ID);
		}

		public AracSaticisi AracSaticisiGetir(int ID)
		{
			return _context.AracSaticilari.FirstOrDefault(u => u.uyeID == ID);
		}

		public Musteri MusteriGetir(int ID)
		{
			return _context.Musteriler.FirstOrDefault(u => u.uyeID == ID);
        }

        public Uye UyeEkle(Uye uye, string? telefonNumarasi = null)
        {
			if (telefonNumarasi != null)
			{
				AracSaticisi satici = new AracSaticisi(uye, telefonNumarasi);
				_context.AracSaticilari.Add(satici);
                _context.SaveChanges();
                satici.uyeID = satici.ID; // Uyeler tablosuyla bağlar.
                _context.AracSaticilari.Update(satici);
				uye = satici;
            }
			else
			{
				Musteri musteri = new Musteri(uye);
                _context.Musteriler.Add(musteri);
                _context.SaveChanges();
				musteri.uyeID = musteri.ID; // Uyeler tablosuyla bağlar.
                _context.Musteriler.Update(musteri);
				uye = musteri;
            }
            _context.SaveChanges();
            return uye;
        }

        public Uye UyeGetir(int ID)
		{
			return _context.Uyeler.FirstOrDefault((u => u.ID == ID));
		}
	}
}
