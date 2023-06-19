using UsedVehicleSystem.Database;
using UsedVehicleSystem.Models;

namespace UsedVehicleSystem.Repository
{

    public interface ISistemYoneticisiRepository
    {
        public SistemYoneticisi GirisYap(string takmaAd, string sifre);
        public SistemYoneticisi YoneticiGetir(int ID);
    }

    public class SistemYoneticisiRepository : ISistemYoneticisiRepository
    {
        SystemDBContext _context;

        public SistemYoneticisiRepository(SystemDBContext context)
        {
            _context = context;
        }

        public SistemYoneticisi GirisYap(string takmaAd, string sifre)
        {
            return (SistemYoneticisi)_context.SistemYoneticileri.FirstOrDefault(s => s.takmaAd == takmaAd);
        }

        public SistemYoneticisi YoneticiGetir(int ID)
        {
            return _context.SistemYoneticileri.FirstOrDefault(s => s.ID == ID);
        }
    }
}