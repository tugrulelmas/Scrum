
using System;
using System.Text;

namespace AbiokaScrum.Api.Exceptions
{
    public class GlobalException : ApiException
    {
        public GlobalException(string errorMessage)
            : this(errorMessage, null) {

        }

        public GlobalException(Exception exception)
            : this(string.Empty, exception) {

        }

        public GlobalException(string errorMessage, Exception exception)
            : base(errorMessage, exception) {
            var error = new StringBuilder();
            error.AppendFormat("Message: {0}", errorMessage);
            error.AppendLine();
            error.AppendFormat("Exception: {0}", exception);

            ContentValue = error.ToString();
        }
    }
}