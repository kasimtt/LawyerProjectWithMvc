using LawyerProject.Application.Repositories;
using LawyerProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerProject.Persistence.Context;

namespace LawyerProject.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly LawyerProjectContext _context;

        public WriteRepository(LawyerProjectContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        #region Add Operations

        public bool Add(T model)
        {
            
            EntityEntry entityEntry = Table.Add(model);
            return entityEntry.State == EntityState.Added;
        }

        public bool AddRange(List<T> models)
        {
            Table.AddRange(models);
            return true;
        }

        public async Task<bool> AddAsync(T model)
        {
           
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> models)
        {
            await Table.AddRangeAsync(models);  
            return true;
        }

        #endregion


        #region Update Operations

        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }

        public bool UpdateRange(List<T> models)
        {
            Table.UpdateRange(models);
            return true;
        }

        #endregion



        #region Remove Operations

        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> models)
        {
            Table.RemoveRange(models);
           
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            T model = await Table.FirstOrDefaultAsync(model => model.ObjectId == id);
            
            return Remove(model);

        }

        #endregion


        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
