using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Common.JsonExtension;

public static class JsonExtentions
{
    public static readonly JavaScriptEncoder Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

    /// <summary>
    ///     设置type头这样可以实现接口的序列化反序列化
    /// </summary>
    public static readonly JsonSerializerSettings Instance = new()
    {
        TypeNameHandling = TypeNameHandling.All,
        MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
    };

    public static JsonSerializerOptions CreateJsonSerializerOptions(bool camelCase = false)
    {
        var opt = new JsonSerializerOptions { Encoder = Encoder };
        if (camelCase)
        {
            opt.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            opt.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        }

        opt.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
        return opt;
    }

    public static string ToJsonString(this object value, bool camelCase = false)
    {
        var opt = CreateJsonSerializerOptions(camelCase);
        return JsonSerializer.Serialize(value, value.GetType(), opt);
    }

    public static T? ParseJson<T>(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return default;
        var opt = CreateJsonSerializerOptions();
        return JsonSerializer.Deserialize<T>(value, opt);
    }

    public static string NewtonsoftToJsonString(this object value)
    {
        return JsonConvert.SerializeObject(value, Instance);
    }

    public static T NewtonsoftParseJson<T>(this string value)
    {
        return JsonConvert.DeserializeObject<T>(value, Instance);
    }
}