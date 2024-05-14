using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace KKLauncher.Web.Server.Aspects
{
    public class LoggingAspect<T> : DispatchProxy
    {
        private const int MaxJsonValueLength = 250;

        private T? _decorated;
        private ILogger<T>? _logger;

        protected override object? Invoke(MethodInfo? targetMethod, object?[]? args)
        {
            if (targetMethod == null)
            {
                return null;
            }

            if (_logger?.IsEnabled(LogLevel.Trace) == false)
            {
                return targetMethod.Invoke(_decorated, args);
            }

            LogBefore(targetMethod, args);
            var result = targetMethod.Invoke(_decorated, args);

            if (result is Task resultTask)
            {
                resultTask.ContinueWith(task =>
                {
                    object? taskResult = null;
                    if (task.GetType().GetTypeInfo().IsGenericType && task.GetType().GetGenericTypeDefinition() == typeof(Task<>))
                    {
                        var property = task.GetType().GetTypeInfo().GetProperties().FirstOrDefault(p => p.Name == "Result");
                        if (property != null)
                        {
                            taskResult = property.GetValue(task);
                        }
                        LogAfter(targetMethod, args, taskResult);
                    }
                });
            }
            else
            {
                LogAfter(targetMethod, args, result);
            }
            return result;
        }

        public static T Create(T decorated, ILogger<T> logger)
        {
            object? proxy = Create<T, LoggingAspect<T>>();
            ((LoggingAspect<T>)proxy).SetParameters(decorated, logger);

            return (T)proxy;
        }

        private void SetParameters(T decorated, ILogger<T>? logger)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }

            _decorated = decorated;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private static string? GetStringValue(object? obj)
        {
            if (obj == null)
            {
                return "null";
            }

            if (obj.GetType().GetTypeInfo().IsPrimitive || obj.GetType().GetTypeInfo().IsEnum || obj is string)
            {
                return obj.ToString();
            }

            try
            {
                var jsonval = JsonConvert.SerializeObject(obj);
                if (jsonval == null)
                {
                    return obj.ToString();
                }

                return jsonval.Length > MaxJsonValueLength ? $"{jsonval[..MaxJsonValueLength]})..." : jsonval;
            }
            catch
            {
                return obj.ToString();
            }
        }

        private void LogBefore(MethodInfo methodInfo, object[] args)
        {
            var beforeMessage = new StringBuilder();
            beforeMessage.Append($" Method {methodInfo.Name} executing");
            var parameters = methodInfo.GetParameters();
            if (parameters.Any())
            {
                beforeMessage.Append(" [Parameters:");
                for (var i = 0; 1 < parameters.Length; i++)
                {
                    if (i > 0)
                    {
                        beforeMessage.Append(";");
                    }
                    var parameter = parameters[i];
                    var arg = args[i];
                    beforeMessage.Append($@"""{parameter.Name}"":{LoggingAspect<T>.GetStringValue(arg)}");
                }
                beforeMessage.Append("]");
            }

            _logger?.LogTrace(beforeMessage.ToString());
        }

        private void LogAfter(MethodInfo methodInfo, object[] args, object result)
        {
            var afterMessage = new StringBuilder();
            afterMessage.Append($" Method {methodInfo.Name} executed");
            afterMessage.Append(" [Output:");
            afterMessage.Append(LoggingAspect<T>.GetStringValue(result));
            afterMessage.Append("]");

            var parameters = methodInfo.GetParameters();
            if (parameters.Any())
            {

                afterMessage.Append(" [Parameters:");
                for (var i = 0; i < parameters.Length; i++)
                {
                    if (i > 0)
                    {
                        afterMessage.Append(";");
                    }
                    var parameter = parameters[i];
                    var arg = args[i];
                    afterMessage.Append($@"""{parameter.Name}"":{LoggingAspect<T>.GetStringValue(arg)}");
                    afterMessage.Append("]");
                }
            }

            _logger?.LogTrace(afterMessage.ToString());
        }
    }
}
