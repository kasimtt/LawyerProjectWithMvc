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
    internal class AdvertConfiguration : IEntityTypeConfiguration<Advert>
    {
        public void Configure(EntityTypeBuilder<Advert> builder)
        {
            builder.HasKey(a => a.ObjectId);
            builder.Property(a => a.CaseType).HasMaxLength(60).IsRequired();
            builder.Property(a=>a.CaseDate).IsRequired();
            builder.Property(a=>a.Price).IsRequired();
            builder.Property(a=>a.City).IsRequired()
                .HasMaxLength(50);
            builder.Property(a=>a.District).IsRequired()
                .HasMaxLength(50);
            builder.Property(a=>a.Address).IsRequired()
                .HasMaxLength(75);
            builder.Property(a => a.CasePlace).IsRequired()
                .HasMaxLength(50);
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a=>a.DataState).IsRequired();
            builder.Property(a=>a.UpdatedDate).IsRequired(false);
            builder.Property(a=>a.Description).IsRequired(false).HasMaxLength(1000);

            builder.HasOne(a => a.User).WithMany(u => u.Adverts).HasForeignKey(a => a.IdUserFK).IsRequired(false);

        }
    }
}
