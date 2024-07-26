using LawyerProject.Application.Repositories.CasePdfFileRepositories;
using LawyerProject.Domain.Entities;
using LawyerProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Persistence.Repositories.CasePdfFileRepositories
{
    public class CasePdfFileReadRepository : ReadRepository<CasePdfFile>, ICasePdfFileReadRepository
    {
        public CasePdfFileReadRepository(LawyerProjectContext context) : base(context)
        {
        }
    }
}
