namespace assignment3.Models
{
    public class Major
    {
        public int MajorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
