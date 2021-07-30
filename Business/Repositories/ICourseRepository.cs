using courseApi.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace courseApi.Business.Repositories
{
   public interface ICourseRepository
    {

        void Store(Course course);
        void Commit();
        IList<Course> GetUserCourse(int userId);
    }
}
