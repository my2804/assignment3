using assignment3.Models;

namespace assignment3.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Students.Any()) return;

            var majors = new List<Major>
        {
            new Major { Name = "Computer Science", Description = "CS major" },
            new Major { Name = "Mathematics",      Description = "Math major" },
            new Major { Name = "Physics",          Description = "Physics major" },
            new Major { Name = "History",          Description = "History major" },
            new Major { Name = "Biology",          Description = "Biology major" },
        };
            context.Majors.AddRange(majors);

            var courses = new List<Course>
        {
            new Course { Title = "AI Fundamentals",      Description = "Intro to AI",   Credits = 3, Type = "Lecture" },
            new Course { Title = "Biology I",            Description = "Basic Bio",      Credits = 3, Type = "Lecture" },
            new Course { Title = "Calculus I",           Description = "Calc 1",         Credits = 4, Type = "Lecture" },
            new Course { Title = "Data Structures",      Description = "DS course",      Credits = 3, Type = "Lecture" },
            new Course { Title = "Ethics in Science",    Description = "Ethics",         Credits = 2, Type = "Seminar" },
            new Course { Title = "Intro to Programming", Description = "Intro prog",     Credits = 3, Type = "Lecture" },
            new Course { Title = "Linear Algebra",       Description = "Lin Alg",        Credits = 3, Type = "Lecture" },
            new Course { Title = "Modern History",       Description = "History survey", Credits = 3, Type = "Lecture" },
            new Course { Title = "Physics",              Description = "Physics I",      Credits = 4, Type = "Lecture" },
        };
            context.Courses.AddRange(courses);
            context.SaveChanges();

           
            var alice = new Student { Name = "Alice Anderson", MajorId = majors[0].MajorId };
            var bob = new Student { Name = "Bob Bennett", MajorId = majors[1].MajorId };
            var carol = new Student { Name = "Carol Campbell", MajorId = majors[2].MajorId };
            var judy = new Student { Name = "Judy Jones", MajorId = majors[3].MajorId };
            context.Students.AddRange(alice, bob, carol, judy);
            context.SaveChanges();

            
            var dave = new Student { Name = "Dave Dawson", MajorId = majors[0].MajorId, MentorId = alice.StudentId };
            var eve = new Student { Name = "Eve Edwards", MajorId = majors[2].MajorId, MentorId = alice.StudentId };
            var frank = new Student { Name = "Frank Fisher", MajorId = majors[4].MajorId, MentorId = bob.StudentId };
            var grace = new Student { Name = "Grace Green", MajorId = majors[3].MajorId, MentorId = bob.StudentId };
            var heidi = new Student { Name = "Heidi Hughes", MajorId = majors[1].MajorId, MentorId = carol.StudentId };
            var ivan = new Student { Name = "Ivan Iverson", MajorId = majors[0].MajorId, MentorId = carol.StudentId };
            context.Students.AddRange(dave, eve, frank, grace, heidi, ivan);
            context.SaveChanges();

            var details = new List<StudentDetails>
        {
            new StudentDetails { StudentId = alice.StudentId, PhoneNumber = "555-0101", Address = "100 Main Ln"      },
            new StudentDetails { StudentId = bob.StudentId,   PhoneNumber = "555-0103", Address = "150 Elm St"       },
            new StudentDetails { StudentId = carol.StudentId, PhoneNumber = "555-0105", Address = "175 Maple Ave"    },
            new StudentDetails { StudentId = judy.StudentId,  PhoneNumber = "555-0109", Address = "700 Pine Blvd"    },
            new StudentDetails { StudentId = dave.StudentId,  PhoneNumber = "555-0102", Address = "200 Oak Ave"      },
            new StudentDetails { StudentId = eve.StudentId,   PhoneNumber = "555-0104", Address = "250 Birch Rd"     },
            new StudentDetails { StudentId = frank.StudentId, PhoneNumber = "555-0106", Address = "300 Cedar St"     },
            new StudentDetails { StudentId = grace.StudentId, PhoneNumber = "555-0107", Address = "350 Walnut Blvd"  },
            new StudentDetails { StudentId = heidi.StudentId, PhoneNumber = "555-0108", Address = "400 Cherry Ln"    },
            new StudentDetails { StudentId = ivan.StudentId,  PhoneNumber = "555-0110", Address = "450 Poplar Ave"   },
        };
            context.StudentDetails.AddRange(details);

           
            alice.Courses.Add(courses[0]); alice.Courses.Add(courses[3]); alice.Courses.Add(courses[5]);
            bob.Courses.Add(courses[2]); bob.Courses.Add(courses[6]);
            carol.Courses.Add(courses[8]); carol.Courses.Add(courses[4]);
            judy.Courses.Add(courses[7]); judy.Courses.Add(courses[4]);
            dave.Courses.Add(courses[0]); dave.Courses.Add(courses[5]);
            eve.Courses.Add(courses[8]); eve.Courses.Add(courses[3]);
            frank.Courses.Add(courses[1]); frank.Courses.Add(courses[4]);
            grace.Courses.Add(courses[7]);
            heidi.Courses.Add(courses[2]); heidi.Courses.Add(courses[6]);
            ivan.Courses.Add(courses[0]); ivan.Courses.Add(courses[5]);

            context.SaveChanges();
        }
    }
}
