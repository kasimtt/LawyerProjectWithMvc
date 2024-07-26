using LawyerProject.Application.Repositories.CasePdfFileRepositories;
using LawyerProject.Application.Repositories.CaseRepositories;
using LawyerProject.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using C = LawyerProject.Domain.Entities;

namespace LawyerProject.Application.Features.Commands.CasePdfFiles.RemoveCasePdfFile
{
    public class RemoveCasePdfFileCommandHandler : IRequestHandler<RemoveCasePdfFileCommandRequest, RemoveCasePdfFileCommandResponse>
    {
        private readonly ICaseReadRepository _caseReadRepository;
        private readonly ICaseWriteRepository _caseWriteRepository;

        public RemoveCasePdfFileCommandHandler(ICaseReadRepository caseReadRepository, ICaseWriteRepository caseWriteRepository)
        {
            _caseReadRepository = caseReadRepository;
            _caseWriteRepository = caseWriteRepository;
        }

        public async Task<RemoveCasePdfFileCommandResponse> Handle(RemoveCasePdfFileCommandRequest request, CancellationToken cancellationToken)
        {
            C.Case? _case = await _caseReadRepository.Table.Include(c => c.CasePdfFiles).FirstOrDefaultAsync(c => c.ObjectId == request.Id); // case(dava) bulunuyor
            CasePdfFile? casePdfFile = _case?.CasePdfFiles?.FirstOrDefault(p => p.ObjectId == request.ImageId); // davanın dosyaları bulunuyor
          
            if (casePdfFile != null) {
                _case?.CasePdfFiles?.Remove(casePdfFile);  //dosya silinip kaydediliyor
                await _caseWriteRepository.SaveAsync();
            }
            // veritabanına kayıt yapılıyor.// dosyalarda mahkeme ve yasal bilgiler bulunduğu için komple silme yetkisi kullanıcılara verilecek.

            return new RemoveCasePdfFileCommandResponse
            {
                Success = true,
            };
        }
    }
}
