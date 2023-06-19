using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;

namespace UsedVehicleSystem.Services
{
    public interface IAracYonetimi
    {
        public bool MarkaEkle(AracMarkasi aracMarkasi);
        public bool ModelEkle(AracModeli aracModeli);
        public List<Arac> TumAraclariGetir();
        public List<AracMarkasi> TumMarkalariGetir();
        public List<AracModeli> TumModelleriGetir();
        public Arac AracGetir(int ID);
        public AracModeli ModelGetir(int ID);
        public AracMarkasi MarkaGetir(int ID);
        public Arac AracEkle(Arac arac);
        public bool AracSil(int ID);
        public bool AracDonanimiEkle(AracDonanimi donanim);
    }

    public class AracYonetimi : IAracYonetimi
    {
        IAracRepository _aracRepository;

        public AracYonetimi(IAracRepository aracRepository)
        {
            _aracRepository = aracRepository;
        }

        public Arac AracGetir(int ID) => _aracRepository.AracGetir(ID);

        public Arac AracEkle(Arac arac) => _aracRepository.AracEkle(arac);

        public bool AracSil(int ID) => _aracRepository.AracSil(ID);

        public bool MarkaEkle(AracMarkasi aracMarkasi) => _aracRepository.MarkaEkle(aracMarkasi);

        public bool ModelEkle(AracModeli aracModeli) => _aracRepository.ModelEkle(aracModeli);

        public bool AracDonanimiEkle(AracDonanimi donanim) => _aracRepository.DonanimEkle(donanim);

        public List<AracMarkasi> TumMarkalariGetir() => _aracRepository.TumMarkalariGetir();

        public List<AracModeli> TumModelleriGetir() => _aracRepository.TumModelleriGetir();

        public List<Arac> TumAraclariGetir() => _aracRepository.TumAraclariGetir();

        public AracModeli ModelGetir(int ID) => _aracRepository.ModelGetir(ID);

        public AracMarkasi MarkaGetir(int ID) => _aracRepository.MarkaGetir(ID);
    }
}
