using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for Entities
        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuring the Student Entity
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FirstName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.LastName)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.DateOfBirth)
                      .IsRequired();
            });

            // Configuring the Grade Entity
            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Subject)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Score)
                      .IsRequired();

                // Setting up the Foreign Key
                entity.HasOne(e => e.Student)
                      .WithMany(s => s.Grades)
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
