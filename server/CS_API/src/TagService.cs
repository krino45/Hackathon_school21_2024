using MongoDB.Driver;
using MyApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi
{
    public class TagService
    {
        private readonly IMongoClient _mongoClient;

        public TagService(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task<string> GetTagCollectionJSON()
        {
            var database = _mongoClient.GetDatabase("eventsgroup");
            var collection = await database.GetCollection<PreferenceTag>("tags")
                                           .Find(FilterDefinition<PreferenceTag>.Empty)
                                           .ToListAsync();

            return JsonConvert.SerializeObject(collection);
        }
    }
}
