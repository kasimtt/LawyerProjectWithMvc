using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Exceptions
{
    public class NotFoundCaseException : Exception
    {
        public NotFoundCaseException(): base("Dava Bulunamadı")
        {
        }

        public NotFoundCaseException(string? message) : base(message)
        {
        }

        public NotFoundCaseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
