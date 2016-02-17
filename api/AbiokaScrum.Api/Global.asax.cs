﻿using AbiokaScrum.Api.IoC;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace AbiokaScrum.Api
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e) {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            Bootstrapper.Initialise();

            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new DIControllerActivator());
        }

        public override void Dispose() {
            //DependencyContainer.Container.Dispose();
            base.Dispose();
        }
    }
}