using Amazon.Util.Internal;
using MongoDB.Bson;
using MongoDB.Driver;
using MyApi.Models;
using System.Reflection;

namespace MyApi
{
    public static class TagService
    {
        public static async Task<string> GetTagCollectionJSON()
        {
            var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
            if (connectionString == null)
            {
                Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
                Environment.Exit(0);
            }
            var client = new MongoClient(connectionString);
            List<PreferenceTag> collection = await client.GetDatabase("eventsgroup").GetCollection<PreferenceTag>("tags").Find(FilterDefinition<PreferenceTag>.Empty).ToListAsync();
            string collection_json = collection.ToJson();
            return collection_json;

        }
    }
}