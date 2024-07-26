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
    public class CaseWriteRepository : WriteRepository<Case>, ICaseWriteRepository
    {
        private readonly LawyerProjectContext _context;
        public CaseWriteRepository(LawyerProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
