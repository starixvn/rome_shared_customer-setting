using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapApi.Client.Exceptions
{
    public class NapApiClientUnauthorizedException : Exception
    {
        public NapApiClientUnauthorizedException()
        : base() { }

        public NapApiClientUnauthorizedException(string message)
        : base(message) { }

        public NapApiClientUnauthorizedException(string format, params object[] args)
        : base(string.Format(format, args)) { }

        public NapApiClientUnauthorizedException(string message, Exception innerException)
        : base(message, innerException) { }

        public NapApiClientUnauthorizedException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }
    }
}
