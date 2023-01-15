using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;
using FluentValidation;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using MediatR.Pipeline;
using MS2Project.Application.Bases.Validation;
using MS2Project.Infrastructure.Processing;

namespace MS2Project.Infrastructure.IoC.AutofacSettings;

public class MediatorModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterSource(new ScopedContravariantRegistrationSource(
            typeof(IRequestHandler<,>),
            typeof(INotificationHandler<>),
            typeof(IValidator<>)
        ));
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

        var mediatrOpenTypes = new[]
        {
            typeof(IRequestHandler<,>),
            typeof(INotificationHandler<>),
            typeof(IValidator<>)
        };

        foreach (var mediatrOpenType in mediatrOpenTypes)
        {
            builder
                .RegisterAssemblyTypes(Assemblies.Application, ThisAssembly)
                .AsClosedTypesOf(mediatrOpenType)
                .FindConstructorsWith(new AllConstructorFinder())
                .AsImplementedInterfaces();
        }

        builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

        builder.Register<ServiceFactory>(ctx =>
        {
            var c = ctx.Resolve<IComponentContext>();
            return t => c.Resolve(t);
        });

        builder.RegisterGeneric(typeof(CommandValidationBehavior<,>)).As(typeof(IPipelineBehavior<,>));
    }

    private class ScopedContravariantRegistrationSource : IRegistrationSource
    {
        private readonly IRegistrationSource _source = new ContravariantRegistrationSource();
        private readonly List<Type> _types = new();

        public ScopedContravariantRegistrationSource(params Type[] types)
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));
            if (!types.All(x => x.IsGenericTypeDefinition))
                throw new ArgumentException("Supplied types should be generic type definitions");
            _types.AddRange(types);
        }

        public IEnumerable<IComponentRegistration> RegistrationsFor(
            Service service,
            Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
        {
            var components = _source.RegistrationsFor(service, registrationAccessor);
            foreach (var c in components)
            {
                var defs = c.Target.Services
                    .OfType<TypedService>()
                    .Select(x => x.ServiceType.GetGenericTypeDefinition());

                if (defs.Any(_types.Contains))
                    yield return c;
            }
        }

        public bool IsAdapterForIndividualComponents => _source.IsAdapterForIndividualComponents;
    }
}
