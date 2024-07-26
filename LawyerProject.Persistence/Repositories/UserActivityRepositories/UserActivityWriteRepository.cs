using LawyerProject.Application.Repositories.UserActivityRepositories;
using LawyerProject.Domain.Entities;
using LawyerProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Persistence.Repositories.UserActivityRepositories
{
    public class UserActivityWriteRepository : WriteRepository<UserActivity>, IUserActivityWriteRepository
    {
        private readonly LawyerProjectContext _context;
        public UserActivityWriteRepository(LawyerProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
