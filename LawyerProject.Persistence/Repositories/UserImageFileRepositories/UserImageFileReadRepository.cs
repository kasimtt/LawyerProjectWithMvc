using LawyerProject.Application.Repositories.UserImageFileRepositories;
using LawyerProject.Domain.Entities;
using LawyerProject.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Persistence.Repositories.UserImageFileRepositories
{
    public class UserImageFileReadRepository : ReadRepository<UserImageFile>, IUserImageFileReadRepository
    {
        public UserImageFileReadRepository(LawyerProjectContext context) : base(context)
        {
        }
    }
}
