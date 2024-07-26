
using LawyerProject.Domain.Entities.Identity;
using LawyerProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Domain.Entities
{
    public class Case : BaseEntity
    {
        public string IdUserFK { get; set; }
        public AppUser? User { get; set; }
        public int CaseNumber { get; set; }
        public string? CaseNot { get; set; } 
        public string? CaseDescription { get; set; } 
        public string CaseType { get; set; } = string.Empty;
        public DateTime? CaseDate { get; set; }
        public ICollection<CasePdfFile>? CasePdfFiles { get; set; }



        /*** IdKullanıcıFK:
** DavaId : 
** DavaNo :
** DavaNot: 
** DavaTuru : -->Enum olacak
** DavaAcıklamalar:
** DavaKonusu:
** DavaDosyalar:
** DuruşmaTarihi: --> Duruşma tarihi hatırlatıcı eklenecek. Mobilden bildirim gönderilecek.
*/
    }
}
