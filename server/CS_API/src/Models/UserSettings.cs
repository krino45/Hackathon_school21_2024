using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;

namespace MyApi.Models
{
    public class UserSettings
    {
        public string? Id { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }
        public List<string>? Preferences { get; set; }
    }
}
