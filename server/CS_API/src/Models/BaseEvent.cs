using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyApi.Models
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(PrivateEvent), typeof(RoundtableEvent), typeof(IndustryEvent), typeof(PublicEvent))]
    public class BaseEvent
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("attendees_id")]
        public List<string>? AttendeesId { get; set; }
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("venue")]
        public string? Venue { get; set; }
        [BsonElement("start_time")]
        public DateTime? StartTime { get; set; }
        [BsonElement("end_time")]
        public DateTime? EndTime { get; set; }
        [BsonElement("min_attendess")]
        public int? MinAttendees { get; set; }
        [BsonElement("archive_event")]
        public bool? ArchiveEvent { get; set; }
    }
}
