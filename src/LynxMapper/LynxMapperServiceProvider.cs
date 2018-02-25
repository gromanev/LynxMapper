using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LynxMapper.Configs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LynxMapper
{
    public class LynxMapperServiceProvider
    {
        internal static IServiceCollection Collection;
        private readonly IOptions<RegisterTransformatorOptions> _options;

        public LynxMapperServiceProvider(IOptions<RegisterTransformatorOptions> options)
        {
            _options = options;

            var t = _options.Value;
        }

        public IServiceCollection GetServices()
        {
            return Collection;
        }
    }
}
