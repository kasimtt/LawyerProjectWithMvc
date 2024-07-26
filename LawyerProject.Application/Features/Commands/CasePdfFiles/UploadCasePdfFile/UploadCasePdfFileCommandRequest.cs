using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Commands.CasePdfFiles.UploadCasePdfFile
{
    public class UploadCasePdfFileCommandRequest: IRequest<UploadCasePdfFileCommandResponse>
    {
        public int Id { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
    }
}
