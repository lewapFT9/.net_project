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
			// Relacja UserAccount <-> Reservation
			modelBuilder.Entity<Reservation>()
				.HasOne(r => r.UserAccount)
				.WithMany() // Brak kolekcji nawigacyjnej w UserAccount
				.HasForeignKey(r => r.UserAccountId)
				.OnDelete(DeleteBehavior.NoAction); // NoAction dla UserAccount

			// Relacja Movie <-> Screening
			modelBuilder.Entity<Screening>()
				.HasOne(s => s.Movie)
				.WithMany() // Brak kolekcji nawigacyjnej w Movie
				.HasForeignKey(s => s.MovieId)
				.OnDelete(DeleteBehavior.NoAction); // NoAction dla Movie

			// Relacja CinemaHall <-> Screening
			modelBuilder.Entity<Screening>()
				.HasOne(s => s.CinemaHall)
				.WithMany() // Brak kolekcji nawigacyjnej w CinemaHall
				.HasForeignKey(s => s.CinemaHallId)
				.OnDelete(DeleteBehavior.NoAction); // NoAction dla CinemaHall

			// Relacja ScreeningAttendance <-> Screening
			modelBuilder.Entity<ScreeningAttendance>()
				.HasOne(sa => sa.Screening)
				.WithMany() // Brak kolekcji nawigacyjnej w Screening
				.HasForeignKey(sa => sa.ScreeningId)
				.OnDelete(DeleteBehavior.NoAction); // NoAction dla ScreeningAttendance -> Screening

			// Relacja ScreeningAttendance <-> Movie
			modelBuilder.Entity<ScreeningAttendance>()
				.HasOne(sa => sa.Movie)
				.WithMany() // Brak kolekcji nawigacyjnej w Movie
				.HasForeignKey(sa => sa.MovieId)
				.OnDelete(DeleteBehavior.NoAction); // NoAction dla ScreeningAttendance -> Movie

			// Relacja Seat <-> Screening
			modelBuilder.Entity<Seat>()
				.HasOne(s => s.Screening)
				.WithMany() // Brak kolekcji nawigacyjnej w Screening
				.HasForeignKey(s => s.ScreeningId)
				.OnDelete(DeleteBehavior.NoAction); // NoAction dla Seat -> Screening

			// Relacja Reservation <-> Seat
			modelBuilder.Entity<Reservation>()
				.HasOne(r => r.Seat)
				.WithMany() // Brak kolekcji nawigacyjnej w Seat
				.HasForeignKey(r => r.SeatId)
				.OnDelete(DeleteBehavior.NoAction); // NoAction dla Reservation -> Seat

			base.OnModelCreating(modelBuilder);
		}


	}
}
