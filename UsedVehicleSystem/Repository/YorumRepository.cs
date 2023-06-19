using UsedVehicleSystem.Database;
using UsedVehicleSystem.Models;

namespace UsedVehicleSystem.Repository
{
    public interface IYorumRepository
    {
        public Yorum YorumGetir(int ID);
        public List<Yorum> TumYorumlariGetir(int AracSaticisi);
        public Yorum YorumEkle(int AracSaticisi, string Mesaj, string ad);
    }

    public class YorumRepository : IYorumRepository
    {
        private SystemDBContext dbContext;

        public YorumRepository(SystemDBContext context)
        {
            dbContext = context;
        }

        public List<Yorum> TumYorumlariGetir(int AracSaticisi)
        {
            return dbContext.Yorumlar.Where(y => y.AracSaticisiID == AracSaticisi).ToList();
        }

        public Yorum YorumEkle(int AracSaticisi, string Mesaj, string ad)
        {
            Yorum yorum = new Yorum() {
                AracSaticisiID = AracSaticisi,
                yorumIcerigi = Mesaj,
                musteriAdi = ad,
            };

            dbContext.Yorumlar.Add(yorum);
            dbContext.SaveChanges();

            return yorum;
        }

        public Yorum YorumGetir(int ID)
        {
            return dbContext.Yorumlar.FirstOrDefault(y => y.yorumID == ID);
        }
    }
}
