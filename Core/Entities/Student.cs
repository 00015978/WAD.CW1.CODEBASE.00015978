namespace Core.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }

        // Navigation Property
        public ICollection<Grade> Grades { get; set; }
    }
}
