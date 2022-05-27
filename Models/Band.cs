using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab7.Models
{
    [Table("Bands")]
	public class Band
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Year { get; set; }
		[Required]
		public string Country { get; set; }
	}
}
