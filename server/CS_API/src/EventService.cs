using MongoDB.Driver;
using MyApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi
{
    public class EventService
    {
        private readonly IMongoClient _mongoClient;

        public EventService(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task<string> GetEventCollectionJSON()
        {
            var database = _mongoClient.GetDatabase("eventsgroup");
            var collection = await database.GetCollection<PreferenceTag>("Users")
                                           .Find(FilterDefinition<PreferenceTag>.Empty)
                                           .ToListAsync();

            return JsonConvert.SerializeObject(collection);
        }
    }
}
