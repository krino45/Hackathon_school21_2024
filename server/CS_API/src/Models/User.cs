using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;

namespace MyApi.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string? name { get; set; }

        public string? lastname { get; set; }

        public string? secondname {get; set;}

        public string? email {get; set;}

        public string? roles {get; set;}

        [BsonElement("events")]
        public string? EventId { get; private set; }

        public User()
        { }
        public User(string eventid)
        {
            EventId = eventid;
        }

    }
}
