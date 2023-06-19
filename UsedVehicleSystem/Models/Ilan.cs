using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedVehicleSystem.Models
{
    public class Ilan
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string ilanAdi { get; set; }

        [ForeignKey("AracSaticisi")]
        public int aracSaticisiID { get; set; }

        [NotMapped]
		public AracSaticisi aracSaticisi { get; set; }

        [ForeignKey("Arac")]
        public int ilandakiAracID { get; set; }

        [NotMapped]
		public Arac ilandakiArac { get; set; }
        public double ilanFiyati { get; set; }

        [NotMapped]
        public List<string> imgSrcs { get; set; }
        public bool yayindaMi { get; set; }
        public bool onayliMi { get; set; }
    }
}
