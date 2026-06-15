namespace assignment3.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }

        public int? MajorId { get; set; }
        public Major? Major { get; set; }

        public int? MentorId { get; set; }
        public Student? Mentor { get; set; }

        public StudentDetails? StudentDetails { get; set; }

        public ICollection<Student> Mentees { get; set; } = new List<Student>();
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
