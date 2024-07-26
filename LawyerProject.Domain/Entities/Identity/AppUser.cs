
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LawyerProject.Domain.Entities.Identity
{
    public class AppUser: IdentityUser<string>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenEndDate { get; set; }
        public ICollection<Case>? Cases { get; set; }
        public ICollection<Advert>? Adverts { get; set; }


        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        // public string? ProfileImage { get; set; }
        // public ICollection<Case>? Cases { get; set; }
        // public ICollection<Advert>? Adverts { get; set; }
    }
}
