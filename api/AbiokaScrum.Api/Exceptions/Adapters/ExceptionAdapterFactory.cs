using System;

namespace AbiokaScrum.Exceptions.Adapters
{
    public class ExceptionAdapterFactory
    {
        public static IExceptionAdapter GetAdapter(Exception exception) {
            if (exception is IApiException) {
                return new ApiExceptionAdapter((IApiException)exception);
            } else if (exception is AggregateException) {
                return new AggregateExceptionAdapter((AggregateException)exception);
            } else if (exception is ArgumentNullException) {
                return new ArgumentNullExceptionAdapter((ArgumentNullException)exception);
            } else {
                return new ExceptionAdapter((Exception)exception);
            }
        }
    }
}
