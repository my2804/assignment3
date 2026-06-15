using System.ComponentModel.DataAnnotations;

namespace assignment3.Models
{
    public class StudentDetails
    {
        [Key]
        public int StudentId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public Student Student { get; set; }
    }
}
