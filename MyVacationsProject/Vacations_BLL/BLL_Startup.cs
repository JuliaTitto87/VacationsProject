using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vacations_BLL.Contracts;

namespace Vacations_BLL
{
    internal struct InterfaceToImplementation
        {
            public Type Implementation;
            public Type Interface;
        }
        public static class BLL_Startup
    {
            public static void RegisterModule(IServiceCollection services)
            {
                var currentAssembly = Assembly.GetAssembly(typeof(BLL_Startup));

                var allTypesInThisAssembly = currentAssembly.GetTypes();

                var serviceTypes = allTypesInThisAssembly
                    .Where(type =>
                        type.IsAssignableTo(typeof(IService))
                        && !type.IsInterface
                    );

                var interfaceToImplementationMap = serviceTypes.Select(serviceType => {
                    var implementation = serviceType;
                    var @interface = serviceType.GetInterfaces()
                        .First(serviceInterface => serviceInterface != typeof(IService));

                    return new InterfaceToImplementation
                    {
                        Interface = @interface,
                        Implementation = implementation,
                    };
                });

                foreach (var serviceToInterface in interfaceToImplementationMap)
                {
                    services.AddTransient(serviceToInterface.Interface, serviceToInterface.Implementation);
                }
            }
        }
    }
