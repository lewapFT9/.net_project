using Microsoft.EntityFrameworkCore;

namespace TicketReservationApplication.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserAccount> UserAccounts { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<CinemaHall> CinemaHalls { get; set; }
		public DbSet<Screening> Screenings { get; set; }
		public DbSet<ScreeningAttendance> ScreeningAttendances { get; set; }
		public DbSet<Seat> Seats { get; set; }
		public DbSet<Reservation> Reservations { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Reservation>()
				.HasOne(r => r.UserAccount)
				.WithMany() 
				.HasForeignKey(r => r.UserAccountId)
				.OnDelete(DeleteBehavior.NoAction); 

			modelBuilder.Entity<Screening>()
				.HasOne(s => s.Movie)
				.WithMany() 
				.HasForeignKey(s => s.MovieId)
				.OnDelete(DeleteBehavior.NoAction); 

			modelBuilder.Entity<Screening>()
				.HasOne(s => s.CinemaHall)
				.WithMany() 
				.HasForeignKey(s => s.CinemaHallId)
				.OnDelete(DeleteBehavior.NoAction); 

			modelBuilder.Entity<ScreeningAttendance>()
				.HasOne(sa => sa.Screening)
				.WithMany() 
				.HasForeignKey(sa => sa.ScreeningId)
				.OnDelete(DeleteBehavior.NoAction); 

			modelBuilder.Entity<ScreeningAttendance>()
				.HasOne(sa => sa.Movie)
				.WithMany() 
				.HasForeignKey(sa => sa.MovieId)
				.OnDelete(DeleteBehavior.NoAction); 

			modelBuilder.Entity<Seat>()
				.HasOne(s => s.Screening)
				.WithMany() 
				.HasForeignKey(s => s.ScreeningId)
				.OnDelete(DeleteBehavior.NoAction);
            
			modelBuilder.Entity<Seat>()
                .HasOne(s => s.Account)
                .WithMany()
                .HasForeignKey(s => s.UserAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reservation>()
				.HasOne(r => r.Seat)
				.WithMany()
				.HasForeignKey(r => r.SeatId)
				.OnDelete(DeleteBehavior.NoAction); 

			base.OnModelCreating(modelBuilder);
		}


	}
}
