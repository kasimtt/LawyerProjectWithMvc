using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.DTOs.TokenDtos
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; } //vade, token süresi
        public string RefreshToken { get; set; } 
    }
}
