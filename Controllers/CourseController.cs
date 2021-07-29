using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using product_categoryApi.Models;
using product_categoryApi.Filter;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using product_categoryApi.Business.Entities;
using product_categoryApi.Business.Repositories;

namespace product_categoryApi.Controllers
{

    [Route("api/v1/courses")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {

        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        ///<summary>
        ///This service gets all courses
        ///</summary>
        ///<returns>Returns status code of 200 and Courses list</returns>
        [SwaggerResponse(statusCode:200,description:"Courses list",Type =typeof(IEnumerable<CourseVMInput>))]
      [SwaggerResponse(statusCode:401,description:"Courses not found ")]
      [SwaggerResponse(statusCode:500,description:"Opsi! Internal server error",Type =typeof(GenericErrorVMOutput))]
     [HttpGet]
     [Route("")]
        public async Task<IActionResult> Get()
        {
           
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var cursos = _courseRepository.GetUserCourse(userId).Select(course=>new CourseVMOutput(){
            Name=course.Name,
            Description=course.Description,
            Username=course.User.Username
            });
            return Ok(cursos);

        }

        ///<summary>
        ///This service creates a new Course
        ///</summary>
        ///<param name="course">Course Model</param>
        ///<returns>Returns status code of 201 and the created course</returns>
        [SwaggerResponse(statusCode: 201, description: "Course created successfuly", Type = typeof(CourseVMInput))]
        [SwaggerResponse(statusCode: 400, description:"Validation error", Type = typeof(ValidationErrorVMOutput))]
        [SwaggerResponse(statusCode: 401, description: "Not authorized")]
        [SwaggerResponse(statusCode: 500, description: "Ops! Internal server error", Type = typeof(GenericErrorVMOutput))]
        [HttpPost]
        [Route("create")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Post(CourseVMInput course) {

            var courseInput = new Course()
            {
                Name=course.Name,
                Description=course.Description,
                UserId=int.Parse(User.FindFirst(user=>user.Type==ClaimTypes.NameIdentifier)?.Value)
            };

            _courseRepository.Store(courseInput);
            _courseRepository.Commit();


            return Created("", courseInput);
        }

    }
}
