using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;
using UsedVehicleSystem.Services;

namespace UsedVehicleSystem.Facade
{

    public interface IFacadeService
    {
        public (List<AracMarkasi>, List<AracModeli>) AracMarkaModelGetir();
        public Uye UyeGetir(int ID);
        public void YorumEkle(int saticiID, string yorumIcerigi, string musteriAdi);
        public List<Yorum> TumYorumlariGetir(int ID);
        public AracMarkasi AracMarkaGetir(int ID);
        public Arac AracEkle(Arac arac);
        public void AracSil(int aracID);
        public bool AracDonanimiEkle(AracDonanimi donanim);
        public AracModeli AracModelGetir(int ID);
        public Ilan IlanGetir(int ID);
        public List<Ilan> TumIlanlariGetir();
        public List<Ilan> IlanlariListele(AracSaticisi aracSaticisi);
        public Ilan IlanEkle(Ilan ilan, Arac ilandakiArac, AracSaticisi aracSaticisi, string? AracDonanimlari);
        public AracSaticisi? AracSaticisiGetir(int ID);
    }

    public class FacadeService : IFacadeService
    {
        public IIlanYonetimi _ilanYonetimi;
        public IHesapYonetimi _hesapYonetimi;
        public IAracYonetimi _aracYonetimi;
        public IYorumYonetimi _yorumYonetimi;

        public FacadeService(IIlanYonetimi ilanYonetimi, IHesapYonetimi hesapYonetimi, IAracYonetimi aracYonetimi, IYorumYonetimi yorumYonetimi)
        {
            _ilanYonetimi = ilanYonetimi;
            _hesapYonetimi = hesapYonetimi;
            _aracYonetimi = aracYonetimi;
            _yorumYonetimi = yorumYonetimi;
        }

        public (List<AracMarkasi>, List<AracModeli>) AracMarkaModelGetir()
        {
            return (_aracYonetimi.TumMarkalariGetir(), _aracYonetimi.TumModelleriGetir());
        }

        public AracMarkasi AracMarkaGetir(int ID) => _aracYonetimi.MarkaGetir(ID);
        public AracModeli AracModelGetir(int ID) => _aracYonetimi.ModelGetir(ID);
        public Arac AracEkle(Arac arac) => _aracYonetimi.AracEkle(arac);
        public bool AracDonanimiEkle(AracDonanimi donanim) => _aracYonetimi.AracDonanimiEkle(donanim);
        public Ilan IlanEkle(Ilan ilan, Arac ilandakiArac, AracSaticisi aracSaticisi,string? AracDonanimlari)
        {
            ilan.ilandakiAracID = ilandakiArac.ID;
            ilan.ilandakiArac = ilandakiArac;
            ilan.aracSaticisiID = aracSaticisi.ID;
            ilan.aracSaticisi = aracSaticisi;


            List<string> sdonanimlar = AracDonanimlari?.Split(",")?.ToList() ?? new List<string>();
            if (sdonanimlar.Count > 0)
            {
                sdonanimlar.ForEach(donanim =>
                {
                    this.AracDonanimiEkle(new AracDonanimi() { donanimAdi = donanim.Trim(), bagliOlduguAracID = ilandakiArac.ID });
                });
            }

            _ilanYonetimi.IlanEkle(ilan);
            return ilan;
        }

        public Uye UyeGetir(int ID)
        {
            return _hesapYonetimi.UyeGetir(ID);
        }

        public List<Yorum> TumYorumlariGetir(int ID)
        {
            return _yorumYonetimi.TumYorumlariGetir(ID);
        }

        public void YorumEkle(int saticiID, string yorumIcerigi, string musteriAdi)
        {
            _yorumYonetimi.YorumEkle(saticiID, yorumIcerigi, musteriAdi);
        }

        public Ilan IlanGetir(int ID)
        {
            return _ilanYonetimi.IlanGetir(ID);
        }

        public List<Ilan> TumIlanlariGetir()
        {
            return _ilanYonetimi.TumIlanlariGetir();
        }

        public List<Ilan> IlanlariListele(AracSaticisi aracSaticisi)
        {
            return _ilanYonetimi.IlanlariListele(aracSaticisi);
        }
        public AracSaticisi? AracSaticisiGetir(int ID)
        {
            if ((_hesapYonetimi.UyeVarMi(ID) && !_hesapYonetimi.MusteriMi(ID) ? _hesapYonetimi.UyeGetir(ID) : null) == null) return null;
            return _hesapYonetimi.AracSaticisiGetir(ID);
        }
        public void AracSil(int aracID)
        {
            _aracYonetimi.AracSil(aracID);
        }
    }
}
