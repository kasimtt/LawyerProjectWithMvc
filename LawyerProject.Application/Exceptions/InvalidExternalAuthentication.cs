using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Exceptions
{
    public class InvalidExternalAuthentication : Exception
    {
        public InvalidExternalAuthentication(): base("Kimlik Doğrulama Hatasi")
        {
        }

        public InvalidExternalAuthentication(string? message) : base(message)
        {
        }

        public InvalidExternalAuthentication(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
