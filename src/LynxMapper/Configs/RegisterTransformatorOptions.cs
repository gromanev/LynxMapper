using System;
using System.Collections.Generic;
using System.Text;
using LynxMapper.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace LynxMapper.Configs
{
    public class RegisterTransformatorOptions
    {
        /// <summary>
        /// Register tranformator
        /// </summary>
        /// <typeparam name="TAbstract"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        public void Reg<TAbstract, TImplement>()
            where TAbstract: class, ILynxTransformator
            where TImplement: class, TAbstract
        {
            LynxMapperServiceProvider.Collection.AddTransient<TAbstract, TImplement>();
        }
    }
}
