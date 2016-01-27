using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.IoC
{
    public static class DependencyContainer
    {
        private static readonly object LockObj = new object();

        private static IDependencyContainer container;

        public static IDependencyContainer Container
        {
            get { return container; }

            private set
            {
                lock (LockObj) {
                    container = value;
                }
            }
        }

        public static void SetContainer(IDependencyContainer container) {
            Container = container;
        }
    }
}