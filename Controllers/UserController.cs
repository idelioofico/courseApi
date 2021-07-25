using Microsoft.AspNetCore.Mvc;
using product_categoryApi.Filter;
using product_categoryApi.Models;
using product_categoryApi.Models.VM;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;

namespace product_categoryApi.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    { 
       ///<summary>
       /// This service authenticates a registered and active user
       /// </summary>
       /// <param name="loginVM">Login View Model </param>
       /// <returns>Returns OK status, user data and token in case of success</returns> 
       [SwaggerResponse(statusCode: 200, description: "Login sucess",Type =typeof(LoginVMInput))]
       [SwaggerResponse(statusCode: 400, description: "Validation error", Type= typeof(ValidationErrorVMOutput))]
       [SwaggerResponse(statusCode: 500, description: "Opsi! Internal server error",Type= typeof(GenericErrorVMOutput))]
       [HttpPost]
       [Route("login")]
       [ModelStateValidationFilter]
        public IActionResult Login(LoginVMInput loginVM)
        {
            /*if (!ModelState.IsValid) {
                return BadRequest(new ValidationErrorVMOutput(ModelState.SelectMany(sm=>sm.Value.Errors).Select(s=>s.ErrorMessage)));
            }*/
            return Ok(loginVM);
        }


        /// <summary>
        /// This service creates a user
        /// </summary>
        /// <param name="registerVMInput">Register View Model</param>
        /// <returns>Returns status code of 201 and created user</returns>
        [SwaggerResponse(statusCode:200,description: "User created successfuly",Type =typeof(RegisterVMInput))]
        [SwaggerResponse(statusCode:400,description:"Validation error",Type =typeof(ValidationErrorVMOutput))]
        [SwaggerResponse(statusCode:500,description:"Opsi! Internal server error",Type =typeof(GenericErrorVMOutput))]
        [Route("register")]
        [HttpPost]
        [ModelStateValidationFilter]
        public IActionResult Register(RegisterVMInput registerVMInput) {
            
            return Created("",registerVMInput);
        }
    }
}
