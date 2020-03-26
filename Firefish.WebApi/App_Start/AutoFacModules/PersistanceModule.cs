using Autofac;
using Firefish.Domain.Repositories;
using Firefish.Persistance.Config;
using Firefish.Persistance.Repositories;

namespace Firefish.WebApi.AutoFacModules
{
    public class PersistanceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CandidateRepository>().As<ICandidateRepository>().InstancePerRequest();
            builder.RegisterType<SkillRepository>().As<ISkillRepository>().InstancePerRequest();
            builder.RegisterType<PersistanceConfig>().As<IPersistanceConfig>().SingleInstance();
        }
    }
}