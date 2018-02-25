using System;
using LynxMapper.Abstractions;
using LynxMapper.Configs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LynxMapper
{
    public static class LynxMapperBuilderExtension
    {
        public static IServiceCollection AddLynxMapperTransformators(this IServiceCollection services,
            Action<RegisterTransformatorOptions> options)
        {
            services.Configure(options);
            services.AddTransient<LynxMapperServiceProvider>();

            LynxMapperServiceProvider.Collection = services;

            return services;
        }

        /// <summary>
        /// Add to service collection container
        /// </summary>
        /// <param name="services"></param>
        /// <param name="lynxMapperOptions">Params for set up mapping (register transformators and they types)</param>
        public static void AddLynxMapper(this IServiceCollection services, Action<LynxMapperOptions> lynxMapperOptions)
        {
            services.AddTransient<ILynxMapper, LynxMapper>();

            services.Configure(lynxMapperOptions);
        }
    }
}
