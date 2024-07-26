using f =LawyerProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LawyerProject.Application.Repositories.FileRepositories;
using LawyerProject.Persistence.Context;

namespace LawyerProject.Persistence.Repositories.FileRepositories
{
    public class FileWriteRepository : WriteRepository<f.File>, IFileWriteRepository
    {
        public FileWriteRepository(LawyerProjectContext context) : base(context)
        {
        }
    }
}
