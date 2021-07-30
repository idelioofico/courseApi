using Microsoft.EntityFrameworkCore;
using courseApi.Business.Entities;
using courseApi.Configurations;
using courseApi.Infraestructure.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace courseApi.Infraestructure.Data
{
    public class ApplicationDbContext:DbContext
    {
     
    
 
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new CourseMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}
