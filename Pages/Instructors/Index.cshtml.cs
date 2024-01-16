using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Uni.Models;

namespace Uni.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly Uni.Data.SchoolContext _context;
        public InstructorIndexData InstructorData { get; set; }=new InstructorIndexData();
        public int InstructorID { get; set; }
        public int CourseID { get; set; }
        public IndexModel(Uni.Data.SchoolContext context)
        {
            _context = context;
        }

       

        public async Task OnGetAsync(int? id, int? courseID)
        {
            InstructorData.Instructors = 
                await _context.Instructors
                .Include(x=> x.OfficeAssignment)
                .Include(x=> x.Courses)
                    .ThenInclude(x=> x.Department)
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id!=null)
            {
                InstructorID = id.Value;
                Instructor instructor = InstructorData.Instructors
                    .Where(i => i.ID == id.Value).Single();
                InstructorData.Courses = instructor.Courses;
            }
            if(courseID!=null)
            {
                CourseID = courseID.Value;
               InstructorData.Enrollments =
                    await _context.Enrollments
                    .Where(x=> x.CourseID== courseID.Value)
                    .Include(x=> x.Student)
                    .ToListAsync();

            }
        }
    }
}
