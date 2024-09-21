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
        public async Task<string> AddEventAsyncJSON(string json)
        {
            try
            {
                await Console.Out.WriteLineAsync("JSON FROM ADD ASYNC: " + json);
                var jsonObj = JObject.Parse(json);

                if (!jsonObj.ContainsKey("eventType"))
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "eventType is required in the JSON." });
                }

                string eventType = jsonObj["eventType"]!.ToString();
                BaseEvent newEvent;
                switch (eventType)
                {
                    case "t1":
                        newEvent = JsonConvert.DeserializeObject<PrivateEvent>(json);
                        break;

                    case "t2":
                        newEvent = JsonConvert.DeserializeObject<RoundtableEvent>(json);
                        break;

                    case "t3":
                        newEvent = JsonConvert.DeserializeObject<IndustryEvent>(json);
                        break;

                    case "t4":
                        newEvent = JsonConvert.DeserializeObject<PublicEvent>(json);
                        break;

                    default:
                        throw new ArgumentException("Invalid eventType specified.");
                }
                
                await _event_repo.AddEventAsync(newEvent);
                return JsonConvert.SerializeObject(new { Success = true, Message = "Event added." });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format: " + ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred while adding the event." + ex.Message });
            }
        }


    public async Task<string?> GetAllEventsAsyncJSON()
        {
            return JsonConvert.SerializeObject(await _event_repo.GetAllEventsAsync());
        }

        public async Task<string?> GetEventByIdAsyncJSON(string json)
        {
            try
            {
                var jsonObj = JObject.Parse(json);
                var id = jsonObj["id"]?.ToString();

                if (string.IsNullOrEmpty(id))
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Event ID is required." });
                }

                var _event = await _event_repo.GetEventByIdAsync(new ObjectId(id));

                if (_event == null)
                {
                    return JsonConvert.SerializeObject(new { Success = false, Message = "Event not found." });
                }

                return JsonConvert.SerializeObject(new { Success = true, data = _event });
            }
            catch (JsonException ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "Invalid JSON format.", Error = ex.Message });
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { Success = false, Message = "An error occurred.", Error = ex.Message });
            }
        }

        /*
        public async Task<string?> GetEventsByTimeRangeAsyncJSON(string start, string end)
        {
            return JsonConvert.SerializeObject(await _event_repo.GetEventsByTimeRangeAsync(DateTime.Parse(start), DateTime.Parse(end)));
        }

        public async Task<string?> GetEventsByVenueAsyncJSON(Venue venue)
        {
            return JsonConvert.SerializeObject(await _event_repo.GetEventsByVenueAsync(venue));
        }
        //public async Task<string?> GetEventsByVenueAsyncJSON(string venueName, string address, string city)
        //{
        //    return JsonConvert.SerializeObject(await _event_repo.GetEventsByVenueAsync(venueName, address, city));
        //}
        public async Task<string?> GetEventsByNameAsyncJSON(string eventName)
        {
            return JsonConvert.SerializeObject(await _event_repo.GetEventsByNameAsync(eventName));
        }
        public async Task<string?> SearchEventsByNameAsyncJSON(string partialName)
        {
            return JsonConvert.SerializeObject(await _event_repo.SearchEventsByNameAsync(partialName));
        }*/
    }
}
