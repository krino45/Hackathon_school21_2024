using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;

namespace MyApi.Models
{
    public class PreferenceEventId
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("event_id")]
        public string? EventId { get; private set; }

        public PreferenceEventId()
        { }
        public PreferenceEventId(string eventid)
        {
            EventId = eventid;
        }

    }
}
