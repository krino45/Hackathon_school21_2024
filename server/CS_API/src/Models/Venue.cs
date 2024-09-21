using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.InteropServices.Marshalling;

namespace MyApi.Models
{
    public class Venue
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("city")]
        public string? City { get; private set; }

        [BsonElement("address")]
        public string? Address { get; private set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("capacity")]
        public int? Capacity { get; set; }

        [BsonElement("eventIDs")]
        public List<string>? EventIDs { get; set; } 

        public override string ToString()
        {
            return "г. " + this.City + ", " + this.Address + " (" + this.Name + ")";
        }

    }
}
