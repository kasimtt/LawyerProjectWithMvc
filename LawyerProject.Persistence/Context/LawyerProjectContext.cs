using LawyerProject.Domain.Entities;
using LawyerProject.Domain.Entities.Calculation;
using LawyerProject.Domain.Entities.Identity;
using LawyerProject.Persistence.EntityConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Persistence.Context
{
    public class LawyerProjectContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public LawyerProjectContext(DbContextOptions<LawyerProjectContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;  //introducing hatası için yazıldı
            }
            //configurasyonlar buraya eklenecek
            modelBuilder.Entity<AppUser>()
              .HasMany(u => u.Adverts)
              .WithOne(a => a.User)
            .HasForeignKey(a => a.IdUserFK);

            modelBuilder.Entity<AppUser>()
                .HasMany(u => u.Cases)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.IdUserFK);

            modelBuilder.Entity<AppRole>()
                .HasMany(a => a.EndPoints)
                .WithMany(a => a.Roles);

            modelBuilder.Entity<NetToGross>().HasKey(n => n.ObjectId);

            modelBuilder.ApplyConfiguration(new AdvertConfiguration());
            modelBuilder.ApplyConfiguration(new CaseConfiguration());
            modelBuilder.ApplyConfiguration(new UserActivityConfiguration());
            modelBuilder.ApplyConfiguration(new FileConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
            modelBuilder.ApplyConfiguration(new EndpointConfiguration());



            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker : Entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir. Update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.

            var datas = ChangeTracker
                  .Entries<BaseEntity>();

            foreach (var data in datas)
            {

                switch (data.State)
                {
                    case EntityState.Modified:
                        data.Entity.UpdatedDate = DateTime.UtcNow;
                        //data.Entity.DataState = Domain.Enums.DataState.Active;
                        if (data.Entity.DataState != Domain.Enums.DataState.Deleted)
                        {
                            data.Entity.DataState = Domain.Enums.DataState.Active;
                        }

                        break;
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.UtcNow;
                        data.Entity.DataState = Domain.Enums.DataState.Active;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);

        }


        public DbSet<Advert> Adverts { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<UserActivity> UserActivitys { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<CasePdfFile> CasesPdfFiles { get; set; }
        public DbSet<UserImageFile> FilesImages { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<NetToGross> NetToGrosss { get; set; }

    }
}
