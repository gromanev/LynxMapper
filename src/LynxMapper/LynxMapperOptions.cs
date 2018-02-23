using System;
using System.Collections.Generic;
using System.Linq;

namespace LynxMapper
{
    public class LynxMapperOptions
    {
        public Dictionary<string, Delegate> Dictionary;

        public void RegisterTransformator<T1, T2>(Func<T2, T1> func)
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
    }
}