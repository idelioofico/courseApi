using product_categoryApi.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_categoryApi.Business.Repositories
{
   public interface ICourseRepository
    {

        void Store(Course course);
        void Commit();
        IList<Course> GetUserCourse(int userId);
    }
}
