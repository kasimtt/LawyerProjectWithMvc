using LawyerProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Persistence.EntityConfiguration
{
    public class MenuConfiguration : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(a => a.ObjectId);
            builder.Property(a => a.Name).HasMaxLength(25).IsRequired();

            builder.HasMany(a => a.EndPoints).WithOne(a => a.Menu).HasForeignKey(a => a.MenuId);

        }
    }
}
