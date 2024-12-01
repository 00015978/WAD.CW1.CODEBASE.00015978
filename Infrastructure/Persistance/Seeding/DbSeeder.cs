using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Seeding
{
    public static class DbSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(2000, 5, 1) },
                new Student { Id = 2, FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateTime(1999, 8, 15) }
            );

            // Seed Grades
            modelBuilder.Entity<Grade>().HasData(
                new Grade { Id = 1, StudentId = 1, Subject = "Math", Score = 95.5 },
                new Grade { Id = 2, StudentId = 1, Subject = "Science", Score = 88.0 },
                new Grade { Id = 3, StudentId = 2, Subject = "Math", Score = 76.0 },
                new Grade { Id = 4, StudentId = 2, Subject = "History", Score = 84.5 }
            );
        }
    }
}
