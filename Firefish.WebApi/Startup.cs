using Autofac.Integration.WebApi;
using Firefish.WebApi;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace Firefish.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);

            var container = AutoFacConfig.BuildContainer();

            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //middleware
            app.UseAutofacMiddleware(container); //all container registered owin middleware will be added to pipeline here in order it was registered
            app.UseAutofacWebApi(httpConfiguration);
            app.UseWebApi(httpConfiguration);
        }
    }
}