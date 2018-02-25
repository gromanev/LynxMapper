using System;
using System.Linq;
using LynxMapper.Abstractions;
using Microsoft.Extensions.Options;

namespace LynxMapper
{
    public class LynxMapper : ILynxMapper
    {
        private readonly IOptions<LynxMapperOptions> _options;

        public LynxMapper(IOptions<LynxMapperOptions> options)
        {
            _options = options;
        }

        public T1 Map<T1, T2>(T2 value)
            where T1 : class
            where T2 : class
        {
            var options = _options.Value;

            var myFunc = FindTargetFunc<T1, T2>(options);

            if (myFunc == null)
            {
                throw new ArgumentException("Using unregistered transformator");
            }

            var result = myFunc(value);

            return result;
        }


        public static Func<T2, T1> FindTargetFunc<T1, T2>(LynxMapperOptions options)
        {
            var key = $"({typeof(T1).FullName})-({typeof(T2).FullName})";

            return (Func<T2, T1>)options.Dictionary.FirstOrDefault(x => x.Key.Equals(key)).Value;
        }
    }
}