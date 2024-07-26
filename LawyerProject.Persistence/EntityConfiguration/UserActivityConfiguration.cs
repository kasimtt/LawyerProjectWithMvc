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
    public class UserActivityConfiguration : IEntityTypeConfiguration<UserActivity>
    {
        public void Configure(EntityTypeBuilder<UserActivity> builder)
        {
            builder.HasKey(uk => uk.ObjectId);
            builder.Property(uk => uk.Data).IsRequired()
                .HasMaxLength(100);
            builder.Property(uk=>uk.IpAdresi).IsRequired(false)
                .HasMaxLength(20);
            builder.Property(uk=>uk.KullaniciId).IsRequired(false) 
                .HasMaxLength(20); //ne olduğunu bilmiyorum bakıcam
            builder.Property(uk=>uk.Path).IsRequired()
                .HasMaxLength(100);
            builder.Property(uk => uk.Tarih).IsRequired();
            builder.Property(a => a.CreatedDate).IsRequired();
            builder.Property(a => a.DataState).IsRequired();
            builder.Property(a => a.UpdatedDate).IsRequired(false);
        }
    }
}
