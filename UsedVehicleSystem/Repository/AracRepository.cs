using UsedVehicleSystem.Database;
using UsedVehicleSystem.Models;

namespace UsedVehicleSystem.Repository
{

    public interface IAracRepository
    {
		public bool MarkaEkle(AracMarkasi marka);

        public bool ModelEkle(AracModeli model);

        public List<Arac> TumAraclariGetir();

        public List<AracModeli> TumModelleriGetir();

        public List<AracMarkasi> TumMarkalariGetir();

        public Arac AracGetir(int ID);

        public AracModeli ModelGetir(int ID);

        public AracMarkasi MarkaGetir(int ID);

        public Arac AracEkle(Arac arac);

        public bool AracSil(int ID);

        public bool DonanimEkle(AracDonanimi donanim);

        public List<AracDonanimi> DonanimlariGetir(int aracID);

    }

    public class AracRepository : IAracRepository
    {
        SystemDBContext _context;

		public AracRepository(SystemDBContext context)
		{
			_context = context;
        }

        public Arac AracGetir(int ID)
        {
            if (_context.Araclar.FirstOrDefault(a => a.ID == ID) == null)
                return null;

            Arac dondurulecekArac = _context.Araclar.FirstOrDefault(a => a.ID == ID);
            dondurulecekArac.aracMarkasi = this.MarkaGetir(dondurulecekArac.aracMarkasiID);
            dondurulecekArac.aracModeli = this.ModelGetir(dondurulecekArac.aracModeliID);
            if (this.DonanimlariGetir(dondurulecekArac.ID) != null)
                dondurulecekArac.aracDonanimlari = this.DonanimlariGetir(dondurulecekArac.ID);

            return dondurulecekArac;
        }

        public bool AracSil(int ID)
        {
            if(_context.Araclar.FirstOrDefault(a => a.ID == ID) != null)
            {
                _context.Araclar.Remove(AracGetir(ID));
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public Arac AracEkle(Arac arac)
        {
            if(_context.Araclar.FirstOrDefault(a => a.ID == arac.ID) == null)
            {
                _context.Araclar.Add(arac);
                _context.SaveChanges();
                return arac;
            }
            else
            {
                return null;
            }
        }

        public List<Arac> TumAraclariGetir()
        {
            return _context.Araclar.ToList();
        }

        public List<AracModeli> TumModelleriGetir()
        {
            return _context.AracModelleri.ToList();
        }

        public List<AracMarkasi> TumMarkalariGetir()
        {
            return _context.AracMarkalari.ToList();
        }

        public AracModeli ModelGetir(int ID)
        {
            return _context.AracModelleri.FirstOrDefault(a => a.ID == ID);
        }

        public AracMarkasi MarkaGetir(int ID)
        {
            return _context.AracMarkalari.FirstOrDefault(a => a.ID == ID);
        }

        public bool MarkaEkle(AracMarkasi marka)
		{
			if (marka == null)
				return false;

            if(!_context.AracMarkalari.Any(u => u.markaAdi == marka.markaAdi))
            {
			    _context.AracMarkalari.Add(marka);
                _context.SaveChanges();
                return true;
            }
			return false;
		}

        public bool DonanimEkle(AracDonanimi donanim)
        {
            if (donanim == null)
                return false;

            _context.AracDonanimlari.Add(donanim);
            _context.SaveChanges();
            return true;
        }

        public List<AracDonanimi> DonanimlariGetir(int aracID)
        {
            List<AracDonanimi> donanimlar = new List<AracDonanimi>();

            if (_context.AracDonanimlari.Any(a => a.bagliOlduguAracID == aracID) != null)
                donanimlar = _context.AracDonanimlari.Where(a => a.bagliOlduguAracID == aracID).ToList();
            else
                donanimlar = null;

            return donanimlar;
        }

		public bool ModelEkle(AracModeli model)
        {
            if (model == null)
                return false;

            if(!_context.AracModelleri.Any(u => u.modelAdi == model.modelAdi))
            {
                _context.AracModelleri.Add(model);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
	}
}