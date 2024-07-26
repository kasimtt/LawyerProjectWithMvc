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
    public class EndpointConfiguration : IEntityTypeConfiguration<Endpoint>
    {
        public void Configure(EntityTypeBuilder<Endpoint> builder)
        {
            builder.HasKey(a => a.ObjectId);
            builder.Property(a => a.ActionType).HasMaxLength(25).IsRequired();
            builder.Property(a => a.HttpType).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Definition).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Code).HasMaxLength(100).IsRequired();

            builder.HasMany(a => a.Roles).WithMany(a => a.EndPoints);
            builder.HasOne(a => a.Menu).WithMany(a => a.EndPoints).HasForeignKey(a => a.MenuId);
        }
    }
}
