using System;
using Microsoft.Extensions.DependencyInjection;

namespace LynxMapper
{
    public static class LynxMapperBuilderExtension
    {
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
