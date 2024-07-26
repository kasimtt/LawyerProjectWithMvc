using LawyerProject.Application.Repositories.CalculationRepositories.NetToGrossRepository;
using LawyerProject.Domain.Entities.Calculation;
using LawyerProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Persistence.Repositories.CalculationRepositories.NetToGrossRepository
{
    public class NetToGroosWriteRepository : WriteRepository<NetToGross>, INetToGroosWriteRepository
    {
        public NetToGroosWriteRepository(LawyerProjectContext context) : base(context)
        {
        }
    }
}
