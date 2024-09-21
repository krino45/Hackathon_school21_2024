using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.InteropServices.Marshalling;

namespace MyApi.Models
{
    public class Venues
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("address")]
        public string? Address { get; private set; }

        [BsonElement("venuename")]
        public string? VenueName { get; set; }

        [BsonElement("capacity")]
        public int? Capacity { get; set; }

        [BsonElement("eventIDs")]
        public List<ObjectId>? eventIDs { get; set; } 

    }
}
