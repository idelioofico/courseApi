using Microsoft.AspNetCore.Mvc;
using product_categoryApi.Filter;
using product_categoryApi.Models;
using product_categoryApi.Models.VM;
using Swashbuckle.AspNetCore.Annotations;
using product_categoryApi.Models.VM.User;
using product_categoryApi.Business.Entities;
using product_categoryApi.Business.Repositories;
using product_categoryApi.Configurations;

namespace product_categoryApi.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authentication;

        public UserController(IUserRepository userRepository, IAuthenticationService authentication)
        {
            _userRepository = userRepository;
            _authentication = authentication;
        }

        ///<summary>
        /// This service authenticates a registered and active user
        /// </summary>
        /// <param name="loginVM">Login View Model </param>
        /// <returns>Returns OK status, user data and token in case of success</returns> 
        [SwaggerResponse(statusCode: 200, description: "Login sucess", Type = typeof(LoginVMInput))]
        [SwaggerResponse(statusCode: 400, description: "Validation error", Type = typeof(ValidationErrorVMOutput))]
        [SwaggerResponse(statusCode: 500, description: "Opsi! Internal server error", Type = typeof(GenericErrorVMOutput))]
        [HttpPost]
        [Route("login")]
        [ModelStateValidationFilter]

        public IActionResult Login(LoginVMInput loginVMInput)
        {

            var user = _userRepository.GetUser(loginVMInput);

            if (user == null) {
                return BadRequest("Opsi, an error ocurred trying to login");
            }
            var userOutput = new UserVMOutput()
            {
                Id=user.Id,
                Username=user.Username,
                Email=user.Email
            };
            var token = _authentication.GenerateToken(userOutput); 

            return Ok(new { Token = token, User = userOutput });
        }


        /// <summary>
        /// This service creates a user
        /// </summary>
        /// <returns>Returns status code of 201 and created user</returns>
        [SwaggerResponse(statusCode: 200, description: "User created successfuly", Type = typeof(RegisterVMInput))]
        [SwaggerResponse(statusCode: 400, description: "Validation error", Type = typeof(ValidationErrorVMOutput))]
        [SwaggerResponse(statusCode: 500, description: "Opsi! Internal server error", Type = typeof(GenericErrorVMOutput))]
        [Route("register")]
        [HttpPost]
        [ModelStateValidationFilter]
        public IActionResult Register(RegisterVMInput registerVMInput)
        {

            var user = new User()
            {
                Username = registerVMInput.Username,
                Email = registerVMInput.Email,
                Password = registerVMInput.Password
            };

            _userRepository.Store(user);
            _userRepository.Commit();

            return Created("", user);

        }
    }
}
