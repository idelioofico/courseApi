using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using product_categoryApi.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_categoryApi.Configurations
{
    public class DbFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        /*  public ApplicationDbContext CreateDbContext(string[] args)
          {
             // var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
              //optionsBuilder.UseSqlServer("Server=localhost;Database=Curso;User=sa;Password=<oficoidelio@gmail.com>");
              //ApplicationDbContext context = new ApplicationDbContext();
              return context;
          }*/
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
