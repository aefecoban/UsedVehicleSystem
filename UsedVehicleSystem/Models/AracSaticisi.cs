using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedVehicleSystem.Models
{
    public class AracSaticisi : Uye
    {
        /*
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int saticiID { get; set; }
        */

        [ForeignKey("Uye")]
        public int uyeID { get; set; }

        public string telefonNumarasi { get; set; }

        public List<Yorum> yorumlar { get; set; }

        public AracSaticisi(){ }

        public AracSaticisi(Uye uye)
        {
            ID = uye.ID;
            ad = uye.ad;
            soyad = uye.soyad;
            eposta = uye.eposta;
            sifre = uye.sifre;
            uyeID = uye.ID;
        }

        public AracSaticisi(Uye uye, string telefonNumarasi)
        {
            ID = uye.ID;
            ad = uye.ad;
            soyad = uye.soyad;
            eposta = uye.eposta;
            sifre = uye.sifre;
            uyeID = uye.ID;
            this.telefonNumarasi = telefonNumarasi;
        }

        public AracSaticisi(int uyeID, string telefon, List<Yorum> yorumlar)
        {
            
        }
    }
}
