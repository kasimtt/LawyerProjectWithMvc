using LawyerProject.Application.Repositories.AdvertRepositories;
using LawyerProject.Domain.Entities;
using LawyerProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Persistence.Repositories.AdvertRepositories
{
    public class AdvertWriteRepository : WriteRepository<Advert>, IAdvertWriteRepository
    {
        private readonly LawyerProjectContext _context;
        public AdvertWriteRepository(LawyerProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
