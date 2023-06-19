using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedVehicleSystem.Models
{
    public class AracDonanimi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Arac")]
        public int bagliOlduguAracID { get; set; }

        [Required]
        public string donanimAdi { get; set; }
    }
}
