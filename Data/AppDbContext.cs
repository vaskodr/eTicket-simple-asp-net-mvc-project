using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using finalTicket.Models;

namespace finalTicket.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany()
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Ticket)
                .WithMany()
                .HasForeignKey(o => o.TicketId);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Movie)
                .WithMany(m => m.Tickets)
                .HasForeignKey(t => t.MovieId);
        }
    }
}