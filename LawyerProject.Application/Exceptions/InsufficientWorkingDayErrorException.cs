using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Exceptions
{
    public class InsufficientWorkingDayErrorException : Exception
    {
        public InsufficientWorkingDayErrorException() : base("Kıdem Tazminatı İçin En Az 1 Yıl Çalışma Süresi Gerekmektedir!")
        {
        }

        public InsufficientWorkingDayErrorException(string? message) : base(message)
        {
        }

        public InsufficientWorkingDayErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
