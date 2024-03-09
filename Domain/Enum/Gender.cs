using System.Text.Json.Serialization;

namespace Domain.Enum
{
    // [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male = 1,
        Female
    }
}