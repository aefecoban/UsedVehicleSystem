using UsedVehicleSystem.Database;
using UsedVehicleSystem.Mediator;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UsedVehicleSystem.Repository
{
	public interface IIlanRepository
	{
		Ilan IlanEkle(Ilan ilan);
		bool IlanSil(int ID);
		Ilan IlanGuncelle(int ID, Ilan ilan);
		List<Ilan> TumIlanlariGetir();
		public List<Ilan> IlanlariGetir(int saticiID);
        public Ilan IlanGetir(int ID);
		public bool IlanOnayla(int ID);
    }

	public class IlanRepository : IIlanRepository
	{
		SystemDBContext _context;
		//private IAracRepository _aracRepository;


        public IlanRepository(SystemDBContext context, IAracRepository aracRepository)
        {
            _context = context;
            //_aracRepository = aracRepository;
        }

        public bool IlanOnayla(int ID)
		{
			if(_context.Ilanlar.FirstOrDefault(i=>i.ID == ID) == null)
			{
				return false;
			}
			else
			{
				Ilan onaylanacakIlan = _context.Ilanlar.FirstOrDefault(i => i.ID == ID);

				if(onaylanacakIlan.onayliMi)
				{
					return true;
				}
				else
				{
					onaylanacakIlan.onayliMi = true;
					onaylanacakIlan.yayindaMi = true;
					_context.Ilanlar.Update(onaylanacakIlan);
					_context.SaveChanges();
					return true;
				}

            }
			return true;
		}

        public Ilan IlanGuncelle(int ID, Ilan ilan)
		{
			_context.Ilanlar.Update(ilan);
			if(ilan.ilandakiArac !=null)
            {
                _context.Araclar.Update(ilan.ilandakiArac);
                if (ilan.ilandakiArac.aracMarkasi != null)
                    _context.AracMarkalari.Update(ilan.ilandakiArac.aracMarkasi);
				if(ilan.ilandakiArac.aracModeli !=null)
                    _context.AracModelleri.Update(ilan.ilandakiArac.aracModeli);
            }

			_context.SaveChanges();

			return ilan;


            /*
			Ilan guncellenecekIlan = _context.Ilanlar.FirstOrDefault(i => i.ID == ID);

            if (guncellenecekIlan == null)
			{
				return null;
			}
			else
            {
                guncellenecekIlan.ilanAdi = ilan.ilanAdi;
                guncellenecekIlan.ilanFiyati = ilan.ilanFiyati;
                guncellenecekIlan.yayindaMi = false;
                guncellenecekIlan.onayliMi = false;

                Arac arac = _context.Araclar.FirstOrDefault(a => a.ID == guncellenecekIlan.ilandakiAracID);
                arac.aracMarkasiID = ilan.ilandakiArac.aracMarkasiID;
                arac.aracModeliID = ilan.ilandakiArac.aracModeliID;
                arac.aracMarkasi = _aracRepository.MarkaGetir(arac.aracMarkasiID);
                arac.aracModeli = _aracRepository.ModelGetir(arac.aracModeliID);


				_context.Araclar.Update(arac);
                _context.Ilanlar.Update(guncellenecekIlan);
				_context.SaveChanges();
				return guncellenecekIlan;
			}*/
        }

		public Ilan IlanEkle(Ilan ilan)
		{
			if(_context.Ilanlar.FirstOrDefault(i => i.ID == ilan.ID) == null)
			{
                _context.Ilanlar.Add(ilan);
				_context.SaveChanges();
				return ilan;
			}
			else
			{
				return null;
			}
		}

		public Ilan IlanGetir(int ilanID)
		{
			return _context.Ilanlar.FirstOrDefault(i => i.ID == ilanID);
        }

        public List<Ilan> TumIlanlariGetir()
        {
            return _context.Ilanlar.ToList();
        }

        public List<Ilan> IlanlariGetir(int saticiID)
		{
			return _context.Ilanlar.Where(i => i.aracSaticisiID == saticiID).ToList();
		}

        public bool IlanSil(int ID)
		{
			if(_context.Ilanlar.FirstOrDefault(i => i.ID == ID) == null)
				return false;

			_context.Ilanlar.Remove(_context.Ilanlar.FirstOrDefault(i => i.ID == ID));
			_context.SaveChanges();
			return true;
		}

    }
}
