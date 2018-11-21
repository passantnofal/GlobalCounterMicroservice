using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Counter.Command
{
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// AddCommand inject the command services here
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCommand(this IServiceCollection services)
        {
            services.AddTransient<CounterCommand>();
            return services;
        }
    }
}
