using MongoDB.Bson;
using MongoDB.Driver;
using MyApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi.Repositories
{
    public class EventRepository
    {
        private readonly IMongoCollection<BaseEvent> _eventListCollection;
        private readonly IMongoCollection<PrivateEvent> _privateEventCollection;
        private readonly IMongoCollection<RoundtableEvent> _roundtableEventCollection;
        private readonly IMongoCollection<IndustryEvent> _industryEventCollection;
        private readonly IMongoCollection<PublicEvent> _publicEventCollection;

        public EventRepository(IMongoClient client)
        {
            var database = client.GetDatabase("eventsgroup");
            _eventListCollection = database.GetCollection<BaseEvent>("EventList");
            _privateEventCollection = database.GetCollection<PrivateEvent>("PrivateEvents");
            _roundtableEventCollection = database.GetCollection<RoundtableEvent>("RoundtableEvents");
            _industryEventCollection = database.GetCollection<IndustryEvent>("IndustryEvents");
            _publicEventCollection = database.GetCollection<PublicEvent>("PublicEvents");
        }

        public async Task AddEventAsync(BaseEvent newEvent)
        {
            switch (newEvent)
            {
                case PrivateEvent privateEvent:
                    await _privateEventCollection.InsertOneAsync(privateEvent);
                    break;

                case RoundtableEvent roundtableEvent:
                    await _roundtableEventCollection.InsertOneAsync(roundtableEvent);
                    break;

                case IndustryEvent industryEvent:
                    await _industryEventCollection.InsertOneAsync(industryEvent);
                    break;

                case PublicEvent publicEvent:
                    await _publicEventCollection.InsertOneAsync(publicEvent);
                    break;
            }

            await _eventListCollection.InsertOneAsync(newEvent);
        }

        public async Task<List<BaseEvent>> GetAllEventsAsync()
        {
            return await _eventListCollection.Find(_ => true).ToListAsync();
        }

        public async Task<BaseEvent?> GetEventByIdAsync(ObjectId id)
        {
            var filter = Builders<BaseEvent>.Filter.Eq(e => e.Id, id);
            return await _eventListCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<BaseEvent>?> GetEventsByTimeRangeAsync(DateTime start, DateTime end)
        {
            var filter = Builders<BaseEvent>.Filter.And(
                Builders<BaseEvent>.Filter.Gte(e => e.StartTime, start),
                Builders<BaseEvent>.Filter.Lte(e => e.EndTime, end)
            );
            return await _eventListCollection.Find(filter).ToListAsync();
        }

        //public async Task<List<BaseEvent>?> GetEventsByVenueAsync(string venueName, string address, string city)
        //{
        //    var filter = Builders<BaseEvent>.Filter.And(
        //        Builders<BaseEvent>.Filter.Eq(e => e.Venue.Name, venueName),
        //        Builders<BaseEvent>.Filter.Eq(e => e.Venue.Address, address),
        //        Builders<BaseEvent>.Filter.Eq(e => e.Venue.City, city)
        //        );
        //    return await _eventListCollection.Find(filter).ToListAsync();
        //}
        public async Task<List<BaseEvent>?> GetEventsByVenueAsync(Venue venue)
        {
            var venueEvents = venue.EventIDs;
            if (venueEvents == null || !venueEvents.Any())
                return null;
            List<ObjectId> eventIDs = new List<ObjectId>();
            foreach(var venueEvent in venueEvents)
            {
                eventIDs.Add(new ObjectId(venueEvent));
            }
             
            var filter = Builders<BaseEvent>.Filter.In(e => e.Id, eventIDs);
            return await _eventListCollection.Find(filter).ToListAsync();
        }
        public async Task<List<BaseEvent>?> GetEventsByNameAsync(string eventName)
        {
            var filter = Builders<BaseEvent>.Filter.Eq(e => e.Name, eventName);
            return await _eventListCollection.Find(filter).ToListAsync();
        }
        public async Task<List<BaseEvent>?> SearchEventsByNameAsync(string partialName)
        {
            var filter = Builders<BaseEvent>.Filter.Regex(e => e.Name, new BsonRegularExpression(partialName, "i"));
            return await _eventListCollection.Find(filter).ToListAsync();
        }

    }
}
