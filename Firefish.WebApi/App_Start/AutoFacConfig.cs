using Autofac;
using Autofac.Integration.WebApi;
using Firefish.WebApi.AutoFacModules;
using System.Reflection;

namespace Firefish.WebApi
{
    public class AutoFacConfig
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register Services

            builder.RegisterModule<ApplicationModule>();
            builder.RegisterModule<PersistanceModule>();

            var container = builder.Build();

            return container;
        }
    }
}