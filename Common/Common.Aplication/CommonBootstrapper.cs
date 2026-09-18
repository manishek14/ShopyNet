using Common.Aplication.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Aplication
{
    public class CommonBootstrapper
    {
        public static void Init(IServiceCollection service)
        {
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(CommandValidationBehavior<,>));
        }
    }
}