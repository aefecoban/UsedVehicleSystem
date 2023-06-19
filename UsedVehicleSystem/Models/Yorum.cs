using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedVehicleSystem.Models
{
    public class Yorum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int yorumID { get; set; }

        [ForeignKey("AracSaticisi")]
        public int AracSaticisiID { get; set; }

        [Required]
        public string musteriAdi { get; set; }

        [Required]
        public string yorumIcerigi { get; set; }
    }
}
