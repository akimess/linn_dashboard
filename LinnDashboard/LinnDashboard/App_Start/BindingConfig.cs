using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Script.Serialization;

namespace LinnDashboard
{
    public class BindingConfig
    {
        public static void RegisterBindings(HttpConfiguration config)
        {
            config.ParameterBindingRules.Insert(0, HookupParameterBinding);
        }

        public static HttpParameterBinding HookupParameterBinding(HttpParameterDescriptor descriptor)
        {
            var supportedMethods = descriptor.ActionDescriptor.SupportedHttpMethods;

            // Only apply this binder on POST and PUT operations
            if (supportedMethods.Contains(HttpMethod.Post) || supportedMethods.Contains(HttpMethod.Get))
            {
                var types = new Type[]
                {
                    typeof(string),
                    typeof(int),
                    typeof(decimal),
                    typeof(double),
                    typeof(bool),
                    typeof(DateTime),
                    typeof(byte[]),
                    typeof(Guid)
                };

                var baseTypes = new Type[]
                {
                    typeof(Enum)
                };

                var type = GetType(descriptor);
                if (types.Any(t => t == type) == true || baseTypes.Any(t => t == type.BaseType) == true)
                    return new SimpleParameterBinding(descriptor);

                return new ComplexParameterBinding(descriptor);
            }

            return null;
        }

        public static Type GetType(HttpParameterDescriptor descriptor)
        {
            return Nullable.GetUnderlyingType(descriptor.ParameterType) ?? descriptor.ParameterType;
        }
    }

    public class SimpleParameterBinding : HttpParameterBinding
    {
        private const string MultipleBodyParameters = "MultipleBodyParameters";

        public SimpleParameterBinding(HttpParameterDescriptor descriptor)
            : base(descriptor)
        {

        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            string stringValue = null;

            var col = TryReadBody(actionContext.Request);
            if (col != null)
                stringValue = col[Descriptor.ParameterName];

            if (stringValue == null)
            {
                var query = actionContext.Request.GetQueryNameValuePairs();
                if (query != null)
                {
                    var matches = query.Where(kv => kv.Key.ToLower() == Descriptor.ParameterName.ToLower());
                    if (matches.Count() > 0)
                        stringValue = matches.First().Value;
                }
            }

            var value = StringToType(stringValue);
            SetValue(actionContext, value);

            var tcs = new TaskCompletionSource<AsyncVoid>();
            tcs.SetResult(default(AsyncVoid));

            return tcs.Task;
        }

        private object StringToType(string stringValue)
        {
            if (stringValue == null) return null;

            var type = BindingConfig.GetType(Descriptor);
            if (type == typeof(string))
                return stringValue;

            if (type == typeof(int))
            {
                int value;
                if (int.TryParse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out value) == true)
                    return value;
                else
                    return null;
            }

            if (type == typeof(Int32))
            {
                Int32 value;
                if (Int32.TryParse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out value) == true)
                    return value;
                else
                    return null;
            }

            if (type == typeof(Int64))
            {
                Int64 value;
                if (Int64.TryParse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out value) == true)
                    return value;
                else
                    return null;
            }

            if (type == typeof(decimal))
            {
                decimal value;
                if (decimal.TryParse(stringValue, NumberStyles.Number, CultureInfo.InvariantCulture, out value) == true)
                    return value;
                else
                    return null;
            }

            if (type == typeof(double))
            {
                double value;
                if (double.TryParse(stringValue, NumberStyles.Number | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value) == true)
                    return value;
                else
                    return null;
            }

            if (type == typeof(DateTime))
            {
                DateTime value;
                if (DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.None, out value) == true)
                    return value;
                else
                    return null;
            }

            if (type == typeof(Guid))
            {
                Guid value;
                if (Guid.TryParse(stringValue, out value) == true)
                    return value;
                else
                    return null;
            }

            if (type == typeof(bool))
            {
                bool value;
                if (bool.TryParse(stringValue, out value) == true)
                    return value;
                else if (stringValue == "true" || stringValue == "on" || stringValue == "1")
                    return true;
                else
                    return null;
            }

            if (type.BaseType == typeof(Enum))
            {
                try
                {
                    var converter = TypeDescriptor.GetConverter(type);
                    if (converter != null)
                        return (Enum)converter.ConvertFromString(stringValue);
                }
                catch (Exception ex) { }
            }

            return null;
        }

        private NameValueCollection TryReadBody(HttpRequestMessage request)
        {
            object result = null;

            if (!request.Properties.TryGetValue(MultipleBodyParameters, out result))
            {
                var contentType = request.Content.Headers.ContentType;

                if (contentType == null || contentType.MediaType != "application/x-www-form-urlencoded")
                    result = null;
                else
                    result = request.Content.ReadAsFormDataAsync().Result;

                request.Properties.Add(MultipleBodyParameters, result);
            }

            return result as NameValueCollection;
        }

        private struct AsyncVoid
        {

        }
    }

    public class ComplexParameterBinding : HttpParameterBinding
    {
        private const string MultipleBodyParameters = "MultipleBodyParameters";
        private JsonSerializerSettings jsonSettings;

        public ComplexParameterBinding(HttpParameterDescriptor descriptor)
            : base(descriptor)
        {
            jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }

        public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            string stringValue = null;

            var col = TryReadBody(actionContext.Request);
            if (col != null)
                stringValue = col[Descriptor.ParameterName];

            if (stringValue == null)
            {
                var query = actionContext.Request.GetQueryNameValuePairs();
                if (query != null)
                {
                    var matches = query.Where(kv => kv.Key.ToLower() == Descriptor.ParameterName.ToLower());
                    if (matches.Count() > 0)
                        stringValue = matches.First().Value;
                }
            }

            var value = StringToType(stringValue);
            SetValue(actionContext, value);

            var tcs = new TaskCompletionSource<AsyncVoid>();
            tcs.SetResult(default(AsyncVoid));

            return tcs.Task;
        }

        private object StringToType(string stringValue)
        {
            object value = null;

            if (stringValue != null)
            {
                try
                {
                    value = JsonConvert.DeserializeObject(stringValue, Descriptor.ParameterType, jsonSettings);
                }
                catch (Exception ex)
                {
                    // TODO: Save error to logs
                }
            }

            return value;
        }

        private NameValueCollection TryReadBody(HttpRequestMessage request)
        {
            object result = null;

            if (!request.Properties.TryGetValue(MultipleBodyParameters, out result))
            {
                var contentType = request.Content.Headers.ContentType;

                if (contentType == null || contentType.MediaType != "application/x-www-form-urlencoded")
                    result = null;
                else
                    result = request.Content.ReadAsFormDataAsync().Result;

                request.Properties.Add(MultipleBodyParameters, result);
            }

            return result as NameValueCollection;
        }

        private struct AsyncVoid
        {

        }
    }
}