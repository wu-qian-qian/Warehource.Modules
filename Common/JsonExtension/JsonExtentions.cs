using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Common.JsonExtension;

public static class JsonExtentions
{
    public static readonly JavaScriptEncoder Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

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
}

public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    private readonly string _dateFormatString;

    public DateTimeJsonConverter()
    {
        _dateFormatString = "yyyy-MM-dd HH:mm:ss";
    }

    public DateTimeJsonConverter(string dateFormatString)
    {
        _dateFormatString = dateFormatString;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        if (str == null)
            return default;
        return DateTime.Parse(str);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        //固定用服务器所在的时区，前端如果想适应用户的时区，请自己调整
        writer.WriteStringValue(value.ToString(_dateFormatString));
    }
}