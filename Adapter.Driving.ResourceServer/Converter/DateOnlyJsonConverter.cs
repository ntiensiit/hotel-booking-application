using System.Text.Json;
using System.Text.Json.Serialization;

namespace Adapter.Driving.ResourceServer.Converter;

public class DateOnlyJsonConverter(string serializationFormat) : JsonConverter<DateOnly>
{
    private readonly string _serializationFormat = serializationFormat;

    public DateOnlyJsonConverter()
        : this("yyyy-MM-dd") { }

    public override DateOnly Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return DateOnly.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_serializationFormat));
    }
}
