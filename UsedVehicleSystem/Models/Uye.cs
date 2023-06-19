using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedVehicleSystem.Models
{
	public class Uye
	{
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
		public string ad { get; set; }

        [Required]
        public string soyad { get; set; }

        [Required]
        public string eposta { get; set; }

        [Required]
        public string sifre { get; set; }

        public Uye() { }

        public Uye(string ad, string soyad, string eposta, string sifre) {
            this.ad = ad;
            this.soyad = soyad;
            this.eposta = eposta;
            this.sifre = sifre;
        }
    }
}
