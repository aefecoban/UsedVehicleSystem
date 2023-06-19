using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedVehicleSystem.Models
{
    public class AracModeli
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

        [Required]
        public string modelAdi { get; set; }

        [Required]
        public string sanziman { get; set; }

        [Required]
        public string yakitTuru { get; set; }

        [Required]
        public string motorTuru { get; set; }

        [Required]
        public int uretimYili { get; set; }

        [Required]
        public string aracTuru { get; set; }
    }
}
