using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;

namespace UsedVehicleSystem.Services
{
    public interface ISistemYonetimi
    {
        public SistemYoneticisi GirisYap(string takmaAd, string sifre);
        public SistemYoneticisi YoneticiGetir(int ID);
        public List<Ilan> TumIlanlariGetir();
        public Ilan IlanGetir(int ID);
        public bool IlanOnayla(int ID);
        public Arac AracGetir(int ID);
        public bool AracMarkaEkle(AracMarkasi marka);
        public bool AracModelEkle(AracModeli model);
        public List<Arac> TumAraclariGetir();
        public List<AracModeli> TumAracModelleriniGetir();
        public List<AracMarkasi> TumAracMarkalariniGetir();
    }

    public class SistemYonetimi : ISistemYonetimi
    {
        IHesapYonetimi _hesapYonetimi;
        IIlanYonetimi _ilanYonetimi;
        IAracYonetimi _aracYonetimi;
        ISistemYoneticisiRepository _sistemYoneticisiRepository;

        public SistemYonetimi(IHesapYonetimi hesapYonetimi, IIlanYonetimi ilanYonetimi, IAracYonetimi aracYonetimi, ISistemYoneticisiRepository sistemYoneticisiRepository)
        {
            _hesapYonetimi = hesapYonetimi;
            _ilanYonetimi = ilanYonetimi;
            _aracYonetimi = aracYonetimi;
            _sistemYoneticisiRepository = sistemYoneticisiRepository;
        }

        public bool IlanOnayla(int ID) => _ilanYonetimi.IlanOnayla(ID);

        public List<AracModeli> TumAracModelleriniGetir() => _aracYonetimi.TumModelleriGetir();

        public List<AracMarkasi> TumAracMarkalariniGetir() => _aracYonetimi.TumMarkalariGetir();

        public Arac AracGetir(int ID) => _aracYonetimi.AracGetir(ID);

        public bool AracMarkaEkle(AracMarkasi marka) => _aracYonetimi.MarkaEkle(marka);

        public bool AracModelEkle(AracModeli model) => _aracYonetimi.ModelEkle(model);

        public SistemYoneticisi GirisYap(string takmaAd, string sifre) => _sistemYoneticisiRepository.GirisYap(takmaAd, sifre);

        public SistemYoneticisi YoneticiGetir(int ID) => _sistemYoneticisiRepository.YoneticiGetir(ID);

        public Ilan IlanGetir(int ID) => _ilanYonetimi.IlanGetir(ID);

        public List<Arac> TumAraclariGetir() => _aracYonetimi.TumAraclariGetir();

        public List<Ilan> TumIlanlariGetir() => _ilanYonetimi.TumIlanlariGetir();
    }
}
