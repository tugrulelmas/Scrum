using AbiokaScrum.Api.Data;
using AbiokaScrum.Api.Data.Transactional.Dapper.Operations;
using AbiokaScrum.Api.IoC;
using System.Linq;
using System.Reflection;
using System.Web.Http;

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
            DependencyContainer.Container.Register(typeof(IBoardOperation), typeof(BoardOperation));
            DependencyContainer.Container.Register(typeof(ILabelOperation), typeof(LabelOperation));

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