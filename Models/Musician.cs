using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab7.Models
{
	[Table("Musicians")]
	public class Musician
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Age { get; set; }

		[Required]
		[ForeignKey("Instruments")]
		public string Instrument { get; set; }

		[Required]
		[ForeignKey("Bands")]
		public string Band { get; set; }

	}
}
