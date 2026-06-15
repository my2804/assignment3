namespace assignment3.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Credits { get; set; }
        public string Type { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
