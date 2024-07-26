using LawyerProject.Application.Repositories.CaseRepositories;
using LawyerProject.Domain.Entities;
using LawyerProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Persistence.Repositories.CaseRepositories
{
    public class CaseReadRepository : ReadRepository<Case>, ICaseReadRepository
    {
        private readonly LawyerProjectContext _context;
        public CaseReadRepository(LawyerProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
