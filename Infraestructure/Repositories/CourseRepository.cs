using Microsoft.EntityFrameworkCore;
using product_categoryApi.Business.Entities;
using product_categoryApi.Business.Repositories;
using product_categoryApi.Infraestructure.Data;
using System.Collections.Generic;
using System.Linq;


namespace product_categoryApi.Infraestructure.Repositories
{
    public class CourseRepository: ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Course> GetUserCourse(int userId)
        {
            return _context.Courses.Include(i=>i.User).Where(course => course.UserId == userId).ToList();
        }

        public void Store(Course course)
        { 
           _context.Courses.Add(course);
           
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
