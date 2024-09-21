using MongoDB.Bson;
using MongoDB.Driver;
using MyApi.Models;
using MyApi.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi
{
    public class EventService
    {
        private EventRepository _event_repo;

        public EventService(IMongoClient mongoClient)
        {
            _event_repo = new EventRepository(mongoClient);
        }
        public async Task AddEventAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);

                if (!jsonObj.ContainsKey("eventType"))
                {
                    throw new ArgumentException("eventType is required in the JSON.");
                }
                string eventType = jsonObj["eventType"]?.ToString();
                BaseEvent newEvent;
                switch (eventType)
                {
                    case "PrivateEvent":
                        newEvent = JsonConvert.DeserializeObject<PrivateEvent>(json);
                        break;

                    case "RoundtableEvent":
                        newEvent = JsonConvert.DeserializeObject<RoundtableEvent>(json);
                        break;

                    case "IndustryEvent":
                        newEvent = JsonConvert.DeserializeObject<IndustryEvent>(json);
                        break;

                    case "PublicEvent":
                        newEvent = JsonConvert.DeserializeObject<PublicEvent>(json);
                        break;

                    default:
                        throw new ArgumentException("Invalid eventType specified.");
                }
                await _event_repo.AddEventAsync(newEvent);
            }
            catch (JsonException ex)
            {
                throw new ArgumentException("Invalid JSON format.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the event.", ex);
            }
        }


    public async Task<string?> GetAllEventsAsyncJSON()
        {
            return JsonConvert.SerializeObject(await _event_repo.GetAllEventsAsync());
        }

        public async Task<string?> GetEventByIdAsyncJSON(string id)
        {
            return JsonConvert.SerializeObject(await _event_repo.GetEventByIdAsync(new ObjectId(id)));
        }

        public async Task<string?> GetEventsByTimeRangeAsyncJSON(string start, string end)
        {
            return JsonConvert.SerializeObject(await _event_repo.GetEventsByTimeRangeAsync(DateTime.Parse(start), DateTime.Parse(end)));
        }

        public async Task<string?> GetEventsByVenueAsyncJSON(Venues venue)
        {
            return JsonConvert.SerializeObject(await _event_repo.GetEventsByVenueAsync(venue));
        }
        public async Task<string?> GetEventsByVenueAsyncJSON(string venueName, string address)
        {
            return JsonConvert.SerializeObject(await _event_repo.GetEventsByVenueAsync(venueName, address));
        }
        public async Task<string?> GetEventsByNameAsyncJSON(string eventName)
        {
            return JsonConvert.SerializeObject(await _event_repo.GetEventsByNameAsync(eventName));
        }
        public async Task<string?> SearchEventsByNameAsyncJSON(string partialName)
        {
            return JsonConvert.SerializeObject(await _event_repo.SearchEventsByNameAsync(partialName));
        }
    }
}
