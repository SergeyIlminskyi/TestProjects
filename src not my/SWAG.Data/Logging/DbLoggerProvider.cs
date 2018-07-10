using Microsoft.Extensions.Logging;
using System;

namespace SWAG.Data.Logging
{
    public class DbLoggerProvider
        : ILoggerProvider
    {
        protected Func<String, LogLevel, Boolean> Filter { get; private set; }

        public DbLoggerProvider(Func<String, LogLevel, Boolean> filter)
        {
            Filter = filter;
        }

        public ILogger CreateLogger(String categoryName)
        {
            return new DbLogger(categoryName, Filter);
        }

        public void Dispose()
        { }
    }
}
