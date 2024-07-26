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
    public class UserActivityReadRepository : ReadRepository<UserActivity>, IUserActivityReadRepository
    {
        private readonly LawyerProjectContext _context;
        public UserActivityReadRepository(LawyerProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
