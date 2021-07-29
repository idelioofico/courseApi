using Microsoft.IdentityModel.Tokens;
using product_categoryApi.Business.Entities;
using product_categoryApi.Business.Repositories;
using product_categoryApi.Infraestructure.Data;
using product_categoryApi.Models.VM;
using product_categoryApi.Models.VM.User;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace product_categoryApi.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
       private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            this._context = context;
        }


        public void Store(User user)
        {
            _context.Users.Add(user);

        }

        public void Commit() {

            _context.SaveChanges();
        }

        /* public Object Authenticate(LoginVMInput userVMInput,string key)
         {

             /* var userVMOutput = _context.Users.FirstOrDefault(user=>user.Username==userVMInput.Username&&user.Password==userVMInput.Password);

              var secret = Encoding.ASCII.GetBytes(key);
              var symetricSecurityKey = new SymmetricSecurityKey(secret);
              var securityTokenDescriptor = new SecurityTokenDescriptor
              {
                  Subject = new ClaimsIdentity(new Claim[] {
                      new Claim(ClaimTypes.NameIdentifier,userVMOutput.Id.ToString()),
                      new Claim(ClaimTypes.Name,userVMOutput.Username.ToString()),
                      new Claim(ClaimTypes.Email,userVMOutput.Email.ToString())
                  }),
                  Expires = DateTime.UtcNow.AddDays(1),
                  SigningCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
              };

              var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
              var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
              var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);
              var token = _authentication.GenerateToken(tokenGenerated);

             //return new {Token=token,User= userVMInput};
         }*/

        public User GetUser(LoginVMInput loginVMInput)
        {
            return _context.Users.FirstOrDefault(user => user.Username == loginVMInput.Username && user.Password == loginVMInput.Password);
        }
    }
}
