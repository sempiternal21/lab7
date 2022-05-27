using Microsoft.EntityFrameworkCore;
using lab7.Models;

namespace lab7
{
	public class MusiciansDBContext : DbContext
	{
		public MusiciansDBContext() : this(false)
		{ }
		public MusiciansDBContext(bool bFromScratch) : base()
		{
			if (bFromScratch)
			{
				Database.EnsureDeleted();
				Database.EnsureCreated();
			}
		}
		public MusiciansDBContext(DbContextOptions<MusiciansDBContext> options)
		: base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=MusiciansDB;Trusted_Connection=TRUE");
			}
		}
		// Коллекции сущностей
		public DbSet<Musician> Musicians { get; set; }
		public DbSet<Band> Bands { get; set; }
		public DbSet<Instrument> Instruments { get; set; }
	}
}
