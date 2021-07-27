using Microsoft.AspNetCore.Http;
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
using Microsoft.EntityFrameworkCore;
using product_categoryApi.Infraestructure.Data;

namespace product_categoryApi.Controllers
{
    [Route("api/v1/courses")]
    [ApiController]
    [Authorize]
    public class CourseController : ControllerBase
    {

       ///<summary>
       ///This service gets all courses
       ///</summary>
      ///<returns>Returns status code of 200 and Courses list</returns>
      [SwaggerResponse(statusCode:200,description:"Courses list",Type =typeof(IEnumerable<Course>))]
      [SwaggerResponse(statusCode:401,description:"Courses not found ")]
      [SwaggerResponse(statusCode:500,description:"Opsi! Internal server error",Type =typeof(GenericErrorVMOutput))]
     [HttpGet]
     [Route("")]
        public async Task<IActionResult> Get()
        {
            var authUser = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            List<Course> courses =new List<Course> {
                new Course{
                    Id=1,
                    Name="Entity Framework",
                    Description="Beginers guide into .NET ORM"
                },
                new Course{
                    Id=2,
                    Name="Introduction to ASP.NET",
                    Description="Intermediate level in .net"
                },
                 new Course{
                    Id=2,
                    Name="JWT ",
                    Description="Intermediate level in .net"
                },
                  new Course{
                    Id=3,
                    Name="Visual studio",
                    Description="Intermediate level in .net"
                },
                   new Course{
                    Id=4,
                    Name="Git basics",
                    Description="Intermediate level in .net"
                },
                    new Course{
                    Id=5,
                    Name="Introduction to Github",
                    Description="Intermediate level in .net"
                },
                     new Course{
                    Id=6,
                    Name="PostGrees SQL",
                    Description="Intermediate level in .net"
                }
            };

            return Ok(courses);
        }

        ///<summary>
        ///This service creates a new Course
        ///</summary>
        ///<param name="course">Course Model</param>
        ///<returns>Returns status code of 201 and the created course</returns>
        [SwaggerResponse(statusCode: 201, description: "Course created successfuly", Type = typeof(Course))]
        [SwaggerResponse(statusCode: 400, description:"Validation error", Type = typeof(ValidationErrorVMOutput))]
        [SwaggerResponse(statusCode: 401, description: "Not authorized")]
        [SwaggerResponse(statusCode: 500, description: "Ops! Internal server error", Type = typeof(GenericErrorVMOutput))]
        [HttpPost]
        [Route("create")]
        [ModelStateValidationFilter]
        public async Task<IActionResult> Post(Course course) {

            
            return Created("", course);
        }

    }
}
