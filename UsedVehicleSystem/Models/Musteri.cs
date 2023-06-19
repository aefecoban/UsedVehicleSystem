using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UsedVehicleSystem.Models;

namespace UsedVehicleSystem.Models
{
    public class Musteri : Uye
    {
        /*
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int musteriID { get; set; }
        */

        [ForeignKey("Uye")]
        public int uyeID { get; set; }

        public Musteri()
        {

        }

        public Musteri(Uye uye)
        {
            ad = uye.ad;
            soyad = uye.soyad;
            eposta = uye.eposta;
            sifre = uye.sifre;
            ID = uye.ID;
            uyeID = uye.ID;
        }
    }
}