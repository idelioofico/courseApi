using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using courseApi.Business.Entities;

namespace courseApi.Infraestructure.Data.Mappings
{
    public class CourseMapping : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable("TB_COURSE");
            builder.HasKey(course => course.id);
            builder.Property(course => course.id).ValueGeneratedOnAdd();
            builder.Property(course => course.Name);
            builder.Property(course => course.Description);
            builder.HasOne(course => course.User).WithMany().HasForeignKey(fk => fk.UserId);
           
        }
    }
}
