using AbiokaScrum.Api.Exceptions;
using System;

namespace AbiokaScrum.Api.Helper
{
    internal class Safely
    {
        internal static TResult Run<TResult>(Func<TResult> func) {
            try {
                TResult result = func();
                return result;
            } catch (Exception ex) {
                throw new GlobalException(ex);
            }
        }

        internal static void Run(Action action) {
            try {
                action();
            } catch (Exception ex) {
                throw new GlobalException(ex);
            }
        }
    }
}