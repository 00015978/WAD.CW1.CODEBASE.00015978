namespace Core.Entities
{
    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; } // Foreign Key
        public string Subject { get; set; }
        public double Score { get; set; }

        // Navigation Property
        public Student Student { get; set; }
    }
}
