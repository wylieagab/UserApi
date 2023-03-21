using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UserApi.Models.Extensions
{

    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        private readonly string[] _formats;

        public DateTimeJsonConverter(params string[] formats)
        {
            _formats = formats;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();
            if (DateTime.TryParseExact(stringValue, _formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDateTime))
            {
                return parsedDateTime;
            }

            throw new JsonException($"Invalid date format. Expected formats: {string.Join(", ", _formats)}");
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(_formats[2], CultureInfo.InvariantCulture));
        }
    }
}
