using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NapApi.Client.Exceptions
{
    public class NapApiClientException : Exception
    {
        public NapApiClientException()
        : base() { }

        public NapApiClientException(string message)
        : base(message) { }

        public NapApiClientException(string format, params object[] args)
        : base(string.Format(format, args)) { }

        public NapApiClientException(string message, Exception innerException)
        : base(message, innerException) { }

        public NapApiClientException(string format, Exception innerException, params object[] args)
        : base(string.Format(format, args), innerException) { }
    }
}
