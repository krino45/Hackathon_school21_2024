using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyApi.Models
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(PrivateEvent), typeof(RoundtableEvent), typeof(IndustryEvent), typeof(PublicEvent))]
    public class BaseEvent
    {
        public ObjectId Uid { get; set; }
        public List<ObjectId>? AttendeesId { get; set; }
        public string Name { get; set; }
        public Venues Venue { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? MinAttendees { get; set; }
        public bool? archiveEvent { get; set; }
    }
}
