using Newtonsoft.Json.Converters;
using System;
using Newtonsoft.Json;
using System.Globalization;

namespace PlaySimple.Common
{
    public class PlaySimpleDateConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.ParseExact(reader.Value.ToString(), "d/M/yyyy", CultureInfo.InvariantCulture);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString("dd/MM/yyyy"));
        }
    }
}