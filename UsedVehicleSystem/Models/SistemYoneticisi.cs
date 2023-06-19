using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedVehicleSystem.Models
{
    public class SistemYoneticisi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public string takmaAd { get; set; }

        [Required]
        public string sifre { get; set; }
    }
}
