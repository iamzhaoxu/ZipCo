using System;
using Microsoft.Extensions.Logging;

namespace ZipCo.Users.Test.Shared
{
    public abstract class MockLogger<T> : ILogger<T>
    {
        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            => MockLog(logLevel, formatter(state, exception), exception);
        public abstract void MockLog(LogLevel logLevel, string message, Exception ex);
        public virtual bool IsEnabled(LogLevel logLevel) => true;
        public abstract IDisposable BeginScope<TState>(TState state);
    }
}
