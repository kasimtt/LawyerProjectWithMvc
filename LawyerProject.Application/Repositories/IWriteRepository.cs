using LawyerProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Add Operations

        bool Add(T model);
        bool AddRange(List<T> models);
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> models);

        #endregion


        #region Update Operations

        bool Update(T model);
        bool UpdateRange(List<T> models);

        #endregion


        #region Remove Operations

        bool Remove(T model);

        bool RemoveRange(List<T> models);

        Task<bool> RemoveAsync(int id);

        #endregion

        void Save();
        Task<int> SaveAsync();
    }

}
