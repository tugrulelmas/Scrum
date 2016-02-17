using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;

namespace AbiokaScrum.Api.IoC
{
    public class CastleContainer : IDependencyContainer
    {
        public CastleContainer() {
            container = new WindsorContainer();
        }

        /// <summary>
        /// Resolve the target type with necessary dependencies.
        /// </summary>
        public object Resolve(Type targetType) {
            if (container.Kernel.HasComponent(targetType)) {
                return container.Resolve(targetType);
            }
            return null;
        }

        /// <summary>
        /// Resolves all registered instances for a specific service type.
        /// </summary>
        public IList<object> ResolveAll(Type serviceType) {
            if (container.Kernel.HasComponent(serviceType)) {
                return new List<object>((object[])container.ResolveAll(serviceType));
            }
            return null;
        }

        public readonly IWindsorContainer container;

        public T Resolve<T>() {
            return container.Resolve<T>();
        }

        public void Register(Type interfaceType) {
            container.Register(Component.For(interfaceType).LifestylePerWebRequest());
        }

        public void Register(Type interfaceType, Type implementationType) {
            container.Register(Component.For(interfaceType).ImplementedBy(implementationType).LifestylePerWebRequest());
        }

        public void RegisterSingleton(Type interfaceType, Type implementationType) {
            container.Register(Component.For(interfaceType).ImplementedBy(implementationType).LifeStyle.Singleton);
        }

        public void Release(object instance) {
            container.Release(instance);
        }

        public void Dispose() {
            container.Dispose();
        }
    }
}