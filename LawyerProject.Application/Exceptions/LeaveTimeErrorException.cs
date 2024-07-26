using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerProject.Application.Exceptions
{
    public class LeaveTimeErrorException : Exception
    {
        public LeaveTimeErrorException() : base("Yıllık İzin Süresi Hesaplanamaz!")
        {
        }

        public LeaveTimeErrorException(string? message) : base(message)
        {
        }

        public LeaveTimeErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
