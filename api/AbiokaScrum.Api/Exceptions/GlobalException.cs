
using System;
namespace AbiokaScrum.Exceptions
{
    public class GlobalException : ApiException
    {
        public GlobalException(string errorMessage)
            : this(errorMessage, null) {

        }

        public GlobalException(Exception exception)
            : base(string.Empty, exception) {

        }

        public GlobalException(string errorMessage, Exception exception)
            : base(errorMessage, exception) {

        }
    }
}