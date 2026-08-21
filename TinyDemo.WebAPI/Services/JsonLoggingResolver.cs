using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace TinyDemo.WebAPI.Services
{
    public class JsonLoggingResolver : IJsonTypeInfoResolver
    {
        private readonly IJsonTypeInfoResolver _inner;

        public JsonLoggingResolver(IJsonTypeInfoResolver inner)
        {
            _inner = inner;
        }

        public JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            Console.WriteLine($"[AOT JSON] Requested metadata for: {type.FullName}");

            return _inner.GetTypeInfo(type, options);
        }
    }
}