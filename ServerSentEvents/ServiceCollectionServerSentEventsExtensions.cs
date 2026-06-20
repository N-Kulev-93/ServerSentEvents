using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSentEvents
{
    public static class ServiceCollectionServerSentEventsExtensions
    {
        public static IServiceCollection AddServerSentEventsServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
