using MongoDB.Driver;
using MyApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApi
{
    public class UserService
    {
        private readonly IMongoClient _mongoClient;

        public UserService(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task<string> GetUserJSON()
        {
            var database = _mongoClient.GetDatabase("eventsgroup");
            var collection = await database.GetCollection<User>("Users")
                                           .Find(FilterDefinition<User>.Empty)
                                           .ToListAsync();

            return JsonConvert.SerializeObject(collection);
        }
    }
}
