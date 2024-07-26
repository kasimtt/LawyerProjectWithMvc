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
    public class FileConfiguration : IEntityTypeConfiguration<Domain.Entities.File>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.File> builder)
        {
            builder.HasKey(f => f.ObjectId);
            builder.Property(f=>f.FileName).IsRequired().HasMaxLength(50);
            builder.Property(f=>f.Path).IsRequired().HasMaxLength(255);
            builder.Property(f=>f.Storage).IsRequired(false).HasMaxLength(30);
           
          
        }
    }
}
