using Microsoft.EntityFrameworkCore;
//using FreelancePlatform.Domain.Common;
using FreelancePlatform.Domain.Entities;

namespace FreelancePlatform.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Offer> Proposals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>()
                .HasOne(j => j.Client)
                .WithMany()
                .HasForeignKey(j => j.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>()
                .HasOne(p => p.Freelancer)
                .WithMany()
                .HasForeignKey(p => p.FreelancerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>()
                .HasOne(p => p.Job)
                .WithMany(j => j.Proposals)
                .HasForeignKey(p => p.JobId)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Job>(entity =>
    {
        entity.Property(e => e.Budget)
              .HasColumnType("decimal(18,2)");
    });

    modelBuilder.Entity<Offer>(entity =>
    {
        entity.Property(e => e.BidAmount)
              .HasColumnType("decimal(18,2)");
    });
        }
        
    }
}