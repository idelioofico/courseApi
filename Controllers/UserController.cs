using Microsoft.AspNetCore.Mvc;
using product_categoryApi.Filter;
using product_categoryApi.Models;
using product_categoryApi.Models.VM;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Text;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using product_categoryApi.Models.VM.User;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using product_categoryApi.Infraestructure.Data;
using product_categoryApi.Business.Entities;

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

            var userVMOutput = new UserVMOutput {
                Id = 1,
                Username="iofico",
                Email="oficoidelio@gmail.com",
            };

            var secret = Encoding.ASCII.GetBytes("Mz@{djaloOfico}!3$2021\\API");
            var symetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier,userVMOutput.Id.ToString()),
                    new Claim(ClaimTypes.Name,userVMOutput.Username.ToString()),
                    new Claim(ClaimTypes.Email,userVMOutput.Email.ToString())
                }),
                Expires=DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);

            return Ok(new {
            Token=token,
            User=userVMOutput
            });
        }


        /// <summary>
        /// This service creates a user
        /// </summary>
        /// <returns>Returns status code of 201 and created user</returns>
        [SwaggerResponse(statusCode:200,description: "User created successfuly",Type =typeof(RegisterVMInput))]
        [SwaggerResponse(statusCode:400,description:"Validation error",Type =typeof(ValidationErrorVMOutput))]
        [SwaggerResponse(statusCode:500,description:"Opsi! Internal server error",Type =typeof(GenericErrorVMOutput))]
        [Route("register")]
        [HttpPost]
        [ModelStateValidationFilter]
        public IActionResult Register(RegisterVMInput registerVMInput) {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlServer("Server=localhost;Database=Curso;User=sa;Password=<oficoidelio@gmail.com>");
            ApplicationDbContext context = new ApplicationDbContext(options);

            var pendingMigrations = context.Database.GetPendingMigrations();

            if (pendingMigrations.Count() > 0) {

                context.Database.Migrate();
            }

            var user = new User()
            {
                Username=registerVMInput.Username,
                Email=registerVMInput.Email,
                Password=registerVMInput.Password
            };

            context.Users.Add(user);
            context.SaveChanges();

            return Created("",user);
        }
    }
}
