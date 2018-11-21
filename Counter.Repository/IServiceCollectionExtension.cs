using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Counter.Repository
{
 public   static  class IServiceCollectionExtension
    {
        /// <summary>
        /// AddRepository inject the repository services here
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<CounterRepository>();
            return services;
        }
    }
}
