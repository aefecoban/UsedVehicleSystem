using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;

namespace UsedVehicleSystem.Services
{
    public interface IYorumYonetimi
    {
        public Yorum YorumGetir(int ID);
        public List<Yorum> TumYorumlariGetir(int AracSaticisi);
        public Yorum YorumEkle(int AracSaticisi, string Mesaj, string ad);
    }

    public class YorumYonetimi : IYorumYonetimi
    {

        IYorumRepository _yorumRepository;

        public YorumYonetimi(IYorumRepository yorumRepository)
        {
            _yorumRepository = yorumRepository;
        }

        public List<Yorum> TumYorumlariGetir(int AracSaticisi)
        {
            return _yorumRepository.TumYorumlariGetir(AracSaticisi);
        }

        public Yorum YorumGetir(int ID)
        {
            return _yorumRepository.YorumGetir(ID);
        }
        public Yorum YorumEkle(int AracSaticisi, string Mesaj, string ad)
        {
            return _yorumRepository.YorumEkle(AracSaticisi, Mesaj, ad);
        }
    }
}
