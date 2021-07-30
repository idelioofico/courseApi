using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using courseApi.Business.Entities;

namespace courseApi.Infraestructure.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TB_USERS");
            builder.HasKey(user=>user.Id);
            builder.Property(user => user.Id).ValueGeneratedOnAdd();
            builder.Property(user => user.Username);
            builder.Property(user => user.Email);
            builder.Property(user => user.Password);
        }
    }
}
