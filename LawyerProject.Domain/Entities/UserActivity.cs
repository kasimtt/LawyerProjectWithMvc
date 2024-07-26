using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Domain.Entities
{
    public class UserActivity : BaseEntity
    {
        public string Data { get; set; } = string.Empty;
        public string IpAdresi { get; set; } = string.Empty;
        public string KullaniciId { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public DateTime Tarih { get; set; } = DateTime.Now;
    }
}
