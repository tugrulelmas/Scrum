using System;

namespace AbiokaScrum.Api.IoC
{
    public interface IDependencyContainer
    {
        T Resolve<T>();
        object Resolve(Type type);
        void Release(object instance);
        void Register(Type interfaceType);
        void Register(Type interfaceType, Type implementationType);
        void RegisterSingleton(Type interfaceType, Type implementationType);
        void Dispose();
    }
}