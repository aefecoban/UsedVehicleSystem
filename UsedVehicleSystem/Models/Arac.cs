using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsedVehicleSystem.Models
{
    public class Arac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int kilometre { get; set; }

        [ForeignKey("AracModeli")]
        public int aracModeliID { get; set; }

        [ForeignKey("AracMarkasi")]
        public int aracMarkasiID { get; set; }

        [NotMapped]
        public AracModeli aracModeli { get; set; }

        [NotMapped]
        public AracMarkasi aracMarkasi { get; set; }

        [NotMapped]
        public List<AracDonanimi> aracDonanimlari { get; set; }
    }
}
