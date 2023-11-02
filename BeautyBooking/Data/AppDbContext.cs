using BeautyBooking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BeautyBooking.Data
{
	public class AppDbContext :DbContext
	{
		//public AppDbContext()
		//{

		//}
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<FreeTime>().HasOne(ft => ft.Record).WithOne(r => r.FreeTime);

			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Client> Clients { get; set; }
		public DbSet<Master> Masters { get; set; }
		public DbSet<Note> Notes { get; set; }
		public DbSet<FreeTime> FreeTimes { get; set; }
		public DbSet<Record> Records { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<GroupOfServices> GroupsOfServices { get; set; }
	}
}
