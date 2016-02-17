﻿using AbiokaScrum.Api.IoC;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace AbiokaScrum.Api
{
    public class DIControllerActivator : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType) {
            var controller = (IHttpController)DependencyContainer.Container.Resolve(controllerType);
            request.RegisterForDispose(new Release(() => DependencyContainer.Container.Release(controller)));
            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action release;

            public Release(Action release) {
                this.release = release;
            }

            public void Dispose() {
                release();
            }
        }
    }
}