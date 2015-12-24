using AbiokaScrum.Authentication;
using System;

namespace AbiokaScrum.Api.Authentication
{
    public class Context
    {
        private static readonly string contextName = "_AbiokaContext_";

        public Context() {
            ContextHolder.SetData(contextName, this);
        }

        public ICustomPrincipal Principal { get; set; }

        public static Context Current {
            get {
                object obj = ContextHolder.GetData(contextName);
                if (obj == null) {
                    throw new ApplicationException("Abioka context is empty");
                }
                return (Context)obj;
            }
        }
    }
}