using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Data.Dapper;
using AbiokaScrum.Api.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AbiokaScrum.Api
{
    public class Bootstrapper
    {
        public static void Initialise() {
            DependencyContainer.SetContainer(new CastleContainer());

            DependencyContainer.Container.Register(typeof(IUnitOfWork), typeof(DapperUnitOfWork));
        }
    }
}