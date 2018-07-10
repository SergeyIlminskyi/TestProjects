using Microsoft.Extensions.Logging;
using SWAG.Data.Logging;
using System;

namespace SWAG.Data
{
    public static class DbLoggerExtensions
    {
        public static ILoggerFactory AddContext(this ILoggerFactory factory, LogLevel minLevel)
        {
            return AddContext(factory, (_, logLevel) => logLevel >= minLevel);
        }

        public static ILoggerFactory AddContext(this ILoggerFactory factory,
            Func<String, LogLevel, Boolean> filter)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            factory.AddProvider(new DbLoggerProvider(filter));
            return factory;
        }
    }
}
