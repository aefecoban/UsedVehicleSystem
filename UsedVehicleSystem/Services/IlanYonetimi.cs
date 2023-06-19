using UsedVehicleSystem.Database;
using UsedVehicleSystem.Mediator;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;

namespace UsedVehicleSystem.Services
{
	public interface IIlanYonetimi
	{
		public void IlanYayindanKaldir(Ilan ilan);
		public List<Ilan> IlanAra(Ilan filtrelenmisIlan);
		public Ilan IlanGuncelle(int ilanID, Ilan guncelIlan);
		public List<Ilan> TumIlanlariGetir();
		public List<Ilan> IlanlariListele(AracSaticisi saticisi);
		public Ilan IlanEkle(Ilan ilan);
		public bool IlanSil(int ID);
		public Ilan IlanGetir(int ilanID);
		public List<Ilan> IlanKarsiasitr(Ilan ilan1, Ilan ilan2);
		public bool IlanOnayla(int ID);
    }

	public class IlanYonetimi : IIlanYonetimi
	{
		private IIlanRepository _ilanRepository;
        private IRepoMediator _repoMediator;

        public IlanYonetimi(IRepoMediator repoMediator, IIlanRepository ilanRepository)
		{
			_repoMediator = repoMediator;
            _ilanRepository = ilanRepository;
		}

        public void IlanYayindanKaldir(Ilan ilan)
		{
			_ilanRepository.IlanSil(ilan.ID);
		}

        public bool IlanOnayla(int ID) => _ilanRepository.IlanOnayla(ID);

        public List<Ilan> IlanAra(Ilan filtrelenmisIlan)
        {
            return _repoMediator.IlanAra(filtrelenmisIlan);
        }

        public Ilan IlanGuncelle(int ilanID, Ilan guncelIlan)
        {
			return _ilanRepository.IlanGuncelle(ilanID, guncelIlan);
        }

        public List<Ilan> TumIlanlariGetir()
        {
            return _ilanRepository.TumIlanlariGetir();
        }

        public List<Ilan> IlanlariListele(AracSaticisi saticisi)
		{
			return _ilanRepository.IlanlariGetir(saticisi.ID);
		}

        public Ilan IlanEkle(Ilan ilan)
		{
			return _ilanRepository.IlanEkle(ilan);
		}

        public bool IlanSil(int ID)
		{
			return _ilanRepository.IlanSil(ID);
		}

		public Ilan IlanGetir(int ilanID)
        {
            return _repoMediator.IlanGetir(ilanID);
		}

		public List<Ilan> IlanKarsiasitr(Ilan ilan1, Ilan ilan2)
		{
			throw new NotImplementedException();
		}
	}
}
