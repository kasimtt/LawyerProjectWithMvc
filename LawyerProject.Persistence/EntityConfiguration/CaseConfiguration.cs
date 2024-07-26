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
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            builder.HasKey(c => c.ObjectId);
            builder.Property(c => c.IdUserFK).IsRequired();
            builder.Property(c => c.CaseNumber).IsRequired();
            builder.Property(c => c.CaseNot).IsRequired(false)
                .HasMaxLength(200);
            builder.Property(c=>c.CaseDescription).IsRequired(false)
                .HasMaxLength(1000);
            builder.Property(c=>c.CaseType).HasMaxLength(60).IsRequired();
            builder.Property(c => c.CaseDate).IsRequired(false);

            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.DataState).IsRequired();
            builder.Property(a => a.UpdatedDate).IsRequired(false);

            builder.HasOne(c => c.User).WithMany(u => u.Cases).HasForeignKey(c => c.IdUserFK);
            builder.HasMany(c => c.CasePdfFiles).WithMany(u => u.Cases);

        }
    }
}
