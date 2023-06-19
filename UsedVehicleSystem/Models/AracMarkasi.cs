using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UsedVehicleSystem.Models
{
    public class AracMarkasi
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { get; set; }

        [Required]
        public string markaAdi { get; set; }
    }
}
