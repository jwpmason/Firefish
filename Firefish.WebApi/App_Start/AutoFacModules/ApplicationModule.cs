using Autofac;
using Firefish.Application.Implementations;
using Firefish.Application.Interfaces;

namespace Firefish.WebApi.AutoFacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CandidateRetrievalService>().As<ICandidateRetrievalService>().InstancePerRequest();
            builder.RegisterType<CandidateManipulationService>().As<ICandidateManipulationService>().InstancePerRequest();
            builder.RegisterType<SkillService>().As<ISkillService>().InstancePerRequest();
        }
    }
}