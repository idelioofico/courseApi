using product_categoryApi.Business.Entities;
using product_categoryApi.Business.Repositories;
using product_categoryApi.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
            _context.SaveChanges();
        }

        public void Commit() {

            _context.SaveChanges();
        }
    }
}
