using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api_estoque.Converters
{
    public class JsonDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string _format;

        public JsonDateTimeConverter(string format)
        {
            _format = format;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString();
            return DateTime.ParseExact(dateString, _format, null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_format));
        }
    }
}
