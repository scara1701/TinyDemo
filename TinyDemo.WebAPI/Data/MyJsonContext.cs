using System.Text.Json.Serialization;
using TinyDemo.SharedLib.Entities;

namespace TinyDemo.WebAPI.Data
{
    [JsonSerializable(typeof(Lotto))]
    [JsonSerializable(typeof(List<Lotto>))]
    public partial class MyJsonContext : JsonSerializerContext
    {
    }
}
