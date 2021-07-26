using Microsoft.EntityFrameworkCore;
using product_categoryApi.Business.Entities;
using product_categoryApi.Infraestructure.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace product_categoryApi.Infraestructure.Data
{
    public class ApplicationDbContext:DbContext
    {
     
    
        public ApplicationDbContext(DbContextOptionsBuilder<ApplicationDbContext> options)
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
