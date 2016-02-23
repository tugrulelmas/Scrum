using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Data.Transactional.Dapper.Operations;
using AbiokaScrum.Api.IoC;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Mongo = AbiokaScrum.Api.Data.NoSql.Mongo;

namespace AbiokaScrum.Api
{
    public class Bootstrapper
    {
        public static void Initialise() {
            DependencyContainer.SetContainer(new CastleContainer());

            DependencyContainer.Container.Register(typeof(IOperation<>), typeof(Operation<>));
            DependencyContainer.Container.Register(typeof(ICardOperation), typeof(CardOperation));
            DependencyContainer.Container.Register(typeof(IListOperation), typeof(ListOperation));
            DependencyContainer.Container.Register(typeof(IUserOperation), typeof(UserOperation));

            var useMongoDb = ConfigurationManager.AppSettings["UseMongoDb"];
            if (!string.IsNullOrEmpty(useMongoDb) && useMongoDb.ToLowerInvariant() == bool.TrueString.ToLowerInvariant())
            {
                DependencyContainer.Container.Register(typeof(IBoardOperation), typeof(Mongo.Operations.BoardOperation));
                DependencyContainer.Container.Register(typeof(ILabelOperation), typeof(Mongo.Operations.LabelOperation));
            }
            else
            {
                DependencyContainer.Container.Register(typeof(ILabelOperation), typeof(LabelOperation));
                DependencyContainer.Container.Register(typeof(IBoardOperation), typeof(BoardOperation));
            }

            var controllerTypes =
                from t in Assembly.GetExecutingAssembly().GetTypes()
                where typeof(ApiController).IsAssignableFrom(t)
                select t;

            foreach (var t in controllerTypes)
            {
                DependencyContainer.Container.Register(t);
            }
        }
    }
}