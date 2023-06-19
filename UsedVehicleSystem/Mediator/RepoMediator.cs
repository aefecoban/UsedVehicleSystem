using System.Collections.Generic;
using UsedVehicleSystem.Models;
using UsedVehicleSystem.Repository;

namespace UsedVehicleSystem.Mediator
{
    public interface IRepoMediator
    {
        public Arac AracGetir(int ID);
        public Uye UyeGetir(int UyeID);
        public Ilan IlanGetir(int IlanID);
        public List<Ilan> IlanAra(Ilan filtrelenmisIlan);
    }

    public class RepoMediator : IRepoMediator
    {
        IUyeRepository uyeRepository;
        IAracRepository aracRepository;
        IIlanRepository ilanRepository;
        ISistemYoneticisiRepository sistemYoneticisiRepository;

        public RepoMediator(IUyeRepository uyeRepository, IAracRepository aracRepository, IIlanRepository ilanRepository, ISistemYoneticisiRepository sistemYoneticisiRepository)
        {
            this.uyeRepository = uyeRepository;
            this.aracRepository = aracRepository;
            this.ilanRepository = ilanRepository;
            this.sistemYoneticisiRepository = sistemYoneticisiRepository;
        }

        public Ilan IlanGetir(int IlanID)
        {
            Ilan dondurulecekIlan = this.ilanRepository.IlanGetir(IlanID);
            dondurulecekIlan.ilandakiArac = this.aracRepository.AracGetir(dondurulecekIlan.ilandakiAracID);
            dondurulecekIlan.aracSaticisi = this.uyeRepository.AracSaticisiGetir(dondurulecekIlan.aracSaticisiID);
            return dondurulecekIlan;
        }

        public List<Ilan> IlanAra(Ilan filtrelenmisIlan)
        {
            List<Ilan> ilanlar = ilanRepository.TumIlanlariGetir();
            List<Ilan> sonuclar = new List<Ilan>();

            foreach (Ilan i in ilanlar)
            {
                i.ilandakiArac = aracRepository.AracGetir(i.ilandakiAracID);
                i.ilandakiArac.aracMarkasi = aracRepository.MarkaGetir(i.ilandakiArac.aracMarkasiID);
                i.ilandakiArac.aracModeli = aracRepository.ModelGetir(i.ilandakiArac.aracModeliID);
                if (aracRepository.DonanimlariGetir(i.ilandakiArac.ID) != null)
                    i.ilandakiArac.aracDonanimlari = aracRepository.DonanimlariGetir(i.ilandakiArac.ID);
            }
            foreach (Ilan i in ilanlar)
            {
                if ((!string.IsNullOrEmpty(filtrelenmisIlan.ilanAdi) && i.ilanAdi.Contains(filtrelenmisIlan.ilanAdi))
                || (!string.IsNullOrEmpty(filtrelenmisIlan.ilandakiArac.aracModeli.sanziman) && i.ilanAdi.Contains(filtrelenmisIlan.ilandakiArac.aracModeli.sanziman))
                || (!string.IsNullOrEmpty(filtrelenmisIlan.ilandakiArac.aracModeli.yakitTuru) && i.ilanAdi.Contains(filtrelenmisIlan.ilandakiArac.aracModeli.yakitTuru))
                || (!string.IsNullOrEmpty(filtrelenmisIlan.ilandakiArac.aracModeli.motorTuru) && i.ilanAdi.Contains(filtrelenmisIlan.ilandakiArac.aracModeli.motorTuru))
                || (!string.IsNullOrEmpty(filtrelenmisIlan.ilandakiArac.aracModeli.aracTuru) && i.ilandakiArac.aracModeli.aracTuru == filtrelenmisIlan.ilandakiArac.aracModeli.aracTuru)
                || (!string.IsNullOrEmpty(filtrelenmisIlan.ilandakiArac.aracMarkasi.markaAdi) && i.ilandakiArac.aracMarkasi.markaAdi == filtrelenmisIlan.ilandakiArac.aracMarkasi.markaAdi))
                {
                    sonuclar.Add(i);
                }
            }
            return sonuclar;
        }

        public Uye UyeGetir(int UyeID)
        {
            return this.uyeRepository.UyeGetir(UyeID);
        }

        public Arac AracGetir(int ID)
        {
            return this.aracRepository.AracGetir(ID);
        }
    }
}
