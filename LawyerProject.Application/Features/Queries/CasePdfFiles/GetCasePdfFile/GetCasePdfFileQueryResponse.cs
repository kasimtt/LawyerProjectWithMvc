using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Features.Queries.CasePdfFiles.GetCasePdfFile
{
    public class GetCasePdfFileQueryResponse 
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public int Id { get; set; }
    }
}
