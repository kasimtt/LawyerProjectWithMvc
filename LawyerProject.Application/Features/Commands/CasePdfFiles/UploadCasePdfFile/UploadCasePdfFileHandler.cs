using LawyerProject.Application.Abstractions.Storage;
using LawyerProject.Application.Exceptions;
using LawyerProject.Application.Repositories.CasePdfFileRepositories;
using LawyerProject.Application.Repositories.CaseRepositories;
using LawyerProject.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using C = LawyerProject.Domain.Entities;

namespace LawyerProject.Application.Features.Commands.CasePdfFiles.UploadCasePdfFile
{
    public class UploadCasePdfFileHandler : IRequestHandler<UploadCasePdfFileCommandRequest, UploadCasePdfFileCommandResponse>
    {
        private readonly ICaseReadRepository _caseReadRepository;
        private readonly ICasePdfFileWriteRepository _casePdfFileWriteRepository;
        private readonly IStorageService _storageService;
       
        public UploadCasePdfFileHandler(ICaseReadRepository caseReadRepository, ICasePdfFileWriteRepository casePdfFileWriteRepository, IStorageService storageService)
        {
            _caseReadRepository = caseReadRepository;
            _casePdfFileWriteRepository = casePdfFileWriteRepository;
            _storageService = storageService;
        }

        public async Task<UploadCasePdfFileCommandResponse> Handle(UploadCasePdfFileCommandRequest request, CancellationToken cancellationToken)
        {
            C.Case _case = await _caseReadRepository.GetByIdAsync(request.Id);
            if(_case == null)
            {
                throw new NotFoundCaseException();
            }

            List<(string fileName, string pathOrContainer)> datas = await _storageService.UploadAsync("cases-file", request.FormFiles);
            await _casePdfFileWriteRepository.AddRangeAsync(datas.Select(r => new CasePdfFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainer,
                Storage = _storageService.StorageName,
                Cases = new List<C.Case> { _case }

            }).ToList());

            await _casePdfFileWriteRepository.SaveAsync();
            return new UploadCasePdfFileCommandResponse
            {
                Success = true,
            };
        }
    }
}
