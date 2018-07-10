using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace SWAG.Data.Logging
{
    public class DbLogger
        : ILogger
    {
        private Boolean SelfException { get; set; } = false;

        private String CategoryName { get; set; }

        protected Func<String, LogLevel, Boolean> Filter { get; private set; }

        protected SqlHelper SqlHelper { get; private set; }

        public DbLogger(String categoryName, Func<String, LogLevel, Boolean> filter)
        {
            SqlHelper = new SqlHelper();
            CategoryName = categoryName;
            Filter = filter;
        }

        public Boolean IsEnabled(LogLevel logLevel)
        {
            return (Filter == null || Filter(CategoryName, logLevel));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
            Exception exception, Func<TState, Exception, String> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (SelfException)
            {
                SelfException = false;
                return;
            }

            SelfException = true;

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            String message = formatter(state, exception);
            if (String.IsNullOrEmpty(message))
            {
                return;
            }

            if (exception != null)
            {
                message += "\n" + exception.ToString();
            }

            try
            {
                Int32? messageMaxLength = GetMessageMaxLength();
                message = messageMaxLength != null && message.Length > messageMaxLength ?
                    message.Substring(0, (Int32)messageMaxLength) : message;

                if (Enum.TryParse(logLevel.ToString(), true, out EventLevel level))
                {
                    SqlHelper.InsertLog(new EventEntity
                    {
                        Code = eventId.Id,
                        Level = level,
                        Message = message,
                    });
                }

                SelfException = false;
            }
            catch (Exception)
            { }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        private Int32? GetMessageMaxLength()
        {
            Int32? maxLength = null;
            IEnumerable<PropertyInfo> properties = typeof(EventEntity).GetProperties()
                .Where(p => p.Name.Equals(nameof(EventEntity.Message)));
            foreach (PropertyInfo property in properties)
            {
                MaxLengthAttribute maxLengthAttr = property.GetCustomAttribute<MaxLengthAttribute>(true);
                if (maxLengthAttr != null)
                {
                    maxLength = maxLengthAttr.Length;
                }
            }

            return maxLength;
        }
    }
}
