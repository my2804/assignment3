namespace assignment3.ViewModels
{
    using assignment3.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class StudentFormModel
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int? MajorId { get; set; }
        public int? MentorId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public List<int> SelectedCourseIds { get; set; } = new();

        public SelectList Majors { get; set; }
        public SelectList Mentors { get; set; }
        public List<Course> AllCourses { get; set; } = new();
    }
}

