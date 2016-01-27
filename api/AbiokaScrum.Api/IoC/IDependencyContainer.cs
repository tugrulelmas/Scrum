using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api.IoC
{
    public interface IDependencyContainer
    {
        T Resolve<T>();
        object Resolve(Type type);
        void Register(Type interfaceType, Type implementationType);
        void RegisterSingleton(Type interfaceType, Type implementationType);
    }
}