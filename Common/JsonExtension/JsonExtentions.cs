using Newtonsoft.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Common.JsonExtension;

public static class JsonExtentions
{
    public static readonly JavaScriptEncoder Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

    //  设置 type   
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
        return System.Text.Json.JsonSerializer.Serialize(value, value.GetType(), opt);
    }

    public static T? ParseJson<T>(this string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return default;
        var opt = CreateJsonSerializerOptions();
        return System.Text.Json.JsonSerializer.Deserialize<T>(value, opt);
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