using Avalonia.Logging;

namespace CrepeDuChef.Avalonia.Tests.Support
{
    /// <summary>
    /// Collects binding warnings and errors so a test can fail on a broken binding,
    /// which Avalonia otherwise only logs.
    /// </summary>
    public sealed class BindingErrorSink : ILogSink, IDisposable
    {
        private readonly ILogSink? _previous;
        private readonly List<string> _errors = [];

        public IReadOnlyList<string> Errors => _errors;

        public BindingErrorSink()
        {
            _previous = Logger.Sink;
            Logger.Sink = this;
        }

        public bool IsEnabled(LogEventLevel level, string area)
            => area == LogArea.Binding && level >= LogEventLevel.Warning;

        public void Log(LogEventLevel level, string area, object? source, string messageTemplate)
            => _errors.Add(messageTemplate);

        public void Log(LogEventLevel level, string area, object? source, string messageTemplate, params object?[] propertyValues)
            => _errors.Add($"{messageTemplate} | {string.Join(", ", propertyValues)}");

        public void Dispose() => Logger.Sink = _previous;
    }
}
