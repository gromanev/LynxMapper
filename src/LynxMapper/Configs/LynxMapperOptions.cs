using System;
using System.Collections.Generic;
using System.Linq;
using LynxMapper.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LynxMapper.Configs
{
    public class LynxMapperOptions
    {
        private readonly LynxMapperServiceProvider _serviceProvider;

        public LynxMapperOptions()
        {
            _serviceProvider = LynxMapperServiceProvider.Collection
                .BuildServiceProvider()
                .GetService<LynxMapperServiceProvider>();
        }

        internal Dictionary<string, Delegate> Dictionary;

        public void RegisterFor<T1, T2>(Func<T2, T1> func)
        {
            var key = $"({typeof(T1).FullName})-({typeof(T2).FullName})";

            if (Dictionary == null)
            {
                Dictionary = new Dictionary<string, Delegate>();
            }

            var containsFunc = Dictionary.FirstOrDefault(x => x.Key.Equals(key)).Value;

            if (containsFunc != null)
            {
                throw new ArgumentException("This expression is already registered");
            }

            Dictionary.Add(key, func);
        }

        public TAbstract GetTransformator<TAbstract>() 
            where TAbstract : ILynxTransformator
        {
            var targetService = _serviceProvider.GetServices().BuildServiceProvider().GetService<TAbstract>();

            if (targetService == null)
            {
                throw new ArgumentException("Transformator unregistred!");
            }

            return targetService;
        }
    }
}