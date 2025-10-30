using Common.Shared;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure.Dependy
{
    /// <summary>
    /// 特性依赖注入扩展
    /// </summary>
    public static class DIExtension
    {
        public static IServiceCollection DependyConfiguration(this IServiceCollection services, Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes().Where(p => p.IsClass && !p.IsAbstract))
                {
                    var att = type.GetCustomAttribute<DependyAttrubite>();
                    if (att != null)
                    {
                        switch (att.LifeTime)
                        {
                            case DependyLifeTimeEnum.Scoped:
                                services.AddScoped(att.BaseType, type);
                                break;
                            case DependyLifeTimeEnum.Singleton:
                                services.AddSingleton(att.BaseType, type);
                                break;
                            case DependyLifeTimeEnum.Transient:
                                services.AddTransient(att.BaseType, type);
                                break;
                        }
                    }
                }
            }

            return services;
        }
    }
}