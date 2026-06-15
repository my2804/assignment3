using assignment3.Data;
using assignment3.Models;
using Microsoft.AspNetCore.Mvc;

namespace assignment3.Controllers
{
    using assignment3.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class StudentsController : Controller
    {
        private readonly AppDbContext _db;
        public StudentsController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index(string? name, int? majorId, int? mentorId)
        {
            var query = _db.Students
                .Include(s => s.Major)
                .Include(s => s.Mentor)
                .Include(s => s.StudentDetails)
                .Include(s => s.Courses)
                .Include(s => s.Mentees)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(s => s.Name.Contains(name));
            if (majorId.HasValue)
                query = query.Where(s => s.MajorId == majorId);
            if (mentorId.HasValue)
                query = query.Where(s => s.MentorId == mentorId);

            ViewBag.Majors = new SelectList(await _db.Majors.ToListAsync(), "MajorId", "Name");
            ViewBag.Mentors = new SelectList(await _db.Students.ToListAsync(), "StudentId", "Name");
            ViewBag.NameFilter = name;
            ViewBag.MajorFilter = majorId;
            ViewBag.MentorFilter = mentorId;

            return View(await query.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _db.Students
                .Include(s => s.Major)
                .Include(s => s.Mentor)
                .Include(s => s.StudentDetails)
                .Include(s => s.Courses)
                .Include(s => s.Mentees)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null) return NotFound();
            return View(student);
        }

        public async Task<IActionResult> Create()
        {
            return View(await BuildFormViewModel(null));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentFormModel vm)
        {
            if (!ModelState.IsValid)
                return View(await BuildFormViewModel(null, vm));

            var student = new Student
            {
                Name = vm.Name,
                MajorId = vm.MajorId,
                MentorId = vm.MentorId,
                StudentDetails = new StudentDetails
                {
                    PhoneNumber = vm.PhoneNumber,
                    Address = vm.Address
                }
            };

            if (vm.SelectedCourseIds?.Any() == true)
            {
                var courses = await _db.Courses
                    .Where(c => vm.SelectedCourseIds.Contains(c.CourseId))
                    .ToListAsync();
                student.Courses = courses;
            }

            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _db.Students
                .Include(s => s.StudentDetails)
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null) return NotFound();

            var vm = new StudentFormModel
            {
                StudentId = student.StudentId,
                Name = student.Name,
                MajorId = student.MajorId,
                MentorId = student.MentorId,
                PhoneNumber = student.StudentDetails?.PhoneNumber,
                Address = student.StudentDetails?.Address,
                SelectedCourseIds = student.Courses.Select(c => c.CourseId).ToList()
            };

            return View(await BuildFormViewModel(id, vm));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, StudentFormModel vm)
        {
            if (!ModelState.IsValid)
                return View(await BuildFormViewModel(id, vm));

            var student = await _db.Students
                .Include(s => s.StudentDetails)
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null) return NotFound();

            student.Name = vm.Name;
            student.MajorId = vm.MajorId;
            student.MentorId = vm.MentorId;

            if (student.StudentDetails == null)
                student.StudentDetails = new StudentDetails { StudentId = id };

            student.StudentDetails.PhoneNumber = vm.PhoneNumber;
            student.StudentDetails.Address = vm.Address;

            var selectedCourses = await _db.Courses
                .Where(c => vm.SelectedCourseIds.Contains(c.CourseId))
                .ToListAsync();
            student.Courses.Clear();
            foreach (var c in selectedCourses) student.Courses.Add(c);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _db.Students
                .Include(s => s.Major)
                .Include(s => s.Mentor)
                .Include(s => s.Courses)
                .Include(s => s.Mentees)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _db.Students
                .Include(s => s.StudentDetails)
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.StudentId == id);

            if (student == null) return NotFound();

            var mentees = await _db.Students.Where(s => s.MentorId == id).ToListAsync();
            foreach (var m in mentees) m.MentorId = null;

            student.Courses.Clear();
            if (student.StudentDetails != null) _db.StudentDetails.Remove(student.StudentDetails);
            _db.Students.Remove(student);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<StudentFormModel> BuildFormViewModel(int? editingId, StudentFormModel? vm = null)
        {
            vm ??= new StudentFormModel();

            var mentorQuery = _db.Students.AsQueryable();
            if (editingId.HasValue)
                mentorQuery = mentorQuery.Where(s => s.StudentId != editingId.Value);

            vm.Majors = new SelectList(await _db.Majors.ToListAsync(), "MajorId", "Name", vm.MajorId);
            vm.Mentors = new SelectList(await mentorQuery.ToListAsync(), "StudentId", "Name", vm.MentorId);
            vm.AllCourses = await _db.Courses.OrderBy(c => c.Title).ToListAsync();

            return vm;
        }
    }
}

